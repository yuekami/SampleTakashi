Public Class index
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' ページロード時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = True Then
            Me.postBackAccess()
        Else
            Me.PageInit()
        End If

    End Sub

    ''' <summary>
    ''' ページ初期化
    ''' </summary>
    Private Sub PageInit()

        Me.userName.Value = ""
        Me.text.Value = ""
        Me.output.Text = ""

        Dim postList As List(Of Posts) = Me.DbAccesser()

        If Not postList Is Nothing Then
            Me.output.Text += "<table border=""1"" id=""table""><tr><th>名前</th><th>投稿文</th><th>投稿年</th><th>投稿時間</th><th>削除ボタン</th></tr>"
        End If

        For Each post As Posts In postList

            Dim tdUserName As String = $"<tr><td>{post.UserName}</td>"
            Dim tdPostText As String = $"<td>{post.PostText}</td>"
            Dim tdYmd As String = $"<td>{post.Ymd.Substring(0, 4)}年 {post.Ymd.Substring(4, 2)}月{post.Ymd.Substring(6, 2)}日</td>"
            Dim tdTime As String = $"<td>{post.Time.Substring(0, 2)}：{post.Time.Substring(2, 2)}：{post.Time.Substring(4, 2)}</td></tr>"

            Me.output.Text += tdUserName + tdPostText + tdYmd + tdTime
        Next

        If Not postList Is Nothing Then Me.output.Text += "</table>"

    End Sub

    ''' <summary>
    ''' ポストバックアクセス
    ''' </summary>
    Private Sub postBackAccess()

        ' テキストボックスの中身を変数に入れる。
        Dim userName As String = Me.userName.Value
        Dim postText As String = Me.text.Value

        ' その他登録に必要な情報生成
        Dim dt As DateTime = DateTime.Now
        Dim ymd As String = dt.ToString("yyyyMMdd")
        Dim time As String = dt.ToString("hhmmss")


        If Not String.IsNullOrEmpty(userName) AndAlso Not String.IsNullOrEmpty(postText) Then
            Me.DbWriter(userName, postText, ymd, time)

        End If

        Me.userName.Value = ""
        Me.text.Value = ""

        Page.Response.Redirect("index.aspx")

    End Sub

    ''' <summary>
    ''' DB接続(select)
    ''' </summary>
    Private Function DbAccesser() As List(Of Posts)

        Try
            ' serverIP
            Dim serverName As String = "*:*:*:*"
            'DB名
            Dim dataBase As String = "***"
            ' ログインアカウント情報
            Dim userid As String = "***"
            Dim pwd As String = "***"

            Using con As New SqlClient.SqlConnection()

                con.ConnectionString =
                    " Data Source = " & serverName &
                    ";Initial Catalog = " & dataBase &
                    ";User ID = " & userid &
                    ";Password = " & pwd

                con.Open()

                Dim queryString = "Select userName, postText, ymd, time from post"

                Dim cmd As New SqlClient.SqlCommand(queryString, con)

                Using reader As SqlClient.SqlDataReader = cmd.ExecuteReader()

                    Dim postList As New List(Of Posts)

                    While reader.Read()

                        Dim post As New Posts(reader(0), reader(1), reader(2).PadLeft(8, "0"c), reader(3).PadLeft(6, "0"c))
                        postList.Add(post)

                    End While

                    Return postList
                End Using
                con.Close()
            End Using
        Catch ex As Exception

            Return Nothing
        End Try


    End Function

    ''' <summary>
    ''' DB接続(insert)
    ''' </summary>
    ''' <param name="userName"></param>
    ''' <param name="postText"></param>
    ''' <param name="ymd"></param>
    ''' <param name="time"></param>
    Private Sub DbWriter(ByVal userName As String, ByVal postText As String, ByVal ymd As String, ByVal time As String)

        Try
            ' serverIP
            Dim serverName As String = "*:*:*:*"
            'DB名
            Dim dataBase As String = "***"
            ' ログインアカウント情報
            Dim userid As String = "***"
            Dim pwd As String = "***"

            Using con As New SqlClient.SqlConnection()

                con.ConnectionString =
                    " Data Source = " & serverName &
                    ";Initial Catalog = " & dataBase &
                    ";User ID = " & userid &
                    ";Password = " & pwd


                Dim queryString = $"insert into post select(select max(id) from post)+1, '{userName}', '{postText}', '{ymd}', {time}"

                Dim cmd As New SqlClient.SqlCommand(queryString, con)

                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()


            End Using
        Catch ex As Exception

        End Try
    End Sub

End Class
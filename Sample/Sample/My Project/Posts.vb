Public Class Posts

    Private _userName As String
    Public Property UserName() As String
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
        End Set
    End Property

    Private _postText As String
    Public Property PostText() As String
        Get
            Return _postText
        End Get
        Set(value As String)
            _postText = value
        End Set
    End Property

    Private _ymd As String
    Public Property Ymd() As String
        Get
            Return _ymd
        End Get
        Set(value As String)
            _ymd = value
        End Set
    End Property

    Private _time As String
    Public Property Time() As String
        Get
            Return _time
        End Get
        Set(value As String)
            _time = value
        End Set
    End Property

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="userName"></param>
    ''' <param name="postText"></param>
    ''' <param name="ymd"></param>
    ''' <param name="time"></param>
    Public Sub New(ByVal userName As String, ByVal postText As String, ByVal ymd As String, ByVal time As String)

        _userName = userName
        _postText = postText
        _ymd = ymd
        _time = time

    End Sub

End Class

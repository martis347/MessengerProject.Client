namespace Messenger.WebApi.Enums
{
    public enum RequestAction
    {
        Register = 'r',
        JoinRoom = 'j',
        CreateRoom = 'c',
        Message = 'm',
        Exit = (char)27
    }
}
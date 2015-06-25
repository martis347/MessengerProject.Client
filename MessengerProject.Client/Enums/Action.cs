namespace MessengerProject.Client.Enums
{
    public enum Action
    {
        Register = 'r',
        JoinRoom = 'j',
        CreateRoom = 'c',
        Message = 'm',
        Exit = (char)27
    }
}
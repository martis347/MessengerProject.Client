namespace Messenger.WebApi
{
    public enum RequestStatus
    {
        UserAlreadyExists,
        RoomAlreadyExists,
        UserNotFound,
        RoomNotFound,
        RoomIsFull,
        Success,
        RoomIsEmpty,
        ConnectionError
    }
}
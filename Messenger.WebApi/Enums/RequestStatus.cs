namespace Messenger.WebApi.Enums
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
        ConnectionError,
        UsernameIsNull,
        RoomNameIsNull
    }
}
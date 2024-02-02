namespace Oostel.API.APIRoutes
{
    public class HostelRoute
    {
        public const string CreateHostel = "hostel/create-hostel";
        public const string GetAllHostels = "hostel/get-all-hostels";
        public const string GetHostelById = "hostel/get-hostel-by-id";
        public const string UpdateHostel = "hostel/update-hostel";
        public const string GetMyHostels = "hostel/get-my-hostels";
        public const string GetMyLikedHostels = "hostel/get-my-liked-hostels";
        public const string GetHostelLikedUsers = "hostel/get-hostel-liked-users";

        public const string CreateRoomForHostel = "hostel/create-room-for-hostel";
        public const string GetARoomForHostel = "hostel/get-a-room-for-hostel";
        public const string GetAllRoomsForHostel = "hostel/get-all-rooms-for-hostel";
        public const string UpdateRoom = "hostel/update-room";
        public const string GetAvailableRoomsPerHostel = "hostel/get-available-rooms-per-hostel";
        public const string CreateRoomCollections = "hostel/create-room-collections";

        public const string AddHostelLikes = "hostel/add-hostel-likes";

        public const string DeleteHostelPicture = "hostel/delete-hostel-picture";
    }
}

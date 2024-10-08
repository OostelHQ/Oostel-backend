﻿namespace Oostel.API.APIRoutes
{
    public class UserProfileRoute
    {
        public const string CreateUserProfile = "user-profile/create-student-profile";
        public const string UpdateStudentProfile = "user-profile/update-student-profile";
        public const string GetAllStudents = "user-profile/get-all-students";
        public const string GetStudentById = "user-profile/get-student-by-id";

        public const string CreateLandlordProfile = "user-profile/create-landlord-profile";
        public const string UpdateLandlordProfile = "user-profile/update-landlord-profile";
        public const string GetAllLandlords = "user-profile/get-all-landlords";
        public const string GetLandlordById = "user-profile/get-landlord-by-id";

        public const string OpenToRoommate = "user-profile/open-to-roommate";

        public const string ProfileViewsCount = "user-profile/profile-views-count";

        public const string AddStudentLikes = "user-profile/like-student-profile";
        public const string GetMyLikedStudents = "user-profile/get-my-liked-students";
        public const string GetAStudentLikedUsers = "user-profile/ get-a-student-liked-users";

        public const string UploadUserProfilePicture = "user-profile/upload-user-profile-picture";
        public const string SendAgentInvitationCode = "user-profile/send-an-invitation-code";


        public const string CreateAgentProfile = "user-profile/create-agent-profile";
        public const string UpdateAgentProfile = "user-profile/update-agent-profile";
        public const string GetAllAgents = "user-profile/get-all-agents";
        public const string GetAgentById = "user-profile/get-agent-by-id";

        public const string AcceptLanlordInvitation = "user-profile/accept-landlord-invitation";
    }
}

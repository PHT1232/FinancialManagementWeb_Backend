using Microsoft.Extensions.FileProviders;

namespace TeamManagementProject_Backend.Global
{
    public static class AppFolders
    {
        public static string ProfilePictures;
        public static string UserProfilePictures;
        public static string UserDefaultProfilePictures;
        public static string GroupProfilePictures;
        public static string ChatPictures;
        public static string PersonalChatPictures;
        public static string GroupChatPictures;

        public static void Init(IWebHostEnvironment env)
        {
            ProfilePictures = Path.Combine(env.ContentRootPath, $"Pictures{Path.DirectorySeparatorChar}Profile");
            UserProfilePictures = Path.Combine(ProfilePictures, "Users");
            UserDefaultProfilePictures = Path.Combine(UserProfilePictures, "Default");
            GroupProfilePictures = Path.Combine(ProfilePictures, "Groups");

            ChatPictures = Path.Combine(env.ContentRootPath, $"Pictures{Path.DirectorySeparatorChar}Chats");
            PersonalChatPictures = Path.Combine(ChatPictures, "Personals");
            GroupChatPictures = Path.Combine(ChatPictures, "Groups");

            if (!Directory.Exists(ProfilePictures))
            {
                Directory.CreateDirectory(ProfilePictures);
            }
            if (!Directory.Exists(UserProfilePictures))
            {
                Directory.CreateDirectory(UserProfilePictures);
            }
            if (!Directory.Exists(GroupProfilePictures))
            {
                Directory.CreateDirectory(GroupProfilePictures);
            }
            if (!Directory.Exists(ChatPictures))
            {
                Directory.CreateDirectory(ChatPictures);
            }
            if (!Directory.Exists(PersonalChatPictures))
            {
                Directory.CreateDirectory(PersonalChatPictures);
            }            
            if (!Directory.Exists(GroupChatPictures))
            {
                Directory.CreateDirectory(GroupChatPictures);
            }
        }
    }
}

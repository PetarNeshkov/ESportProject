namespace E_SportManager.Common
{
    public static class GlobalConstants
    {
        public static class Player
        {
            public const int PlayerNameMinLength = 3;
            public const int PlayerNameMaxLength = 30;
            public const int PlayerDescriptionMinLength = 10;
            public const int PlayerDescriptionMaxLength = 1500;
        }

        public static class User
        {
            public const int UserUsernameMaxLength = 30;
            public const int UserUsernameMinLength = 4;
            public const int UserPasswordMaxLength = 50;
            public const int UserPasswordMinLength = 6;
        }
    }
}
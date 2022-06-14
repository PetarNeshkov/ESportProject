namespace E_SportManager.Common
{
    public static class GlobalConstants
    {
        public const string GlobalMessageKey = "GlobalMessage";
        public static class Player
        {
            public const int PlayerNameMinLength = 3;
            public const int PlayerNameMaxLength = 30;
            public const int PlayerDescriptionMinLength = 10;
            public const int PlayerDescriptionMaxLength = 1500;
            public const int PlayersPerPage = 4;
        }

        public static class Team
        {
            public const int TeamNameMinLength = 3;
            public const int TeamNameMaxLength = 30;
            public const int TeamsPerPage = 4;
        }

        public static class User
        {
            public const int UserUsernameMaxLength = 30;
            public const int UserUsernameMinLength = 4;
            public const int UserPasswordMaxLength = 50;
            public const int UserPasswordMinLength = 6;
        }

        public static class Administrator
        {
            public const string AdministratorRoleName = "Administrator";
            public const string AdministratorUsername = "Admin";
            public const string AdministratorEmail = "admin@esportmanager.net";
            public const string AdministratorPassword = "admin567";
        }
    }
}
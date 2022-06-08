
namespace E_SportManager.Common
{
    public static class ErrorMessages
    {
        public static class Player
        {
            public const string NameLengthErrorMessage= "The {0} must be at least {2} and at max {1} characters long.";
            public const string DescriptionLengthErrorMessage= "The {0} must be at least {1} characters long.";
            public const string PlayerExistingErrorMessage = "There is already player with this name";
        }
    }
}

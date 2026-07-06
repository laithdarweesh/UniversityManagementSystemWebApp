namespace UniversityManagementSystem.Domain.Shared.Guards
{
    public static class Ensure
    {
        //Validate Positive Id
        public static void ValidatePositiveId(int id, string parameterName)
        {
            if (id <= 0)
                throw new ArgumentException($"{parameterName} must be greater than zero");
        }
    }
}
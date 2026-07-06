namespace UniversityManagementSystem.Application.Commands.Persons
{
    public class UpdatePersonCommand
    {
        public string? Email { get; }
        public string? ImagePath { get; }
        public UpdatePersonCommand(string? email, string? imagePath)
        {
            this.Email = email;
            this.ImagePath = imagePath;
        }
    }
}

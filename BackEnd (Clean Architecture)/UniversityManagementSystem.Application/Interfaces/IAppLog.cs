namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IAppLog
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Interfaces.Admins
{
    public interface IAdminRepository
    {
        int Add(Admin admin);
        void Update(Admin admin);
        void Delete(int adminId);
        Admin? GetByAdminId(int adminId);
        Admin? GetByPersonId(int personId);
        List<Admin> GetAll();
        void ChangeStatus(int adminId, byte status);
        bool ExistsByAdminId(int adminId);
        bool ExistsByPersonId(int personId);
    }
}
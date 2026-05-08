using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Admin
    {
        public int AdminId { get; private set; }
        public int PersonId { get; private set; }
        public string AdminName { get; private set; }
        public string PasswordHash { get; private set; }
        public byte PermissionLevel { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public int CreatedByAdminId { get; private set; }
        public DateTime LastStatusDate { get; private set; }
        private Admin() { }
        private Admin(int adminId, int personId, string adminName, string passwordHash,
                      byte permissionLevel, int createdByAdminId)
        {
            _ValidateAdminName(adminName);
            _ValidatePermission(permissionLevel);
            _ValidatePasswordHash(passwordHash);
            _ValidatePerson(personId);
            _ValidateCreatedBy(createdByAdminId);

            this.AdminId = adminId;
            this.PersonId = personId;
            this.AdminName = adminName;
            this.PasswordHash = passwordHash;
            this.PermissionLevel = permissionLevel;
            this.CreatedByAdminId = createdByAdminId;

            this.IsActive = true;
            this.CreatedDate = DateTime.UtcNow;
            this.LastStatusDate = DateTime.UtcNow;
        }
        public static (Admin admin, string password) Add(int personId, string adminName,
                                                         byte permissionLevel, int createdByAdminId)
        {
            string randomPassword = _GenerateRandomPassword();
            _ValidatePassword(randomPassword);

            string hashPassword = _HashPassword(randomPassword);
            _ValidatePasswordHash(hashPassword);

            var admin = new Admin(0, personId, adminName, hashPassword, permissionLevel, createdByAdminId);
            return (admin, randomPassword);
        }
        public void ChangeAdminName(string newAdminName)
        {
            _EnsureActive();
            _ValidateAdminName(newAdminName);

            this.AdminName = newAdminName;
            _UpdateLastModifiedDate();
        }
        public void ChangePassword(string oldPassword, string newPassword)
        {
           _EnsureActive();

            if (!_VerifyPassword(oldPassword))
                throw new ArgumentException("Invalid password");

            _ValidatePassword(newPassword);

            string passwordHash = _HashPassword(newPassword);

            _ValidatePasswordHash(passwordHash);

            this.PasswordHash = passwordHash;
            _UpdateLastModifiedDate(); // must be registered by Admin Id, not any Admin
        }
        public void ChangePermissionLevel(byte newPermissionLevel)
        {
            _EnsureActive();
            _ValidatePermission(newPermissionLevel);

            this.PermissionLevel = newPermissionLevel;
            _UpdateLastModifiedDate();
        }
        public void DeactivateAccount()
        {
            if (!this.IsActive)
                return;

            this.IsActive = false;
            _UpdateLastModifiedDate();
        }
        public void ActivateAccount()
        {
            if (this.IsActive)
                return;

            this.IsActive = true;
            _UpdateLastModifiedDate();
        }


        private void _EnsureActive()
        {
            if (!this.IsActive)
                throw new ArgumentException("Admin is not active");
        }
        private void _UpdateLastModifiedDate()
        {
            this.LastStatusDate = DateTime.UtcNow;
        }

        //Methods related to password and security
        private static string _GenerateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }
        private static string _HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
        private bool _VerifyPassword(string password)
        {
            return this.PasswordHash == _HashPassword(password);
        }

        //Validation Methods
        private void _ValidateAdminName(string adminName)
        {
            if (string.IsNullOrWhiteSpace(adminName))
                throw new ArgumentException("Admin name is required");

            if (adminName.Length < 8)
                throw new ArgumentException("Admin name too short");

            // No spaces + only letters/numbers
            if (!adminName.All(char.IsLetterOrDigit))
                throw new ArgumentException("Admin name must be letters and numbers only");
        }
        private static void _ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required");
        }
        private static void _ValidatePasswordHash(string hashPassword)
        {
            if (string.IsNullOrWhiteSpace(hashPassword))
                throw new ArgumentException("Invalid password hash");
        }
        private void _ValidatePermission(byte permissionLevel)
        {
            if (permissionLevel < 1 || permissionLevel > 64)
                throw new ArgumentException("Invalid permissionLevel");
        }
        private void _ValidatePerson(int personId)
        {
            if (personId <= 0)
                throw new ArgumentException("Invalid person id");
        }
        private void _ValidateCreatedBy(int createdById)
        {
            if (createdById <= 0)
                throw new ArgumentException("Invalid creator");
        }
    }
}
using UniversityManagementSystem.Domain.Shared.Guards;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Admin
    {
        public int AdminId { get; private set; }
        public int PersonId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int CreatedByUserId { get; private set; }
        public byte Status { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public int? UpdatedByUserId { get; private set; }
        private Admin() { }
        private Admin(int adminId, int personId, DateTime createdAt, int createdByUserId,
                      byte status, DateTime? updatedAt, int? updatedByUserId)
        {
            //adminId only allowed to equal "0" when creating new Admin

            if (adminId < 0)
                throw new ArgumentException("AdminId cannot be negative", nameof(adminId));

            //Ensure positive id for personId, createdByUserId and updatedByUserId 

            Ensure.ValidatePositiveId(personId, nameof(personId));
            Ensure.ValidatePositiveId(createdByUserId, nameof(createdByUserId));

            if (updatedByUserId.HasValue)
                Ensure.ValidatePositiveId(updatedByUserId.Value, nameof(updatedByUserId));

            //Validate status range

            _ValidateStatus(status);

            this.AdminId = adminId;
            this.PersonId = personId;
            this.CreatedAt = createdAt;
            this.CreatedByUserId = createdByUserId;
            this.Status = status;
            this.UpdatedAt = updatedAt;
            this.UpdatedByUserId = updatedByUserId;
        }

        /// <summary>
        /// Creates a new Admin entity before saving it to database.
        /// AdminId is initialized with 0 because the database
        /// will generate the identity value.
        /// </summary>
        public static Admin Create(int personId, int createdByUserId, byte status)
        {
            return new Admin(0, personId, DateTime.UtcNow, createdByUserId, status, null, null);
        }

        /// <summary>
        /// Loads an existing Admin entity from database data.
        /// Used by repositories when reconstructing domain objects.
        /// </summary>
        public static Admin Load(int adminId, int personId, DateTime createdAt, int createdByUserId,
                                 byte status, DateTime? updatedAt, int? UpdatedByUserId)
        {
            return new Admin(adminId, personId, createdAt, createdByUserId, status, updatedAt, UpdatedByUserId);
        }
        public void MarkAsUpdated(int userId)
        {
            Ensure.ValidatePositiveId(userId, nameof(userId));

            this.UpdatedAt = DateTime.UtcNow;
            this.UpdatedByUserId = userId;
        }

        public void SetStatus(byte newStatus)
        {
            _ValidateStatus(newStatus);
            this.Status = newStatus;
        }

        //Validate status
        private void _ValidateStatus(byte status)
        {
            if (status < 1 || status > 5)
                throw new ArgumentOutOfRangeException(nameof(status), "Status must be between 1 and 5");
        }

        //public void ChangeAdminName(string newAdminName)
        //{
        //    _EnsureActive();
        //    _ValidateAdminName(newAdminName);

        //    this.AdminName = newAdminName;
        //    _UpdateLastModifiedDate();
        //}
        //public void ChangePassword(string oldPassword, string newPassword)
        //{
        //    _EnsureActive();

        //    if (!_VerifyPassword(oldPassword))
        //        throw new ArgumentException("Invalid password");

        //    _ValidatePassword(newPassword);

        //    string passwordHash = _HashPassword(newPassword);

        //    _ValidatePasswordHash(passwordHash);

        //    this.PasswordHash = passwordHash;
        //    _UpdateLastModifiedDate(); // must be registered by Admin Id, not any Admin
        //}
        //public void DeactivateAccount()
        //{
        //    if (!this.IsActive)
        //        return;

        //    this.IsActive = false;
        //    _UpdateLastModifiedDate();
        //}
        //public void ActivateAccount()
        //{
        //    if (this.IsActive)
        //        return;

        //    this.IsActive = true;
        //    _UpdateLastModifiedDate();
        //}

        //Methods related to password and security

        //private static string _GenerateRandomPassword()
        //{
        //    return Guid.NewGuid().ToString("N").Substring(0, 8);
        //}
        //private static string _HashPassword(string password)
        //{
        //    return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        //}
        //private bool _VerifyPassword(string password)
        //{
        //    return this.PasswordHash == _HashPassword(password);
        //}

        //Validation Methods
        //private void _ValidateAdminName(string adminName)
        //{
        //    if (string.IsNullOrWhiteSpace(adminName))
        //        throw new ArgumentException("Admin name is required");

        //    if (adminName.Length < 8)
        //        throw new ArgumentException("Admin name too short");

        //    // No spaces + only letters/numbers
        //    if (!adminName.All(char.IsLetterOrDigit))
        //        throw new ArgumentException("Admin name must be letters and numbers only");
        //}
        //private static void _ValidatePassword(string password)
        //{
        //    if (string.IsNullOrWhiteSpace(password))
        //        throw new ArgumentException("Password is required");
        //}
        //private static void _ValidatePasswordHash(string hashPassword)
        //{
        //    if (string.IsNullOrWhiteSpace(hashPassword))
        //        throw new ArgumentException("Invalid password hash");
        //}
        //private void _ValidatePermission(byte permissionLevel)
        //{
        //    if (permissionLevel < 1 || permissionLevel > 64)
        //        throw new ArgumentException("Invalid permissionLevel");
        //}
    }
}
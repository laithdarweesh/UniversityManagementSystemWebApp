using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Doctor
    {
        public int DoctorId { get; private set; }
        public int PersonId { get; private set; }
        public int CollegeId { get; private set; }
        public int DepartmentId { get; private set; }
        public byte DoctorStatus { get; private set; }
        public decimal Salary { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public int CreatedByAdminId { get; private set; }
        public DateTime LastStatusDate { get; private set; }
        public byte PermissionLevel { get; private set; }
        public bool IsActive { get; private set; }
        public string PasswordHash { get; private set; }
        private Doctor() { }
        private Doctor(int doctorId, int personId, int collegeId, int departmentId, byte doctorStatus, decimal salary, 
                       int createdByAdminId, byte permissionLevel, bool isActive, string passwordHash)
        {
            this.DoctorId = doctorId;
            this.PersonId = personId;
            this.CollegeId = collegeId;
            this.DepartmentId = departmentId;
            this.DoctorStatus = doctorStatus;
            this.Salary = salary;
            this.CreatedByAdminId = createdByAdminId;
            this.PermissionLevel = permissionLevel;
            this.IsActive = isActive;   
            this.PasswordHash = passwordHash;
        }
    }
}

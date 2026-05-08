using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Shared.Utilities;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Phone
    {
        public int PhoneId { get; private set; }
        public string PhoneNumber { get; private set; }
        public int PersonId { get; private set; }
        private Phone() { }
        private Phone(int PhoneId, string PhoneNumber, int PersonId)
        {
            _ValidatePhone(PhoneNumber);
            _ValidatePerson(PersonId);

            this.PhoneId = PhoneId;
            this.PhoneNumber = PhoneNumber;
            this.PersonId = PersonId;
        }
        public void SetPhone(string NewPhoneNumber)
        {
            _ValidatePhone(NewPhoneNumber);
            this.PhoneNumber = NewPhoneNumber;
        }
        public static Phone Add(string PhoneNumber, int PersonId)
        {
            return new Phone(0,PhoneNumber,PersonId);
        }
        private void _ValidatePhone(string PhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
                throw new ArgumentException("Phone number is required");

            if (!clsValidation.ValidateInteger(PhoneNumber))
                throw new ArgumentException("Phone must contain digits only");

            if (PhoneNumber.Length < 9 || PhoneNumber.Length > 15)
                throw new ArgumentException("Invalid phone number length");
        }
        private void _ValidatePerson(int PersonId)
        {
            if (PersonId <= 0)
                throw new ArgumentException("Invalid person id");
        }
    }
}
using UniversityManagementSystem.Domain.Shared.Guards;
using UniversityManagementSystem.Shared.Utilities;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Phone
    {
        public int PhoneId { get; private set; }
        public string PhoneNumber { get; private set; }
        public int PersonId { get; private set; }
        private Phone() { }
        private Phone(int phoneId, string phoneNumber, int personId)
        {
            if (phoneId < 0)
                throw new ArgumentException("PhoneId cannot be negative", nameof(phoneId));

            _ValidatePhone(phoneNumber);
            Ensure.ValidatePositiveId(personId, nameof(personId));

            this.PhoneId = phoneId;
            this.PhoneNumber = phoneNumber;
            this.PersonId = personId;
        }

        /// <summary>
        /// Creates a new Phone entity before saving it to database.
        /// PhoneId is initialized with 0 because the database
        /// will generate the identity value.
        /// </summary>
        public static Phone Create(string phoneNumber, int personId)
        {
            return new Phone(0, phoneNumber, personId);
        }

        /// <summary>
        /// Loads an existing Phone entity from database data.
        /// Used by repositories when reconstructing domain objects.
        /// </summary>
        public static Phone Load(int phoneId, string phoneNumber, int personId)
        {
            return new Phone(phoneId, phoneNumber, personId);
        }

        //Set Phone
        public void SetPhoneNumber(string newPhoneNumber)
        {
            _ValidatePhone(newPhoneNumber);
            this.PhoneNumber = newPhoneNumber;
        }

        private static void _ValidatePhone(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required");

            if (!Validation.ValidateInteger(phoneNumber))
                throw new ArgumentException("Phone must contain digits only");

            if (phoneNumber.Length < 9 || phoneNumber.Length > 15)
                throw new ArgumentException("Invalid phone number length");
        }
    }
}
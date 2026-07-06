using UniversityManagementSystem.Domain.Shared.Guards;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Address
    {
        public int AddressId { get; private set; }
        public string AddressName { get; private set; }
        public int PersonId { get; private set; }
        private Address() { }
        private Address(int addressId, string addressName, int personId)
        {
            if (addressId < 0)
                throw new ArgumentException("AddressId cannot be negative", nameof(addressId));

            //Validate Address and Person Id
            _ValidateAddress(addressName);
            Ensure.ValidatePositiveId(personId, nameof(personId));

            this.AddressId = addressId;
            this.AddressName = addressName;
            this.PersonId = personId;
        }

        /// <summary>
        /// Creates a new Address entity before saving it to database.
        /// AddressId is initialized with 0 because the database
        /// will generate the identity value.
        /// </summary>
        public static Address Create(string addressName, int personId)
        {
            return new Address(0, addressName, personId);
        }

        /// <summary>
        /// Loads an existing Address entity from database data.
        /// Used by repositories when reconstructing domain objects.
        /// </summary>
        public static Address Load(int addressId, string addressName, int personId)
        {
            return new Address(addressId, addressName, personId);
        }

        //Set AddressName
        public void SetAddressName(string newAddressName)
        {
            //Validate Address
            _ValidateAddress(newAddressName);
            this.AddressName = newAddressName;
        }

        //Validation Methods
        private static void _ValidateAddress(string addressName)
        {
            if (string.IsNullOrWhiteSpace(addressName))
                throw new ArgumentException("Invalid address name");
        }
    }
}
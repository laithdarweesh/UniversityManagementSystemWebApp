using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Address
    {
        public int AddressID { get; private set; }
        public string AddressName { get; private set; }
        public int PersonId { get; private set; }
        private Address() { }
        private Address(int AddressID, string AddressName, int PersonId)
        {
            //Validate Address and Person Id
            _ValidateAddress(AddressName);
            _ValidatePerson(PersonId);

            this.AddressID = AddressID;
            this.AddressName = AddressName;
            this.PersonId = PersonId;
        }
        public void SetAddressName(string NewAddressName)
        {
            //Validate Address
            _ValidateAddress(NewAddressName);
            this.AddressName = NewAddressName;
        }
        public static Address Add(string AddressName, int PersonId)
        {
            return new Address(0, AddressName, PersonId);
        }

        //Validation Methods
        private void _ValidateAddress(string AddressName)
        {
            if (string.IsNullOrWhiteSpace(AddressName))
                throw new ArgumentException("Invalid address name");
        }
        private void _ValidatePerson(int PersonId)
        {
            if (PersonId <= 0)
                throw new ArgumentException("Invalid person id");
        }
    }
}
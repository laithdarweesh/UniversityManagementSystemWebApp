using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Interfaces.Phones
{
    public interface IPhoneRepository
    {
        int Add(Phone Phone);
        bool Update(Phone Phone);
        Phone Get(int PhoneId);
        Phone Get(string PhoneNumber);
        List<Phone> GetAllPhones();
        List<Phone> GetAllPhonesByPerson(int PersonId);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Interfaces.Persons
{
    public interface IPersonRepository
    {
        int Add(Person Person);
        bool Update(Person Person);
        bool Delete(int PersonId);
        Person GetByPersonId(int PersonId);
        Person GetByNationalNo(string NationalNo);
        List<Person> GetAllPeople();
        bool IsPersonExist(int PersonId);
        bool IsPersonExist(string NationalNo);
    }
}
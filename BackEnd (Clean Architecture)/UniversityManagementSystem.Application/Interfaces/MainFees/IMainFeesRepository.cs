using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Interfaces.MainFee;

public interface IMainFeesRepository
{
    int Add(MainFees MainFees);
    bool Update(MainFees MainFees);
    bool Delete(int FeeId);
    MainFees GetFeeInfoById(int FeeId);
    List<MainFees> GetAllFees();
}
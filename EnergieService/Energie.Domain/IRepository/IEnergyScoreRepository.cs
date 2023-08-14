using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface IEnergyScoreRepository
    {
        Task<List<EnergyScore>> GetAllEnergyScores(string userId);
        Task<EnergyScore> AddEnergyScoreAsync(EnergyScore energyScore);
        Task<EnergyScore> GetCompanyUserEnergyScore(int userId);
        Task<List<Month>> GetAllMonth();

        //for department
        Task<Department> GetDepartmentIdByUser(string email);
        Task<List<EnergyScore>> GetDepartmentScoreList(int departmentId);
        Task<List<EnergyScore>> GetAllDepartmentEnergyScores(int departmentId);

        //For MonthlyScore Email
        Task<IList<EnergyScore>> GetAllUserEnergyScores(); 

    }
}

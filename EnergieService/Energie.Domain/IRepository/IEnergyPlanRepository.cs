using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface IEnergyPlanRepository
    {
        Task AddEnergyPlanAsync(EnergyPlan energyPlan);
        Task<IList<EnergyPlan>> GetEnergyPlanListAsync(string userEmail);
        Task<EnergyPlan> GetEnergyPlanForUserAsync(int userId, int favouriteTipId, int tiptype);

        Task<EnergyPlan> GetEnergyPlanbyIdAsync (int id, string userEmail);  

        Task DeleteEnergyPlanAsync(int Id); 

        Task UpdateEnergyPlanAsync (EnergyPlan energyPlan);

        Task<List<DateTime>> GetEnergyPlanDeadlineAsync(string useremail);

        Task<List<EnergyPlan>> GetEnergyPlansForDeadline(string userEmail); 
        

        //For Department 

        Task AddDepartmentEnergyPlanAsync(DepartmentEnergyPlan EnergyPlan);

        Task<DepartmentEnergyPlan> GetDepartmentEnergyPlanAsync(int departmentId, int favouriteTipId, int Tiptype);
        Task<IList<DepartmentEnergyPlan>> GetDepartmentEnergyPlanListAsync(string useremail);
        Task<DepartmentEnergyPlan> GetDepartmentEnergyPlanByIdAsync(int Id, string userEmail);
        Task UpdateDepartmentEnergyPlanAsync(DepartmentEnergyPlan energyPlan);
        Task DeleteDepartmentEnergyPlanAsync(int id, string userEmail);


        //for email
        Task<IList<EnergyPlan>> GetEnergyPlanAsync();

    }
}

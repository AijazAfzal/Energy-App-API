using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface IEnergyTipRepository
    {
        //tips are added by superAdmin for personal
        Task AddEnergyTipAsync(Tip tip);
        Task<List<Tip>> GetEnergyTipAsync();
        Task<Tip> GetEnergyTipByIdAsync(int id);
        Task UpdateEnergyTipAsync(Tip tip);
        Task<int> DeleteEnergyTipAsync(int tipId);

        // tips are added by superAdmin for department
        Task AddDepartmentTipAsync(DepartmentTip depatrmentTips);
        Task<IList<DepartmentTip>> GetDepatrmentTipListAsync();
        Task<DepartmentTip> GetDepatrmentTipByIdAsync(int Id);
        Task UpdateDepartmentTipAsync(DepartmentTip departmentTip);
        Task DeleteDepartmentTipAsync(int Id);
    }
}

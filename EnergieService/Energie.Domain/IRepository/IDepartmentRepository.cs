using Energie.Domain.Domain;

namespace Energie.Domain.IRepository
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartmentListAsync(int companyid);
        Task<List<string>> GetDepartmentByCompanyIdAsync(int companyId);
        Task<int> AddDepartmentAsync(Department companyDepartment);
        Task<List<CompanyUser>> GetDepartmentUserAsync(int departmentId);
        //Task GetDepartmentList(int? companyId);
    }
}

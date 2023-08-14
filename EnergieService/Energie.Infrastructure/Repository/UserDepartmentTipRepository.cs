using Energie.Domain;
using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Energie.Model;
using Energie.Model.Response;
using Microsoft.EntityFrameworkCore;

namespace Energie.Infrastructure.Repository
{
    public class UserDepartmentTipRepository : IUserDepartmentTipRepository
    {
        readonly AppDbContext _appDbContext;
        public UserDepartmentTipRepository(AppDbContext appDbContext)
        {
            _appDbContext=appDbContext; 
        }

        public async Task AddUserDepartmentTipAsync(UserDepartmentTip userDepartmentTip)
        {
            await _appDbContext.UserDepartmentTip.AddAsync(userDepartmentTip);
            await _appDbContext.SaveChangesAsync(); 
           
        }

        public async Task DeleteUserDepartmentTipAsync(int id)
        {
           var userdepartmenttip= await _appDbContext.UserDepartmentTip.Where(x=>x.Id==id).FirstOrDefaultAsync();
            _appDbContext.UserDepartmentTip.Remove(userdepartmenttip); 
            await _appDbContext.SaveChangesAsync(); 
           
        }

        public async Task<List<UserDepartmentTip>> GetUserDepartmentTipsAddedByCollegue(string userEmail)
        {
            var currentdatee = DateTime.UtcNow;
            var currentUser = await _appDbContext.CompanyUser.FirstOrDefaultAsync(x => x.Email == userEmail);
            return await _appDbContext.UserDepartmentTip.Include(x => x.CompanyUser).Where(x => x.CompanyUser!.DepartmentID == currentUser!.DepartmentID && x.CreatedOn >= currentdatee.AddDays(-2)).ToListAsync(); 
        }

        public async Task<UserDepartmentTip> GetUserDepartmentTipbyIdAsync(int Id)
        {
           return await _appDbContext.UserDepartmentTip.Where(x => x.Id == Id).FirstOrDefaultAsync();   

        }

        public async Task<List<UserDepartmentTip>> GetUserDepartmentTipListAsync(int id) 
        {
            return await _appDbContext.UserDepartmentTip
                         .Where(x=>x.CompanyUser.DepartmentID==id)
                         .Include(x=>x.CompanyUser.Department)
                         .Include(x=>x.EnergyAnalysisQuestions)
                         .Include(x=>x.EnergyAnalysisQuestions.EnergyAnalysis)
                         .ToListAsync(); 
        }

        public async Task UpdateUserDepartmentTipAsync(UserDepartmentTip userDepartmentTip)
        {
            _appDbContext.UserDepartmentTip.Update(userDepartmentTip); 
            await _appDbContext.SaveChangesAsync(); 
            
        }
    }
}

using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Energie.Infrastructure.Repository
{
    public class EnergyTipRepository : IEnergyTipRepository
    {
        private readonly AppDbContext _appDbContext;
        public EnergyTipRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region these tips are created by superAdmin for personal tip for user
        public async Task AddEnergyTipAsync(Tip tip)
        {
            await _appDbContext.Tips.AddAsync(tip);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<List<Tip>> GetEnergyTipAsync()
        {
            return await _appDbContext.Tips
                        .Include(X => X.EnergyAnalysisQuestions)
                        .Include(x => x.EnergyAnalysisQuestions.EnergyAnalysis)
                        .ToListAsync();

        }
        public async Task<Tip> GetEnergyTipByIdAsync(int id)
        {
            return await _appDbContext.Tips.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task UpdateEnergyTipAsync(Tip tip)
        {
            _appDbContext.Tips.Update(tip);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<int> DeleteEnergyTipAsync(int tipId)
        {
            var category = await _appDbContext.Tips.Where(x => x.Id == tipId).FirstOrDefaultAsync();
            _appDbContext.Tips.Remove(category);
            return await _appDbContext.SaveChangesAsync();
        }
        #endregion

        #region these tips are created by superAdmin for department tip for user

        public async Task AddDepartmentTipAsync(DepartmentTip depatrmentTips)
        {
            await _appDbContext.DepatrmentTip.AddAsync(depatrmentTips);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<IList<DepartmentTip>> GetDepatrmentTipListAsync()
        {
            return await _appDbContext.DepatrmentTip
                                       .Include(x => x.EnergyAnalysisQuestions)
                                       .Include(x => x.EnergyAnalysisQuestions.EnergyAnalysis).ToListAsync();
        }
        public async Task<DepartmentTip> GetDepatrmentTipByIdAsync(int Id)
        {
            return await _appDbContext.DepatrmentTip
                         .Where(x => x.ID == Id)
                         .FirstOrDefaultAsync();

        }
        public async Task UpdateDepartmentTipAsync(DepartmentTip departmentTip)
        {
            _appDbContext.DepatrmentTip.Update(departmentTip);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteDepartmentTipAsync(int Id)
        {
            var depatrmentTip = await _appDbContext.DepatrmentTip.Where(x => x.ID == Id).FirstOrDefaultAsync();
            _appDbContext.DepatrmentTip.Remove(depatrmentTip);
            await _appDbContext.SaveChangesAsync();
        }
        #endregion

    }
}

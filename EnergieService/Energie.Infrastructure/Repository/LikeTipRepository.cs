using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Infrastructure.Repository
{
    public class LikeTipRepository : ILikeTipRepository
    {
        private readonly AppDbContext _appDbContext;
        public LikeTipRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddLikeTipAsync(LikeTip likeTip)
        {
            await _appDbContext.LikeTip.AddAsync(likeTip);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<LikeTip> GetLikeTipAsync(int userId, int departmenttipId)
        {
            return await _appDbContext.LikeTip
                        .Where(x => x.DepartmentTipId == departmenttipId && x.CompanyUserID == userId)
                        .FirstOrDefaultAsync();
        }
        public async Task RemoveLikeTipAsync(int userId, int departmentTipId)
        {
            var liketip = await _appDbContext.LikeTip
                                 .Where(x => x.DepartmentTipId == departmentTipId && x.CompanyUserID == userId)
                                 .FirstOrDefaultAsync();
            _appDbContext.LikeTip.Remove(liketip);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<LikeTip>> GetLikeTipListAsync(string userEmail)
        {
            return await _appDbContext.LikeTip.Where(x=> x.CompanyUsers.Email == userEmail).ToListAsync();
        }
    }
}

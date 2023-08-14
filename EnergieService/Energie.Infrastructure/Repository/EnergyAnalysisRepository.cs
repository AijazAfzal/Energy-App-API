using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Infrastructure.Repository
{
    public class EnergyAnalysisRepository : IEnergyAnalysisRepository
    {
        private readonly AppDbContext _appDbContext;
        public EnergyAnalysisRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IList<EnergyAnalysis>> GetAllEnergySourceAsync()
        {
            return await _appDbContext.EnergyAnalysis.ToListAsync();
        }

        public async Task<IList<EnergyAnalysisQuestions>> GetAnalysisQuestionsByIdAsync(int id)
        {
            var energyAnalysisQuestion = await _appDbContext.EnergyAnalysisQuestions
                                               .Where(x => x.EnergyAnalysisID == id).ToListAsync();
            return energyAnalysisQuestion;
        }
        public async Task<IList<EnergyAnalysisQuestions>> GetAllEnergyAnalysisQuestions()
        {
            return await _appDbContext.EnergyAnalysisQuestions
                        .Include(x=> x.EnergyAnalysis).ToListAsync();
        }
        public async Task SetEnergyAnalysis(UserEnergyAnalysis userEnergyAnalysis)
        {
            await _appDbContext.UserEnergyAnalyses.AddAsync(userEnergyAnalysis);
        }
        public async Task<int> SaveChangesForEnergyAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
        public async Task<UserEnergyAnalysis> GetUserEnergyAnalysisAsync(int userId)
        {
            return await _appDbContext.UserEnergyAnalyses.Where(x=> x.CompanyUserID == userId).FirstOrDefaultAsync();
        }

        public async Task DeleteEnergyAnalysis(int id)
        {
            var responses =await _appDbContext.UserEnergyAnalyses.Where(x => x.CompanyUserID == id).ToListAsync();
            _appDbContext.UserEnergyAnalyses.RemoveRange(responses);
        }
        public async Task<IList<UserEnergyAnalysis>> UserEnergyAnalysisAsync(string useremail)
        {
            return await _appDbContext.UserEnergyAnalyses
                        .Include(x => x.EnergyAnalysisQuestions)
                        .Include(x => x.EnergyAnalysisQuestions.EnergyAnalysis)
                        .Include(x => x.CompanyUser).Where(x=> x.CompanyUser.Email == useremail).ToListAsync();
        }
        public async Task<IList<UserEnergyAnalysis>> GetAllDepartmentUserEnergyAnalyses(int departmentId)
        {
            return await _appDbContext
                        .UserEnergyAnalyses
                        .Include(x => x.EnergyAnalysisQuestions)
                        .Include(x => x.EnergyAnalysisQuestions.EnergyAnalysis)
                        .Include(x => x.CompanyUser)
                        .Where(x => x.CompanyUser.DepartmentID == departmentId)
                        .ToListAsync();
        }
        public async Task<EnergyAnalysisQuestions> EnergyAnalysisQuestionsAsyncById(int id)
        {
            var energyAnalysisQuestions = await _appDbContext.EnergyAnalysisQuestions
                                            .Where(x=> x.Id== id).FirstOrDefaultAsync();
            return energyAnalysisQuestions;
        } 

        //For functionApp
        public async Task<IList<UserEnergyAnalysis>> GetAllUserEnergyAnalysis()
        {
            return await _appDbContext.UserEnergyAnalyses.Include(x => x.EnergyAnalysisQuestions)
                                                         .Include(x => x.EnergyAnalysisQuestions.EnergyAnalysis)
                                                         .Include(x => x.CompanyUser)
                                                         .ToListAsync();
        } 
    }
}

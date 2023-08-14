using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface IEnergyAnalysisRepository
    {
        //Task<IList<EnergyAnalysis>> GetAllEnergyAnalysisAsync();
        Task<IList<EnergyAnalysis>> GetAllEnergySourceAsync();
        Task<IList<EnergyAnalysisQuestions>> GetAnalysisQuestionsByIdAsync(int id);
        Task<IList<EnergyAnalysisQuestions>> GetAllEnergyAnalysisQuestions();
        Task SetEnergyAnalysis(UserEnergyAnalysis userEnergyAnalysis);
        Task<int> SaveChangesForEnergyAsync();
        Task<UserEnergyAnalysis> GetUserEnergyAnalysisAsync(int userId);
        Task DeleteEnergyAnalysis(int id);
        Task<IList<UserEnergyAnalysis>> UserEnergyAnalysisAsync(string useremail);
        Task<IList<UserEnergyAnalysis>> GetAllDepartmentUserEnergyAnalyses(int departmentId);
        Task<EnergyAnalysisQuestions> EnergyAnalysisQuestionsAsyncById(int id);

        Task<IList<UserEnergyAnalysis>> GetAllUserEnergyAnalysis(); 
    }
}

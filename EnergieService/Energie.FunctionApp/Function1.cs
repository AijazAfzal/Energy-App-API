using System;
using Energie.Business.IServices;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Energie.Domain.IRepository;
using System.Linq;

namespace Energie.FunctionApp
{
    public class Function1
    {
        readonly IEnergyPlanRepository _energyPlanRepository;
        readonly IUserEnergyTipRepository _userEnergyTipRepository;
        readonly ISendGrid _sendGrid;
        readonly IEnergyAnalysisRepository _energyAnalysisRepository;
        readonly IEnergyScoreRepository _energyScoreRepository;
        public Function1(IEnergyPlanRepository energyPlanRepository
            , IUserEnergyTipRepository userEnergyTipRepository
            , ISendGrid sendGrid
            , IEnergyAnalysisRepository energyAnalysisRepository
            , IEnergyScoreRepository energyScoreRepository)
        {
            _energyPlanRepository = energyPlanRepository;
            _userEnergyTipRepository = userEnergyTipRepository;
            _sendGrid = sendGrid;
            _energyAnalysisRepository = energyAnalysisRepository;
            _energyScoreRepository = energyScoreRepository;
        }
        [FunctionName("EmailDeadlineTimeTrigger")]
        public async Task Run1([TimerTrigger("0 0 9 * * *")] TimerInfo myTimer, ExecutionContext context)
        {
            var getallfavtips = await _userEnergyTipRepository.GetAllFavTipAsync();
            var superadminfavtips = getallfavtips.Item1.Select(x => new Model.Request.UserFavouriteTipRequest
            {
                Id = x.Id,
                Name = x.Tips.Name,
                Description = x.Tips.Description
            }).ToList();
            var employerhelpfav = getallfavtips.Item2.Select(x => new Model.Request.UserFavouriteTipRequest
            {
                Id = x.Id,
                Name = x.CompanyHelp.Name,
                Description = x.CompanyHelp.Description
            }).ToList();
            var userTip = getallfavtips.Item3.Select
                (x => new Model.Request.UserFavouriteTipRequest
                {
                    Id = x.Id,
                    Name = x.EnergyAnalysisQuestions.Name,
                    Description = x.Description
                }).ToList();
            var path = context.FunctionAppDirectory;
            superadminfavtips.AddRange(employerhelpfav);
            superadminfavtips.AddRange(userTip);

            var plans = await _energyPlanRepository.GetEnergyPlanAsync();
            if (plans != null)
            {

                foreach (var item in superadminfavtips)
                {
                    foreach (var item2 in plans)
                    {
                        DateTime currentdatee = DateTime.UtcNow;
                        TimeSpan difference = item2.PlanEndDate.Subtract(currentdatee);
                        int difffernceinDays = (int)Math.Round(difference.TotalDays);

                        if (item2.FavouriteTipId == item.Id && difffernceinDays <= 1)
                        {
                            string body = await _sendGrid.PopulateBodyForDeadlineEmail(item2.CompanyUser.UserName, item.Description, item2.PlanEndDate, path);
                            await _sendGrid.SendMailForPlanDeadline(item2.CompanyUser, body);

                        }
                    }
                }
            }
        }
        [FunctionName("EnergyAnalysisTimeTrigger")]
        public async Task Run2([TimerTrigger("0 0 9 1-7 12 *")] TimerInfo myTimer , ExecutionContext context)
        {
            var EnergyAnalysisList = await _energyAnalysisRepository.GetAllUserEnergyAnalysis();
            var path = context.FunctionAppDirectory;
            var currentdate = DateTime.Now;
            var firstdatee = new DateTime(currentdate.Year, 12, 1);
            var endDatee = firstdatee.AddDays(6);

            foreach (var itemm in EnergyAnalysisList)
            {

                if (currentdate >= firstdatee && currentdate <= endDatee && itemm.CreatedOn.Month != 12)
                {
                    string body = await _sendGrid.PopulateBodyForAnalysisEmail(itemm.CompanyUser.UserName,path);
                    await _sendGrid.SendMailForAnalysis(itemm.CompanyUser, body);
                }
            }
        }
        [FunctionName("MonthlyScoreTimeTrigger")]
        public async Task Run3([TimerTrigger("0 0 9 1 * *")] TimerInfo myTimer, ExecutionContext context)
        {
            var monthlyscores = await _energyScoreRepository.GetAllUserEnergyScores();
            var path = context.FunctionAppDirectory;
            var currentdateee = DateTime.Now;
            foreach (var score in monthlyscores)
            {

                if (score.Year == currentdateee.Year)
                {
                    string body = await _sendGrid.PopulateBodyForMonthlyScoreEmail(score.CompanyUser.UserName,path); 
                    await _sendGrid.SendMailForMonthlyScore(score.CompanyUser, body);
                }
            }

        }

    }
}

using Energie.Domain.Domain;

namespace Energie.Business.Test
{
    public class BaseClass
    {
        public List<Month> GetListOfmonths()
        {
            var months = new List<Month>()
            {
                new Month(){Id = 1 , Name = "January"},
                new Month(){Id = 2 , Name = "February"},
                new Month(){Id = 3 , Name = "March"},
                new Month(){Id = 4 , Name = "April"},
                new Month(){Id = 5 , Name = "May"},
                new Month(){Id = 6 , Name = "June"},
                new Month(){Id = 7 , Name = "July"},
                new Month(){Id = 8 , Name = "August"},
                new Month(){Id = 9 , Name = "September"},
                new Month(){Id = 10 , Name = "October"},
                new Month(){Id = 11 , Name = "November "},
                new Month(){Id = 12 , Name = "December"}
            };
            return months;
        }
        public List<Domain.Domain.EnergyScore> GetListOfEnergyScore()
        {
            var energyScore = new List<Domain.Domain.EnergyScore>()
            {
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(6, 4, 2023, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(7, 3, 2023, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(8, 2, 2023, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(9, 1, 2023, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(5, 12, 2022, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(4, 11, 2022, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(6, 10, 2022, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(7, 9, 2022, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(8, 8, 2022, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(9, 7, 2022, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(10, 6, 2022, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(7, 5, 2022, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(8, 4, 2022, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(6, 3, 2022, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(9, 2, 2022, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(10, 1, 2022, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(5, 12, 2021, 1),
                new Domain.Domain.EnergyScore().SetEnergyScoreForList(7, 11, 2021, 1)
            };
            return energyScore;
        }
        //public List<Domain.Domain.EnergyScore> GetEnergyScoresForTe
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.Domain
{
    public class EnergyAnalysis
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public EnergyAnalysis SetEnergyAnalysis (string name, DateTime createdOn)
        {
            Name = name;
            CreatedOn = createdOn;
            return this; 
        }

        public EnergyAnalysis SetEnergyAnalysisTest(int id, string name, DateTime createdOn)
        {
            id = Id;
            Name = name;
            CreatedOn = createdOn;
            return this;
        }
    }
}

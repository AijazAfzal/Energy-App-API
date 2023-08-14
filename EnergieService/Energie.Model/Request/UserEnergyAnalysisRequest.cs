using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class UserEnergyAnalysisRequestList
    {
        public List<UserEnergyAnalysisRequest> UserEnergyAnalysisRequest { get; set; }
    }
    [ExcludeFromCodeCoverage]

    public class UserEnergyAnalysisRequest
    {
        public string energyAnalysis { get; set; }
        public string[] energyAnalysisQuestions { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class UserEnergyAnalysisResponseList
    {
        public List<UserEnergyAnalysisResponse> EnergyAnalysisResponse { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class UserEnergyAnalysisResponse
    {
        public string energyAnalysis { get; set; }
        public List<EnergyAnalysisQuestion> EnergyAnalysisQuestionsResponse { get; set;  }
    }
    [ExcludeFromCodeCoverage]
    public class EnergyAnalysisQuestion
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; } = false; 
        public string EnergyAnalysis { get; set; }

    }


}

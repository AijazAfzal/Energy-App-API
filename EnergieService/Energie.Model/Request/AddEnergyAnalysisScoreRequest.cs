﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Model.Request
{
    [ExcludeFromCodeCoverage]
    public class AddEnergyAnalysisScoreRequest
    {
        [Required]
        public int[] EnergyAnalysisRecord { get; set; } 
    }
}

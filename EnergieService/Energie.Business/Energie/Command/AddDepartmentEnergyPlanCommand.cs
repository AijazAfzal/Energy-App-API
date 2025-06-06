﻿using Energie.Model;
using MediatR;

namespace Energie.Business.Energie.Command
{
    public class AddDepartmentEnergyPlanCommand : IRequest<ResponseMessage>
    {
        public int FavouriteTipId { get; set; }
        public int ResponsiblePersonId { get; set; }
        public string TipBy { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsReminder { get; set; }

        public string UserEmail { get; set; }
    }
}

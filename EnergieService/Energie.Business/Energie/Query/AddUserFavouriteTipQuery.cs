﻿using Energie.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Business.Energie.Query
{
    public class AddUserFavouriteTipQuery : IRequest<ResponseMessage>
    {
        public int tipID { get ; set; }
    }
}

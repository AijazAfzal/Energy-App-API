using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface ILikeTipRepository
    {
        Task AddLikeTipAsync(LikeTip likeTip);
        Task<LikeTip> GetLikeTipAsync(int userId, int departmenttipId);
        Task RemoveLikeTipAsync(int userId, int departmentTipId);
        Task<List<LikeTip>> GetLikeTipListAsync(string userEmail);
    }
}

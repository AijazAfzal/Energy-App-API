using Energie.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Domain.IRepository
{
    public interface IFeedbackRepository
    {
        Task AddFeedbackAsync(Feedback feedback);
        Task<Feedback> GetCompanyUserFeedback(int userId);

        Task UpdateUserFeedbackAsync(Feedback feedback);

        Task<Feedback> GetFeedbackByUserEmailAsync(string useremail);    
    }
}

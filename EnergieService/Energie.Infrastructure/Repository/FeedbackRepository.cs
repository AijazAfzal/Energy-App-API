using Energie.Domain.Domain;
using Energie.Domain.IRepository;
using Energie.Infrastructure.ApplicationDbContext;
using Energie.Model.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energie.Infrastructure.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext _appDbContext;

        public FeedbackRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;   
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            await _appDbContext.Feedbacks.AddAsync(feedback);
            await _appDbContext.SaveChangesAsync(); 
            //return feedback;
        }

        public async Task<Feedback> GetCompanyUserFeedback(int userId)
        {
            var feedback = await _appDbContext.Feedbacks.Where(x => x.CompanyUserId == userId)
                .Include(x => x.CompanyUser).Include(x => x.CompanyUser.Department)
                .Include(x => x.CompanyUser.Department.Company)
                .FirstOrDefaultAsync();
            return feedback;
        }

        public async Task<Feedback> GetFeedbackByUserEmailAsync(string useremail) 
        {
            return await _appDbContext.Feedbacks.Where(x =>x.CompanyUser.Email == useremail).FirstOrDefaultAsync(); 
        }

        public async Task UpdateUserFeedbackAsync(Feedback feedback)
        {
            _appDbContext.Feedbacks.Update(feedback); 
            await _appDbContext.SaveChangesAsync(); 
        }
    }
}

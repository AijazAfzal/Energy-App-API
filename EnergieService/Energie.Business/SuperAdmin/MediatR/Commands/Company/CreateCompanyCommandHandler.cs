using Energie.Domain.IRepository;
using Energie.Model;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Energie.Business.SuperAdmin.MediatR.Commands.Company
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, ApiResponse>
    {
        private readonly ICompanyRepository<Domain.Domain.Company> _companyRepository;
        private readonly IStringLocalizer<Resources.Resources> _localizer;
        public CreateCompanyCommandHandler(ICompanyRepository<Domain.Domain.Company> companyRepository,
            IStringLocalizer<Resources.Resources> localizer
            )
        {
            _companyRepository = companyRepository;
            _localizer = localizer;
        }

        public async Task<ApiResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (await _companyRepository.GetCompanyByNameAsync(request.CompanyName) is Domain.Domain.Company existingCompany)
            {
                return ApiResponse.ResponseMessages(0, false, _localizer["Bedrijf bestaat al"].Value);
            }

            var newCompany = Domain.Domain.Company.Create(request.CompanyName);
            await _companyRepository.CreateCompanyAsync(newCompany);
            var successMessage = _localizer["Bedrijf hiermee"].Value;

            return ApiResponse.ResponseMessages(newCompany.Id, true, successMessage);
        }
    }
}

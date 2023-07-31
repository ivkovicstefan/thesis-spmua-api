using SPMUA.Model.DTOs.Admin;
using SPMUA.Model.Exceptions;
using SPMUA.Repository.Contracts;
using SPMUA.Service.Contracts;
using SPMUA.Service.Validators;
using SPMUA.Service.Validators.Admin;
using SPMUA.Utility.Security;

namespace SPMUA.Service.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthenticationService _authenticationService;

        public AdminService(IAdminRepository adminRepository,
                            IAuthenticationService authenticationService)
        {
            _adminRepository = adminRepository;
            _authenticationService = authenticationService;
        }

        public async Task<int> RegisterAsync(AdminRegisterDTO adminRegisterDTO)
        {
            int result = 0;

            RequestValidator<AdminRegisterDTOValidator, AdminRegisterDTO> validator = new();

            validator.Validate(adminRegisterDTO);

            if (await _adminRepository.IsAdminEmailAvailableAsync(adminRegisterDTO.AdminEmail))
            {
                adminRegisterDTO.PasswordHash = HashService.HashPassword(adminRegisterDTO.Password);

                result = await _adminRepository.CreateAdminAsync(adminRegisterDTO);
            }
            else
            {
                throw new RequestValidationException("There is already a registered user with this email address.", null);
            }

            return result;
        }

        public async Task<bool> IsAdminEmailAvailableAsync(string adminEmail)
        {
            return await _adminRepository.IsAdminEmailAvailableAsync(adminEmail);
        }

        public async Task<string> LoginAsync(AdminLoginDTO adminLoginDTO)
        {
            string result;

            int adminId = await _adminRepository.AuthenticateAdminAsync(adminLoginDTO);

            result = _authenticationService.GenerateToken(adminId);

            return result;
        }

        public async Task<AdminDTO> GetAdminByIdAsync(int adminId)
        {
            return await _adminRepository.GetAdminByIdAsync(adminId);
        }
    }
}

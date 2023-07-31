using SPMUA.Model.DTOs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Repository.Contracts
{
    public interface IAdminRepository
    {
        Task<int> CreateAdminAsync(AdminRegisterDTO adminRegisterDTO);

        Task<bool> IsAdminEmailAvailableAsync(string adminEmail);

        Task<int> AuthenticateAdminAsync(AdminLoginDTO adminLoginDTO);

        Task<AdminDTO> GetAdminByIdAsync(int adminId);
    }
}

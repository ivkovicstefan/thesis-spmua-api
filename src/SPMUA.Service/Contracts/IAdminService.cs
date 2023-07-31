using SPMUA.Model.DTOs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Service.Contracts
{
    public interface IAdminService
    {
        Task<int> RegisterAsync(AdminRegisterDTO adminRegisterDTO);

        Task<bool> IsAdminEmailAvailableAsync(string adminEmail);

        Task<string> LoginAsync(AdminLoginDTO adminLoginDTO);

        Task<AdminDTO> GetAdminByIdAsync(int adminId);
    }
}

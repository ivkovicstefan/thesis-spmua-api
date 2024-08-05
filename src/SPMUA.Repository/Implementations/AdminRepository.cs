using Microsoft.EntityFrameworkCore;
using SPMUA.Model.DTOs.Admin;
using SPMUA.Model.Exceptions;
using SPMUA.Repository.Models;
using SPMUA.Repository.Contracts;
using SPMUA.Repository.Data;
using SPMUA.Utility.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Repository.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private readonly SpmuaDbContext _spmuaDbContext;

        public AdminRepository(SpmuaDbContext spmuaDbContext) {
            _spmuaDbContext = spmuaDbContext;
        }

        public async Task<int> CreateAdminAsync(AdminRegisterDTO adminRegisterDTO)
        {
            int result = 0;

            try
            {
                Admin newAdmin = new()
                {
                    AdminFirstName = adminRegisterDTO.AdminFirstName,
                    AdminLastName = adminRegisterDTO.AdminLastName,
                    AdminEmail = adminRegisterDTO.AdminEmail,
                    PasswordHash = adminRegisterDTO.PasswordHash,
                };

                _spmuaDbContext.Add(newAdmin);
                await _spmuaDbContext.SaveChangesAsync();

                result = newAdmin.AdminId;
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<bool> IsAdminEmailAvailableAsync(string adminEmail)
        {
            bool result = false;

			try
			{
                Admin? admin = await _spmuaDbContext.Admins.Where(a => a.AdminEmail== adminEmail)
                                                           .FirstOrDefaultAsync();

                result = admin is null;
			}
			catch
			{
				throw;
			}

            return result;
        }

        public async Task<int> AuthenticateAdminAsync(AdminLoginDTO adminLoginDTO)
        {
            int result = 0;

            try
            {
                Admin? admin = await _spmuaDbContext.Admins.Where(a => a.AdminEmail == adminLoginDTO.Email)
                                                           .FirstOrDefaultAsync();
                
                if (admin is null || !HashService.VerifyPassword(adminLoginDTO.Password, admin.PasswordHash))
                {
                    throw new InvalidCredentialsException();
                }
                else {
                    result = admin.AdminId;
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<AdminDTO> GetAdminByIdAsync(int adminId)
        {
            AdminDTO? result = null;

            try
            {
                result = await _spmuaDbContext.Admins.Where(a => a.AdminId == adminId)
                                                     .Select(a => new AdminDTO()
                                                     {
                                                         AdminId = a.AdminId,
                                                         AdminFirstName = a.AdminFirstName,
                                                         AdminLastName = a.AdminLastName,
                                                         AdminEmail = a.AdminEmail
                                                     })
                                                     .FirstOrDefaultAsync(); 
                
                if (result is null)
                {
                    throw new EntityNotFoundException(adminId);
                }
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPMUA.Model.DTOs.ServiceType;
using SPMUA.Service.Contracts;

namespace SPMUA.API.Controllers
{
    [Route("api/service-type")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypeController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        [HttpGet("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Ping()
        {
            return new OkObjectResult("pong");
        }

        [HttpGet("service-types")]
        [ProducesResponseType(typeof(List<ServiceTypeDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllServiceTypesAsync()
        {
            return new OkObjectResult(await _serviceTypeService.GetAllServiceTypesAsync());
        }

        [HttpGet("service-types/{serviceTypeId}")]
        [ProducesResponseType(typeof(ServiceTypeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetServiceTypeByIdAsync(int serviceTypeId)
        {
            return new OkObjectResult(await _serviceTypeService.GetServiceTypeByIdAsync(serviceTypeId));
        }

        [HttpPost("service-type")]
        [ProducesResponseType(typeof(ServiceTypeDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateServiceTypeAsync(ServiceTypeDTO serviceTypeDTO)
        {
            int result = await _serviceTypeService.CreateServiceTypeAsync(serviceTypeDTO);

            return new CreatedAtActionResult(nameof(GetServiceTypeByIdAsync), 
                                             nameof(ServiceTypeController).Replace("Controller", ""), 
                                             new { serviceTypeId = result },
                                             null);
        }

        [HttpPut("service-type")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateServiceTypeAsync(ServiceTypeDTO serviceTypeDTO)
        {
            await _serviceTypeService.UpdateServiceTypeAsync(serviceTypeDTO);

            return new NoContentResult();
        }

        [HttpDelete("service-type/{serviceTypeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteServiceTypeAsync(int serviceTypeId)
        {
            await _serviceTypeService.DeleteServiceTypeAsync(serviceTypeId);

            return new NoContentResult();
        }
    }
}

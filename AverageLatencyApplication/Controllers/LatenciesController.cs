using AverageLatencyApplication.Interfaces;
using AverageLatencyApplication.Models.Dtos;
using AverageLatencyApplication.Models.Responses;
using AverageLatencyApplication.Validators;
using Microsoft.AspNetCore.Mvc;

namespace AverageLatencyApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LatenciesController : ControllerBase
    {
        private readonly ILogger<LatenciesController> _logger;
        private readonly ILatenciesService _latenciesService;

        public LatenciesController(ILatenciesService latenciesService, ILogger<LatenciesController> logger)
        {
            _latenciesService = latenciesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<LatenciesResponse>> GetAsync(string startDate, string endDate)
        {
            var validator = new DateValidator();

            var validationResult = validator.Validate(new DateDto(startDate, endDate));

            if(!validationResult.IsValid)
            {
                return StatusCode(400, validationResult.Errors.Select(x => x.ErrorMessage));
            }

            try
            {
                var result = await _latenciesService.GetAverageLatenciesForPeriod(DateTime.Parse(startDate), DateTime.Parse(endDate));
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}

using System.Collections.Generic;
using NCARB.EesaService.Entities;
using Microsoft.AspNetCore.Mvc;
using NCARB.EesaService.Infrastructure;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

namespace NCARB.EesaService.Controllers
{
    [Route("/api/[controller]")]
    public class ApplicantController : Controller
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly ILogger<ApplicantController> _logger; 

        public ApplicantController(IApplicantRepository applicantRepository, ILogger<ApplicantController> logger)
        {
            _applicantRepository = applicantRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Applicant>> Get() => await _applicantRepository.GetAllAsync();

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var Applicant = await _applicantRepository.GetSingleAsync(id);
            if (Applicant == null)
            {
                return NotFound(); // This makes it return 404; otherwise it will return a 204 (no content) 
            }

            return new ObjectResult(Applicant);
        }

        /// Test: 
        ///   curl -H "Content-Type: application/json" -X POST -d '{"lastName":"IWasPosted"}' http://localhost:5000/api/applicant 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Applicant Applicant)
        {
            _logger.LogDebug("Starting save");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var deficiencies = new List<Deficiency>();
            // deficiencies.Add(new Deficiency {
            //     CategoryDeficiency = new CategoryDeficiency { Category = new Category { Name = "Test Category 1", Area = new Area { Name = "Test Area 1" } } }
            // });

            _applicantRepository.Add(
                new Applicant { 
                        Id = new Random().Next(),
                        LastName = Applicant.LastName,
                        //Deficiencies = deficiencies
                    });

            await _applicantRepository.SaveChangesAsync();

            _logger.LogDebug("Finished save");

            return CreatedAtAction(nameof(Get), new { id = Applicant.LastName }, Applicant);
        }
    }
}

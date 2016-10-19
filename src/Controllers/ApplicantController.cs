using System.Collections.Generic;
using NCARB.EesaService.Entities;
using Microsoft.AspNetCore.Mvc;
using NCARB.EesaService.Infrastructure;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NCARB.EesaService.Controllers
{
    [Route("/api/[controller]")]
    public class ApplicantController : Controller
    {
        private readonly ApplicantContext _context;
        private readonly ILogger<ApplicantController> _logger; 

        public ApplicantController(ApplicantContext context, ILogger<ApplicantController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Applicant>> Get() => await _context.Set<Applicant>().ToListAsync();

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var Applicant = await _context.Applicants.SingleOrDefaultAsync(m => m.Id == id);
            if (Applicant == null)
            {
                return NotFound(); // This makes it return 404; otherwise it will return a 204 (no content) 
            }

            return new ObjectResult(Applicant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Applicant Applicant)
        {
            _logger.LogDebug("Starting save");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Applicants.Add(new Applicant { LastName = Applicant.LastName });
            await _context.SaveChangesAsync();

            _logger.LogDebug("Finished save");

            return CreatedAtAction(nameof(Get), new { id = Applicant.LastName }, Applicant);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using VetClinic.Consultation.Api.Application.Commands;
using VetClinic.Consultation.Api.Application.Services;

namespace VetClinic.Consultation.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultationController(ConsultationService applicationService,
                              ILogger<ConsultationController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Post(StartConsultationCommand command)
        {
            try
            {
                var id = await applicationService.Handle(command);
                return Ok(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("end")]
        public async Task<ActionResult> Post(EndConsultationCommand command)
        {
            try
            {
                await applicationService.Handle(command);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut("diagnosis")]
        public async Task<ActionResult> Put(SetDiagnosisCommand command)
        {
            try
            {
                await applicationService.Handle(command);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpPut("treatment")]
        public async Task<ActionResult> Put(SetTreatmentCommand command)
        {
            try
            {
                await applicationService.Handle(command);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut("weight")]
        public async Task<ActionResult> Put(SetWeightCommand command)
        {
            try
            {
                await applicationService.Handle(command);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut("administerDrug")]
        public async Task<ActionResult> Put(AdministerDrugCommand command)
        {
            try
            {
                await applicationService.Handle(command);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut("registerVitalSigns")]
        public async Task<ActionResult> Put(RegisterVitalSignsCommand command)
        {
            try
            {
                await applicationService.Handle(command);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}

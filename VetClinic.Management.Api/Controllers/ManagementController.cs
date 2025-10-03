using Microsoft.AspNetCore.Mvc;
using VetClinic.Management.Api.Application;

namespace VetClinic.Management.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagementController(ManagementService managementService,
        ICommandHandler<SetWeightCommand> setWeightCommandHandler) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Post(CreatePetCommand command)
        {
            await managementService.Handle(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(SetWeightCommand command)
        {
            await setWeightCommandHandler.Handle(command);
            return Ok();
        }
    }
}

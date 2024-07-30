using Microsoft.AspNetCore.Mvc;

namespace AmpSampleService.APIs;

[ApiController()]
public class AisController : AisControllerBase
{
    public AisController(IAisService service)
        : base(service) { }
}

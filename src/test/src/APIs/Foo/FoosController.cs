using Microsoft.AspNetCore.Mvc;

namespace Test.APIs;

[ApiController()]
public class FoosController : FoosControllerBase
{
    public FoosController(IFoosService service)
        : base(service) { }
}

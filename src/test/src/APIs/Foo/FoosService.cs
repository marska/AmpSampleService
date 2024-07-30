using Test.Infrastructure;

namespace Test.APIs;

public class FoosService : FoosServiceBase
{
    public FoosService(TestDbContext context)
        : base(context) { }
}

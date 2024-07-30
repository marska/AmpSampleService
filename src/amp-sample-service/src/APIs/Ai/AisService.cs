using AmpSampleService.Infrastructure;

namespace AmpSampleService.APIs;

public class AisService : AisServiceBase
{
    public AisService(AmpSampleServiceDbContext context)
        : base(context) { }
}

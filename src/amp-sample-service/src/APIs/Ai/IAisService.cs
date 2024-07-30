using AmpSampleService.APIs.Common;
using AmpSampleService.APIs.Dtos;

namespace AmpSampleService.APIs;

public interface IAisService
{
    /// <summary>
    /// Create one Ai
    /// </summary>
    public Task<Ai> CreateAi(AiCreateInput ai);

    /// <summary>
    /// Delete one Ai
    /// </summary>
    public Task DeleteAi(AiWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Ais
    /// </summary>
    public Task<List<Ai>> Ais(AiFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Ai records
    /// </summary>
    public Task<MetadataDto> AisMeta(AiFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Ai
    /// </summary>
    public Task<Ai> Ai(AiWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Ai
    /// </summary>
    public Task UpdateAi(AiWhereUniqueInput uniqueId, AiUpdateInput updateDto);
}

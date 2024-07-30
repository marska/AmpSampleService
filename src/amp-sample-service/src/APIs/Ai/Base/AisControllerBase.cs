using AmpSampleService.APIs;
using AmpSampleService.APIs.Common;
using AmpSampleService.APIs.Dtos;
using AmpSampleService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmpSampleService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AisControllerBase : ControllerBase
{
    protected readonly IAisService _service;

    public AisControllerBase(IAisService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Ai
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Ai>> CreateAi(AiCreateInput input)
    {
        var ai = await _service.CreateAi(input);

        return CreatedAtAction(nameof(Ai), new { id = ai.Id }, ai);
    }

    /// <summary>
    /// Delete one Ai
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteAi([FromRoute()] AiWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteAi(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Ais
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Ai>>> Ais([FromQuery()] AiFindManyArgs filter)
    {
        return Ok(await _service.Ais(filter));
    }

    /// <summary>
    /// Meta data about Ai records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AisMeta([FromQuery()] AiFindManyArgs filter)
    {
        return Ok(await _service.AisMeta(filter));
    }

    /// <summary>
    /// Get one Ai
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Ai>> Ai([FromRoute()] AiWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Ai(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Ai
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateAi(
        [FromRoute()] AiWhereUniqueInput uniqueId,
        [FromQuery()] AiUpdateInput aiUpdateDto
    )
    {
        try
        {
            await _service.UpdateAi(uniqueId, aiUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}

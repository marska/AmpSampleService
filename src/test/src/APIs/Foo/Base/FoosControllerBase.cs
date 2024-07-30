using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;

namespace Test.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class FoosControllerBase : ControllerBase
{
    protected readonly IFoosService _service;

    public FoosControllerBase(IFoosService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Foo
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Foo>> CreateFoo(FooCreateInput input)
    {
        var foo = await _service.CreateFoo(input);

        return CreatedAtAction(nameof(Foo), new { id = foo.Id }, foo);
    }

    /// <summary>
    /// Delete one Foo
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteFoo([FromRoute()] FooWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteFoo(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Foos
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Foo>>> Foos([FromQuery()] FooFindManyArgs filter)
    {
        return Ok(await _service.Foos(filter));
    }

    /// <summary>
    /// Meta data about Foo records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> FoosMeta([FromQuery()] FooFindManyArgs filter)
    {
        return Ok(await _service.FoosMeta(filter));
    }

    /// <summary>
    /// Get one Foo
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Foo>> Foo([FromRoute()] FooWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Foo(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Foo
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateFoo(
        [FromRoute()] FooWhereUniqueInput uniqueId,
        [FromQuery()] FooUpdateInput fooUpdateDto
    )
    {
        try
        {
            await _service.UpdateFoo(uniqueId, fooUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}

using AmpSampleService.APIs;
using AmpSampleService.APIs.Common;
using AmpSampleService.APIs.Dtos;
using AmpSampleService.APIs.Errors;
using AmpSampleService.APIs.Extensions;
using AmpSampleService.Infrastructure;
using AmpSampleService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AmpSampleService.APIs;

public abstract class AisServiceBase : IAisService
{
    protected readonly AmpSampleServiceDbContext _context;

    public AisServiceBase(AmpSampleServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Ai
    /// </summary>
    public async Task<Ai> CreateAi(AiCreateInput createDto)
    {
        var ai = new AiDbModel { CreatedAt = createDto.CreatedAt, UpdatedAt = createDto.UpdatedAt };

        if (createDto.Id != null)
        {
            ai.Id = createDto.Id;
        }

        _context.Ais.Add(ai);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AiDbModel>(ai.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Ai
    /// </summary>
    public async Task DeleteAi(AiWhereUniqueInput uniqueId)
    {
        var ai = await _context.Ais.FindAsync(uniqueId.Id);
        if (ai == null)
        {
            throw new NotFoundException();
        }

        _context.Ais.Remove(ai);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Ais
    /// </summary>
    public async Task<List<Ai>> Ais(AiFindManyArgs findManyArgs)
    {
        var ais = await _context
            .Ais.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return ais.ConvertAll(ai => ai.ToDto());
    }

    /// <summary>
    /// Meta data about Ai records
    /// </summary>
    public async Task<MetadataDto> AisMeta(AiFindManyArgs findManyArgs)
    {
        var count = await _context.Ais.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Ai
    /// </summary>
    public async Task<Ai> Ai(AiWhereUniqueInput uniqueId)
    {
        var ais = await this.Ais(
            new AiFindManyArgs { Where = new AiWhereInput { Id = uniqueId.Id } }
        );
        var ai = ais.FirstOrDefault();
        if (ai == null)
        {
            throw new NotFoundException();
        }

        return ai;
    }

    /// <summary>
    /// Update one Ai
    /// </summary>
    public async Task UpdateAi(AiWhereUniqueInput uniqueId, AiUpdateInput updateDto)
    {
        var ai = updateDto.ToModel(uniqueId);

        _context.Entry(ai).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Ais.Any(e => e.Id == ai.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}

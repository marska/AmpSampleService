using Microsoft.EntityFrameworkCore;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;
using Test.APIs.Extensions;
using Test.Infrastructure;
using Test.Infrastructure.Models;

namespace Test.APIs;

public abstract class FoosServiceBase : IFoosService
{
    protected readonly TestDbContext _context;

    public FoosServiceBase(TestDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Foo
    /// </summary>
    public async Task<Foo> CreateFoo(FooCreateInput createDto)
    {
        var foo = new FooDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            foo.Id = createDto.Id;
        }

        _context.Foos.Add(foo);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<FooDbModel>(foo.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Foo
    /// </summary>
    public async Task DeleteFoo(FooWhereUniqueInput uniqueId)
    {
        var foo = await _context.Foos.FindAsync(uniqueId.Id);
        if (foo == null)
        {
            throw new NotFoundException();
        }

        _context.Foos.Remove(foo);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Foos
    /// </summary>
    public async Task<List<Foo>> Foos(FooFindManyArgs findManyArgs)
    {
        var foos = await _context
            .Foos.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return foos.ConvertAll(foo => foo.ToDto());
    }

    /// <summary>
    /// Meta data about Foo records
    /// </summary>
    public async Task<MetadataDto> FoosMeta(FooFindManyArgs findManyArgs)
    {
        var count = await _context.Foos.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Foo
    /// </summary>
    public async Task<Foo> Foo(FooWhereUniqueInput uniqueId)
    {
        var foos = await this.Foos(
            new FooFindManyArgs { Where = new FooWhereInput { Id = uniqueId.Id } }
        );
        var foo = foos.FirstOrDefault();
        if (foo == null)
        {
            throw new NotFoundException();
        }

        return foo;
    }

    /// <summary>
    /// Update one Foo
    /// </summary>
    public async Task UpdateFoo(FooWhereUniqueInput uniqueId, FooUpdateInput updateDto)
    {
        var foo = updateDto.ToModel(uniqueId);

        _context.Entry(foo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Foos.Any(e => e.Id == foo.Id))
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

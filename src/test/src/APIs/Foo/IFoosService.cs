using Test.APIs.Common;
using Test.APIs.Dtos;

namespace Test.APIs;

public interface IFoosService
{
    /// <summary>
    /// Create one Foo
    /// </summary>
    public Task<Foo> CreateFoo(FooCreateInput foo);

    /// <summary>
    /// Delete one Foo
    /// </summary>
    public Task DeleteFoo(FooWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Foos
    /// </summary>
    public Task<List<Foo>> Foos(FooFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Foo records
    /// </summary>
    public Task<MetadataDto> FoosMeta(FooFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Foo
    /// </summary>
    public Task<Foo> Foo(FooWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Foo
    /// </summary>
    public Task UpdateFoo(FooWhereUniqueInput uniqueId, FooUpdateInput updateDto);
}

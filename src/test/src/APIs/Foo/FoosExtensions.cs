using Test.APIs.Dtos;
using Test.Infrastructure.Models;

namespace Test.APIs.Extensions;

public static class FoosExtensions
{
    public static Foo ToDto(this FooDbModel model)
    {
        return new Foo
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static FooDbModel ToModel(this FooUpdateInput updateDto, FooWhereUniqueInput uniqueId)
    {
        var foo = new FooDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            foo.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            foo.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return foo;
    }
}

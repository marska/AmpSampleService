using AmpSampleService.APIs.Dtos;
using AmpSampleService.Infrastructure.Models;

namespace AmpSampleService.APIs.Extensions;

public static class AisExtensions
{
    public static Ai ToDto(this AiDbModel model)
    {
        return new Ai
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AiDbModel ToModel(this AiUpdateInput updateDto, AiWhereUniqueInput uniqueId)
    {
        var ai = new AiDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            ai.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            ai.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return ai;
    }
}

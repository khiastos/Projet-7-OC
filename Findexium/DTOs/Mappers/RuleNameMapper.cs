using Findexium.Domain;
using Findexium.DTOs;

namespace Findexium.Mappers
{
    public static class RuleNameMapper
    {
        public static RuleNameDTO ToDto(this RuleName entity)
        {
            return new RuleNameDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Json = entity.Json,
                Template = entity.Template,
                SqlStr = entity.SqlStr,
                SqlPart = entity.SqlPart
            };
        }

        public static RuleName ToEntity(this RuleNameDTO dto)
        {
            return new RuleName
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Json = dto.Json,
                Template = dto.Template,
                SqlStr = dto.SqlStr,
                SqlPart = dto.SqlPart
            };
        }

        public static void ApplyTo(this RuleNameDTO dto, RuleName poco)
        {
            poco.Name = dto.Name;
            poco.Description = dto.Description;
            poco.Json = dto.Json;
            poco.Template = dto.Template;
            poco.SqlStr = dto.SqlStr;
            poco.SqlPart = dto.SqlPart;
        }
    }
}

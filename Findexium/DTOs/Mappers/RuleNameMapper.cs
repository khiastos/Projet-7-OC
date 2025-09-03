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
                RuleNameId = entity.RuleNameId,
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
                RuleNameId = dto.RuleNameId,
                Name = dto.Name,
                Description = dto.Description,
                Json = dto.Json,
                Template = dto.Template,
                SqlStr = dto.SqlStr,
                SqlPart = dto.SqlPart
            };
        }
    }
}

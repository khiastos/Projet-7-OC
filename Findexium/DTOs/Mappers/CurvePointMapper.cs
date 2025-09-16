using Findexium.Domain;
using Findexium.DTOs;

namespace Findexium.Mappers
{
    public static class CurvePointMapper
    {
        public static CurvePointDTO ToDto(this CurvePoint entity)
        {
            return new CurvePointDTO
            {
                Id = entity.Id,
                CurveId = entity.CurveId,
                AsOfDate = entity.AsOfDate,
                Term = entity.Term,
                CurvePointValue = entity.CurvePointValue,
                CreationDate = entity.CreationDate
            };
        }

        public static CurvePoint ToEntity(this CurvePointDTO dto)
        {
            return new CurvePoint
            {
                Id = dto.Id,
                CurveId = dto.CurveId,
                AsOfDate = dto.AsOfDate,
                Term = dto.Term,
                CurvePointValue = dto.CurvePointValue,
                CreationDate = dto.CreationDate
            };
        }

        public static void ApplyTo(this CurvePointDTO dto, CurvePoint poco)
        {
            poco.AsOfDate = dto.AsOfDate;
            poco.Term = dto.Term;
            poco.CurvePointValue = dto.CurvePointValue;
        }
    }
}

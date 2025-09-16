using Findexium.Domain;
using Findexium.DTOs;

namespace Findexium.Mappers
{
    public static class RatingMapper
    {
        public static RatingDTO ToDto(this Rating entity)
        {
            return new RatingDTO
            {
                Id = entity.Id,
                MoodysRating = entity.MoodysRating,
                SandPRating = entity.SandPRating,
                FitchRating = entity.FitchRating,
                OrderNumber = entity.OrderNumber
            };
        }

        public static Rating ToEntity(this RatingDTO dto)
        {
            return new Rating
            {
                Id = dto.Id,
                MoodysRating = dto.MoodysRating,
                SandPRating = dto.SandPRating,
                FitchRating = dto.FitchRating,
                OrderNumber = dto.OrderNumber
            };
        }

        public static void ApplyTo(this RatingDTO dto, Rating poco)
        {
            poco.MoodysRating = dto.MoodysRating;
            poco.SandPRating = dto.SandPRating;
            poco.FitchRating = dto.FitchRating;
            poco.OrderNumber = dto.OrderNumber;
        }
    }
}

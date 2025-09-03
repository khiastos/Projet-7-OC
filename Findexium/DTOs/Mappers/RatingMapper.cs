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
                RatingId = entity.RatingId,
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
                RatingId = dto.RatingId,
                MoodysRating = dto.MoodysRating,
                SandPRating = dto.SandPRating,
                FitchRating = dto.FitchRating,
                OrderNumber = dto.OrderNumber
            };
        }
    }
}

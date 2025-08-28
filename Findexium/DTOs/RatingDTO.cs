﻿using System.ComponentModel.DataAnnotations;

namespace Findexium.DTOs
{
    public class RatingDTO
    {
        [Key]
        public int RatingId { get; set; }
        public string? MoodysRating { get; set; }
        public string? SandPRating { get; set; }
        public string? FitchRating { get; set; }
        public byte? OrderNumber { get; set; }
    }
}

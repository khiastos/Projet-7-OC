using System.ComponentModel.DataAnnotations;

namespace Findexium.DTOs
{
    public class RatingDTO
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string MoodysRating { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string SandPRating { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string FitchRating { get; set; } = default!;

        [Range(0, 100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public byte OrderNumber { get; set; } = default!;
    }
}

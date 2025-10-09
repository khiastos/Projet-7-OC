using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Findexium.DTOs
{
    public class RuleNameDTO
    {
        [JsonIgnore]
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Name { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Description { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Json { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Template { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string SqlStr { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string SqlPart { get; set; } = default!;
    }
}

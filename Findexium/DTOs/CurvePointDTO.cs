using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Findexium.DTOs
{
    public class CurvePointDTO
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        [Range(0, 1_000_000)]
        public byte CurveId { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public DateTime AsOfDate { get; set; }

        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        [Range(0, 1_000_000)]
        public double Term { get; set; }

        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        [Range(0, 1_000_000)]
        public double CurvePointValue { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public DateTime CreationDate { get; set; }
    }
}

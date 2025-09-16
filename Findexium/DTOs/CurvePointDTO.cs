using System.ComponentModel.DataAnnotations;

namespace Findexium.DTOs
{
    public class CurvePointDTO
    {
        [Key]
        public int Id { get; set; }
        public byte? CurveId { get; set; }
        public DateTime? AsOfDate { get; set; }
        public double? Term { get; set; }
        public double? CurvePointValue { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}

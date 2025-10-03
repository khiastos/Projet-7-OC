using System.ComponentModel.DataAnnotations;

namespace Findexium.DTOs
{
    public class BidListDTO
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Account { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string BidType { get; set; } = default!;

        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        [Range(0, 1_000_000)]
        public decimal BidQuantity { get; set; }

        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        [Range(0, 1_000_000)]
        public decimal AskQuantity { get; set; }

        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        [Range(0, 1_000_000)]
        public decimal Bid { get; set; }

        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        [Range(0, 1_000_000)]
        public decimal Ask { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Benchmark { get; set; } = default!;

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public DateTime BidListDate { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Commentary { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string BidSecurity { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string BidStatus { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Trader { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Book { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string CreationName { get; set; } = default!;

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public DateTime CreationDate { get; set; }
        public string RevisionName { get; set; } = default!;

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public DateTime RevisionDate { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string DealName { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string DealType { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string SourceListId { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Side { get; set; } = default!;
    }
}

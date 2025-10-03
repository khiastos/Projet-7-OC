using System.ComponentModel.DataAnnotations;

namespace Findexium.DTOs
{
    public class TradeDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Account { get; set; } = default!;
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string AccountType { get; set; } = default!;

        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        [Range(0, 1_000_000)]
        public decimal BuyQuantity { get; set; }
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        [Range(0, 1_000_000)]
        public decimal SellQuantity { get; set; }
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        [Range(0, 1_000_000)]
        public decimal BuyPrice { get; set; }
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        [Range(0, 1_000_000)]
        public decimal SellPrice { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public DateTime TradeDate { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string TradeSecurity { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string TradeStatus { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Trader { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Benchmark { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string Book { get; set; } = default!;

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public string CreationName { get; set; } = default!;

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
        public DateTime CreationDate { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Ce champs ne doit pas être vide")]
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

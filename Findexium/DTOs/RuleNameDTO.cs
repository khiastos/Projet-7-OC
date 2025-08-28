﻿using System.ComponentModel.DataAnnotations;

namespace Findexium.DTOs
{
    public class RuleNameDTO
    {
        [Key]
        public int RuleNameId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Json { get; set; }
        public string? Template { get; set; }
        public string? SqlStr { get; set; }
        public string? SqlPart { get; set; }
    }
}

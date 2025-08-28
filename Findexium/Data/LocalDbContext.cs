using Findexium.Domain;
using Microsoft.EntityFrameworkCore;

namespace Findexium.Data
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<BidList> BidLists { get; set; }
        public DbSet<CurvePoint> CurvePoints { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RuleName> RuleNames { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Findexium.DTOs.BidListDTO> BidListDTO { get; set; } = default!;
        public DbSet<Findexium.DTOs.CurvePointDTO> CurvePointDTO { get; set; } = default!;
        public DbSet<Findexium.DTOs.RatingDTO> RatingDTO { get; set; } = default!;
        public DbSet<Findexium.DTOs.RuleNameDTO> RuleNameDTO { get; set; } = default!;
        public DbSet<Findexium.DTOs.TradeDTO> TradeDTO { get; set; } = default!;
        public DbSet<Findexium.DTOs.UserDTO> UserDTO { get; set; } = default!;
    }
}
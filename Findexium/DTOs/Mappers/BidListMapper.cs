using Findexium.Domain;
using Findexium.DTOs;

namespace Findexium.Mappers
{
    public static class BidListMapper
    {
        public static BidListDTO ToDto(this BidList entity)
        {
            return new BidListDTO
            {
                Id = entity.Id,
                Account = entity.Account,
                BidType = entity.BidType,
                BidQuantity = entity.BidQuantity,
                AskQuantity = entity.AskQuantity,
                Bid = entity.Bid,
                Ask = entity.Ask,
                Benchmark = entity.Benchmark,
                BidListDate = entity.BidListDate,
                Commentary = entity.Commentary,
                BidSecurity = entity.BidSecurity,
                BidStatus = entity.BidStatus,
                Trader = entity.Trader,
                Book = entity.Book,
                CreationName = entity.CreationName,
                CreationDate = entity.CreationDate,
                RevisionName = entity.RevisionName,
                RevisionDate = entity.RevisionDate,
                DealName = entity.DealName,
                DealType = entity.DealType,
                SourceListId = entity.SourceListId,
                Side = entity.Side
            };
        }

        public static BidList ToEntity(this BidListDTO dto)
        {
            return new BidList
            {
                Id = dto.Id,
                Account = dto.Account,
                BidType = dto.BidType,
                BidQuantity = dto.BidQuantity,
                AskQuantity = dto.AskQuantity,
                Bid = dto.Bid,
                Ask = dto.Ask,
                Benchmark = dto.Benchmark,
                BidListDate = dto.BidListDate,
                Commentary = dto.Commentary,
                BidSecurity = dto.BidSecurity,
                BidStatus = dto.BidStatus,
                Trader = dto.Trader,
                Book = dto.Book,
                CreationName = dto.CreationName,
                CreationDate = dto.CreationDate,
                RevisionName = dto.RevisionName,
                RevisionDate = dto.RevisionDate,
                DealName = dto.DealName,
                DealType = dto.DealType,
                SourceListId = dto.SourceListId,
                Side = dto.Side
            };
        }

        public static void ApplyTo(this BidListDTO dto, BidList poco)
        {
            poco.Account = dto.Account;
            poco.BidType = dto.BidType;
            poco.BidQuantity = dto.BidQuantity;
            poco.AskQuantity = dto.AskQuantity;
            poco.Bid = dto.Bid;
            poco.Ask = dto.Ask;
            poco.Benchmark = dto.Benchmark;
            poco.BidListDate = dto.BidListDate;
            poco.Commentary = dto.Commentary;
            poco.BidSecurity = dto.BidSecurity;
            poco.BidStatus = dto.BidStatus;
            poco.Trader = dto.Trader;
            poco.Book = dto.Book;
            poco.CreationName = dto.CreationName;
            poco.RevisionName = dto.RevisionName;
            poco.DealName = dto.DealName;
            poco.DealType = dto.DealType;
            poco.SourceListId = dto.SourceListId;
            poco.Side = dto.Side;
        }
    }
}

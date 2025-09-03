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
                BidListId = entity.BidListId,
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
                BidListId = dto.BidListId,
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
    }
}

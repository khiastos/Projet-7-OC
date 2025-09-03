using Findexium.Domain;
using Findexium.DTOs;

namespace Findexium.Mappers
{
    public static class TradeMapper
    {
        public static TradeDTO ToDto(this Trade entity)
        {
            return new TradeDTO
            {
                TradeId = entity.TradeId,
                Account = entity.Account,
                AccountType = entity.AccountType,
                BuyQuantity = entity.BuyQuantity,
                SellQuantity = entity.SellQuantity,
                BuyPrice = entity.BuyPrice,
                SellPrice = entity.SellPrice,
                TradeDate = entity.TradeDate,
                TradeSecurity = entity.TradeSecurity,
                TradeStatus = entity.TradeStatus,
                Trader = entity.Trader,
                Benchmark = entity.Benchmark,
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

        public static Trade ToEntity(this TradeDTO dto)
        {
            return new Trade
            {
                TradeId = dto.TradeId,
                Account = dto.Account,
                AccountType = dto.AccountType,
                BuyQuantity = dto.BuyQuantity,
                SellQuantity = dto.SellQuantity,
                BuyPrice = dto.BuyPrice,
                SellPrice = dto.SellPrice,
                TradeDate = dto.TradeDate,
                TradeSecurity = dto.TradeSecurity,
                TradeStatus = dto.TradeStatus,
                Trader = dto.Trader,
                Benchmark = dto.Benchmark,
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

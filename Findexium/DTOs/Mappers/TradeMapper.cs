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
                Id = entity.Id,
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
                Id = dto.Id,
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

        public static void ApplyTo(this TradeDTO dto, Trade poco)
        {
            poco.Account = dto.Account;
            poco.AccountType = dto.AccountType;
            poco.BuyQuantity = dto.BuyQuantity;
            poco.SellQuantity = dto.SellQuantity;
            poco.BuyPrice = dto.BuyPrice;
            poco.SellPrice = dto.SellPrice;
            poco.TradeSecurity = dto.TradeSecurity;
            poco.TradeStatus = dto.TradeStatus;
            poco.Trader = dto.Trader;
            poco.Benchmark = dto.Benchmark;
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

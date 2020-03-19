using System;
using System.Collections.Generic;

namespace Kehyeedra3.Services.Models
{
    public class StoreFront
    {
        public ulong Id { get; set; } = 0;
        public ulong UserId { get; set; } = 0;
        public StoreItemType StoreItemType { get; set; } = 0;

        public ICollection<StoreInventory> Items { get; set; }
    }

    public enum StoreItemType
    {
        Fish = 0,
        Reminders = 1
    }

    public class StoreInventory
    {
        public ulong ItemId { get; set; } = 0;
        public ulong UserId { get; set; } = 0;
        public StoreItemType StoreItemType { get; set; } = 0;
        public string Item { get; set; } = "";
        public int Amount { get; set; } = 0;
        public int Price { get; set; } = 0;
    }

    public class ItemOffer
    {
        public ulong OfferId { get; set; }
        public ulong BuyerId { get; set; }
        public ulong StoreId { get; set; }
        public StoreItemType StoreType { get; set; }
        public ulong ItemId { get; set; }
        public int Amount { get; set; }
        public int OfferAmount { get; set; }
        public bool IsPurchaseFromStore { get; set; }
    }

    public static class StoreUtilities
    {
        public static readonly Dictionary<StoreItemType, Type> StoreItemTypeToObject = new Dictionary<StoreItemType, Type>
        {
            { StoreItemType.Fish, typeof(FishSpecies) },
            { StoreItemType.Reminders, typeof(Reminder) }
        };

        public static dynamic GetObjectDataFromJson(string json, StoreItemType store)
        {
            var type = StoreItemTypeToObject.GetValueOrDefault(store);

            return Newtonsoft.Json.JsonConvert.DeserializeObject(json, type);
        }
    }
}

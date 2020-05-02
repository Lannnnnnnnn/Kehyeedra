using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kehyeedra3.Services.Models
{
    public class StoreFront
    {
        [Key]
        public ulong Id { get; set; }
        public ulong UserId { get; set; } = 0;
        public StoreItemType StoreItemType { get; set; } = 0;

        public ICollection<StoreInventory> Items { get; set; }
        public ICollection<ItemOffer> Offers { get; set; }
    }

    public enum StoreItemType
    {
        Fish = 0,
        Items = 1,
        Reminders = 2
    }

    public class StoreInventory
    {
        [Key]
        public ulong InvId { get; set; }
        public string Item { get; set; } = "";
        public int Amount { get; set; } = 0;
        public int Price { get; set; } = 0;
    }

    public class ItemOffer
    {
        [Key]
        public ulong OfferId { get; set; }
        public ulong BuyerId { get; set; }
        public ulong StoreId { get; set; }
        public ulong ItemId { get; set; }
        public int Amount { get; set; }
        public int OfferAmount { get; set; }
        public bool IsPurchaseFromStore { get; set; }
        public bool IsSellOffer { get; set; } = false;
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

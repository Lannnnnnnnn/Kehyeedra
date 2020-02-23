using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kehyeedra3.Services.Models
{
    public class Fishing
    {
        public ulong Id { get; set; } = 0; // this is userid incase you get alzheimers you stupid baby waa waa
        public ulong LastFish { get; set; } = 0;
        public ulong Xp { get; set; } = 0;
        public ulong TXp { get; set; } = 0;
        public ulong Lvl { get; set; } = 0;

        [Column(TypeName="LONGTEXT")]
        public string Inventory { get; set; } = "[]";

        public List<FishingInventorySlot> GetInventory()
        {
            return JsonConvert.DeserializeObject<List<FishingInventorySlot>>(Inventory);
        }

        public void SetInventory(List<FishingInventorySlot> inv)
        {
            Inventory = JsonConvert.SerializeObject(inv);
        }

    }
    public class FishObject
    {
        public FishSpecies Species;
        public FishWeight Weight;
        public FishRarity Rarity;

        public override string ToString()
        {
            return $"{Weight.ToString()} {Rarity.ToString()} {Species.ToString()}";
        }
    }
    public enum FishSpecies
    {
        //legendary
        LuckyCatfish = 1,
        //rare
        Doomfish = 2,
        Clownfish = 3,
        GenericFish = 4,
        Ultracrab = 5,
        BlobFish = 6,
        Psychedelica = 7,
        //uncommon
        Gigacrab = 8,
        MantisShrimp = 9,
        GoblinFish = 10,
        BatFish = 11,
        FrogFish = 12,
        TigerFish = 13,
        Stargazer = 14,
        Isopod = 15,
        SheepHead = 16,
        //common
        Cod = 17,
        Salmon = 18,
        Pike = 19,
        Bass = 20,
        Crayfish = 21,
        Betta = 22,
        PufferFish = 23,
        Tuna = 24,
        Carp = 25,
        Megacrab = 26
    }
    public enum FishWeight
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }
    public enum FishRarity
    {
        Common = 1,
        Uncommon = 2,
        Rare = 3,
        Special = 4
    }
    public class FishingInventorySlot
    {
        public FishObject Fish;
        public ulong Amount;
    }
}

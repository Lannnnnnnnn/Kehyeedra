using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kehyeedra3.Services.Models
{
    public class Fishing
    {
        public ulong Id { get; set; } = 0; // this is userid incase you get alzheimers you stupid baby waa waa
        public ulong LastFish { get; set; } = 0;
        public ulong Xp { get; set; } = 50;
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
        Teracrab = 4,
        Blobfish = 5,
        Psychedelica = 6,
        //uncommon
        Gigacrab = 7,
        Dopefish = 8,
        Stargazer = 9,
        Isopod = 10,
        Sheephead = 11,
        //common
        Cod = 12,
        Salmon = 13,
        Pufferfish = 14,
        Shrimp = 15,
        Crayfish = 16,
        Betta = 17,
        Carp = 18,
        Megacrab = 19
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
        Legendary = 4
    }
    public class FishingInventorySlot
    {
        public FishObject Fish;
        public ulong Amount;
    }
}

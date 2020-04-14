using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kehyeedra3.Services.Models
{
    public enum FishSize
    {
        Small = 0,
        Medium = 1,
        Large = 2
    }
    public enum FishRarity
    {
        Common, Uncommon, Rare, Legendary, 
        T2Rare, T2Legendary, T2Uncommon, T2Common,
        T3Rare, T3Legendary, T3Uncommon, T3Common,
        T4Rare, T4Legendary, T4Uncommon, T4Common
    }
    public class Fish
    {
        public FishSpecies Id;
        //public byte Ego;
        //public byte Superego;
        public string Name;
        public string Emote;
        public FishRarity Rarity;
    }
    public class Fishing
    {
        public ulong Id { get; set; } = 0; // this is userid incase you get alzheimers you stupid baby waa waa
        public ulong LastFish { get; set; } = 0;
        public ulong Xp { get; set; } = 50;
        public ulong TXp { get; set; } = 0;
        public ulong Lvl { get; set; } = 0;
        public byte RodOwned { get; set; } = 0;
        public byte RodUsed { get; set; } = 0;

        [Column(TypeName="LONGTEXT")]
        public string Inventory { get; set; } = "{}";

        public Dictionary<FishSpecies, int[]> GetInventory()
        {
            return JsonConvert.DeserializeObject<Dictionary<FishSpecies, int[]>>(Inventory);
        }

        public void SetInventory(Dictionary<FishSpecies, int[]> inv)
        {
            Dictionary<int, int[]> temp = new            Dictionary<int, int[]>();
            foreach(var entry in inv){
                temp.Add((int)entry.Key, entry.Value);
            }
            Inventory = JsonConvert.SerializeObject(temp);
        }

        public static List<Fish> GetFishList()
        {
            return new List<Fish>
            {
                new Fish()
                {
                    Id = FishSpecies.LuckyCatfish,
                    Name = "Lucky Catfish",
                    Emote = "<a:catfishleft:682655661422542888><a:catfishright:682655661481525284>",
                    Rarity = FishRarity.Legendary
                },
                new Fish()
                {
                    Id = FishSpecies.Doomfish,
                    Name = "Doomfish",
                    Emote = "<:doomfish:651879988232060949>",
                    Rarity = FishRarity.Rare
                },
                new Fish()
                {
                    Id = FishSpecies.Clownfish,
                    Name = "Clownfish",
                    Emote = "<:missingRar:682586847100403715>[Clownfish]",
                    Rarity = FishRarity.Rare
                },
                new Fish()
                {
                    Id = FishSpecies.Teracrab,
                    Name = "Teracrab",
                    Emote = "<a:teracrableft:681872487901954123><a:teracrabright:681872487264681984>",
                    Rarity = FishRarity.Rare
                },
                new Fish()
                {
                    Id = FishSpecies.Blobfish,
                    Name = "Blobfish",
                    Emote = "<:missingRar:682586847100403715>[Blobfish]",
                    Rarity = FishRarity.Rare
                },
                new Fish()
                {
                    Id = FishSpecies.Psychedelica,
                    Name = "Psychedelica",
                    Emote = "<a:psychedelicaleft:682606276592664666><a:psychedelicaright:682606278354141249>",
                    Rarity = FishRarity.Rare
                },
                new Fish()
                {
                    Id = FishSpecies.Gigacrab,
                    Name = "Gigacrab",
                    Emote = "<:gigacrab:681871426382594208>",
                    Rarity = FishRarity.Uncommon
                },
                new Fish()
                {
                    Id = FishSpecies.Dopefish,
                    Name = "Dopefish",
                    Emote = "<:missingUnc:682586846857003064>[Dopefish]",
                    Rarity = FishRarity.Uncommon
                },
                new Fish()
                {
                    Id = FishSpecies.Stargazer,
                    Name = "Stargazer",
                    Emote = "<:missingUnc:682586846857003064>[Stargazer]",
                    Rarity = FishRarity.Uncommon
                },
                new Fish()
                {
                    Id = FishSpecies.Isopod,
                    Name = "Isopod",
                    Emote = "<:missingUnc:682586846857003064>[Isopod]",
                    Rarity = FishRarity.Uncommon
                },
                new Fish()
                {
                    Id = FishSpecies.Sheephead,
                    Name = "Sheephead",
                    Emote = "<:sheepheadleft:681200891810021376><:sheepheadright:681200891608563767>",
                    Rarity = FishRarity.Uncommon
                },
                new Fish()
                {
                    Id = FishSpecies.Cod,
                    Name = "Cod",
                    Emote = "<:codleft:695304941715062887><:codright:695304941949943808>",
                    Rarity = FishRarity.Common
                },
                new Fish()
                {
                    Id = FishSpecies.Salmon,
                    Name = "Salmon",
                    Emote = "<:salmonleft:698167269359878236><:salmonright:698167269167202324>",
                    Rarity = FishRarity.Common
                },
                new Fish()
                {
                    Id = FishSpecies.Shrimp,
                    Name = "Shrimp",
                    Emote =  "<:shromp:695335369004023859>",
                    Rarity = FishRarity.Common
                },
                new Fish()
                {
                    Id = FishSpecies.Crayfish,
                    Name = "Crayfish",
                    Emote =  "<:crayfishleft:698157988195598466><:crayfishright:698157988170432602>",
                    Rarity = FishRarity.Common
                },
                new Fish()
                {
                    Id = FishSpecies.Betta,
                    Name = "Betta",
                    Emote = "<:bettaleft:698179217107714088><:bettaright:698179216868638851>",
                    Rarity = FishRarity.Common
                },
                new Fish()
                {
                    Id = FishSpecies.Pufferfish,
                    Name = "Pufferfish",
                    Emote = "<:pufferfish:698203043350708225>",
                    Rarity = FishRarity.Common
                },
                new Fish()
                {
                    Id = FishSpecies.Carp,
                    Name = "Carp",
                    Emote = "<:koicarpleft:698204388556275752><:koicarpright:698204386421374986>",
                    Rarity = FishRarity.Common
                },
                new Fish()
                {
                    Id = FishSpecies.Megacrab,
                    Name = "Megacrab",
                    Emote = "<:megacrab:681871426319286302>",
                    Rarity = FishRarity.Common
                },

                new Fish() //// Tier 2
                {
                    Id = FishSpecies.T2Circusfish,
                    Name = "Circusfish",
                    Emote = "<:missingLeg:682586847830081551>",
                    Rarity = FishRarity.T2Legendary
                },
                new Fish()
                {
                    Id = FishSpecies.T2Swolefish,
                    Name = "Pumped Up Swolefish",
                    Emote = "<:missingLeg:682586847830081551>",
                    Rarity = FishRarity.T2Rare
                },
                new Fish()
                {
                    Id = FishSpecies.T2Gunfish,
                    Name = "Gunfish",
                    Emote = "<:missingLeg:682586847830081551>",
                    Rarity = FishRarity.T2Uncommon
                },
                new Fish()
                {
                    Id = FishSpecies.T2Com,
                    Name = "Missing T2 Common Fish",
                    Emote = "<:missingLeg:682586847830081551>",
                    Rarity = FishRarity.T2Common
                },

                new Fish() //// Tier 3
                {
                    Id = FishSpecies.T3Doomfish,
                    Name = "Spectral Doomfish",
                    Emote = "<:missingLeg:682586847830081551>",
                    Rarity = FishRarity.T3Legendary
                },
                new Fish()
                {
                    Id = FishSpecies.T3Crab,
                    Name = "Revenant Crab",
                    Emote = "<:missingLeg:682586847830081551>",
                    Rarity = FishRarity.T3Rare
                },
                new Fish()
                {
                    Id = FishSpecies.T3Flameworm,
                    Name = "Spectral Flameworm",
                    Emote = "<:missingLeg:682586847830081551>",
                    Rarity = FishRarity.T3Uncommon
                },
                new Fish()
                {
                    Id = FishSpecies.T3Com,
                    Name = "Spectral Shrimp",
                    Emote = "<:missingLeg:682586847830081551>",
                    Rarity = FishRarity.T3Common
                },

                new Fish() //// Tier 4
                {
                    Id = FishSpecies.T4Leg,
                    Name = "Missing T4 Legendary Fish",
                    Emote = "<:missingLeg:682586847830081551>",
                    Rarity = FishRarity.T4Legendary
                },
                new Fish()
                {
                    Id = FishSpecies.T4Rar,
                    Name = "Missing T4 Rare Fish",
                    Emote = "<:missingLeg:682586847830081551>",
                    Rarity = FishRarity.T4Rare
                },
                new Fish()
                {
                    Id = FishSpecies.T4Unc,
                    Name = "Missing T4 Uncommon Fish",
                    Emote = "<:missingLeg:682586847830081551>",
                    Rarity = FishRarity.T4Uncommon
                },
                new Fish()
                {
                    Id = FishSpecies.T4Com,
                    Name = "Missing T4 Common Fish",
                    Emote = "<:missingLeg:682586847830081551>",
                    Rarity = FishRarity.T4Common
                }

           };
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
        Megacrab = 19,
        //T2 Legendary
        T2Circusfish = 20,
        //T2 Rare
        T2Swolefish = 21,
        //T2 Uncommon
        T2Gunfish = 22,
        //T2 Common
        T2Com = 23,
        //T3 Legendary
        T3Doomfish = 24,
        //T3 Rare
        T3Crab = 25,
        //T3 Uncommon
        T3Flameworm = 26,
        //T3 Common
        T3Com = 27,
        //T4 Legendary
        T4Leg = 28,
        //T4 Rare
        T4Rar = 29,
        //T4 Uncommon
        T4Unc = 30,
        //T4 Common
        T4Com = 31
    }
    public class FishingInventorySlot
    {
        public int Id;
        public int[] Amount;
    }
}

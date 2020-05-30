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
        public int Prestige { get; set; } = 0;

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
                    Emote = "<a:clownfishleft:715846565972934718><a:clownfishright:715846565704761424>",
                    Rarity = FishRarity.Rare
                },
                new Fish()
                {
                    Id = FishSpecies.Teracrab,
                    Name = "Teracrab",
                    Emote = "<a:teracrableft:710925664089538691><a:teracrabright:710925663439421512>",
                    Rarity = FishRarity.Rare
                },
                new Fish()
                {
                    Id = FishSpecies.Blobfish,
                    Name = "Blobfish",
                    Emote = "<a:blobfishleft:704386995996065885><a:blobfishright:704386996369358888>",
                    Rarity = FishRarity.Rare
                },
                new Fish()
                {
                    Id = FishSpecies.Psychedelica,
                    Name = "Psychedelica",
                    Emote = "<a:psychedelicaleft:704406253966721135><a:psychedelicaright:704406252125421698>",
                    Rarity = FishRarity.Rare
                },
                new Fish()
                {
                    Id = FishSpecies.Gigacrab,
                    Name = "Gigacrab",
                    Emote = "<:gigacrableft:715626112327221348><:gigacrabright:715626112537067580>",
                    Rarity = FishRarity.Uncommon
                },
                new Fish()
                {
                    Id = FishSpecies.Dopefish,
                    Name = "Dopefish",
                    Emote = "<:dopefishleft:700422139672658009><:dopefishright:700422139643428895>",
                    Rarity = FishRarity.Uncommon
                },
                new Fish()
                {
                    Id = FishSpecies.Stargazer,
                    Name = "Stargazer",
                    Emote = "<:stargazerleft:700414644774240286><:stargazerright:700413063442202684>",
                    Rarity = FishRarity.Uncommon
                },
                new Fish()
                {
                    Id = FishSpecies.Isopod,
                    Name = "Isopod",
                    Emote = "<:isopodleft:700397032271249428><:isopodright:700397031922991206>",
                    Rarity = FishRarity.Uncommon
                },
                new Fish()
                {
                    Id = FishSpecies.Sheephead,
                    Name = "Sheephead",
                    Emote = "<:sheepheadleft:710894977944649728><:sheepheadright:710894979467444284>",
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
                    Emote =  "<:shrimpleft:715074288788570112><:shrimpright:715074288863936552>",
                    Rarity = FishRarity.Common
                },
                new Fish()
                {
                    Id = FishSpecies.Crayfish,
                    Name = "Crayfish",
                    Emote =  "<:crayfishleft:715638929885495377><:crayfishright:715638929843421284>",
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
                    Emote = "<:pufferfishleft:715075414179184691><:pufferfishright:715075414116007937>",
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
                    Name = "Hermit Crab",
                    Emote = "<:hermitcrableft:715071501996392519><:hermitcrabright:715071501971488808>",
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

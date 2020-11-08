using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Kehyeedra3.Services.Models
{
    public class User
    {
        [Key]
        public ulong Id { get; set; } = 0;
        public string Avatar { get; set; } = null;
        public string Username { get; set; } = null;
        public long Money { get; set; } = 0;
        public ulong LastMine { get; set; } = 0;
        [Column(TypeName = "LONGTEXT")]
        public string GeneralInventory { get; set; } = "{}";
        [Column(TypeName = "TINYINT")]
        public BattleFish CurrentBattlefish { get; set; } = 0;
      


        public bool GrantMoney(User bank, long amount)
        {
            if(bank.Money > amount)
            {
                Money += amount;
                bank.Money -= amount;
                return true;
            }

            return false;
        }


        public Dictionary<Items, int[]> GetGenInve()
        {
            return JsonConvert.DeserializeObject<Dictionary<Items, int[]>>(GeneralInventory);
        }

        public void SetGenInve(Dictionary<Items, int[]> inv)
        {
            Dictionary<int, int[]> temp = new Dictionary<int, int[]>();
            foreach (var entry in inv)
            {
                temp.Add((int)entry.Key, entry.Value);
            }
            GeneralInventory = JsonConvert.SerializeObject(temp);
        }

        public static List<Item> ListItems()
        {
            return new List<Item>
            {
                new Item()
                {
                    Id = Items.DirtyBoot,
                    Name = "Lan's Love",
                    Price = 100
                },
                new Item()
                {
                    Id = Items.Lettuce,
                    Name = "Lettuce",
                    Price = 50
                }
            };
        }
        public class Item
        {
            public Items Id { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
        }

        public class BattleFishObject
        {
            [Key]
            public ulong FishId { get; set; }
            [Required]
            public ulong UserId { get; set; }
            [Column(TypeName = "TINYINT")]
            public BattleFish FishType { get; set; } = 0;
            public ulong Xp { get; set; } = 0;
            public ulong NextXp { get; set; } = 50;
            public int Lvl { get; set; } = 0;
            public string Name { get; set; } = "Unnamed";
        }
    }
    public enum BattleFish
    {
        None = 0,
        Herring = 1,
        Birgus = 2,
        Abama = 3,
        Pistashrimp = 4
    }
    public enum Items
    {
        DirtyBoot = 0,
        Lettuce = 1
    }
    public class ItemSlot
    {
        public int Id;
        public int Amount;
    }
}

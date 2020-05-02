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
        public ICollection<BattleFishObject> BattleFish { get; set; }


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

        public void SetGenInv(List<Item> newInv)
        {
            GeneralInventory = JsonConvert.SerializeObject(newInv);
        }
        public List<Item> GetGenInv()
        {
            return JsonConvert.DeserializeObject<List<Item>>(GeneralInventory);
        }
        public static List<Item> ListItems()
        {
            return new List<Item>
            {
                new Item()
                {
                    Id = Items.Item1,
                    Name = "Item1"
                },
                new Item()
                {
                    Id = Items.Item2,
                    Name = "Item1"
                },
            };
        }
        public class Item
        {
            public Items Id { get; set; }
            public string Name { get; set; }
        }
        public class BattleFishObject
        {
            [Key]
            public ulong FishId { get; set; }
            public byte FishType { get; set; } = 0;
            public ulong Xp { get; set; } = 0;
            public ulong NextXp { get; set; } = 50;
            public int Lvl { get; set; } = 0;
            public string Name { get; set; } = "Unnamed";
        }
    }
    public enum Items
    {
        Item1 = 0,
        Item2 = 1,
    }
    public class ItemSlot
    {
        public int Id;
        public int Amount;
    }
}

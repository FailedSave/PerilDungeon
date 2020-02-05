using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Items
{
    public class ClothesEquipment : IEquipment
    {
        public ClothesEquipment()
        {
        }

        public string Name { get => "Ordinary Clothes"; set { } }
        public EquipmentSlot EquipmentSlot { get => EquipmentSlot.Body; }

        public void Equip(Character c)
        {
            return;
        }

        public void Unequip(Character c)
        {
            return;
        }
    }
}

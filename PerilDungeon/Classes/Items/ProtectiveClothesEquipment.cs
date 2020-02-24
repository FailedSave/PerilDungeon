using System;
using System.Collections.Generic;
using System.Text;

namespace PerilDungeon.Classes.Items
{
    class ProtectiveClothesEquipment : IEquipment
    {
        public ProtectiveClothesEquipment()
        {
        }

        public string Name { get => "Spidersilk Clothes"; set { } }
        public EquipmentSlot EquipmentSlot { get => EquipmentSlot.Body; }

        public void Equip(Character c)
        {
            c.AddStatus("Protected");
        }

        public void Unequip(Character c)
        {
            c.RemoveStatus("Protected");
        }
    }
}

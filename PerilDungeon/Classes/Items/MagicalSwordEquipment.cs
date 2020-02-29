using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Items
{
    public class MagicalSwordEquipment : IEquipment
    {
        public MagicalSwordEquipment()
        {
        }

        public string Name { get => "Sword from the Stone"; set { } }
        public EquipmentSlot EquipmentSlot { get => EquipmentSlot.Main; }

        public void Equip(Character c)
        {
            c.Combat += 4;
        }

        public void Unequip(Character c)
        {
            c.Combat -= 4;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes
{
    public enum EquipmentSlot
    {
        Main,
        Body
    }
    public interface IEquipment : IItem
    {
        EquipmentSlot EquipmentSlot { get; }

        void Equip(Character c);
        void Unequip(Character c);
    }
}

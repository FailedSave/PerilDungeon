using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes
{
    public interface ISpell
    {
        int Cost { get; set; }
        string Name { get; set; }
        IEnumerable<string> Cast(Character caster, Character target);
    }
}

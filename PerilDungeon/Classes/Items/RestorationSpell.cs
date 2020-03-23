using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Items
{
    public class RestorationSpell : ISpell
    {
        public int Cost { get => 100; set { } }
        public string Name { get => "Restoration"; set { } }

        public IEnumerable<string> Cast(Character caster, Character target)
        {
            var messages = new List<string>();
            if (caster.HasStatus(Status.Inspired))
            {
                caster.LoseMana(Cost / 2);
            }
            else
            {
                caster.LoseMana(Cost);
            }
            if (target.HasStatus(Status.Petrified))
            {
                target.RemoveStatus(Status.Petrified);
                messages.Add($"{caster.YouOrName} {(caster.IsPlayer ? "summon" : "summons")} energy deep from deep within and {(caster.IsPlayer ? "chant" : "chants")} the restoration spell. A warm, pale green glow suffuses {target.YouOrNameLower}. A minute or so passes while the spell takes effect, and {target.YouAreOrNameIsLower} returned to flesh and blood.");
            }
            else if (target.HasStatus(Status.Polymorphed))
            {
                target.RemoveStatus(Status.Polymorphed);
                messages.Add($"{caster.YouOrName} {(caster.IsPlayer ? "summon" : "summons")} energy deep from deep within and {(caster.IsPlayer ? "chant" : "chants")} the restoration spell. A warm, pale green glow suffuses {target.YouOrNameLower}. A minute or so passes while the spell takes effect, and {target.YouAreOrNameIsLower} returned to {target.YourOrHerLower} {(target.Species == Species.Human ? "human" : "faerie")} form.");
            }
            return messages;
        }
    }
}

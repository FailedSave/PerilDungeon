using System;
using System.Collections.Generic;
using System.Text;

namespace PerilDungeon.Classes.Encounters
{
    class MerchantClothesEncounter : IEncounter
    {
        public string Title { get => "Peaceful Spiderfolk"; set { } }
        public string Description { get => "In a corridor filled with intimidating, sticky spiderwebs, you come upon a small hut carved into the stone. Inside are a trio of humanoid spiders! They're scary, but they seem used to this reaction, and they greet you. In exchange for some of your supplies and treasures worth $50, they can equip one of you with protective spidersilk clothes that will not only keep you warm, but help protect against injury."; set { } }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices
        {
            get
            {
                List<IEncounterChoice> choices = new List<IEncounterChoice>();
                foreach (Character c in Party.PartyMembers)
                {
                    choices.Add(new MerchantBuyEncounterChoice(c, Party, 50, Effect, "You pay the spiderfolk. They measure {0} and with startling speed, weave a tough, new suit of clothes that fit {0} perfectly."));
                }
                choices.Add(new MessageOnlyEncounterChoice("Decline", "You decline the spiders' offer. They wave with their odd claws and bid you good luck in the Delve."));
                return choices;
            }
            set { }
        }

        private void Effect(Character c)
        {
            IEquipment clothes = new Items.ProtectiveClothesEquipment();
            clothes.Equip(c);
            if (c.BodyItem != null)
            {
                c.BodyItem.Unequip(c);
            }
            c.BodyItem = clothes;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerilDungeon.Classes;

namespace PerilDungeon.Data
{
    public class PartyProvider : IPartyProvider
    {

        public PartyProvider()
        {
            Party = getDefaultParty();
        }

        public Party Party { get; private set; }

        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }

        private Party getDefaultParty()
        {
            Party party = new Party();
            Character lorraine = new Character("Lorraine");
            lorraine.IsPlayer = true;
            Character johanna = new Character("Johanna");
            Character cylenae = new Character("Cylenae");
            party.PartyMembers.Add(lorraine);
            party.PartyMembers.Add(johanna);
            party.PartyMembers.Add(cylenae);
            return party;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerilDungeon.Classes;

namespace PerilDungeon.Data
{
    public class PartyProvider : IPartyProvider
    {
        private List<Character> _party;

        public PartyProvider()
        {
            _party = new List<Character>();
            _party.Add(new Character("You"));
            _party.Add(new Character("Johanna"));
            _party.Add(new Character("Cylenae"));
        }

        public List<Character> Party
        {
            get
            {
                return _party;
            }
        }

        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }

    }
}

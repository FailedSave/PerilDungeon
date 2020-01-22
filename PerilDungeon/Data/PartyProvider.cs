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
            _party.Add(new Character("Lorraine"));
            _party.Add(new Character("Johanna"));
            _party.Add(new Character("Cylenae"));
            _party[0].IsPlayer = true;
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

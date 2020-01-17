using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerilDungeon.Classes;

namespace PerilDungeon.Data
{
    interface IPartyProvider
    {
        public List<Character> Party { get; }
    }
}

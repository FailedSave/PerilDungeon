using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes
{
    public interface IEncounter
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Party Party { get; set; }
        public IEnumerable<IEncounterChoice> Choices { get; set; }

    }
}

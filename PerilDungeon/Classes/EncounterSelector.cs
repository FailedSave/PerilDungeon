using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerilDungeon.Classes.Encounters;

namespace PerilDungeon.Classes
{
    public class EncounterSelector
    {
        public static Random rng;

        //Can pass in the encounter you "prefer", but the encounter picker may override (say, with the game over encounter)
        public static IEncounter PickEncounter(Party party, Type preferredEncounter)
        {
            if (rng == null)
            {
                rng = new Random();
            }

            //If the party is at game over, do that
            if (!party.HasCharacterActive)
            {
                return new GameOverEncounter();
            }

            if (preferredEncounter != null)
            {
                return (IEncounter)Activator.CreateInstance(preferredEncounter);
            }

            IEncounter encounter;

            if (rng.NextDouble() > 0.6)
            {
                encounter = new StairsEncounter();
            }
            else
            {
                encounter = new BadAirEncounter();
            }
            encounter.Party = party;
            return encounter;
        }
    }
}

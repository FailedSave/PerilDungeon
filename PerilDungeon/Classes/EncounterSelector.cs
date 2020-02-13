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
        public static Type lastEncounter;

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

            //If the party is out of time, send them to that
            if (party.TimeRemaining <= 0)
            {
                return new TimeUpEncounter();
            }

            if (preferredEncounter != null)
            {
                IEncounter selectedEncounter = (IEncounter)Activator.CreateInstance(preferredEncounter);
                selectedEncounter.Party = party;
                return selectedEncounter;
            }

            Type chosenEncounter = null;
            do
            {
                chosenEncounter = pickEncounterFromWeightedList(generateEncounterTable(party));
            }
            while (chosenEncounter == lastEncounter);

            lastEncounter = chosenEncounter; //can't get the same one twice in a row

            IEncounter encounter = (IEncounter)Activator.CreateInstance(chosenEncounter);

            encounter.Party = party;
            return encounter;
        }

        private static Dictionary<Type, double> generateEncounterTable(Party party)
        {
            var table = new Dictionary<Type, double>();
            table.Add(typeof(StairsEncounter), 10.0);
            table.Add(typeof(BasiliskEncounter), 10.0);
            table.Add(typeof(TrappedDoorEncounter), 10.0);
            table.Add(typeof(BadAirEncounter), 10.0);
            table.Add(typeof(FountainEncounter), 10.0);
            if (party.Depth >= 2)
            {
                table.Add(typeof(GorgonEncounter), 10.0);
            }
            if (party.Depth >= 4)
            {
                table.Add(typeof(GelatinousCubeEncounter), 10.0);
                table.Add(typeof(UnweaverEncounter), 20.0);
            }
            return table;
        }

        private static Type pickEncounterFromWeightedList(Dictionary<Type, double> table)
        {
            double total = table.Values.Sum();
            double target = rng.NextDouble() * total;
            double currentIndex = 0;

            foreach (var entry in table)
            {
                currentIndex += entry.Value;
                if (currentIndex >= target)
                {
                    return entry.Key;
                }
            }
            return typeof(BasicEncounter);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerilDungeon.Classes.Encounters
{
    public class SunstoneClaimEncounterChoice : IEncounterChoice
    {
        public SunstoneClaimEncounterChoice()
        {
            Text = $"Take a Suncrystal";
            IsAvailable = true;
        }

        public string Text { get; set; }
        public Func<Party, IEnumerable<string>> Choose
        {
            get
            {
                List<string> messages = new List<string>();
                return (Party party) =>
                {
                    Character target = party.GetRandomActiveCharacter();
                    if (party.MainQuestProgress == MainQuestProgress.GotBook)
                    {
                        party.MainQuestProgress = MainQuestProgress.GotShard;
                        if (target.IsPlayer)
                        {
                            messages.Add($"You lower your voice, and in a reverent whisper, begin reciting the prayer written in the Kylanian Apocrypha. As you touch the Suncrystal, it glows with an inner yellow light, and a shard detaches from the crystal and comes off freely in your hand. A warm light suffuses the room, invigorating your party. Now to escape!");
                        }
                        else
                        {
                            messages.Add($"{target.Name} lowers her voice, and in a reverent whisper, begins reciting the prayer written in the Kylanian Apocrypha. As she touches the Suncrystal, it glows with an inner yellow light, and a shard detaches from the crystal and comes off freely in her hand. A warm light suffuses the room, invigorating your party. Now to ascend back up to the top floor and escape!");
                        }
                        foreach (Character c in party.PartyMembers)
                        {
                            for (int i = 1; i <= 4; i++)
                            {   //bonus heals
                                c.Rest();
                            }
                        }
                    }
                    else
                    {
                        target.AddStatus(Status.Petrified);
                        if (target.IsPlayer)
                        {
                            messages.Add($"You reach out to the crystal to touch it, but as you do so, you feel an oppressive divine presence; the room seems to darken, the air thickens. You try to draw your hand back, but it's too late. Kylan has turned you into a statue for your impertinence.");
                        }
                        else
                        {
                            messages.Add($"{target.Name} reaches out to the crystal to touch it, but as she does, she feels an oppressive divine presence; the room seems to darken, the air thickens. She tries to draw her hand back, but it's too late. Kylan has turned her into a statue for her impertinence.");
                        }
                    }

                    return messages;
                };
            }
            set { }
        }
        public bool IsAvailable { get; set; }

        public IEncounter GetNextEncounter(Party p, IEncounter encounter)
        {
            return EncounterSelector.PickEncounter(p, typeof(ExplorationEncounter));
        }
    }
}

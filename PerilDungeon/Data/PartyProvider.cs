using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerilDungeon.Classes;
using PerilDungeon.Classes.Items;

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

        public void ResetGame()
        {
            Party = getDefaultParty();
        }

        private Party getDefaultParty()
        {
            Party party = new Party();
            Character lorraine = new Character("Lorraine");
            lorraine.IsPlayer = true;
            lorraine.Health = 100;
            lorraine.MaxHealth = 100;
            lorraine.Mana = 100;
            lorraine.MaxMana = 100;
            lorraine.Combat = 10;
            lorraine.Thievery = 15;
            lorraine.BodyItem = new ClothesEquipment();
            lorraine.Spells.Add(new RestorationSpell());
            lorraine.Species = Species.Human;
            lorraine.NativeSpecies = Species.Human;
            Character johanna = new Character("Johanna");
            johanna.Health = 120;
            johanna.MaxHealth = 120;
            johanna.Mana = 50;
            johanna.MaxMana = 50;
            johanna.Combat = 15;
            johanna.Thievery = 5;
            johanna.BodyItem = new ClothesEquipment();
            johanna.Spells.Add(new RestorationSpell());
            johanna.Species = Species.Human;
            johanna.NativeSpecies = Species.Human;
            Character cylenae = new Character("Cylenae");
            cylenae.Health = 50;
            cylenae.MaxHealth = 50;
            cylenae.Mana = 200;
            cylenae.MaxMana = 200;
            cylenae.Combat = 5;
            cylenae.Thievery = 10;
            cylenae.BodyItem = new ClothesEquipment();
            cylenae.Spells.Add(new RestorationSpell());
            cylenae.Species = Species.Faerie;
            cylenae.NativeSpecies = Species.Faerie;
            party.PartyMembers.Add(lorraine);
            party.PartyMembers.Add(johanna);
            party.PartyMembers.Add(cylenae);
            party.GameOver = false;
            party.MainQuestProgress = MainQuestProgress.Beginning;
            return party;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SabberStoneCoreAi.src.Agent.AlvaroMCTS
{
	class DeckManager
	{
		public List<SabberStoneCore.Model.Card> NaxxramasWarlock { get; set; }
		public List<SabberStoneCore.Model.Card> NaxxramasPriest { get; set; }
		public List<SabberStoneCore.Model.Card> AggroShaman { get; set; }
		public List<SabberStoneCore.Model.Card> ControlWarrior { get; set; }
		public List<SabberStoneCore.Model.Card> MidRangeHunter { get; set; }

		public DeckManager()
		{
			NaxxramasWarlock = new List<SabberStoneCore.Model.Card>();
			NaxxramasPriest = new List<SabberStoneCore.Model.Card>();
			AggroShaman = new List<SabberStoneCore.Model.Card>();
			ControlWarrior = new List<SabberStoneCore.Model.Card>();
			MidRangeHunter = new List<SabberStoneCore.Model.Card>();

			fillNaxxramasWarlock();
			fillNaxxramasPriest();
			fillAggroShaman();
			fillControlWarrior();
			fillMidRangeHunter();
		}

		private void fillNaxxramasWarlock()
		{
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Mortal Coil"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Mortal Coil"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Demonfire"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Drain Life"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Shadow Bolt"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Shadow Bolt"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Shadowflame"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Bane of Doom"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Doomguard"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Twisting Nether"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Lord Jaraxxus"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Crazed Alchemist"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Nerubian Egg"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Nerubian Egg"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Shade of Naxxramas"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Shade of Naxxramas"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Stoneskin Gargoyle"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Stoneskin Gargoyle"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Baron Rivendare"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Abomination"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Shadow Bolt"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Big Game Hunter"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Faceless Manipulator"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Feugen"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Sludge Belcher"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Spectral Knight"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Spectral Knight"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Stalagg"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Frost Elemental"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Kel'Thuzad"));
			NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Ragnaros the Firelord"));

		}

		private void fillNaxxramasPriest()
		{
			NaxxramasPriest.Add(SabberStoneCore.Model.Cards.FromName("Northshire Cleric"));
			NaxxramasPriest.Add(SabberStoneCore.Model.Cards.FromName("Northshire Cleric"));
			NaxxramasPriest.Add(SabberStoneCore.Model.Cards.FromName("Northshire Cleric"));
			NaxxramasPriest.Add(SabberStoneCore.Model.Cards.FromName("Northshire Cleric"));
			NaxxramasPriest.Add(SabberStoneCore.Model.Cards.FromName("Northshire Cleric"));
			NaxxramasPriest.Add(SabberStoneCore.Model.Cards.FromName("Northshire Cleric"));
			NaxxramasPriest.Add(SabberStoneCore.Model.Cards.FromName("Northshire Cleric"));
			NaxxramasPriest.Add(SabberStoneCore.Model.Cards.FromName("Northshire Cleric"));
		}

		private void fillAggroShaman()
		{
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Abusive Sergeant"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Abusive Sergeant"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Argent Squire"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Argent Squire"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Lightning Bolt"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Lightning Bolt"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Rockbiter Weapon"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Rockbiter Weapon"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Sir Finley Mrrgglton"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Tunnel Trogg"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Tunnel Trogg"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Ancestral Knowledge"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Ancestral Knowledge"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Flame Juggler"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Flametongue Totem"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Flametongue Totem"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Lava Shock"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Lava Shock"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Totem Golem"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Totem Golem"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Argent Horserider"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Argent Horserider"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Feral Spirit"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Feral Spirit"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Lava Burst"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Lava Burst"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Flamewreathed Faceless"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Flamewreathed Faceless"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Doomhammer"));
			AggroShaman.Add(SabberStoneCore.Model.Cards.FromName("Doomhammer"));
		}

		private void fillControlWarrior()
		{
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Execute"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Execute"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Shield Slam"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Shield Slam"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Fiery War Axe"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Fiery War Axe"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Revenge"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Revenge"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Slam"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Acolyte of Pain"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Acolyte of Pain"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Bash"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Bash"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Ravaging Ghoul"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Ravaging Ghoul"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Shield Block"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Shield Block"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Elise Starseeker"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Infested Tauren"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Infested Tauren"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Brawl"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Brawl"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Ironforge Portal"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Ironforge Portal"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Cairne Bloodhoof"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Justicar Trueheart"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Sylvanas Windrunner"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Gorehowl"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("Grommash Hellscream"));
			ControlWarrior.Add(SabberStoneCore.Model.Cards.FromName("N'Zoth, the Corruptor"));
		}

		private void fillMidRangeHunter()
		{
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Abusive Sergeant"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Argent Squire"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Fiery Bat"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Fiery Bat"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Tracking"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Dire Wolf Alpha"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Huge Toad"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Huge Toad"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Kindly Grandmother"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Kindly Grandmother"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Quick Shot"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Quick Shot"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Animal Companion"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Animal Companion"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Argent Horserider"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Deadly Shot"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Eaglehorn Bow"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Kill Command"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Kill Command"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Unleash the Hounds"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Unleash the Hounds"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Barnes"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Houndmaster"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Houndmaster"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Infested Wolf"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Infested Wolf"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Savannah Highmane"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Savannah Highmane"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Call of the Wild"));
			MidRangeHunter.Add(SabberStoneCore.Model.Cards.FromName("Call of the Wild"));
		}
	};
		

}

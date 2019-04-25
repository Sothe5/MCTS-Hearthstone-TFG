using System;
using System.Collections.Generic;
using System.Text;

namespace SabberStoneCoreAi.src.Agent.AlvaroMCTS
{
	class DeckManager
	{
		public List<SabberStoneCore.Model.Card> NaxxramasWarlock { get; set; }
		public List<SabberStoneCore.Model.Card> NaxxramasPriest { get; set; }

		public DeckManager()
		{
			NaxxramasWarlock = new List<SabberStoneCore.Model.Card>();
			NaxxramasPriest = new List<SabberStoneCore.Model.Card>();
			fillNaxxramasWarlock();
			fillNaxxramasPriest();
		}

		private void fillNaxxramasWarlock()
		{
			//NaxxramasWarlock.Add(SabberStoneCore.Model.Cards.FromName("Haunted Creeper"));
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
	};
		

}

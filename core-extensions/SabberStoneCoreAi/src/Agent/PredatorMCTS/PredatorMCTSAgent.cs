using System;
using System.Collections.Generic;
using System.Text;

using SabberStoneCoreAi.MC;
using SabberStoneCoreAi.Score;
using SabberStoneCoreAi.Bigram;
using SabberStoneCore.Enums;
using SabberStoneCoreAi.Meta;
using SabberStoneCore.Model;


namespace SabberStoneCoreAi.Agent
{
    class PredatorMCTSAgent : AbstractMCTSAgent
	{
		private PredictionParameters _predictionParameters;

		private BigramMap _map;

		public PredatorMCTSAgent(IScore scoring, MCTSParameters mctsParameters, PredictionParameters predictionParameters)
			: base(scoring, mctsParameters)
		{
			/*deck = ControlWarlock;
			hero = CardClass.WARLOCK;
			*/
			_predictionParameters = predictionParameters;
			_map = BigramMapReader.ParseFile(_predictionParameters.File); 
		}

		public static List<Card> ControlWarlock => new List<Card>(){
				//Dark Pact
				Cards.FromId ("LOOT_017"),
				Cards.FromId ("LOOT_017"),
				// Kobold Librarian
				Cards.FromId ("LOOT_014"),
				Cards.FromId ("LOOT_014"),
				// Defile
				Cards.FromId ("ICC_041"),
				Cards.FromId ("ICC_041"),
				// Stonehill Defender
				Cards.FromId ("UNG_072"),
				Cards.FromId ("UNG_072"),
				// Lesser Amethyst Spellstone
				Cards.FromId ("LOOT_043"),
				Cards.FromId ("LOOT_043"),
				// Hellfire
				Cards.FromId ("CS2_062"),
				Cards.FromId ("CS2_062"),
				// Possessed Lackey
				Cards.FromId ("LOOT_306"),
				Cards.FromId ("LOOT_306"),
				// Rin, the First Disciple
				Cards.FromId ("LOOT_415"),
				// Twisting Nether
				Cards.FromId ("EX1_312"),
				Cards.FromId ("EX1_312"),
				// Voidlord
				Cards.FromId ("LOOT_368"),
				Cards.FromId ("LOOT_368"),
				// Bloodreaver Gul'dan
				Cards.FromId ("ICC_831"),
				// Mistress of Mixtures
				Cards.FromId ("CFM_120"),
				Cards.FromId ("CFM_120"),
				// Doomsayer
				Cards.FromId ("NEW1_021"),
				Cards.FromId ("NEW1_021"),
				// N'Zoth, the Corruptor
				Cards.FromId ("OG_133"),
				// Siphon Soul
				Cards.FromId ("EX1_309"),
				// Skulking Geist
				Cards.FromId ("ICC_701"),
				//Mortal Coil
				Cards.FromId ("EX1_302"),
				// Gnomeferatu
				Cards.FromId ("ICC_407"),
				Cards.FromId ("ICC_407")
			};

		protected override AbstractMCTSSimulator initSimulator(int playerID, IScore scoring)
		{
			return new MCTSSimulatorExt(playerID, scoring,_mctsParameters,
				_predictionParameters, _map)
			{
				Watch = Watch
			};
		}
	}
}

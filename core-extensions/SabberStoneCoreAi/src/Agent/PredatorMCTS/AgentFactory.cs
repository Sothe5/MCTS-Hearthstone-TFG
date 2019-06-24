using System;
using System.Collections.Generic;
using System.Text;

using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using SabberStoneCoreAi.MC;
using SabberStoneCoreAi.Meta;
using SabberStoneCoreAi.Score;
using SabberStoneCore.Tasks.PlayerTasks;


namespace SabberStoneCoreAi.Agent
{
	/// <summary>
	/// Factory singleton class for creating abstract agents.
	/// </summary>
    class AgentFactory
    {
		public List<Card> deck = null;
		public CardClass hero = CardClass.INVALID;

		/// <summary>
		/// The type of heartstone agents.
		/// </summary>
		public enum AgentType
		{
			FlatMCTS,
			MCTS,
			PredatorMCTS,
			ExhaustiveSeachAgent
		}

		/// <summary>
		/// The singleton instance.
		/// </summary>
		private static AgentFactory _instance;

		/// <summary>
		/// The factory instance. 
		/// </summary>
		public static AgentFactory Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new AgentFactory();
				}
				return _instance;
			}
		}

		/// <summary>
		/// The private default constructor.
		/// </summary>
		public AgentFactory()
		{
			deck = ControlWarlock;
			hero = CardClass.WARLOCK;
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

		/// <summary>
		/// Returns a instance of a Hearthstone agent based on the profited card class and agent type.
		/// The card class of the hero is key for configuring the agent correctly, especially the predator MCTS. 
		/// </summary>
		/// <param name="cardClass">the card class of the agent's hero</param>
		/// <param name="deck">the deck of the agent</param>
		/// <param name="type">the type of agent</param>
		/// <returns></returns>
		public AbstractAgent GetAgent(CardClass cardClass, List<Card> deck,  AgentType type)
		{
			double simulationTime = 15000 ;
			IScore scoring = new WeightedScore();

			AbstractAgent agent = new ExhaustiveSeachAgent(10, 200, scoring); 
			switch (type)
			{
				case AgentType.FlatMCTS:
					agent = new FlatMCAgent(scoring);
					break;
				case AgentType.MCTS:
					/*agent = new MCTSAgent(scoring,
						new MCTSParameters
					{
						SimulationTime = simulationTime,
						AggregationTime = 100,
						RolloutDepth = 5,
						UCTConstant = 9000
					});*/
					break;
				case AgentType.PredatorMCTS:
					// the default decks
					if (cardClass == CardClass.WARRIOR)
					{
						Console.WriteLine("Aggro Deck");
						agent = new PredatorMCTSAgent(scoring,
							new MCTSParameters
							{
								SimulationTime = simulationTime,
								AggregationTime = 100,
								RolloutDepth = 5,
								UCTConstant = 9000
							},
							new PredictionParameters
							{
								File = Environment.CurrentDirectory + @"\src\Bigramms\bigramm_1-2017-12-2016.json.gz",
								DecayFactor = 1,
								CardCount = 10,
								StepWidth = 2,
								DeckCount = 1,
								SetCount = 3,
								LeafCount = 5,
								SimulationDepth = 1,
								OverallLeafCount = 5
							});
					}
					else if (cardClass == CardClass.SHAMAN)
					{
						agent = new PredatorMCTSAgent(scoring,
							new MCTSParameters
							{
								SimulationTime = simulationTime,
								AggregationTime = 100,
								RolloutDepth = 5,
								UCTConstant = 9000
							},
							new PredictionParameters
							{
								File = Environment.CurrentDirectory + @"\src\Bigramms\bigramm_1-2017-12-2016.json.gz",
								DecayFactor = 1,
								CardCount = 10,
								StepWidth = 2,
								DeckCount = 1,
								SetCount = 3,
								LeafCount = 5,
								SimulationDepth = 3,
								OverallLeafCount = 5
							});
					}
					else if (cardClass == CardClass.MAGE)
					{
						agent = new PredatorMCTSAgent(scoring,
							new MCTSParameters
							{
								SimulationTime = simulationTime,
								AggregationTime = 100,
								RolloutDepth = 5,
								UCTConstant = 9000
							},
							new PredictionParameters
							{
								File = Environment.CurrentDirectory + @"\src\Bigramms\bigramm_1-2017-12-2016.json.gz",
								DecayFactor = 1,
								CardCount = 10,
								StepWidth = 2,
								DeckCount = 1,
								SetCount = 3,
								LeafCount = 5,
								SimulationDepth = 5,
								OverallLeafCount = 5
							});
					}
					else if (cardClass == CardClass.WARLOCK)
					{
						agent = new PredatorMCTSAgent(scoring,
							new MCTSParameters
							{
								SimulationTime = simulationTime,
								AggregationTime = 100,
								RolloutDepth = 5,
								UCTConstant = 9000
							},
							new PredictionParameters
							{
								File = Environment.CurrentDirectory + @"\src\Bigramms\bigramm_3-2018-10-2017.json.gz",
								DecayFactor = 1,
								CardCount = 10,
								StepWidth = 2,
								DeckCount = 1,
								SetCount = 3,
								LeafCount = 5,
								SimulationDepth = 1,
								OverallLeafCount = 5
							});
					}
					break;
			};
			return agent;
		}

		public class AgentConfig
		{
			public IScore Scoring
			{
				get; protected set;
			}

			public int MaxDepth
			{
				get; protected set;
			}

			public int MaxWidth
			{
				get; protected set;
			}

			public MCTSParameters MCTSParams
			{
				get; protected set;
			}

			public PredictionParameters PredictionParams
			{
				get; protected set;
			}

			public class Builder
			{
				protected IScore _scoring;

				protected int _maxDepth;

				protected int _maxWidth;

				protected MCTSParameters _mctsParams;

				protected PredictionParameters _predictionParams;

				public Builder AddScoring(IScore scoring)
				{
					_scoring = scoring;
					return this;
				}

				public Builder AddDepth(int maxDepth)
				{
					_maxDepth = maxDepth;
					return this;
				}

				public Builder AddWidth(int maxWidth)
				{
					_maxWidth = maxWidth;
					return this;
				}

				public Builder AddMCTSParameters(MCTSParameters mctsParams)
				{
					_mctsParams = mctsParams;
					return this;
				}

				public Builder AddPredictionParameters(PredictionParameters predictionParams)
				{
					_predictionParams = predictionParams;
					return this;
				}

				public AgentConfig Build()
				{
					return new AgentConfig
					{
						Scoring = _scoring,
						MaxDepth = _maxDepth,
						MaxWidth = _maxWidth,
						MCTSParams = _mctsParams,
						PredictionParams = _predictionParams
					};
				}
			}
		}
    }
}

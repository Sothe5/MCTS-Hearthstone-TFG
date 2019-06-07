using SabberStoneCore.Config;
using SabberStoneCore.Enums;
using SabberStoneCoreAi.Agent;
using SabberStoneCoreAi.POGame;
using SabberStoneCoreAi.src.Agent.AlvaroMCTS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SabberStoneCoreAi.Tournament
{
	class Tournaments
	{
		struct deck
		{
			public List<SabberStoneCore.Model.Card> deckList;
			public CardClass heroClass;

			public deck(List<SabberStoneCore.Model.Card> deckList, CardClass heroClass)
			{
				this.deckList = deckList;
				this.heroClass = heroClass;
			}
		}

		private List<AbstractAgent> agents;
		private List<deck> decks;

		public Tournaments(bool stage1, bool stage2, bool stage3, bool stage4, int nLeagues, List<AbstractAgent> agents, List<List<SabberStoneCore.Model.Card>> decksAsList,
			List<CardClass> cardClassList)
		{
			this.decks = new List<deck>();
			for(int i = 0; i < decksAsList.Count; i++)
			{
				this.decks.Add(new deck(decksAsList[i], cardClassList[i]));
			}
			this.agents = agents;

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			if (stage1)
			{
				OneDeckStage(nLeagues, 0);
			}
			if (stage2)
			{
				OneDeckStage(nLeagues, 1);
			}
			if (stage3)
			{
				OneDeckStage(nLeagues, 2);
			}
			if (stage4)
			{
				Stage4(nLeagues);
			}

			Console.WriteLine("\n"+((stopwatch.ElapsedMilliseconds/1000.0)/60.0));
			stopwatch.Stop();
			Console.ReadLine();
		}

		private void OneDeckStage(int nLeagues, int deckId)
		{
			Console.WriteLine("Stage " + (deckId + 1) + " start:");

			int MCTSTotalWins = 0;
			int GreedyTotalWins = 0;
			int TycheTotalWins = 0;

			for (int i = 0; i < nLeagues; i++)
			{
				int MCTSWinsLeague = 0;
				int GreedyWinsLeague = 0;
				int TycheWinsLeague = 0;
				foreach (AbstractAgent agent in agents)
				{
					foreach (AbstractAgent agent2 in agents)
					{
						if (!agent.Equals(agent2))
						{
							GameConfig gameConfig = new GameConfig
							{
								StartPlayer = 1,
								Player1HeroClass = decks[deckId].heroClass,
								Player2HeroClass = decks[deckId].heroClass,
								FillDecks = false,
								Logging = false,
								FillDecksPredictably = true
							};

							gameConfig.Player1Deck = decks[deckId].deckList;
							gameConfig.Player2Deck = decks[deckId].deckList;

							var gameHandler = new POGameHandler(gameConfig, agent, agent2, debug: false);

							Console.WriteLine("PlayGame " + getAgentName(agent.ToString()) + " vs " + getAgentName(agent2.ToString()) +
								" using " + getDeckName(decks[deckId].deckList));

							gameHandler.PlayGames(1);
							GameStats gameStats = gameHandler.getGameStats();

							if ((gameStats.PlayerA_Wins == 1 && getAgentName(agent.ToString()) == "MCTS") ||
								(gameStats.PlayerA_Wins == 0 && getAgentName(agent2.ToString()) == "MCTS"))
								MCTSWinsLeague++;
							if ((gameStats.PlayerA_Wins == 1 && getAgentName(agent.ToString()) == "Greedy") ||
								(gameStats.PlayerA_Wins == 0 && getAgentName(agent2.ToString()) == "Greedy"))
								GreedyWinsLeague++;
							if ((gameStats.PlayerA_Wins == 1 && getAgentName(agent.ToString()) == "Tyche") ||
								(gameStats.PlayerA_Wins == 0 && getAgentName(agent2.ToString()) == "Tyche"))
								TycheWinsLeague++;


							Console.WriteLine(gameStats.PlayerA_Wins + " vs " + gameStats.PlayerB_Wins);
							Console.WriteLine();
						}
					}
				}
				Console.WriteLine("..................");
				Console.WriteLine("   League Ended  ");
				Console.WriteLine("..................");

				int leagueGamesPlayed = MCTSWinsLeague + GreedyWinsLeague + TycheWinsLeague;
				Console.WriteLine("Total games played in this league " + leagueGamesPlayed + "\n");

				MCTSTotalWins += MCTSWinsLeague;
				GreedyTotalWins += GreedyWinsLeague;
				TycheTotalWins += TycheWinsLeague;

				Console.WriteLine("League " + i + " MCTS win percentage " + +MCTSWinsLeague / (float)leagueGamesPlayed);
				Console.WriteLine("League " + i + " Greedy win percentage " + GreedyWinsLeague / (float)leagueGamesPlayed);
				Console.WriteLine("League " + i + " Tyche win percentage " + TycheWinsLeague / (float)leagueGamesPlayed);

			}

			Console.WriteLine("=====================");
			Console.WriteLine("=====================");
			int totalGamesPlayed = MCTSTotalWins + GreedyTotalWins + TycheTotalWins;

			Console.WriteLine("Number of games played: " + totalGamesPlayed);

			Console.WriteLine("MCTS win percentage " + MCTSTotalWins / (float)totalGamesPlayed);
			Console.WriteLine("Greedy win percentage " + GreedyTotalWins / (float)totalGamesPlayed);
			Console.WriteLine("Tyche win percentage " + TycheTotalWins / (float)totalGamesPlayed);
		}

		private void Stage4(int nLeagues)
		{
			Console.WriteLine("Stage " + 4 + " start:");
			int MCTSTotalWins = 0;
			int GreedyTotalWins = 0;
			int TycheTotalWins = 0;

			for (int i = 0; i < nLeagues; i++)
			{
				int MCTSWinsLeague = 0;
				int GreedyWinsLeague = 0;
				int TycheWinsLeague = 0;

				foreach (AbstractAgent agent in agents)
				{
					foreach (AbstractAgent agent2 in agents)
					{
						foreach (deck deck in decks)
						{
							foreach (deck deck2 in decks)
							{
								if (!deck.Equals(deck2) && !agent.Equals(agent2))
								{
									GameConfig gameConfig = new GameConfig
									{
										StartPlayer = 1,
										Player1HeroClass = deck.heroClass,
										Player2HeroClass = deck2.heroClass,
										FillDecks = false,
										Logging = false,
										FillDecksPredictably = true
									};

									gameConfig.Player1Deck = deck.deckList;
									gameConfig.Player2Deck = deck2.deckList;

									var gameHandler = new POGameHandler(gameConfig, agent, agent2, debug: true);

									Console.WriteLine("PlayGame " + getAgentName(agent.ToString()) + " using " + getDeckName(deck.deckList)+ " vs " + getAgentName(agent2.ToString()) +
										" using " + getDeckName(deck2.deckList));

									gameHandler.PlayGames(1);
									GameStats gameStats = gameHandler.getGameStats();

									if ((gameStats.PlayerA_Wins == 1 && getAgentName(agent.ToString()) == "MCTS") ||
										(gameStats.PlayerA_Wins == 0 && getAgentName(agent2.ToString()) == "MCTS"))
										MCTSWinsLeague++;
									if ((gameStats.PlayerA_Wins == 1 && getAgentName(agent.ToString()) == "Greedy") ||
										(gameStats.PlayerA_Wins == 0 && getAgentName(agent2.ToString()) == "Greedy"))
										GreedyWinsLeague++;
									if ((gameStats.PlayerA_Wins == 1 && getAgentName(agent.ToString()) == "Tyche") ||
										(gameStats.PlayerA_Wins == 0 && getAgentName(agent2.ToString()) == "Tyche"))
										TycheWinsLeague++;


									Console.WriteLine(gameStats.PlayerA_Wins + " vs " + gameStats.PlayerB_Wins);
									Console.WriteLine();
								}
							}
						}
					}
				}
				Console.WriteLine("..................");
				Console.WriteLine("   League Ended  ");
				Console.WriteLine("..................");

				int leagueGamesPlayed = MCTSWinsLeague + GreedyWinsLeague + TycheWinsLeague;
				Console.WriteLine("Total games played in this league " + leagueGamesPlayed + "\n");

				MCTSTotalWins += MCTSWinsLeague;
				GreedyTotalWins += GreedyWinsLeague;
				TycheTotalWins += TycheWinsLeague;

				Console.WriteLine("League " + i + " MCTS win percentage " + +MCTSWinsLeague / (float)leagueGamesPlayed);
				Console.WriteLine("League " + i + " Greedy win percentage " + GreedyWinsLeague / (float)leagueGamesPlayed);
				Console.WriteLine("League " + i + " Tyche win percentage " + TycheWinsLeague / (float)leagueGamesPlayed);

			}
			Console.WriteLine("=====================");
			Console.WriteLine("=====================");
			int totalGamesPlayed = MCTSTotalWins + GreedyTotalWins + TycheTotalWins;

			Console.WriteLine("Number of games played: " + totalGamesPlayed);

			Console.WriteLine("MCTS win percentage " + MCTSTotalWins / (float)totalGamesPlayed);
			Console.WriteLine("Greedy win percentage " + GreedyTotalWins / (float)totalGamesPlayed);
			Console.WriteLine("Tyche win percentage " + TycheTotalWins / (float)totalGamesPlayed);
		}

		private string getAgentName(string agent)
		{
			if (agent.ToString() == "SabberStoneCoreAi.Agent.AlvaroAgent")
			{
				return "MCTS";
			} else if (agent.ToString() == "SabberStoneCoreAi.Agent.ParametricGreedyAgent")
			{
				return "Greedy";
			} else if (agent.ToString() == "SabberStoneCoreAi.Agent.TycheAgentCompetition") {
				return "Tyche";
			} else
			{
				return agent;
			}
		}

		private string getDeckName(List<SabberStoneCore.Model.Card> deckList)
		{
			if (deckList.Equals(decks[0].deckList))
			{
				return "AggroShaman";
			} else if (deckList.Equals(decks[1].deckList))
			{
				return "MidRangeHunter";
			} else if (deckList.Equals(decks[2].deckList))
			{
				return "ControlWarrior";
			} else
			{
				return "deckName";
			}
		}
	}
}

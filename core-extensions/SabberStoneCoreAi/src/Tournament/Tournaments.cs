using SabberStoneCore.Config;
using SabberStoneCore.Enums;
using SabberStoneCoreAi.Agent;
using SabberStoneCoreAi.POGame;
using SabberStoneCoreAi.src.Tournament;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

	
		private List<AgentTournament> agents;
		private List<deck> decks;
		private StringBuilder writer;
		private StringBuilder resultsLeague;
		private StringBuilder resultsStage;


		public Tournaments(bool stage1, bool stage2, bool stage3, bool stage4, int nLeagues, List<AbstractAgent> agentAsList, List<List<SabberStoneCore.Model.Card>> decksAsList,
			List<CardClass> cardClassList)
		{
			this.decks = new List<deck>();
			for (int i = 0; i < decksAsList.Count; i++)
			{
				this.decks.Add(new deck(decksAsList[i], cardClassList[i]));
			}
			this.agents = new List<AgentTournament>();
			int id = 0;
			foreach (AbstractAgent a in agentAsList)
			{
				this.agents.Add(new AgentTournament(a,id));
				id++;
			}

			writer = new StringBuilder();
			resultsLeague = new StringBuilder();
			resultsStage = new StringBuilder();

			writer.AppendLine("sep=,");
			writer.AppendLine("Stage,League,Agent1,Deck1,Agent2,Deck2,Victory1,Turns");
			resultsLeague.AppendLine();
			resultsLeague.Append("Stage,League,");
			resultsStage.Append("Stage,");
			foreach (AgentTournament a in agents)
			{
				resultsLeague.Append(getAgentName(a)+ "VictoryLeague,");
				resultsStage.Append(getAgentName(a)+ "VictoryStage,");
			}
			resultsLeague.Append("TotalGamesLeague,");
			resultsStage.Append("TotalGamesStage,");
			foreach (AgentTournament a in agents)
			{
				resultsLeague.Append(getAgentName(a) + "TurnsLeague,");
				resultsStage.Append(getAgentName(a) + "TurnsStage,");
			}
			resultsLeague.AppendLine();
			resultsStage.AppendLine();		

			int totalGamesPlayed = 0;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			if (stage1)
			{
				totalGamesPlayed += OneDeckStage(nLeagues, 0);
			}
			if (stage2)
			{
				totalGamesPlayed += OneDeckStage(nLeagues, 1);
			}
			if (stage3)
			{
				totalGamesPlayed += OneDeckStage(nLeagues, 2);
			}
			if (stage4)
			{
				totalGamesPlayed += Stage4(nLeagues);
			}
			resultsStage.AppendLine();

			foreach (AgentTournament a in agents)
			{
				resultsStage.Append(getAgentName(a) + "TotalWins,");
			}
			resultsStage.Append("TotalGamesPlayed,");
			foreach (AgentTournament a in agents)
			{
				resultsStage.Append(getAgentName(a) + "Turns,");
			}
			resultsStage.AppendLine();

			foreach (AgentTournament a in agents)
			{
				resultsStage.Append(a.finalWins + ",");
			}
			resultsStage.Append(totalGamesPlayed+",");
			foreach (AgentTournament a in agents)
			{
				resultsStage.Append(a.finalTurns + ",");
			}
			resultsStage.AppendLine();

			writer.AppendLine(resultsLeague.ToString());
			writer.AppendLine(resultsStage.ToString());
			File.WriteAllText("C:/Users/User/Desktop/Results.csv", writer.ToString());
			Console.WriteLine("\n"+((stopwatch.ElapsedMilliseconds/1000.0)/60.0));
			foreach (AgentTournament a in agents)
			{
				Console.WriteLine("Final wins "+getAgentName(a)+": " +a.finalWins);
			}
			stopwatch.Stop();
			Console.ReadLine();
		}

		private int OneDeckStage(int nLeagues, int deckId)
		{
			Console.WriteLine("Stage " + (deckId + 1) + " start:");
			for (int i = 0; i < nLeagues; i++)
			{
				foreach (AgentTournament agent in agents)
				{
					foreach (AgentTournament agent2 in agents)
					{
						if (!agent.agentAI.Equals(agent2.agentAI))
						{
							GameConfig gameConfig = new GameConfig
							{
								StartPlayer = 1,
								Player1HeroClass = decks[deckId].heroClass,
								Player2HeroClass = decks[deckId].heroClass,
								FillDecks = false,
								Logging = false,
								FillDecksPredictably = true,
								History = false
							};

							gameConfig.Player1Deck = decks[deckId].deckList;
							gameConfig.Player2Deck = decks[deckId].deckList;

							var gameHandler = new POGameHandler(gameConfig, agent.agentAI, agent2.agentAI, debug: false);

							Console.WriteLine("PlayGame " + getAgentName(agent) + " vs " + getAgentName(agent2) +
								" using " + getDeckName(decks[deckId].deckList));

							gameHandler.PlayGames(1);
							GameStats gameStats = gameHandler.getGameStats();

							if (gameStats.PlayerA_Wins == 1)
							{
								agent.leagueWins++;
							} else
							{
								agent2.leagueWins++;
							}
							agent.leagueTurns += gameStats.Turns;
							agent2.leagueTurns += gameStats.Turns;
							
							writer.AppendLine((deckId + 1)+","+i+","+getAgentName(agent)+","+ getDeckName(decks[deckId].deckList)+","+
								getAgentName(agent2)+","+getDeckName(decks[deckId].deckList)+","+ gameStats.PlayerA_Wins+","+gameStats.Turns);
							Console.WriteLine(gameStats.PlayerA_Wins + " vs " + gameStats.PlayerB_Wins);
							Console.WriteLine();
						}
					}
				}
				Console.WriteLine("..................");
				Console.WriteLine("   League Ended  ");
				Console.WriteLine("..................");
	
				int leagueGamesPlayed = 0;

				foreach(AgentTournament a in agents)
				{
					leagueGamesPlayed += a.leagueWins;
					a.stageWins += a.leagueWins;
					a.stageTurns += a.leagueTurns;
				}

				resultsLeague.Append((deckId + 1) + "," + i + ",");
				foreach (AgentTournament a in agents)
				{
					resultsLeague.Append(a.leagueWins + ",");
				}
				resultsLeague.Append(leagueGamesPlayed + ",");
				foreach (AgentTournament a in agents)
				{
					resultsLeague.Append(a.leagueTurns + ",");
				}
				resultsLeague.AppendLine();

				Console.WriteLine("Total games played in this league " + leagueGamesPlayed + "\n");

				foreach(AgentTournament a in agents)
				{
					Console.WriteLine("League " + i + getAgentName(a)+" win percentage " + a.leagueWins / (float)leagueGamesPlayed);
					a.leagueWins = 0;
					a.leagueTurns = 0;
				}

				

			}

			Console.WriteLine("=====================");
			Console.WriteLine("=====================");
			int totalGamesPlayed = 0;

			foreach (AgentTournament a in agents)
			{
				totalGamesPlayed += a.stageWins;
				a.finalWins += a.stageWins;
				a.finalTurns += a.stageTurns;
			}

			resultsStage.Append((deckId + 1) + ",");
			foreach (AgentTournament a in agents)
			{
				resultsStage.Append(a.stageWins + ",");
			}
			resultsStage.Append(totalGamesPlayed + ",");
			foreach (AgentTournament a in agents)
			{
				resultsStage.Append(a.stageTurns + ",");
			}
			resultsStage.AppendLine();

			Console.WriteLine("Number of games played: " + totalGamesPlayed);

			foreach (AgentTournament a in agents)
			{
				Console.WriteLine(getAgentName(a)+" win percentage " + a.stageWins / (float)totalGamesPlayed);
				a.stageWins = 0;
				a.stageTurns = 0;
			}

		
			return totalGamesPlayed;
		}

		private int Stage4(int nLeagues)
		{
			Console.WriteLine("Stage " + 4 + " start:");


			for (int i = 0; i < nLeagues; i++)
			{
				foreach (AgentTournament agent in agents)
				{
					foreach (AgentTournament agent2 in agents)
					{
						foreach (deck deck in decks)
						{
							foreach (deck deck2 in decks)
							{
								if (!deck.Equals(deck2) && !agent.agentAI.Equals(agent2.agentAI))
								{
									GameConfig gameConfig = new GameConfig
									{
										StartPlayer = 1,
										Player1HeroClass = deck.heroClass,
										Player2HeroClass = deck2.heroClass,
										FillDecks = false,
										Logging = false,
										FillDecksPredictably = true,
										History = false
									};

									gameConfig.Player1Deck = deck.deckList;
									gameConfig.Player2Deck = deck2.deckList;

									var gameHandler = new POGameHandler(gameConfig, agent.agentAI, agent2.agentAI, debug: false);

									Console.WriteLine("PlayGame " + getAgentName(agent) + " using " + getDeckName(deck.deckList)+ " vs " + getAgentName(agent2) +
										" using " + getDeckName(deck2.deckList));

									gameHandler.PlayGames(1);
									GameStats gameStats = gameHandler.getGameStats();

									if (gameStats.PlayerA_Wins == 1)
									{
										agent.leagueWins++;
									} else
									{
										agent2.leagueWins++;
									}
									agent.leagueTurns += gameStats.Turns;
									agent2.leagueTurns += gameStats.Turns;

									writer.AppendLine(4 + "," + i + "," + getAgentName(agent) + "," + getDeckName(deck.deckList) + "," +
										getAgentName(agent2) + "," + getDeckName(deck2.deckList) + "," + gameStats.PlayerA_Wins + "," + gameStats.Turns);
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

				int leagueGamesPlayed = 0;

				foreach (AgentTournament a in agents)
				{
					leagueGamesPlayed += a.leagueWins;
					a.stageWins += a.leagueWins;
					a.stageTurns += a.leagueTurns;
				}

				resultsLeague.Append(4 + "," + i + ",");
				foreach (AgentTournament a in agents)
				{
					resultsLeague.Append(a.leagueWins + ",");
				}
				resultsLeague.Append(leagueGamesPlayed + ",");
				foreach (AgentTournament a in agents)
				{
					resultsLeague.Append(a.leagueTurns + ",");
				}
				resultsLeague.AppendLine();

				Console.WriteLine("Total games played in this league " + leagueGamesPlayed + "\n");

				foreach (AgentTournament a in agents)
				{
					Console.WriteLine("League " + i + getAgentName(a) + " win percentage " + a.leagueWins / (float)leagueGamesPlayed);
					a.leagueWins = 0;
					a.leagueTurns = 0;
				}

			
			}
			Console.WriteLine("=====================");
			Console.WriteLine("=====================");

			int totalGamesPlayed = 0;

			foreach (AgentTournament a in agents)
			{
				totalGamesPlayed += a.stageWins;
				a.finalWins += a.stageWins;
				a.finalTurns += a.stageTurns;
			}

			resultsStage.Append(4 + ",");
			foreach (AgentTournament a in agents)
			{
				resultsStage.Append(a.stageWins + ",");
			}
			resultsStage.Append(totalGamesPlayed + ",");
			foreach (AgentTournament a in agents)
			{
				resultsStage.Append(a.stageTurns + ",");
			}
			resultsStage.AppendLine();

			Console.WriteLine("Number of games played: " + totalGamesPlayed);

			foreach (AgentTournament a in agents)
			{
				Console.WriteLine(getAgentName(a) + " win percentage " + a.stageWins / (float)totalGamesPlayed);
				a.stageWins = 0;
				a.stageTurns = 0;
			}

			
		
			return totalGamesPlayed;
		}

		private string getAgentName(AgentTournament agent)
		{
			if (agent.agentId == 0)
			{
				return "MCTS1";
			} else if (agent.agentId == 1)
			{
				return "Greedy";
			} else if (agent.agentId == 2) {
				return "Tyche";
			} else if (agent.agentId == 3)
			{
				return "MCTS2";
			} else if (agent.agentId == 4)
			{
				return "MCTS3";
			} else
			{
				return agent.agentAI.ToString();
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

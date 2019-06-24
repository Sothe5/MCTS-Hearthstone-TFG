using System;
using SabberStoneCore.Config;
using SabberStoneCore.Enums;
using SabberStoneCoreAi.POGame;
using SabberStoneCoreAi.Agent.ExampleAgents;
using SabberStoneCoreAi.Agent;
using SabberStoneCoreAi.Tournament;
using SabberStoneCoreAi.src.Agent.AlvaroMCTS;
using System.Collections.Generic;

namespace SabberStoneCoreAi
{
	internal class Program
	{

		private static void Main(string[] args)
		{
			AbstractAgent MCTSAgent = new AlvaroAgent(2, 1000, "MaxVictoriesOverVisited", 10, "UCB1", 1, "GreedyPolicy", 1.0, "LinearEstimation", 1,
			   "0.569460712743", "0.958111820041", "0.0689492467097", "0.0", "0.843573987219", "0.700225423688", "0.907680353441", "0.0",
			   "0.993682660717", "1.0", "0.640753949511", "0.992872512338", "0.92870036875", "0.168100484322", "0.870080107454",
			   "0.0", "0.42897762808", "1.0", "0.0", "0.583884736646", "0.0", 0.7f, 0.4f, 0.4f, 0.9f, 0.4f, 0.01f, 0.02f, 0.4f, 0.3f, 0.8f, 0.5f, 0.4f, 0.5f);

			AbstractAgent MCTSAgent2 = new AlvaroAgent(2, 1000, "MaxVictoriesOverVisited", 0.1f, "UCB1", 1, "GreedyPolicy", 1.0, "GradualEstimation", 1,
				"0.569460712743", "0.958111820041", "0.0689492467097", "0.0", "0.843573987219", "0.700225423688", "0.907680353441", "0.0",
				"0.993682660717", "1.0", "0.640753949511", "0.992872512338", "0.92870036875", "0.168100484322", "0.870080107454",
				"0.0", "0.42897762808", "1.0", "0.0", "0.583884736646", "0.0", 0.7f, 0.4f, 4.5f, 0.9f, 2.5f, 0.08f, 0.02f, 0.4f, 0.3f, 0.8f, 0.5f, 0.4f, 0.5f);

			AbstractAgent MCTSAgent3 = new AlvaroAgent(2, 1000, "MaxVictoriesOverVisited", 1, "UCB1", 3, "RandomPolicy", 1.0, "LinearEstimation", 1,
				"0.569460712743", "0.958111820041", "0.0689492467097", "0.0", "0.843573987219", "0.700225423688", "0.907680353441", "0.0",
				"0.993682660717", "1.0", "0.640753949511", "0.992872512338", "0.92870036875", "0.168100484322", "0.870080107454",
				"0.0", "0.42897762808", "1.0", "0.0", "0.583884736646", "0.0", 0.7f, 0.4f, 0.4f, 0.9f, 0.4f, 0.01f, 0.02f, 0.4f, 0.3f, 0.8f, 0.5f, 0.4f, 0.5f);
		
			AbstractAgent MCTSAgent4 = new AlvaroAgent(2, 1000, "MaxVictories", 0.1, "UCB1Heuristic", 1, "GreedyPolicy", 1.0, "LinearEstimation", 1,
				"0.569460712743", "0.958111820041", "0.0689492467097", "0.0", "0.843573987219", "0.700225423688", "0.907680353441", "0.0",
				"0.993682660717", "1.0", "0.640753949511", "0.992872512338", "0.92870036875", "0.168100484322", "0.870080107454",
				"0.0", "0.42897762808", "1.0", "0.0", "0.583884736646", "0.0", 0.7f, 0.4f, 0.4f, 0.9f, 0.4f, 0.01f, 0.02f, 0.4f, 0.3f, 0.8f, 0.5f, 0.4f, 0.5f);

			AbstractAgent MCTSAgent5 = new AlvaroAgent(2, 1000, "MaxVictoriesOverVisited", 0.35f, "UCB1Heuristic", 10, "RandomPolicy", 1.0, "LinearEstimation", 1,
				"0.569460712743", "0.958111820041", "0.0689492467097", "0.0", "0.843573987219", "0.700225423688", "0.907680353441", "0.0",
				"0.993682660717", "1.0", "0.640753949511", "0.992872512338", "0.92870036875", "0.168100484322", "0.870080107454",
				"0.0", "0.42897762808", "1.0", "0.0", "0.583884736646", "0.0", 0.7f, 0.4f, 0.4f, 0.9f, 0.4f, 0.01f, 0.02f, 0.4f, 0.3f, 0.8f, 0.5f, 0.4f, 0.5f);

			AbstractAgent GreedyAgent = new ParametricGreedyAgent("0.569460712743#0.958111820041#0.0689492467097#0.0#0.843573987219#0.700225423688#0.907680353441#0.0#0.993682660717#" +
				"1.0#0.640753949511#0.992872512338#0.92870036875#0.168100484322#0.870080107454#0.0#0.42897762808#1.0#0.0#0.583884736646#0.0");
			AbstractAgent TycheAgent = new TycheAgentCompetition(1f);

			List<AbstractAgent> agents = new List<AbstractAgent>{ GreedyAgent, TycheAgent, MCTSAgent, MCTSAgent2, MCTSAgent3, MCTSAgent4, MCTSAgent5 };

			DeckManager manager = new DeckManager();
			List<List<SabberStoneCore.Model.Card>> decks = new List<List<SabberStoneCore.Model.Card>> { manager.AggroShaman, manager.MidRangeHunter, manager.ControlWarrior };
			List<CardClass> cardClassList = new List<CardClass> { CardClass.SHAMAN, CardClass.HUNTER, CardClass.WARRIOR };

			Tournaments tournament = new Tournaments(true,true,true,true,13,agents,decks,cardClassList);
		}
	}
}

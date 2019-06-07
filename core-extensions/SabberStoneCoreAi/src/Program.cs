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
			Console.WriteLine("Setup gameConfig");

			GameConfig gameConfig = new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.SHAMAN,
				Player2HeroClass = CardClass.SHAMAN,
				FillDecks = false,
				Logging = false,
				FillDecksPredictably = true,
				History = false
			};
		
			DeckManager manager = new DeckManager();
			gameConfig.Player1Deck = manager.AggroShaman;
			gameConfig.Player2Deck = manager.AggroShaman;
				
			Console.WriteLine("Setup POGameHandler");

			/*
			 *2 1000 MaxVictories 10 UCB1Heuristic 10 GreedyPolicy 1.0 BaseEstimation 1 0.569460712743 0.958111820041 0.0689492467097
			 * 0.0 0.843573987219 0.700225423688 0.907680353441 0.0 0.993682660717 1.0 0.640753949511 0.992872512338 0.92870036875 0.168100484322
			 * 0.870080107454 0.0 0.42897762808 1.0 0.0 0.583884736646 0.0 0.9 0.3 0.15 0.9 0.3 0.05 0.3 0.7 0.8 0.5 0.4 0.5
			 * 
			 * Argument of the constructor
			 * 
				2,1000,"MaxVictories",10,"UCB1Heuristic", 10, "GreedyPolicy", 0.7, "BaseEstimation", 1, "0.569460712743","0.958111820041","0.0689492467097","0.0","0.843573987219",
				"0.700225423688","0.907680353441","0.0","0.993682660717","1.0","0.640753949511","0.992872512338","0.92870036875","0.168100484322","0.870080107454",
													"0.0","0.42897762808","1.0","0.0","0.583884736646","0.0", 0.9f, 0.3f, 0.15f, 0.9f, 0.3f, 0.05f, 0.3f, 0.7f, 0.8f, 0.5f, 0.4f, 0.5f
			 */

			/*
			 * Argument style if used in Visual Studio environment
			 * 
			 * 2 1000 MaxVictories 1.5 UCB1Heuristic 10 GreedyPolicy 0.7 BaseEstimation 2 0.569460712743 0.958111820041 0.0689492467097 0.0 0.843573987219 0.700225423688 0.907680353441 0.0 0.993682660717 1.0 0.640753949511 0.992872512338 0.92870036875 0.168100484322 0.870080107454 0.0 0.42897762808 1.0 0.0 0.583884736646 0.0 0.9 0.3 0.15 0.9 0.3 0.05 0.3 0.7 0.8 0.5 0.4 0.5
			 */


			AbstractAgent player1 = new AlvaroAgent(2, 1000, "MaxVictoriesOverVisited", 10, "UCB1Heuristic", 10, "GreedyPolicy", 0.7, "BaseEstimation", 1,
				"0.569460712743", "0.958111820041", "0.0689492467097", "0.0", "0.843573987219", "0.700225423688", "0.907680353441", "0.0",
				"0.993682660717", "1.0", "0.640753949511", "0.992872512338", "0.92870036875", "0.168100484322", "0.870080107454",
				"0.0", "0.42897762808", "1.0", "0.0", "0.583884736646", "0.0", 0.4f, 0.15f, 0.15f, 0.9f, 0.3f, 0.05f, 0.3f, 0.7f, 0.8f, 0.5f, 0.4f, 0.5f);

			AbstractAgent player2 = new ParametricGreedyAgent("0.569460712743#0.958111820041#0.0689492467097#0.0#0.843573987219#0.700225423688#0.907680353441#0.0#0.993682660717#" +
				"1.0#0.640753949511#0.992872512338#0.92870036875#0.168100484322#0.870080107454#0.0#0.42897762808#1.0#0.0#0.583884736646#0.0");
			//AbstractAgent player2 = new TycheAgentCompetition(1.0f);
			
			var gameHandler = new POGameHandler(gameConfig, player1, player2, debug:true);

			Console.WriteLine("PlayGame");
			
			gameHandler.PlayGames(1);
			GameStats gameStats = gameHandler.getGameStats();

			gameStats.printResults();


			Console.WriteLine("Test successful");
			Console.ReadLine();

			// ==========================================================================================
			// ==========================================================================================
			// ==========================================================================================
	/*		AbstractAgent MCTSAgent = new AlvaroAgent(2, 10, "MaxVictoriesOverVisited", 10, "UCB1Heuristic", 10, "GreedyPolicy", 1.0, "BaseEstimation", 1,
				"0.569460712743", "0.958111820041", "0.0689492467097", "0.0", "0.843573987219", "0.700225423688", "0.907680353441", "0.0",
				"0.993682660717", "1.0", "0.640753949511", "0.992872512338", "0.92870036875", "0.168100484322", "0.870080107454",
				"0.0", "0.42897762808", "1.0", "0.0", "0.583884736646", "0.0", 0.9f, 0.3f, 0.15f, 0.9f, 0.3f, 0.05f, 0.3f, 0.7f, 0.8f, 0.5f, 0.4f, 0.5f);

			AbstractAgent GreedyAgent = new ParametricGreedyAgent("0.569460712743#0.958111820041#0.0689492467097#0.0#0.843573987219#0.700225423688#0.907680353441#0.0#0.993682660717#" +
				"1.0#0.640753949511#0.992872512338#0.92870036875#0.168100484322#0.870080107454#0.0#0.42897762808#1.0#0.0#0.583884736646#0.0");
			AbstractAgent TycheAgent = new TycheAgentCompetition(0.01f);

			List<AbstractAgent> agents = new List<AbstractAgent>{ MCTSAgent, GreedyAgent, TycheAgent };

			DeckManager manager = new DeckManager();
			List<List<SabberStoneCore.Model.Card>> decks = new List<List<SabberStoneCore.Model.Card>> { manager.AggroShaman, manager.MidRangeHunter, manager.ControlWarrior };
			List<CardClass> cardClassList = new List<CardClass> { CardClass.SHAMAN, CardClass.HUNTER, CardClass.WARRIOR };

			Tournaments tournament = new Tournaments(true,false,false,false,10,agents,decks,cardClassList);*/
		}
	}
}


/*
 * -----------------------------
 * -------- PARAMETROS ---------
 * -----------------------------
 * 
 * 	EXPLORE_CONSTANT:
 *		Cuanta importancia le da a explorar nuevos caminos sobre el camino que parece más prometedor. [0 - 2]
 *		
 *	MAX_TIME:
 *		Tiempo que se queda ejecutando el bucle principal hasta que toma una decision.  [1000 - 20000]
 *
 *	Formas de elegir la accion final:
 *		[MaxVictories, MaxVisited, MaxVictoriesAndVisited, MaxUCB]
 *
 *	TREE_POLICY:
 *		[UCB1, UCB1Heuristic]
 *
 *	SCORE_IMPORTANCE Factor de importancia de la heuristica sobre el UCB:
 *		[0.25, 0.5, 0.75, 1, 1.25, 1.50, 1.75, 2]
 *
 *	Profundidad como parametro en la seleccion:
 *		Hacer un trackeo de la profundida de cada nodo cuando se crea y que expansion tenga una clausula de que si llega a una cierta profundidad
 *		ya no expanda más y solo simule desde ese punto.  [1 - 7]
 *			- a partir de 6 casi nunca o nunca afecta este parametro para 10.000 ms.
 *
 *	Profundidad como parametro de cuantos pasos hacer hasta realizar estimacion:
 *		[0 - 2000 ]  Depende de cuantos pasos haya hasta que acabe la partida.
 *
 *  Pesos de los campos de la heuristica:
 *		cuanto importa la vida, cuanto importa nosq etc. en paper lo hacen independientemente con un genetico y despues los añaden aqui ya.
 *		[0.0, 1.0]
 *
 *  SIMULATION_POLICY:
 *		[RandomPolicy, GreedyPolicy]
 *
 *	Numero de hijos que se tienen en cuenta durante la simulacion en cada rama:
 *		Mientras simulas ahora mismo se tienen en cuenta todos los hijos y a esos le haces un random, pues puedes cojer K hijos y solo ha esos le haces
 *		el scoring. Puedes tambien hacerselo a todos. Es un porcentaje.
 *		[0.0, 1.0]
 *		
 * 	Formas de especular:
 *		[BaseEstimation, ValueEstimation]
 *
 *	Numero de Simulaciones desde una misma iteracion:
 *		Permite no tener que volver a seleccionar y expandir para conseguir más datos en cada bajada.
 *		[1, infinito]
 *		
 */


// comentar que el reuse es inviable debido a que los paso de hearthstone no son deterministas entonces al jugar una carta que tenga que hace un efecto
// aleatorio en el arbol se asume que es uno de los posibles pero para el juego uno de los efectos ya si que se ha producido.



/*
 *	=================================================================================
 *	Concepto teorico:
 *		Porque cojones vas a priorizar explorar por donde el jugador 2 tiende a perder, no es realista creer que el jugador 2 va a elegir
 *		la peor opcion. No deberia cambiarse para que fuera justo al reves que se prioricen como exploraciones en las que asumes
 *		que el oponente es bueno y no como ahora que se asume que es malo.???
 *
 *	Ahora mismo se hace de esa manera en parte supongo que asumiendo que como no sabes que va a hacer el oponente te da igual que hacer en
 *	esos puntos.  MAYBE : podria ser un parametro el metodo de seleccion de nodos para cuando es un oponente.
 *	https://www.youtube.com/watch?v=xmImNoDc9Z4&list=WL&index=172  min 29
 *	
 */




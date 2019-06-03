using System;
using SabberStoneCore.Config;
using SabberStoneCore.Enums;
using SabberStoneCoreAi.POGame;
using SabberStoneCoreAi.Agent.ExampleAgents;
using SabberStoneCoreAi.Agent;
using SabberStoneCoreAi.src.Agent.AlvaroMCTS;

namespace SabberStoneCoreAi
{
	internal class Program
	{

		private static void Main(string[] args)
		{
			//Console.WriteLine(args.Length);
			Console.WriteLine("Setup gameConfig");

			GameConfig gameConfig = new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.WARLOCK,
				Player2HeroClass = CardClass.WARLOCK,
				FillDecks = false,
				Logging = false,
				FillDecksPredictably = true
			};
		
			DeckManager manage = new DeckManager();
			gameConfig.Player1Deck = manage.NaxxramasWarlock;
			gameConfig.Player2Deck = manage.NaxxramasWarlock;
				
			Console.WriteLine("Setup POGameHandler");

			/* 
				2,10000,"MaxVictories",1,"UCB1Heuristic",5, 10, "GreedyPolicy", 0.7, "ValueEstimation", 2, "0.569460712743","0.958111820041","0.0689492467097","0.0","0.843573987219",
				"0.700225423688","0.907680353441","0.0","0.993682660717","1.0","0.640753949511","0.992872512338","0.92870036875","0.168100484322","0.870080107454",
													"0.0","0.42897762808","1.0","0.0","0.583884736646","0.0", 0.2f, 0.2f, 0.6f, 0.7f, 0.5f, 0.3f, 0.8f, 0.3f, 0.8f, 0.5f, 0.4f, 0.5f
			 */

			/*
			 * Argument style if used in Visual Studio environment
			 * 2 10000 MaxVictories 1 UCB1Heuristic 5 10 GreedyPolicy 0.7 ValueEstimation 2 0.569460712743 0.958111820041 0.0689492467097 0.0 0.843573987219 0.700225423688 0.907680353441 0.0 0.993682660717 1.0 0.640753949511 0.992872512338 0.92870036875 0.168100484322 0.870080107454 0.0 0.42897762808 1.0 0.0 0.583884736646 0.0 0.2 0.2 0.6 0.7 0.5 0.3 0.8 0.3 0.8 0.5 0.4 0.5
			 */


			AbstractAgent player1 = new AlvaroAgent(Double.Parse(args[0]), Int32.Parse(args[1]), args[2], Double.Parse(args[3]), args[4],
			   Int32.Parse(args[5]), Int32.Parse(args[6]), args[7], Double.Parse(args[8]), args[9], Int32.Parse(args[10]),

			   args[11], args[12], args[13], args[14], args[15], args[16], args[17], args[18], args[19], args[20], args[21], args[22],
			   args[23], args[24], args[25], args[26], args[27], args[28], args[29], args[30], args[31],

			  float.Parse(args[32]), float.Parse(args[33]), float.Parse(args[34]), float.Parse(args[35]), float.Parse(args[36]), float.Parse(args[37]),
			  float.Parse(args[38]), float.Parse(args[39]), float.Parse(args[40]), float.Parse(args[41]), float.Parse(args[42]), float.Parse(args[43]));
			AbstractAgent player2 = new TycheAgentCompetition();
			var gameHandler = new POGameHandler(gameConfig, player1, player2, debug:true);

			Console.WriteLine("PlayGame");

			gameHandler.PlayGames(1);
			GameStats gameStats = gameHandler.getGameStats();

			gameStats.printResults();


			Console.WriteLine("Test successful");
			Console.ReadLine();
		}
	}
}


/*
 * -----------------------------
 * -------- PARAMETROS ---------
 * -----------------------------
 * 
 *	Numero de Simulaciones desde una misma iteracion:
 *		Actualmente no se hace pero se podrían hacer más de una simulacion desde el punto actual y cuando se complete ya sigues iterando.
 *
 *
 * =================================
 * ========= COMPLETADOS ===========
 * =================================
 * 
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
 *
 *  SIMULATION_POLICY:
 *		[RandomPolicy, GreedyPolicy]
 *
 *	Numero de hijos que se tienen en cuenta durante la simulacion en cada rama:
 *		Mientras simulas ahora mismo se tienen en cuenta todos los hijos y a esos le haces un random, pues puedes cojer K hijos y solo ha esos le haces
 *		el scoring. Puedes tambien hacerselo a todos
 *		[0.0,1.0]
 *		
 * 	Formas de especular:
 *		[BaseEstimation, ValueEstimation]
 *		
 */





// comentar que el reuse es inviable debido a que los paso de hearthstone no son deterministas entonces al jugar una carta que tenga que hace un efecto
// aleatorio en el arbol se asume que es uno de los posibles pero para el juego uno de los efectos ya si que se ha producido.






// variantes
/*
 * parametros para hacer bateria de test para buscar los optimos
 * 
 *
 * hacer que se pueda comparar con el evolutionary
 *
 *
 * pensar heuristicas
 *
 *
 * cuantos mas parametros mejor
 *
 *	intentar que los mazos sean iguales con semillas o crear nuevos desactivando el auto
 *
 *	al estimar poner uno dos o 20 parametros para estimar
 *
 *	aparte de los pesos.
 *
 *  profundidad maxima - param
 *
 *	si no quieres que llegue al final inventa parametro con conocimiento del juego para estimar si ganas o pierdes dejandolo como 0. algo 1  <=
 *	aunqu si llega al final tiene que ser 0 o 1
 *
 * args
 *
 *
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




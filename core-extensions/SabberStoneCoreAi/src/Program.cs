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
			AbstractAgent player1 = new AlvaroAgent(2,10000,"MaxUCB"); // usar args y hacer casteos
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
 * -------- PARAMETROS ---------
 *
 *	Formas de seleccionar:
 *		Investigar distintas heuristicas unidas al UCB1 o similares. ScoreTask del cotutor normalizada + ucb -> (estadoPosterior - estado anterior)	[1 - 3]
 *		
 *		También plantear crear la propia que solo dependa del estado y no de una transición.
 * 
 *	Pesos de los campos de la heuristica:
 *		cuanto importa la vida, cuanto importa nosq etc. en paper lo hacen independientemente con un genetico y despues los añaden aqui ya.
 * 
 *	Profundidad como parametro de seleccion:
 *		Hacer un trackeo de la profundida de cada nodo cuando se crea y que expansion tenga una clausula de que si llega a una cierta profundidad
 *		ya no expanda más y solo simule desde ese punto.  [1 - fin del juego]
 *
 *	Forma de seleccionar durante las simulaciones:
 *		Que no sea solo random sino "que siga otros metodos" como una heuristica de como es el estado con unos pesos.
 *
 *	Numero de Simulaciones desde una misma iteracion:
 *		Actualmente no se hace pero se podrían hacer más de una simulacion desde el punto actual y cuando se complete ya sigues iterando.
 *
 *	Numero de hijos que se tienen en cuenta durante la simulacion en cada rama:
 *		Mientras simulas ahora mismo se tienen en cuenta todos los hijos y a esos le haces un random, pues puedes cojer K hijos y solo ha esos le haces
 *		el scoring. Puedes tambien hacerselo a todos
 *
 * 	Formas de especular:
 *		Las que a mi se me ocurran como convenientes para hacer un scoring dado un estado.	[1 - 3]
 *
 *	Profundidad como parametro de cuando hacer especulacion:
 *		Dentro de simulacion si (no ha acabado o pasos > NUMERODEPASOS) especulacion. [0 - fin del juego]
 * 
 * ========= COMPLETADOS ===========
 *
 * 	EXPLORE_CONSTANT solo util el ucb1:
 *		Cuanta importancia le da a explorar nuevos caminos sobre el camino que parece más prometedor. [0 - 2]
 *		
 *	MAX_TIME:
 *		Tiempo que se queda ejecutando el bucle principal hasta que toma una decision.  [1000 - 20000]
 *
 *	Formas de elegir la accion final:
 *		[MaxVictories, MaxVisited, MaxVictoriesAndVisited, MaxUCB]
 */



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
 */




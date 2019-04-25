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
			/*System.Collections.Generic.List<SabberStoneCore.Model.Card> list = new System.Collections.Generic.List<SabberStoneCore.Model.Card>();	
			list.Add(SabberStoneCore.Model.Cards.FromName("Haunted Creeper"));
		*/
			DeckManager manage = new DeckManager();
			gameConfig.Player1Deck = manage.NaxxramasWarlock;
			gameConfig.Player2Deck = manage.NaxxramasWarlock;
				
			Console.WriteLine("Setup POGameHandler");
			AbstractAgent player1 = new AlvaroAgent();
			AbstractAgent player2 = new TycheAgentCompetition();
			var gameHandler = new POGameHandler(gameConfig, player1, player2, debug:true);

			Console.WriteLine("PlayGame");

			gameHandler.PlayGames(10);
			GameStats gameStats = gameHandler.getGameStats();

			gameStats.printResults();


			Console.WriteLine("Test successful");
			Console.ReadLine();
		}
	}
}






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




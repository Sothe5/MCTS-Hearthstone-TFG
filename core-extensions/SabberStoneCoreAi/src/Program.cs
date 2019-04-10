using System;
using SabberStoneCore.Config;
using SabberStoneCore.Enums;
using SabberStoneCoreAi.POGame;
using SabberStoneCoreAi.Agent.ExampleAgents;
using SabberStoneCoreAi.Agent;

namespace SabberStoneCoreAi
{
	internal class Program
	{

		private static void Main(string[] args)
		{
			
			Console.WriteLine("Setup gameConfig");

			//todo: rename to Main
			GameConfig gameConfig = new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.HUNTER,
				Player2HeroClass = CardClass.MAGE,
				FillDecks = true,
				Logging = false,
				FillDecksPredictably = false
			};

			Console.WriteLine("Setup POGameHandler");
			AbstractAgent player1 = new AlvaroAgent();
			AbstractAgent player2 = new FaceHunter();
			var gameHandler = new POGameHandler(gameConfig, player1, player2, debug:true);

			Console.WriteLine("PlayGame");
			//gameHandler.PlayGame();
			gameHandler.PlayGames(1);
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
 */




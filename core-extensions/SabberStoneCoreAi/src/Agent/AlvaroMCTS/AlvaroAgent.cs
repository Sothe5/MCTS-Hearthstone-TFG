using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Tasks;
using SabberStoneCoreAi.Agent;
using SabberStoneCoreAi.POGame;

using SabberStoneCoreAi.src.Agent.AlvaroMCTS;
using System.Diagnostics;

namespace SabberStoneCoreAi.Agent
{
	class AlvaroAgent : AbstractAgent
	{
		private Random Rnd = new Random();
		private const int EXPLORE_CONSTANT = 2;
		private const int MAX_TIME = 10000;


		public override void FinalizeAgent() { }

		public override void FinalizeGame() { }

		public override void InitializeAgent() { }

		public override void InitializeGame() { }

		public AlvaroAgent() // aqui es donde pones los parametros y tal 
		{

		}


		public override PlayerTask GetMove(POGame.POGame poGame)
		{
			//foreach (PlayerTask a in poGame.CurrentPlayer.Options()){
			//	Console.WriteLine(a + "----------------------");
			//}
			//POGame.POGame initialState = new POGame.POGame(poGame.getGame, false);

			if(poGame.CurrentPlayer.Options().Count == 1)
			{
				return poGame.CurrentPlayer.Options()[0];
			}

			POGame.POGame initialState = new POGame.POGame(poGame.getGame.Clone(), false);

			Node root = new Node();

			Node selectedNode;
			Node nodeToSimulate;
			float scoreOfSimulation;
			int iterations = 0;

			InitializeRoot(root, initialState);

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (stopwatch.ElapsedMilliseconds <= MAX_TIME) // Hay que poner por algun lado de aqui que si solo hay una opcion que no piense
			{
				poGame = initialState;
				//Console.WriteLine("---------- Other Iteration ------------");
				selectedNode = Selection(root, iterations, ref poGame);
				//Console.WriteLine("Selection done");
				nodeToSimulate = Expansion(selectedNode, ref poGame);
				//Console.WriteLine("Expansion done");
				scoreOfSimulation = Simulation(poGame);
				//Console.WriteLine("Simulation done");
				Backpropagation(nodeToSimulate, scoreOfSimulation);
				//Console.WriteLine("Backpropagation done");
				//Console.WriteLine(stopwatch.ElapsedMilliseconds);
				iterations++;
			}
			stopwatch.Stop();

			return bestTaskOption(root);
		}

		private void InitializeRoot(Node root, POGame.POGame poGame)
		{
			foreach (PlayerTask task in poGame.CurrentPlayer.Options())
			{
				root.children.Add(new Node(task,root));
			}
		}

		private PlayerTask bestTaskOption(Node root)
		{
			//PrintTree(root);
			float best = -1;
			PlayerTask bestTask = null;
			foreach(Node child in root.children)
			{
				if(child.totalValue >= best){
					best = child.totalValue;
					bestTask = child.task;
				}
			}
			return bestTask;
		}

		private Node Selection(Node root, int iterations, ref POGame.POGame poGame)
		{
			Node bestNode = new Node();
			double bestScore = -1;
			double childScore = 0;
			
			foreach (Node node in root.children)
			{
				childScore = ucb1(node, iterations);
				if (childScore > bestScore)
				{
					bestScore = childScore;
					bestNode = node;
				}
			}
			List<PlayerTask> taskToSimulate = new List<PlayerTask>();
			taskToSimulate.Add(bestNode.task);

			poGame = poGame.Simulate(taskToSimulate)[bestNode.task];

			if (poGame == null)
				return root;
			
			if(bestNode.children.Count != 0)
			{
				bestNode = Selection(bestNode,iterations, ref poGame);
			}

			return bestNode;
		}

		//  vi = media de valores					|| ni = Veces visitado		|| N = numero de veces que se realiza una seleccion  || C = entre [0 - 2]
		//  vi = totalValores / Veces visitado		||							|| (Sin contar en la que estas)						 || empírico
		// UCB1(Si) = vi + C * sqrt(ln(N)/ni) value.
		private double ucb1(Node node, int iterations)
		{
			double value;
			if(node.timesVisited > 0)
			{
				value = (node.totalValue / (double)node.timesVisited) + EXPLORE_CONSTANT * Math.Sqrt(Math.Log(iterations) / node.timesVisited);
			} else
			{
				value = Double.MaxValue;
			}
			return value;
		}


		// Depende del turno crea nodos de un tipo o de otro. 
		// Cuando se seleccione EndTurnTask se tendra que cambiar el turno.		AUTO

		private Node Expansion(Node leaf, ref POGame.POGame poGame)
		{
			Node nodeToSimulate;

			if (leaf.timesVisited == 0)
			{
				nodeToSimulate = leaf;
			} else
			{
				if (poGame == null)
					return leaf;
				foreach (PlayerTask task in poGame.CurrentPlayer.Options())
				{
					leaf.children.Add(new Node(task, leaf));

				}

				nodeToSimulate = leaf.children[0]; 
				List<PlayerTask> taskToSimulate = new List<PlayerTask>();
				taskToSimulate.Add(nodeToSimulate.task);
				poGame = poGame.Simulate(taskToSimulate)[nodeToSimulate.task];
				if (poGame == null)
					return leaf;
				taskToSimulate.Clear();
			}
			return nodeToSimulate;
		}

		// me dan un nodo y yo simulo "hasta el final" y devuelvo un float que si llega al final sera 1 o 0 y sino llega tendre que hacer prediccion
		private float Simulation(POGame.POGame poGame)
		{
			float result = -1;

			List<PlayerTask> taskToSimulate = new List<PlayerTask>();
			if (poGame == null)
				return 0.5f;
			while (poGame.getGame.State != SabberStoneCore.Enums.State.COMPLETE)
			{
				taskToSimulate.Add(poGame.CurrentPlayer.Options()[Rnd.Next(0, poGame.CurrentPlayer.Options().Count-1)]);
				poGame = poGame.Simulate(taskToSimulate)[taskToSimulate[0]];
				taskToSimulate.Clear();

				if(poGame == null)
				{
					return 0.5f;
				}
			}

			SabberStoneCore.Model.Entities.Controller Player1;
			if(poGame.getGame.CurrentPlayer.PlayerId == 1)
			{
				Player1 = poGame.getGame.CurrentPlayer;
			} else
			{
				Player1 = poGame.getGame.CurrentOpponent;
			}

			if (Player1.PlayState == SabberStoneCore.Enums.PlayState.CONCEDED
				|| Player1.PlayState == SabberStoneCore.Enums.PlayState.LOST)
			{
				result = 0;
			} else if (Player1.PlayState == SabberStoneCore.Enums.PlayState.WON)
			{
				result = 1;
			}

			return result;
		}

		// iterate over the tree until parent == null sumando puntuaciones a los dos campos del nodo.
		private void Backpropagation(Node node, float result)
		{
			node.timesVisited++;
			node.totalValue += result;
			if(node.parent != null)
			{
				Backpropagation(node.parent, result);
			}
		}

		private void PrintTree(Node node)
		{
			foreach (Node nodes in node.children)
			{
				Console.WriteLine("Total value = " + nodes.totalValue + ", "+ "Times visited = "+nodes.timesVisited);
				PrintTree(nodes);
			}
			Console.WriteLine("===");
		}
	}

}

/*
 *	1º Crear Seleccion, Expansion, Simulation y Backpropagation.
 *  2º Pensar en que tiene que haber en cada nodo ==> si el estado del tablero, si una sola de las tasks, la lista de tasks?
 *  3º Solo hay que hacer un codigo que devuelva UNA unica task.
 *  4º Mi turno acaba cuando eliga la task de terminar turno.
 *  5º Probablemente la idea sea que cada nodo es una task y que corra el algoritmo del MTCS para cada seleccion de dentro de options()
 *			dicho de otra manera :=>: MCTS de cero = juega una carta
 *									  MCTS de cero = ataca con un minion
 *									  ...
 *									  ...
 *									  MCTS de cero = EndTurnTask.
 *									  Este seria el plain MTCS que creo que he visto siempre.
 *									  plantear el del estado del tablero o si hay otras opciones
 *
 *	La anchura del arbol en cada profundidad queda definida por la cantidad de task que puedas hacer ese "turno".
 *
 *	La idea es que si la simulacion llega al end game -> devuelve 1 o 0
 *
 *
 *	Cuando expanda y simule tengo que tener en cuenta de quien es el turno para expandir con opciones de juego o con las del rival.
 *	
 */

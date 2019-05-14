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

//		======== PARAMETERS ==========
		private double EXPLORE_CONSTANT = 2;
		private int MAX_TIME = 10000;
		private string SELECTION_ACTION = "MaxVictories";


		public override void FinalizeAgent() { }

		public override void FinalizeGame() { }

		public override void InitializeAgent() { }

		public override void InitializeGame() { }

		public AlvaroAgent() { }

		public AlvaroAgent(int exploreConstant, int maxTime, string selectionAction) // aqui es donde pones los parametros y tal 
		{
			EXPLORE_CONSTANT = exploreConstant;
			MAX_TIME = maxTime;
			SELECTION_ACTION = selectionAction;
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
			
			return SelectAction.selectTask(SELECTION_ACTION, root, iterations, EXPLORE_CONSTANT);
		}

		private void InitializeRoot(Node root, POGame.POGame poGame)
		{
			foreach (PlayerTask task in poGame.CurrentPlayer.Options())
			{
				root.children.Add(new Node(task,root,true));
			}
		}

	 // Plantear cambiar todo el diseño para que el arbol sea solo de mi jugador y que despues de cada accion que haga mi jugador haya
	 // un metodo para que me de el estado de juego resultante de que mi oponente haga acciones random hasta que me vuelva a tocar.
	 // de este modo en el backpropagation no se tiene en cuenta el segundo jugador.
		private Node Selection(Node root, int iterations, ref POGame.POGame poGame)
		{
			Node bestNode = new Node();
			double bestScore = -1;
			//double worstScore = Double.MaxValue;
			double childScore = 0;

			POGame.POGame pOGameIfSimulationFail = new POGame.POGame(poGame.getGame.Clone(), false);
			//if (root.children[0].isPlayer1Node)
			//{
				foreach (Node node in root.children)
				{
					childScore = TreePolicies.ucb1(node, iterations, EXPLORE_CONSTANT);
					if (childScore > bestScore)
					{
						bestScore = childScore;
						bestNode = node;
					}
			/*	}
			} else
			{
				foreach (Node node in root.children)
				{
					childScore = ucb1(node, iterations);
					if (childScore < worstScore)				// menor probabilidad de que gane el jugador 1
					{
						worstScore = childScore;
						bestNode = node;
					}
				}
			*/}
			
			List<PlayerTask> taskToSimulate = new List<PlayerTask>();
			taskToSimulate.Add(bestNode.task);

			poGame = poGame.Simulate(taskToSimulate)[bestNode.task];

			// Simulate have failed
			if (poGame == null) 
			{
				root.children.Remove(bestNode);
				poGame = pOGameIfSimulationFail;
				return Selection(root,iterations, ref poGame);
			}
			
			if(bestNode.children.Count != 0)
			{
				bestNode = Selection(bestNode,iterations, ref poGame);
			}

			return bestNode;
		}


		// Depende del turno crea nodos de un tipo o de otro. 
		// Cuando se seleccione EndTurnTask se tendra que cambiar el turno.		AUTO

		private Node Expansion(Node leaf, ref POGame.POGame poGame)
		{
			Node nodeToSimulate;
			POGame.POGame pOGameIfSimulationFail = new POGame.POGame(poGame.getGame.Clone(), false);
			if (leaf.timesVisited == 0)
			{
				nodeToSimulate = leaf;
			} else
			{
				//if (poGame == null)
				//	return leaf;

				foreach (PlayerTask task in poGame.CurrentPlayer.Options())
				{
					leaf.children.Add(new Node(task, leaf, poGame.getGame.CurrentPlayer.PlayerId == 1));

				}

				nodeToSimulate = leaf.children[0]; 
				List<PlayerTask> taskToSimulate = new List<PlayerTask>();
				taskToSimulate.Add(nodeToSimulate.task);
				poGame = poGame.Simulate(taskToSimulate)[nodeToSimulate.task];
				
				while(poGame == null && leaf.children.Count != 0)
				{
					poGame = pOGameIfSimulationFail;
					taskToSimulate.Clear();
					leaf.children.Remove(leaf.children[0]);
					nodeToSimulate = leaf.children[0];
					taskToSimulate.Add(nodeToSimulate.task);
					poGame = poGame.Simulate(taskToSimulate)[nodeToSimulate.task];
				}	
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

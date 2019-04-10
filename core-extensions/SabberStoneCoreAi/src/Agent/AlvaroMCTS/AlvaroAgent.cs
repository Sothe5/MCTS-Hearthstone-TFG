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
		private const int MAX_TIME = 10;


		public override void FinalizeAgent() { }

		public override void FinalizeGame() { }

		public override void InitializeAgent() { }

		public override void InitializeGame() { }


		public override PlayerTask GetMove(SabberStoneCoreAi.POGame.POGame poGame)
		{
			foreach (PlayerTask a in poGame.CurrentPlayer.Options()){
				Console.WriteLine(a + "----------------------");
			}

			Node root = new Node();
			Node selectedNode;
			InitializeRoot(root, poGame);
			
			int iterations = 0;

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			
			while (stopwatch.ElapsedMilliseconds <= MAX_TIME)
			{
				selectedNode = Selection(root, iterations);
				//Console.WriteLine(stopwatch.ElapsedMilliseconds);

				iterations++;
			}
			stopwatch.Stop();

			return bestTaskOption(root);
		}

		private void InitializeRoot(Node root, SabberStoneCoreAi.POGame.POGame poGame)
		{
			foreach (PlayerTask task in poGame.CurrentPlayer.Options())
			{
				root.children.Add(new Node(task,root));
			}
		}

		private PlayerTask bestTaskOption(Node root)
		{
			int best = -1;
			PlayerTask bestTask = null;
			foreach(Node child in root.children)
			{
				if(child.totalValue > best){
					best = child.totalValue;
					bestTask = child.task;
				}
			}
			return bestTask;
		}

		private Node Selection(Node root, int iterations)
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

			if(bestNode.children.Count != 0)
			{
				bestNode = Selection(bestNode,iterations);
			}

			return bestNode;
		}

		//  vi = media de valores					|| ni = Veces visitado		|| N = numero de veces que se realiza una seleccion  || C = entre [2 - 0]
		//  vi = totalValores / Veces visitado		||							|| (Sin contar en la que estas)						 || empírico
		// UCB1(Si) = vi + C * sqrt(ln(N)/ni) value.
		private double ucb1(Node node, int iterations)
		{
			double aux = Double.PositiveInfinity;
			if(node.timesVisited > 0)
				aux = EXPLORE_CONSTANT * Math.Sqrt(Math.Log(iterations) / node.timesVisited);
			return (node.totalValue / (double)node.timesVisited) + aux;
		}


		// Depende del turno crea nodos de un tipo o de otro. 
		// Actualizar leafs
		// Cuando se seleccione EndTurnTask se tendra que cambiar el turno.
		// Se salta si timesVisited es 0.
		private void Expansion()
		{

		}



		private void Simulation()
		{

		}

		// iterate over the tree until parent == null sumando puntuaciones a los dos campos del nodo.
		private void Backpropagation()
		{

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

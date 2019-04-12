using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Tasks;

namespace SabberStoneCoreAi.src.Agent.AlvaroMCTS
{
	class Node
	{
		public int totalValue	{ get; set; } // de que gana el jugador 1.
		public int timesVisited { get; set; }
		public Node parent		{ get; set; }
		public PlayerTask task	{ get; set; }
		public List<Node> children;

		// QUIZAS añadir un campo para guardar: de que jugador es el nodo que posee la opcion y se setearia en expansion
		// y en la inicializacion.
		// En la approach base no es necesario pero en otras puede que si.


		public Node()
		{
			totalValue = 0;
			timesVisited = 0;
			parent = null;
			task = null;
			children = new List<Node>();
		}

		public Node(PlayerTask task, Node parent)
		{
			totalValue = 0;
			timesVisited = 0;
			this.parent = parent;
			this.task = task;
			children = new List<Node>();
		}

	}
}

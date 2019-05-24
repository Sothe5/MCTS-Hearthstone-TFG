﻿using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Tasks;

namespace SabberStoneCoreAi.src.Agent.AlvaroMCTS
{
	class Node
	{
		public float totalValue		{ get; set; } // de que gana el jugador 1.
		public int timesVisited		{ get; set; }
		public Node parent			{ get; set; }
		public PlayerTask task		{ get; set; }
		public bool isPlayer1Node	{ get; set; }
		public int depth			{ get; set; }
		public List<Node> children;

		public Node()
		{
			totalValue = 0;
			timesVisited = 0;
			parent = null;
			task = null;
			isPlayer1Node = false;
			depth = 0;
			children = new List<Node>();
		}

		public Node(PlayerTask task, Node parent, bool isPlayer1Node, int depth)
		{
			totalValue = 0;
			timesVisited = 0;
			this.parent = parent;
			this.task = task;
			this.isPlayer1Node = isPlayer1Node;
			this.depth = depth;
			children = new List<Node>();
		}

	}
}

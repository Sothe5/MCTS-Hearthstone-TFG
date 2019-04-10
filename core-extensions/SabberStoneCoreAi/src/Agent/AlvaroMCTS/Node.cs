using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Tasks;

namespace SabberStoneCoreAi.src.Agent.AlvaroMCTS
{
	class Node
	{
		public int totalValue	{ get; set; }
		public int timesVisited { get; set; }
		public Node parent		{ get; set; }
		public PlayerTask task	{ get; set; }
		public List<Node> children;


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

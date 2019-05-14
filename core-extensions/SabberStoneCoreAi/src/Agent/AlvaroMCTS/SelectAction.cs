﻿using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Tasks;

namespace SabberStoneCoreAi.src.Agent.AlvaroMCTS
{
	class SelectAction
	{
		public static PlayerTask selectTask(string selectionMethod, Node root, int iterations, double exploreConstant)
		{
			PlayerTask task;
			switch (selectionMethod)
			{
				case "MaxVictories":
					task = MaxVictories(root);
					break;
				case "MaxVisited":
					task = MaxVisited(root);
					break;
				case "MaxVictoriesAndVisited":
					task = MaxVictoriesAndVisited(root);
					break;
				case "MaxUCB":
					task = MaxUCB(root, iterations, exploreConstant);
					break;
				default:
					task = null;
					break;
			}

			return task;
		}

		private static PlayerTask MaxVictories(Node root)
		{
			double best = -1;
			PlayerTask bestTask = null;
			foreach (Node child in root.children)
			{
				if (child.totalValue >= best)
				{
					best = child.totalValue;
					bestTask = child.task;
				}
			}
			return bestTask;
		}

		private static PlayerTask MaxVisited(Node root)
		{
			double best = -1;
			PlayerTask bestTask = null;
			foreach (Node child in root.children)
			{
				if (child.timesVisited >= best)
				{
					best = child.timesVisited;
					bestTask = child.task;
				}
			}
			return bestTask;
		}

		private static PlayerTask MaxVictoriesAndVisited(Node root)
		{
			double best = -1;
			PlayerTask bestTask = null;
			foreach (Node child in root.children)
			{
				if (child.timesVisited+child.totalValue >= best)
				{
					best = child.timesVisited + child.totalValue;
					bestTask = child.task;
				}
			}
			return bestTask;
		}

		private static PlayerTask MaxUCB(Node root, int iterations, double exploreConstant)
		{
			double best = -1;
			PlayerTask bestTask = null;
			foreach (Node child in root.children)
			{
				double score = TreePolicies.ucb1(root, iterations, exploreConstant);
				if (score >= best)
				{
					best = score;
					bestTask = child.task;
				}
			}
			return bestTask;
		}
	}


	

}

﻿using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Tasks;
using SabberStoneCoreAi.Agent;

namespace SabberStoneCoreAi.src.Agent.AlvaroMCTS
{
	class SimulationPolicies
	{

		public static PlayerTask selectSimulationPolicy(string simulationPolicy, POGame.POGame poGame, Random Rnd, ParametricGreedyAgent greedyAgent,
			double CHILDREN_CONSIDERED_SIMULATING)
		{
			PlayerTask task;
			switch (simulationPolicy)
			{
				case "RandomPolicy":
					task = randomTask(poGame,Rnd);
					break;
				case "GreedyPolicy":
					task = greedyTask(poGame, greedyAgent, Rnd, CHILDREN_CONSIDERED_SIMULATING);
					break;
				default:
					task = null;
					break;
			}

			return task;
		}

		private static PlayerTask randomTask(POGame.POGame poGame, Random Rnd)
		{
			return poGame.CurrentPlayer.Options()[Rnd.Next(0, poGame.CurrentPlayer.Options().Count - 1)];
		}

		private static PlayerTask greedyTask(POGame.POGame poGame, ParametricGreedyAgent greedyAgent, Random Rnd, double CHILDREN_CONSIDERED_SIMULATING)
		{
			PlayerTask bestTask = null;
			double bestScore = Double.MinValue;
			double score = 0;
			List<PlayerTask> taskToSimulate = new List<PlayerTask>();
			POGame.POGame stateAfterSimulate = null;

			List<PlayerTask> options = poGame.CurrentPlayer.Options();

			int cutPoint = (int) Math.Ceiling(poGame.CurrentPlayer.Options().Count * CHILDREN_CONSIDERED_SIMULATING);
			while(options.Count > cutPoint)
			{
				options.Remove(options[Rnd.Next(0, options.Count - 1)]);
			}
	
			foreach (PlayerTask task in poGame.CurrentPlayer.Options())
			{
				taskToSimulate.Add(task);
				stateAfterSimulate = poGame.Simulate(taskToSimulate)[task];

				score = greedyAgent.scoreTask(poGame, stateAfterSimulate);
				if (score > bestScore)
				{
					bestScore = score;
					bestTask = task;
				}
				taskToSimulate.Clear();
			}
			return bestTask;
		}

	}
}

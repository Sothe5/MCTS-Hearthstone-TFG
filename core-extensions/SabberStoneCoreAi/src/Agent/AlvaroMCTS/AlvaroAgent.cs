using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Tasks;
using SabberStoneCoreAi.src.Agent;
using SabberStoneCoreAi.src.Agent.AlvaroMCTS;
using System.Diagnostics;


namespace SabberStoneCoreAi.Agent
{
	class AlvaroAgent : AbstractAgent
	{
		private Random Rnd = new Random();
		private ParametricGreedyAgent greedyAgent;

		//		======== PARAMETERS ==========
		private double EXPLORE_CONSTANT = 2;
		private int MAX_TIME = 1000;
		private string SELECTION_ACTION_METHOD = "MaxVictories";
		private string TREE_POLICY = "UCB1";
		private double SCORE_IMPORTANCE = 1;
		private int TREE_MAXIMUM_DEPTH = 10;
		private string SIMULATION_POLICY = "RandomPolicy";
		private double CHILDREN_CONSIDERED_SIMULATING = 1;
		private string ESTIMATION_MODE = "BaseEstimation";
		private int NUM_SIMULATIONS = 1;

		public override void FinalizeAgent() { }

		public override void FinalizeGame() { }

		public override void InitializeAgent() { }

		public override void InitializeGame() { }

		public AlvaroAgent() { }

		public AlvaroAgent(double exploreConstant, int maxTime, string selectionAction, double scoreImportance, string treePolicy, int treeMaximumDepth,
						   string simulationPolicy, double childrenConsideredSimulating, string estimationMode, int numSimulations,
						
						   string HERO_HEALTH_REDUCED, string HERO_ATTACK_REDUCED, string MINION_HEALTH_REDUCED, string MINION_ATTACK_REDUCED,
							string MINION_APPEARED, string MINION_KILLED, string SECRET_REMOVED, string MANA_REDUCED, string M_HEALTH,
							 string M_ATTACK, string M_HAS_CHARGE, string M_HAS_DEAHTRATTLE, string M_HAS_DIVINE_SHIELD, string M_HAS_INSPIRE,
							  string M_HAS_LIFE_STEAL, string M_HAS_STEALTH, string M_HAS_TAUNT, string M_HAS_WINDFURY, string M_RARITY, string M_MANA_COST,
							  string M_POISONOUS,

							  float weaponAttack, float weaponDurability, float health, float boardStats, float handSize, float deckRemaining,
							  float mana, float secret, float overload, float minionCost, float secretCost, float cardCost, float weaponCost)
		{
			EXPLORE_CONSTANT = exploreConstant;
			MAX_TIME = maxTime;
			SELECTION_ACTION_METHOD = selectionAction;
			SCORE_IMPORTANCE = scoreImportance;
			TREE_POLICY = treePolicy;
			TREE_MAXIMUM_DEPTH = treeMaximumDepth;
			SIMULATION_POLICY = simulationPolicy;
			CHILDREN_CONSIDERED_SIMULATING = childrenConsideredSimulating;
			ESTIMATION_MODE = estimationMode;
			NUM_SIMULATIONS = numSimulations;

			greedyAgent = new ParametricGreedyAgent(HERO_HEALTH_REDUCED + "#" + HERO_ATTACK_REDUCED + "#" + MINION_HEALTH_REDUCED + "#" + MINION_ATTACK_REDUCED + "#" +
				MINION_APPEARED + "#" + MINION_KILLED + "#" + SECRET_REMOVED + "#" + MANA_REDUCED + "#" + M_HEALTH + "#" +
				M_ATTACK + "#" + M_HAS_CHARGE + "#" + M_HAS_DEAHTRATTLE + "#" + M_HAS_DIVINE_SHIELD + "#" + M_HAS_INSPIRE + "#" +
				M_HAS_LIFE_STEAL + "#" + M_HAS_STEALTH + "#" + M_HAS_TAUNT + "#" + M_HAS_WINDFURY + "#" + M_RARITY + "#" + M_MANA_COST + "#" + M_POISONOUS);

			Estimator.setWeights(weaponAttack, weaponDurability, health, boardStats, handSize, deckRemaining, mana, secret, overload, minionCost, secretCost,
				cardCost, weaponCost, float.Parse(M_HAS_CHARGE), float.Parse(M_HAS_DEAHTRATTLE), float.Parse(M_HAS_DIVINE_SHIELD),
				float.Parse(M_HAS_INSPIRE), float.Parse(M_HAS_LIFE_STEAL), float.Parse(M_HAS_TAUNT), float.Parse(M_HAS_WINDFURY));
		}


		public override PlayerTask GetMove(POGame.POGame poGame)
		{
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
			while (stopwatch.ElapsedMilliseconds <= MAX_TIME)
			{
				poGame = initialState;
				selectedNode = Selection(root, iterations, ref poGame);
				nodeToSimulate = Expansion(selectedNode, ref poGame);

				for(int i = 0; i < NUM_SIMULATIONS; i++)
				{
					scoreOfSimulation = Simulation(nodeToSimulate, poGame);
					Backpropagation(nodeToSimulate, scoreOfSimulation);
					iterations++;
				}
			}
			stopwatch.Stop();
		
			return SelectAction.selectTask(SELECTION_ACTION_METHOD, root, iterations, EXPLORE_CONSTANT);
		}

		private void InitializeRoot(Node root, POGame.POGame poGame)
		{
			foreach (PlayerTask task in poGame.CurrentPlayer.Options())
			{
				root.children.Add(new Node(task,root,root.depth+1));
			}
		}

		private Node Selection(Node root, int iterations, ref POGame.POGame poGame)
		{
			Node bestNode = new Node();
			double bestScore = double.MinValue;
			double childScore = 0;

			POGame.POGame pOGameIfSimulationFail = new POGame.POGame(poGame.getGame.Clone(), false);

			foreach (Node node in root.children)
			{
				childScore = TreePolicies.selectTreePolicy(TREE_POLICY, node, iterations, EXPLORE_CONSTANT, ref poGame, SCORE_IMPORTANCE, greedyAgent);
				if (childScore > bestScore)
				{
					bestScore = childScore;
					bestNode = node;
				}
			}
			List<PlayerTask> taskToSimulate = new List<PlayerTask>();
			taskToSimulate.Add(bestNode.task);
	
			if(bestNode.task.PlayerTaskType != PlayerTaskType.END_TURN)
				poGame = poGame.Simulate(taskToSimulate)[bestNode.task];

			if (poGame == null) 
			{
				root.children.Remove(bestNode);
				if(root.children.Count == 0)
					root = root.parent;
				
				poGame = pOGameIfSimulationFail;
				return Selection(root,iterations, ref poGame);
			}
			
			if(bestNode.children.Count != 0)
			{
				bestNode = Selection(bestNode,iterations, ref poGame);
			}

			return bestNode;
		}

		private Node Expansion(Node leaf, ref POGame.POGame poGame)
		{
			Node nodeToSimulate;
			POGame.POGame pOGameIfSimulationFail = new POGame.POGame(poGame.getGame.Clone(), false);
			if (leaf.timesVisited == 0 || leaf.depth >= TREE_MAXIMUM_DEPTH || leaf.task.PlayerTaskType == PlayerTaskType.END_TURN)
			{
				nodeToSimulate = leaf;
			} else
			{

				foreach (PlayerTask task in poGame.CurrentPlayer.Options())
				{
					leaf.children.Add(new Node(task, leaf, leaf.depth+1));
				}

				nodeToSimulate = leaf.children[0]; 
				List<PlayerTask> taskToSimulate = new List<PlayerTask>();
				taskToSimulate.Add(nodeToSimulate.task);
				if (nodeToSimulate.task.PlayerTaskType != PlayerTaskType.END_TURN)
					poGame = poGame.Simulate(taskToSimulate)[nodeToSimulate.task];
				
				while(poGame == null)
				{
					if (leaf.children.Count <= 1)
						return leaf;
					poGame = pOGameIfSimulationFail;
					taskToSimulate.Clear();
					leaf.children.Remove(leaf.children[0]);
					nodeToSimulate = leaf.children[0];
					taskToSimulate.Add(nodeToSimulate.task);
					if (nodeToSimulate.task.PlayerTaskType != PlayerTaskType.END_TURN)
						poGame = poGame.Simulate(taskToSimulate)[nodeToSimulate.task];
				}	
			}
			return nodeToSimulate;
		}
		
		private float Simulation(Node nodeToSimulate, POGame.POGame poGame)
		{
			float result = -1;
			int simulationSteps = 0;
			PlayerTask task = null;

			List<PlayerTask> taskToSimulate = new List<PlayerTask>();
			if (poGame == null)
				return 0.5f;
			
			if(nodeToSimulate.task.PlayerTaskType == PlayerTaskType.END_TURN)
				return Estimator.estimateFromState(ESTIMATION_MODE, poGame);
				
			while (poGame.getGame.State != SabberStoneCore.Enums.State.COMPLETE)
			{
				task = SimulationPolicies.selectSimulationPolicy(SIMULATION_POLICY, poGame, Rnd, greedyAgent, CHILDREN_CONSIDERED_SIMULATING);
				taskToSimulate.Add(task);
				if (task.PlayerTaskType != PlayerTaskType.END_TURN)
					poGame = poGame.Simulate(taskToSimulate)[taskToSimulate[0]];

				taskToSimulate.Clear();

				if(poGame == null)
					return 0.5f;

				if(task.PlayerTaskType == PlayerTaskType.END_TURN) 
					return Estimator.estimateFromState(ESTIMATION_MODE, poGame);

				simulationSteps++;
			}


			if (poGame.CurrentPlayer.PlayState == SabberStoneCore.Enums.PlayState.CONCEDED
				|| poGame.CurrentPlayer.PlayState == SabberStoneCore.Enums.PlayState.LOST)
			{
				result = 0;
			} else if (poGame.CurrentPlayer.PlayState == SabberStoneCore.Enums.PlayState.WON)
			{
				result = 1;
			}

			return result;
		}

		private void Backpropagation(Node node, float result)
		{
			node.timesVisited++;
			node.totalValue += result;
			if(node.parent != null)
			{
				Backpropagation(node.parent, result);
			}
		}
	}

}

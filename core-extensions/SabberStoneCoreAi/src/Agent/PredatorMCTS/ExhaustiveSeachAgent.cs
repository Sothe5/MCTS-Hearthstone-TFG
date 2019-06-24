using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using SabberStoneCore.Tasks;
using SabberStoneCoreAi.POGame;
using SabberStoneCoreAi.Score;
using SabberStoneCoreAi.Nodes;
using SabberStoneCore.Tasks.PlayerTasks;


namespace SabberStoneCoreAi.Agent
{
	class ExhaustiveSeachAgent : AbstractAgentExt
	{
		private int _maxDepth;

		private int _maxWidth;

		public ExhaustiveSeachAgent(int maxDepth, int maxWidth, IScore scoring)
			: base(scoring)
		{
			_maxDepth = maxDepth;
			_maxWidth = maxWidth;
		}

		protected override List<PlayerTask> getSolutions(POGame.POGame poGame, int playerID, IScore scoring)
		{
			List<POOptionNode> solutionNodes = POOptionNode.GetSolutions(poGame, playerID, scoring, _maxDepth, _maxWidth);
			var solutions = new List<PlayerTask>();
			solutionNodes.OrderByDescending(p => p.Score).First().PlayerTasks(ref solutions);
			return solutions;
		}
	}
}

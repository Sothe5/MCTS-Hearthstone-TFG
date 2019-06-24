using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using SabberStoneCore.Tasks;
using SabberStoneCoreAi.Agent;
using SabberStoneCoreAi.Nodes;
using SabberStoneCoreAi.POGame;
using SabberStoneCoreAi.Score;
using SabberStoneCore.Tasks.PlayerTasks;


namespace SabberStoneCoreAi.Agent
{
	class FlatMCAgent : AbstractAgentExt
	{
		public FlatMCAgent(IScore scoring)
			: base(scoring) { }


		protected override List<PlayerTask> getSolutions(POGame.POGame poGame, int playerID, IScore scoring)
		{
			var solutionNode = FlatMCOptionNode.GetSolutions(poGame, playerID, scoring, 10);
			var solutions = new List<PlayerTask>();
			solutionNode.PlayerTasks(ref solutions);
			return solutions;
		}
	}
}

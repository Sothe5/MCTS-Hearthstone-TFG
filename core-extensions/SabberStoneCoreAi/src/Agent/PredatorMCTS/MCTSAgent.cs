using System;
using System.Collections.Generic;
using System.Text;

using SabberStoneCoreAi.Score;
using SabberStoneCoreAi.MC;
using SabberStoneCore.Tasks.PlayerTasks;


namespace SabberStoneCoreAi.Agent
{
	class MCTSAgent : AbstractMCTSAgent
	{
		public MCTSAgent(IScore scoring, MCTSParameters mctsParameters)
			: base(scoring, mctsParameters) { }

		protected override AbstractMCTSSimulator initSimulator(int playerID, IScore scoring)
		{
			return new MCTSSimulator(playerID, scoring, _mctsParameters)
			{
				Watch = Watch
			};
		}
	}
}

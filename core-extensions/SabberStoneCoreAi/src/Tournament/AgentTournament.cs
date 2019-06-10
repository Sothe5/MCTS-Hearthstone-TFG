using SabberStoneCoreAi.Agent;
using System;
using System.Collections.Generic;
using System.Text;

namespace SabberStoneCoreAi.src.Tournament
{
	class AgentTournament
	{
		public AbstractAgent agentAI;
		public int agentId { get; set; }
		public int leagueWins { get; set; }
		public int stageWins { get; set; }
		public int finalWins { get; set; }
		public int leagueTurns { get; set; }
		public int stageTurns { get; set; }
		public int finalTurns { get; set; }

		public AgentTournament(AbstractAgent agentAI, int agentId)
		{
			this.agentAI = agentAI;
			this.agentId = agentId;
			finalWins = 0;
			leagueWins = 0;
			stageWins = 0;
			leagueTurns = 0;
			stageTurns = 0;
			finalTurns = 0;
		}

	}
}

using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Tasks;
using SabberStoneCoreAi.POGame;

namespace SabberStoneCoreAi.Agent
{
	class MCTSAgent : AbstractAgent
	{
		public override void InitializeAgent()
		{
		
		}

		public override void InitializeGame()
		{

		}

		public override PlayerTask GetMove(POGame.POGame poGame)
		{
			return poGame.CurrentPlayer.Options()[0];
		}

		public override void FinalizeGame()
		{

		}

		public override void FinalizeAgent()
		{
	
		}
	}
}

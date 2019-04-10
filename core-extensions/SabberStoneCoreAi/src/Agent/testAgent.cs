using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Tasks;
using SabberStoneCoreAi.Agent;
using SabberStoneCoreAi.POGame;

namespace SabberStoneCoreAi.Agent {

	class TestAgent : AbstractAgent {

		private Random rnd = new Random();

		public override void FinalizeAgent() {
			
		}

		public override void FinalizeGame() {
		
		}

		public override PlayerTask GetMove(POGame.POGame poGame) {
			PlayerTask turnList = new PlayerTask();
			List<PlayerTask> options = poGame.CurrentPlayer.Options();

			PlayerTask option = options[rnd.Next(options.Count)];
			turnList = option;

			return turnList;
		}

		public override void InitializeAgent() { 
			rnd = new Random();
		}

		public override void InitializeGame() {

		}
	}
}


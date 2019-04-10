using System;
using System.Collections.Generic;
using System.Text;

namespace SabberStoneCoreAi.Agent
{
    static class TyConst
    {
		public const bool LOG_UNKNOWN_CORRECTIONS = false;
		public const bool LOG_UNKNOWN_SECRETS = false;

		public const double MAX_EPISODE_TIME = 5.0f;
		public const double MAX_SIMULATION_TIME = 10.0f;
		public const double MAX_TURN_TIME = 15.0;

		public const double DECREASE_SIMULATION_TIME = MAX_SIMULATION_TIME * 0.4;
	}
}

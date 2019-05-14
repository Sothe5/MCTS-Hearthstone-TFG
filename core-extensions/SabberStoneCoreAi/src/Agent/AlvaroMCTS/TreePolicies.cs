using System;
using System.Collections.Generic;
using System.Text;

namespace SabberStoneCoreAi.src.Agent.AlvaroMCTS
{
	class TreePolicies
	{

		//  vi = media de valores					|| ni = Veces visitado		|| N = numero de veces que se realiza una seleccion  || C = entre [0 - 2]
		//  vi = totalValores / Veces visitado		||							|| (Sin contar en la que estas)						 || empírico
		// UCB1(Si) = vi + C * sqrt(ln(N)/ni) value.
		public static double ucb1(Node node, int iterations, double EXPLORE_CONSTANT)
		{
			double value;
			if (node.timesVisited > 0)
			{
				value = (node.totalValue / (double)node.timesVisited) + EXPLORE_CONSTANT * Math.Sqrt(Math.Log(iterations) / node.timesVisited);
			} else
			{
				value = Double.MaxValue;
			}
			return value;
		}

	}
}

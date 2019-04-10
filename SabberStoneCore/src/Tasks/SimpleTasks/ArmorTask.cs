﻿using SabberStoneCore.Model.Entities;

namespace SabberStoneCore.Tasks.SimpleTasks
{
	public class ArmorTask : SimpleTask
	{
		private ArmorTask(bool useNumber, int amount)
		{
			UseNumber = useNumber;
			Amount = amount;
		}

		/// <summary>
		/// Adding the amount as Armor.
		/// </summary>
		public ArmorTask(int amount)
		{
			UseNumber = false;
			Amount = amount;
		}

		/// <summary>
		/// Adding the value contained in Number as Armor.
		/// </summary>
		public ArmorTask()
		{
			UseNumber = true;
			Amount = 0;
		}

		public bool UseNumber { get; set; }
		public int Amount { get; set; }

		public override TaskState Process()
		{
			var source = Source as IPlayable;
			if (source == null)
			{
				return TaskState.STOP;
			}
			Controller.Hero.GainArmor(source, UseNumber ? Number : Amount);
			return TaskState.COMPLETE;
		}

		public override ISimpleTask Clone()
		{
			var clone = new ArmorTask(UseNumber, Amount);
			clone.Copy(this);
			return clone;
		}
	}
}
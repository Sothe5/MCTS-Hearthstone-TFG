﻿using System;

namespace SabberStoneCore.Exceptions
{
	/// <summary>
	/// General exception used within the library.
	/// </summary>
	/// <seealso cref="Exception" />
	public abstract class SabberStoneExceptions : Exception
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	{
		protected SabberStoneExceptions() { }

		protected SabberStoneExceptions(string message) : base(message) { }

		protected SabberStoneExceptions(string message, Exception innerException) : base(message, innerException) { }
	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

	/// <summary>
	/// TODO
	/// </summary>
	/// <seealso cref="SabberStoneExceptions" />
	/// <autogeneratedoc />
	public class GameException : SabberStoneExceptions
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	{
		public GameException() { }

		public GameException(string message) : base(message) { }

		public GameException(string message, Exception innerException) : base(message, innerException) { }
	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

	/// <summary>
	/// TODO
	/// </summary>
	/// <seealso cref="SabberStoneCore.Exceptions.SabberStoneExceptions" />
	/// <autogeneratedoc />
	public class ZoneException : SabberStoneExceptions
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	{
		public ZoneException() { }

		public ZoneException(string message) : base(message) { }

		public ZoneException(string message, Exception innerException) : base(message, innerException) { }
	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

	/// <summary>
	/// TODO
	/// </summary>
	/// <seealso cref="SabberStoneCore.Exceptions.SabberStoneExceptions" />
	/// <autogeneratedoc />
	public class ControllerException : SabberStoneExceptions
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	{
		public ControllerException() { }

		public ControllerException(string message) : base(message) { }

		public ControllerException(string message, Exception innerException) : base(message, innerException) { }
	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

	/// <summary>
	/// TODO
	/// </summary>
	/// <seealso cref="SabberStoneCore.Exceptions.SabberStoneExceptions" />
	/// <autogeneratedoc />
	public class EntityException : SabberStoneExceptions
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	{
		public EntityException() { }

		public EntityException(string message) : base(message) { }

		public EntityException(string message, Exception innerException) : base(message, innerException) { }
	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

	/// <summary>
	/// TODO
	/// </summary>
	/// <seealso cref="SabberStoneCore.Exceptions.SabberStoneExceptions" />
	/// <autogeneratedoc />
	public class EnchantException : SabberStoneExceptions
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	{
		public EnchantException() { }

		public EnchantException(string message) : base(message) { }

		public EnchantException(string message, Exception innerException) : base(message, innerException) { }
	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

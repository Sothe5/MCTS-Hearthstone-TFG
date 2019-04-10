﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SabberStoneKettleSimulator
{
	class KettleServer
	{
		private Socket Listener;

		public KettleServer(IPEndPoint address)
		{
			Listener = new Socket(SocketType.Stream, ProtocolType.IP);
			Listener.Bind(address);
			Listener.Listen(32);
			Console.WriteLine("Listening on socket.");
		}

		// Keeps listening for incoming connections. For each connection, it spins up a KettleSession with his own simulator.
		public void Enter()
		{
			while (true)
			{
				Socket client = Listener.Accept();
				Console.WriteLine("Accepted client");
				try
				{
					// For now we run each session in a new thread. If/when the server needs to scale to tens/hundreds of games, this should be changed
					//KettleSessionTest session = new KettleSessionTest(client);
					KettleSession session = new KettleSession(client);
					new Thread(session.Enter).Start();
				}
				catch (IOException)
				{
					Console.WriteLine("Connection closed");
				}
			}
		}
	}
}

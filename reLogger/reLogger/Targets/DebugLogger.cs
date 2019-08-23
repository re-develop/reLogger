using reLogger.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace reLogger.Targets
{
	public class DebugLogger : ILoggerTarget
	{
		public string Name => "DebugLogger";
		public Guid Id { get; private set; }

		public HashSet<ILoggerCategory> Whitelist { get; private set; } = new HashSet<ILoggerCategory>();

		public DebugLogger()
		{
			Id = Guid.NewGuid();
		}

		public void Dispose()
		{
		}

		public void Write(string message, ILoggerCategory loggerCategory)
		{
			if (Whitelist.Contains(loggerCategory))
			{
				Debug.WriteLine(message);
			}
		}
	}
}
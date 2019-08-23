using reLogger.Abstract;
using reWork.Unmanaged;
using System;
using System.Collections.Generic;

namespace reLogger.Targets
{
	public class ConsoleLogger : ILoggerTarget
	{
		public string Name => "ConsoleLogger";

		public Guid Id { get; private set; }

		public HashSet<ILoggerCategory> Whitelist { get; private set; } = new HashSet<ILoggerCategory>();

		public ConsoleLogger()
		{
			Id = Guid.NewGuid();
		}

		public void Dispose()
		{
			if (ConsoleHandler.CreatedConsole)
				ConsoleHandler.DisposeConsole();
		}

		public void Write(string message, ILoggerCategory loggerCategory)
		{
			if (Whitelist.Contains(loggerCategory))
			{
				if (!ConsoleHandler.HasConsole())
					ConsoleHandler.CreateConsole();

				Console.WriteLine(message);
			}
		}
	}
}
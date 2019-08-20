using System;

namespace reLogger.Abstract
{
	public interface ILogger
	{
		string Name { get; }
		Guid Id { get; set; }

		ILoggerSource LoggerSource { get; }
		ILoggerCategory LoggerCategory { get; }

		void Log(string message);
	}
}
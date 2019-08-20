using System;

namespace reLogger.Abstract
{
	public interface ILoggerTarget : IDisposable
	{
		string Name { get; }
		Guid Id { get; }

		void Write(string message, ILoggerCategory loggerCategory);
	}
}
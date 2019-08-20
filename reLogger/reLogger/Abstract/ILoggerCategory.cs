using System;

namespace reLogger.Abstract
{
	public interface ILoggerCategory
	{
		string Name { get; }
		Guid Id { get; }
		bool IsEnabled { get; }
	}
}
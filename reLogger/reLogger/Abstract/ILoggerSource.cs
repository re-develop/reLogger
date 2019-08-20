using System.Collections.Generic;

namespace reLogger.Abstract
{
	public interface ILoggerSource
	{
		IReadOnlyList<ILoggerTarget> LogTargets { get; }
		IReadOnlyList<ILoggerCategory> LoggerCategories { get; }
		string LogFormat { get; set; }

		void Write(string message, ILoggerCategory loggerCategory);

		ILogger GetLogger(ILoggerCategory loggerCategory, string name);

		ILoggerCategory GetCategory(ILoggerTarget loggerTarget, string name);
	}
}
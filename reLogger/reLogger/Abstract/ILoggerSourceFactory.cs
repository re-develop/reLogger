namespace reLogger.Abstract
{
	public interface ILoggerSourceFactory
	{
		ILoggerSource CreateNew(string name, string format = default, params string[] targets);

		ILoggerSourceFactory AddTarget(ILoggerTarget loggerTarget);

		ILoggerSourceFactory RemoveTarget(ILoggerTarget loggerTarget); // Might not be required.
	}
}
namespace reLogger.Abstract
{
	public interface ILoggerFactory
	{
		ILoggerSourceFactory CreateSourceFactory();

		ILoggerCategoryFactory CreateCategoryFactory();
	}
}
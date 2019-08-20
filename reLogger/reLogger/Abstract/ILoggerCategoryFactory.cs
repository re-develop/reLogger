namespace reLogger.Abstract
{
	public interface ILoggerCategoryFactory
	{
		ILoggerCategory GetOrCreate(string name);
	}
}
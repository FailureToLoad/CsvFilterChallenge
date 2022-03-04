namespace CsvReader;

/// <summary>
/// Represents operating context for a strategy.
/// </summary>
public interface IContext
{
    /// <summary>
    /// Transition from the current context to the next context to progress through the strategy
    /// </summary>
    /// <returns>The next context required to advance the strategy</returns>
    public IContext Transition();
}
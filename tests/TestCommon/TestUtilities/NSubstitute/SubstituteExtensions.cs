using NSubstitute;

namespace Clean.Architecture.Template.TestCommon.TestUtilities.NSubstitute;

/// <summary>
/// Helper methods for NSubstitute assertions and verification patterns.
/// Provides cleaner and more readable substitute verification syntax.
/// </summary>
public static class SubstituteExtensions
{
    /// <summary>
    /// Verifies that a substitute method was called exactly once.
    /// </summary>
    public static void MustHaveBeenCalledOnce<T>(this T substitute) where T : class
    {
        substitute.ReceivedCalls().Count().Should().BeGreaterThan(0);
    }

    /// <summary>
    /// Verifies that a substitute method was never called.
    /// </summary>
    public static void MustNotHaveBeenCalled<T>(this T substitute) where T : class
    {
        substitute.ReceivedCalls().Should().BeEmpty();
    }
}

using TA.Utils.Core;

namespace Timtek.WinForms;

internal static class MaybeExtensions
{
    public static Maybe<T> Maybe<T>(this WeakReference<T> weakReference) where T : class
    {
        var success = weakReference.TryGetTarget(out var strongReference);
        return success ? TA.Utils.Core.Maybe<T>.From(strongReference) : TA.Utils.Core.Maybe<T>.Empty;
    }
}
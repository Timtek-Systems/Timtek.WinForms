namespace Timtek.WinForms.Gauge;

/// <summary>
///     Event argument for <see cref="ValueInRangeChanged" /> event.
/// </summary>
public class ValueInRangeChangedEventArgs : EventArgs
{
    public ValueInRangeChangedEventArgs(AGaugeRange range, float value, bool inRange)
    {
        Range = range;
        Value = value;
        InRange = inRange;
    }

    /// <summary>
    ///     Affected GaugeRange
    /// </summary>
    public AGaugeRange Range { get; private set; }

    /// <summary>
    ///     Gauge Value
    /// </summary>
    public float Value { get; private set; }

    /// <summary>
    ///     True if value is within current range.
    /// </summary>
    public bool InRange { get; private set; }
}
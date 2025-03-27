using System;

namespace iShape.Triangulation.Validation
{
    /// <summary>
    /// バリデーションの結果
    /// </summary>
    public enum ValidationResult
    {
        None,
        OverLap,
        CounterClockwise,
        Valid
    }
}
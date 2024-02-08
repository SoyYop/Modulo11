

using System.Runtime.CompilerServices;

/// <summary>
/// If the rut is out of bounds
/// </summary>
public class RutOutOfRangeException : System.ArgumentOutOfRangeException
    {
        public RutOutOfRangeException() { }
        public RutOutOfRangeException(string message = "Number is not within allowed range") : base(message) { }

        public RutOutOfRangeException(long leftBoundary, long rightBoundary, long number, [CallerArgumentExpression(nameof(number))] string? paramName = null) 
            : base($"Number {paramName}={number} is not within {leftBoundary} and {rightBoundary}") { }

        public RutOutOfRangeException(string message, System.Exception inner) : base(message, inner) { }

        public RutOutOfRangeException(long leftBoundary, long rightBoundary, long number, System.Exception inner, [CallerArgumentExpression(nameof(number))] string? paramName = null) 
            : base($"Number {paramName}={number} is not within {leftBoundary} and {rightBoundary}", inner) { }

    }
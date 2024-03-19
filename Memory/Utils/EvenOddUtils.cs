namespace Memory.Utils;

public static class EvenOddUtils {
    /// <summary>
    /// Calculates the nth even number using recursion.
    /// </summary>
    /// <param name="n">The position in the sequence of even numbers to calculate, starting with 0 as the first even number.</param>
    /// <returns>The nth even number.</returns>
    /// <exception cref="ArgumentException">Thrown when a negative number is passed as input.</exception>
    public static int RecursiveEven(int n) {
        if (n < 0) {
            throw new ArgumentException("Natural numbers only");
        }

        if (n == 1) {
            return 0;
        }

        return RecursiveEven(n - 1) + 2;
    }

    /// <summary>
    /// Calculates the nth number in the Fibonacci sequence using recursion.
    /// </summary>
    /// <param name="n">The position in the Fibonacci sequence to retrieve. The sequence starts with 0 as the first index.</param>
    /// <returns>The nth Fibonacci number.</returns>
    public static int RecursiveFibonacci(int n) {
        if (n == 0) {
            return 0;
        }

        if (n == 1) {
            return 1;
        }

        return RecursiveFibonacci(n - 2) + RecursiveFibonacci(n - 1);
    }

    /// <summary>
    /// Calculates the nth even number using an iterative approach.
    /// </summary>
    /// <param name="n">The position in the sequence of even numbers to calculate, starting with 0 as the first even number.</param>
    /// <returns>The nth even number.</returns>
    public static int IterativeEven(int n) {
        var result = 0;

        for (var i = 2; i <= n; i++) {
            result += 2;
        }

        return result;
    }

    /// <summary>
    /// Calculates the nth number in the Fibonacci sequence using an iterative approach.
    /// </summary>
    /// <param name="n">The position in the Fibonacci sequence to retrieve. The sequence starts with 0 as the first number.</param>
    /// <returns>The nth Fibonacci number.</returns>
    public static int IterativeFibonacci(int n) {
        if (n == 0) return 0;
        if (n == 1) return 1;

        var prev = 0;
        var curr = 1;

        for (var i = 2; i <= n; i++) {
            var next = prev + curr;
            prev = curr;
            curr = next;
        }

        return curr;
    }
}
namespace Memory.Utils;

public static class EvenOddUtils {
    public static int RecursiveEven(int n) {
        if (n < 0) {
            throw new ArgumentException("Natural numbers only");
        }

        if (n == 1) {
            return 0;
        }

        return RecursiveEven(n - 1) + 2;
    }

    public static int RecursiveFibonacci(int n) {
        if (n == 0) {
            return 0;
        }

        if (n == 1) {
            return 1;
        }

        return RecursiveFibonacci(n - 2) + RecursiveFibonacci(n - 1);
    }

    public static int IterativeEven(int n) {
        var result = 0;

        for (var i = 1; i <= n; i++) {
            result += 2;
        }

        return result;
    }

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


// Assuming the sequence 0, 1, 1, 2, 3, 5, ... (https://en.wikipedia.org/wiki/Fibonacci_sequence)

// Assuming 0 to be counted as even number #1

// Assuming 0 to be counted as even number #1
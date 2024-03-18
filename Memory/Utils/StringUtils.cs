using System.Text;

namespace Memory.CollectionExaminer;

public static class StringUtils {
    public static bool HasBalancedBrackets(string input) {
        var openers = new HashSet<char>() {
            '(',
            '[',
            '{',
        };

        var closers = new Dictionary<char, char>() {
            {')', '('},
            {']', '['},
            {'}', '{'},
        };

        var bracketsStack = new Stack<char>();

        foreach (var c in input) {
            if (openers.Contains(c)) {
                bracketsStack.Push(c);
                continue;
            }

            if (closers.ContainsKey(c)) {
                var couldPop = bracketsStack.TryPop(out var popped);

                if (!couldPop) {
                    return false;
                }

                if (popped != closers[c]) {
                    return false;
                }
            }
        }

        return bracketsStack.Count <= 0;
    }

    private static string ReverseText(string input) {
        StringBuilder stringBuilder = new StringBuilder();

        var stack = new Stack<char>();

        foreach (var c in input) {
            stack.Push(c);
        }

        while (true) {
            var couldPop = stack.TryPop(out var c);

            if (!couldPop) {
                break;
            }

            stringBuilder.Append(c);
        }

        return stringBuilder.ToString();
    }
}
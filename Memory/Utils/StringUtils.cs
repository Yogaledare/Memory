using System.Text;

namespace Memory.Utils;

public static class StringUtils {
    /// <summary>
    /// Determines whether the given string has balanced brackets.
    /// </summary>
    /// <param name="input">The string to check for balanced brackets.</param>
    /// <returns>True if the brackets in the string are balanced; otherwise, false.</returns>
    /// <remarks>
    /// This method checks for balanced parentheses '()', square brackets '[]', and curly braces '{}'.
    /// A string is considered to have balanced brackets if each opening bracket has a corresponding closing bracket of the same type and they are correctly nested.
    /// </remarks>
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

    /// <summary>
    /// Reverses the given string.
    /// </summary>
    /// <param name="input">The string to be reversed.</param>
    /// <returns>The reversed string.</returns>
    /// <remarks>
    /// This method reverses the characters in the input string using a stack to reverse the order of characters.
    /// </remarks>
    public static string ReverseText(string input) {
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
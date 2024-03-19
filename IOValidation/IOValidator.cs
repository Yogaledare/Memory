using LanguageExt.Common;
using System.ComponentModel.DataAnnotations;
using LanguageExt;

namespace IOValidation;

public static class IOValidator {
    /// <summary>
    /// Prompts the user for input, validates it using the provided validator function, and repeats the prompt if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of the validated input.</typeparam>
    /// <param name="prompt">The message to display when prompting the user.</param>
    /// <param name="validator">A function to validate the user's input.</param>
    /// <returns>The validated input of type T.</returns>
    /// <exception cref="InvalidOperationException">Thrown if parsing fails.</exception>
    public static T RetrieveInput<T>(string prompt, Func<string?, Result<T>> validator) {
        T? output = default;

        while (true) {
            Console.Write(prompt);
            var input = Console.ReadLine();
            var result = validator(input);

            bool shouldBreak = result.Match(
                Succ: validatedSentence => {
                    output = validatedSentence;
                    return true;
                },
                Fail: ex => {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            );

            if (shouldBreak) {
                break;
            }
        }

        if (output == null) throw new InvalidOperationException("Parsing failed");
        return output;
    }


    /// <summary>
    /// Validates a sentence ensuring it contains at least three words.
    /// </summary>
    /// <param name="input">The input sentence to validate.</param>
    /// <returns>A Result containing the sentence split into words if valid, otherwise an error.</returns>
    public static Result<string[]> ValidateSentence(string? input) {
        if (string.IsNullOrWhiteSpace(input)) {
            var error = new ValidationException("Error: null or empty input");
            return new Result<string[]>(error);
        }

        var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length < 3) {
            var error = new ValidationException("Error: Sentence needs to be at least three words long");
            return new Result<string[]>(error);
        }

        return tokens;
    }


    /// <summary>
    /// Validates that the input string represents a non-negative integer.
    /// </summary>
    /// <param name="input">The input string to validate.</param>
    /// <returns>A Result containing the parsed number if valid, otherwise an error.</returns>
    public static Result<int> ValidateNumber(string? input) {
        if (string.IsNullOrEmpty(input)) {
            var error = new ValidationException("Error: null or empty input");
            return new Result<int>(error);
        }

        var tokens = input.Split(' ');

        if (tokens.Length > 1) {
            var error = new ValidationException("Error: too many inputs");
            return new Result<int>(error);
        }

        if (!int.TryParse(tokens[0], out int number)) {
            var error = new ValidationException("Error: cannot parse integer");
            return new Result<int>(error);
        }

        if (number < 0) {
            var error = new ValidationException("Error: cannot have negative number");
            return new Result<int>(error);
        }

        return number;
    }


    /// <summary>
    /// Validates that the input string represents a positive integer greater than zero.
    /// </summary>
    /// <param name="input">The input string to validate.</param>
    /// <returns>
    /// A Result containing the parsed positive integer if valid; otherwise, an error.
    /// The method returns an error if the input is null, empty, not an integer, or is less than or equal to zero.
    /// </returns>
    /// <remarks>
    /// This method extends <see cref="ValidateNumber"/> to ensure the input represents a positive integer.
    /// A positive integer is defined as any whole number greater than zero.
    /// In case of failure, the Result will contain a <see cref="ValidationException"/> with an appropriate error message.
    /// </remarks>
    public static Result<int> ValidatePositiveInt(string? input) {
        var numberResult = ValidateNumber(input);

        return numberResult.Match(
            Succ: number => {
                if (number == 0) {
                    var error = new ValidationException("Error: cannot be 0");
                    return new Result<int>(error);
                }

                return number;
            },
            Fail: exception => new Result<int>(exception)
        );
    }


    /// <summary>
    /// Validates that the input string is neither null, empty, nor consists solely of whitespace.
    /// </summary>
    /// <param name="input">The input string to validate.</param>
    /// <returns>A Result containing the original string if valid; otherwise, an error.</returns>
    public static Result<string> ValidatePlainString(string? input) {
        if (string.IsNullOrEmpty(input)) {
            var error = new ValidationException("Error: null or empty input");
            return new Result<string>(error);
        }

        if (string.IsNullOrWhiteSpace(input)) {
            var error = new ValidationException("Error: null or whitespace input");
            return new Result<string>(error);
        }

        return input;
    }


    /// <summary>
    /// Represents a command with an optional value.
    /// </summary>
    /// <param name="Nav">The navigation character indicating the action to be taken.</param>
    /// <param name="MaybeValue">An optional value associated with the command. Presence and requirement of the value depends on the command type.</param>
    public record Command(char Nav, Option<string> MaybeValue);


    /// <summary>
    /// Validates user input as a command for collection manipulation, taking into account whether the collection treats '-' as requiring an argument.
    /// </summary>
    /// <param name="input">The raw input string from the user.</param>
    /// <param name="isStackOrQueue">Indicates whether the command is for a stack or queue, which affects how '-' commands are interpreted.</param>
    /// <returns>A Result containing a Command object if the input is valid; otherwise, an error.</returns>
    /// <remarks>
    /// This method differentiates between commands that do and do not require additional arguments based on the collection type (e.g., stack or queue) and the specific command given.
    /// </remarks>
    public static Result<Command> ValidateCommand(string? input, bool isStackOrQueue) {
        if (string.IsNullOrEmpty(input)) {
            var error = new InvalidOperationException("Error: null or empty input");
            return new Result<Command>(error);
        }

        var nav = input[0];
        var value = input[1..];

        var shouldTakeArg = new bool[] {
            nav is '+',
            nav is '-' && !isStackOrQueue
        };

        var shouldTakeNoArg = new bool[] {
            nav is '-' && isStackOrQueue,
            nav is 'q',
            nav is 'l' && isStackOrQueue
        };

        if (shouldTakeArg.Any(x => x)) {
            var validationResult = ValidatePlainString(value);
            return validationResult.Match(
                Succ: s => new Command(nav, Option<string>.Some(s)),
                Fail: e => new Result<Command>(e));
        }

        if (shouldTakeNoArg.Any(x => x)) {
            return string.IsNullOrWhiteSpace(value)
                ? new Result<Command>(new Command(nav, Option<string>.None))
                : new Result<Command>(new ArgumentException("Error: No argument needed"));
        }

        return new Result<Command>(new ArgumentException("Error: Invalid input"));
    }
}
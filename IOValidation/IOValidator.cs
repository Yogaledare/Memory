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


    public record Command(char Nav, Option<string> MaybeValue);


    public static Result<Command> ValidateCommand(string? input, bool isStackOrQueue) {
        if (string.IsNullOrEmpty(input)) {
            var error = new InvalidOperationException("Input is null or empty.");
            return new Result<Command>(error);
        }

        var nav = input[0];
        var value = input[1..];

        var shouldTakeNoArg = new bool[] {
            nav is '+',
            nav is '-' && !isStackOrQueue
        };

        var shouldTakeArg = new bool[] {
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
                : new Result<Command>(new ArgumentException("No argument needed"));
        }

        return new Result<Command>(new ArgumentException("Invalid input"));
    }
}



// var stringValidationResult = ValidatePlainString(value);


// var shouldTakeNoArg = nav is '+' || nav is '-' && !isStackOrQueue;
// var shouldTakeArg = nav is '-' && isStackOrQueue || nav is 'q' || nav is 'l' && isStackOrQueue;


// if (nav is '+' || nav is '-' && !isStackOrQueue) {
//     return stringValidationResult.Match(
//         Succ: s => new Command(nav, s),
//         Fail: e => new Result<Command>(e)
//     );
// }
//
// if (nav is '-' && isStackOrQueue) {
//     return stringValidationResult.Match(
//         Succ: s => hasArgumentFail,
//         Fail: e => lacksArgumentSucc
//     );
// }
//
// if (nav is '-' && !isStackOrQueue) {
//     return stringValidationResult.Match(
//         Succ: s => new Command(nav, s),
//         Fail: e => new Result<Command>(e)
//     );
// }
//
// if (nav is 'q') {
//     return stringValidationResult.Match(
//         Succ: s => hasArgumentFail,
//         Fail: e => lacksArgumentSucc
//     );
// }
//
// if (nav is 'l' && isStackOrQueue) {
//     return stringValidationResult.Match(
//         Succ: s => hasArgumentFail,
//         Fail: e => lacksArgumentSucc
//     );
// }


// var hasArgumentFail = new Result<Command>(new ArgumentException("No argument needed"));
// var lacksArgumentSucc = new Command(nav, Option<string>.None);


// var listCommands = "+-q";
// var stackQueueCommands = "+-ql";


//
//
//         
//         
// var isValueValidString = ValidatePlainString(value).Match(
//     Succ: s => true, 
//     Fail: e => false
// );
//
//


// if (!isStackOrQueue) {
//     if (nav is '+' or '-') {
//         // var stringValidationResult = ValidatePlainString(value);
//         return stringValidationResult.Match(
//             Succ: s => new Command(nav, Option<string>.Some(s)),
//             Fail: e => new Result<Command>(e)
//         );
//     }
//
// }
//
// else {
//     if (nav is '+') {
//         // var stringValidationResult = ValidatePlainString(value);
//         return stringValidationResult.Match(
//             Succ: s => new Command(nav, Option<string>.Some(s)),
//             Fail: e => new Result<Command>(e)
//         ); 
//     }
//             
//     if (nav is -) 
//             
// }
// if (!listCommands.Contains(nav)) {
//     var error = new ArgumentException("Command has to start with +, - or q");
//     return new Result<Command>(error);
// }
//
// if ()
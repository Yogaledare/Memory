using LanguageExt;
using LanguageExt.Common;
using Memory.Utils.CollectionHandlers;
using IOValidation;
using static IOValidation.IOValidator; 

namespace Memory.Utils;

public static class CollectionExaminer {
    public static void ExamineCollection(ICollectionHandler collectionHandler) {
        PrintMenu(collectionHandler);

        while (true) {
            // var commandResult = AskForCommand();
            var isStackOrQueue = collectionHandler.IsStackOrQueue;

            var command = RetrieveInput("Command: ", input => ValidateCommand(input, isStackOrQueue));

            var shouldBreakLoop = ProcessCommand(collectionHandler, command); 
            
            // var shouldBreakLoop = commandResult.Match(
            //     Succ: command => {
            //         var shouldBreak = ProcessCommand(collectionHandler, command);
            //         return shouldBreak;
            //     },
            //     Fail: exception => {
            //         Console.WriteLine((string?) exception.Message);
            //         return false;
            //     }
            // );

            if (shouldBreakLoop) {
                break;
            }
        }
    }

    private static void PrintMenu(ICollectionHandler collectionHandler) {
        Console.WriteLine();
        Console.WriteLine("Available commands:");
        Console.WriteLine($"+input to add to {collectionHandler.Name}");
        var removeCommand = collectionHandler.IsStackOrQueue ?  "-" : "-input";
        Console.WriteLine($"{removeCommand} to remove from {collectionHandler.Name}");

        if (collectionHandler.IsStackOrQueue) {
            Console.WriteLine($"l to run preset input sequence (ICA queue)");
        }

        Console.WriteLine($"q to quit");
        Console.WriteLine();
    }

    // private record Command(char Nav, Option<string> MaybeValue);

    // private static Result<IOValidator.Command> AskForCommand() {
    //     var input = Console.ReadLine();
    //     if (string.IsNullOrEmpty(input)) {
    //         var error = new InvalidOperationException("Input is null or empty.");
    //         return new Result<IOValidator.Command>(error);
    //     }
    //
    //     var nav = input[0];
    //     var value = input[1..];
    //     var maybeValue = string.IsNullOrEmpty(value) ? Option<string>.None : Option<string>.Some(value);
    //     return new IOValidator.Command(nav, maybeValue);
    // }

    private static bool ProcessCommand(ICollectionHandler collectionHandler, IOValidator.Command command) {
        var nav = command.Nav;
        var maybeValue = command.MaybeValue;

        switch (nav) {
            case '+':
                AddAction(collectionHandler, maybeValue);
                break;
            case '-':
                RemoveAction(collectionHandler, maybeValue);
                break;
            case 'l':
                // if (!collectionHandler.MenuConf.IsStackOrQueue) {
                //     Console.WriteLine("Bad input. Try again.");
                //     break;
                // }

                ICAExampleAction(collectionHandler);
                break;
            case 'q':
                Console.WriteLine($"Leaving {collectionHandler.Name} examiner...");
                return true;
            // default:
            //     Console.WriteLine("Bad input. Try again.");
            //     break;
        }

        return false;
    }

    private static void AddAction(ICollectionHandler collectionHandler, Option<string> maybeValue) {
        var performed = maybeValue.Match(
            Some: value => {
                collectionHandler.Add(value);
                Console.WriteLine($"Added {value} to {collectionHandler.Name}");
                var statusUpdate = collectionHandler.GetRepresentation();
                Console.WriteLine(statusUpdate);
                return true;
            },
            None: () => {
                Console.WriteLine("Could not add. Value needed!");
                return false;
            }
        );
    }

    private static void RemoveAction(ICollectionHandler collectionHandler, Option<string> maybeValue) {
        var removalResult = collectionHandler.Remove(maybeValue);

        var result = removalResult.Match(
            Succ: removedItem => $"Removed {removedItem} from {collectionHandler.Name}",
            Fail: err => $"Could not remove: {err.Message}");

        Console.WriteLine(result);
        var statusUpdate = collectionHandler.GetRepresentation();
        Console.WriteLine(statusUpdate);
    }

    private static void ICAExampleAction(ICollectionHandler collectionHandler) {
        AddAction(collectionHandler, "Kalle");
        AddAction(collectionHandler, "Greta");
        RemoveAction(collectionHandler, Option<string>.None);
        AddAction(collectionHandler, "Stina");
        RemoveAction(collectionHandler, Option<string>.None);
        AddAction(collectionHandler, "Olle");
    }
}
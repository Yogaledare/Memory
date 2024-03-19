using LanguageExt;
using LanguageExt.Common;
using Memory.Utils.CollectionHandlers;
using IOValidation;
using static IOValidation.IOValidator; 

namespace Memory.Utils;

/// <summary>
/// Provides functionality to examine and manipulate collections through a console interface.
/// </summary>
public static class CollectionExaminer {
    /// <summary>
    /// Initiates a loop that processes user commands to manipulate the specified collection.
    /// </summary>
    /// <param name="collectionHandler">The collection handler that defines how the collection is manipulated.</param>
    /// <remarks>
    /// The method continuously prompts the user for commands until the 'quit' command is entered.
    /// Commands include adding to, removing from, and displaying the state of the collection.
    /// </remarks>
    public static void ExamineCollection(ICollectionHandler collectionHandler) {
        PrintMenu(collectionHandler);

        while (true) {
            var isStackOrQueue = collectionHandler.IsStackOrQueue;
            var command = RetrieveInput("Command: ", input => ValidateCommand(input, isStackOrQueue));
            var shouldBreakLoop = ProcessCommand(collectionHandler, command); 

            if (shouldBreakLoop) {
                break;
            }
        }
    }

    /// <summary>
    /// Prints the command menu to the console, based on the capabilities of the provided collection handler.
    /// </summary>
    /// <param name="collectionHandler">The collection handler whose capabilities determine the available commands.</param>
    /// <remarks>
    /// This method displays a dynamic command menu which may include options to add, remove, or process items in the collection,
    /// as well as running a preset input sequence for stack or queue collections.
    /// </remarks>
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

    /// <summary>
    /// Processes a single command entered by the user, performing actions on the collection accordingly.
    /// </summary>
    /// <param name="collectionHandler">The collection handler to perform actions on.</param>
    /// <param name="command">The command to process, including the action to take and any necessary value.</param>
    /// <returns>True if the command indicates to exit the loop, otherwise false.</returns>
    /// <remarks>
    /// Commands processed include adding items to, removing items from the collection, running a preset input sequence,
    /// and quitting the examination loop.
    /// </remarks>
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
                ICAExampleAction(collectionHandler);
                break;
            case 'q':
                Console.WriteLine($"Leaving {collectionHandler.Name} examiner...");
                return true;
        }

        return false;
    }

    /// <summary>
    /// Performs an 'add' action on the collection, using the provided value.
    /// </summary>
    /// <param name="collectionHandler">The collection handler on which to perform the add action.</param>
    /// <param name="maybeValue">The value to add to the collection, if present.</param>
    /// <remarks>
    /// If no value is provided, the method outputs a message indicating that a value is needed.
    /// </remarks>
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

    /// <summary>
    /// Performs a 'remove' action on the collection, optionally using the provided value.
    /// </summary>
    /// <param name="collectionHandler">The collection handler from which to remove an item.</param>
    /// <param name="maybeValue">An optional value indicating which item to remove from the collection.</param>
    /// <remarks>
    /// The behavior of the removal depends on the collection type and whether a value is provided.
    /// </remarks>
    private static void RemoveAction(ICollectionHandler collectionHandler, Option<string> maybeValue) {
        var removalResult = collectionHandler.Remove(maybeValue);

        var result = removalResult.Match(
            Succ: removedItem => $"Removed {removedItem} from {collectionHandler.Name}",
            Fail: err => $"Could not remove: {err.Message}");

        Console.WriteLine(result);
        var statusUpdate = collectionHandler.GetRepresentation();
        Console.WriteLine(statusUpdate);
    }

    /// <summary>
    /// Runs a preset sequence of add and remove actions on the collection.
    /// </summary>
    /// <param name="collectionHandler">The collection handler on which to perform the preset actions.</param>
    /// <remarks>
    /// This method demonstrates a typical use case or test sequence for manipulating the collection,
    /// such as simulating a queue at a grocery store.
    /// </remarks>
    private static void ICAExampleAction(ICollectionHandler collectionHandler) {
        AddAction(collectionHandler, "Kalle");
        AddAction(collectionHandler, "Greta");
        RemoveAction(collectionHandler, Option<string>.None);
        AddAction(collectionHandler, "Stina");
        RemoveAction(collectionHandler, Option<string>.None);
        AddAction(collectionHandler, "Olle");
    }
}
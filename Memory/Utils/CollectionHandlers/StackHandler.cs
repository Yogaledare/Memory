using LanguageExt;
using LanguageExt.Common;

namespace Memory.Utils.CollectionHandlers;

/// <summary>
/// Handles operations for a stack collection, supporting pushing and popping items, and providing a representation of its current state.
/// </summary>
public class StackHandler : ICollectionHandler {
    /// <summary>
    /// Gets the name of the collection handler, "Stack".
    /// </summary>
    public string Name { get; } = "Stack";

    /// <summary>
    /// Indicates that this handler is for a collection that behaves like a stack or queue, which is true for StackHandler.
    /// </summary>
    public bool IsStackOrQueue { get; } = true;

    private readonly Stack<string> _stack = new Stack<string>();

    /// <summary>
    /// Adds an item to the top of the stack.
    /// </summary>
    /// <param name="item">The item to be pushed onto the stack.</param>
    public void Add(string item) {
        _stack.Push(item);
    }

    /// <summary>
    /// Removes the item from the top of the stack.
    /// </summary>
    /// <param name="maybeItem">Ignored for stacks, as removal is based on stack order.</param>
    /// <returns>The item popped from the stack, or an error if the stack is empty or an item is provided (which is not supported).</returns>
    public Result<string> Remove(Option<string> maybeItem) {
        return maybeItem.Match(
            Some: item => {
                var error = new ArgumentException("Cannot have an input to the remove command for a stack.");
                return new Result<string>(error);
            },
            None: () => {
                var didPop = _stack.TryPop(out var popped);

                if (!didPop) {
                    var error = new InvalidOperationException("Stack was already empty.");
                    return new Result<string>(error);
                }

                if (popped is null) {
                    var error = new InvalidOperationException("Popped a null string for some reason.");
                    return new Result<string>(error);
                }

                return popped;
            }
        );
    }

    /// <summary>
    /// Provides a string representation of the stack's current state.
    /// </summary>
    /// <returns>A string showing the elements currently in the stack.</returns>
    public string GetRepresentation() {
        var output = "[" + string.Join(", ", _stack) + "]";
        return output;
    }
}
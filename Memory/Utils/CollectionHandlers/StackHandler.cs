using LanguageExt;
using LanguageExt.Common;

namespace Memory.Utils.CollectionHandlers;

public class StackHandler : ICollectionHandler {

    public string Name { get; } = "Stack";
    public bool IsStackOrQueue { get; } = true;

    private readonly Stack<string> _stack = new Stack<string>();

    public void Add(string item) {
        _stack.Push(item);
    }

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

    public string GetRepresentation() {
        var output = "[" + string.Join(", ", _stack) + "]";
        return output;
    }
}


//
//
// if (!string.IsNullOrEmpty(item)) {
//     var error = new ArgumentException("Cannot have an input to the remove command.");
//     return new Result<string>(error);
// }
//
// var didPop = _stack.TryPop(out var popped);
//
// if (!didPop) {
//     var error = new InvalidOperationException("Stack was already empty.");
//     return new Result<string>(error);
// }
//
// if (popped is null) {
//     var error = new InvalidOperationException("Popped a null string for some reason.");
//     return new Result<string>(error);
// }
//
// return popped;
//

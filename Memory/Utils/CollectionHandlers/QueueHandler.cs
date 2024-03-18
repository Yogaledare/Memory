using LanguageExt;
using LanguageExt.Common;

namespace Memory.Utils.CollectionHandlers;

public class QueueHandler : ICollectionHandler {
    public MenuConf MenuConf { get; } = new MenuConf("queue", false, true);

    private readonly Queue<string> _queue = new Queue<string>();

    public void Add(string item) {
        _queue.Enqueue(item);
    }

    public Result<string> Remove(Option<string> maybeItem) {
        return maybeItem.Match(
            Some: item => {
                var error = new ArgumentException("Cannot have an input to the remove command for a queue.");
                return new Result<string>(error);
            },
            None: () => {
                var didDequeue = _queue.TryDequeue(out var removed);

                if (!didDequeue) {
                    var error = new InvalidOperationException("Queue was already empty.");
                    return new Result<string>(error);
                }

                if (removed is null) {
                    var error = new InvalidOperationException("Dequeued a null string for some reason.");
                    return new Result<string>(error);
                }

                return removed;
            }
        );
    }

    public string GetRepresentation() {
        var output = "[" + string.Join(", ", _queue) + "]";
        return output;
    }
}

//             
//             
//             
// if (!string.IsNullOrEmpty(item)) {
//     var error = new ArgumentException("Cannot have an input to the remove command.");
//     return new Result<string>(error);
// }
//
// var didDequeue = _queue.TryDequeue(out var removed);
//
// if (!didDequeue) {
//     var error = new InvalidOperationException("Queue was already empty.") ;
//     return new Result<string>(error);
// }
//
// if (removed is null) {
//     var error = new InvalidOperationException("Dequeued a null string for some reason.") ;
//     return new Result<string>(error);
// }
//
// return removed;
//
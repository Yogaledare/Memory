using LanguageExt;
using LanguageExt.Common;

namespace Memory.Utils.CollectionHandlers;

/// <summary>
/// Handles operations for a queue collection, supporting adding and removing items, and providing a representation of its current state.
/// </summary>
public class QueueHandler : ICollectionHandler {
    /// <summary>
    /// Gets the name of the collection handler, "Queue".
    /// </summary>
    public string Name { get; } = "Queue";

    /// <summary>
    /// Indicates that this handler is for a collection that behaves like a stack or queue, which is true for QueueHandler.
    /// </summary>
    public bool IsStackOrQueue { get; } = true;

    private readonly Queue<string> _queue = new Queue<string>();

    /// <summary>
    /// Adds an item to the queue.
    /// </summary>
    /// <param name="item">The item to be enqueued.</param>
    public void Add(string item) {
        _queue.Enqueue(item);
    }

    /// <summary>
    /// Removes the item at the front of the queue.
    /// </summary>
    /// <param name="maybeItem">Ignored for queues, as removal is based on queue order.</param>
    /// <returns>The item removed from the queue, or an error if the queue is empty or an item is provided (which is not supported).</returns>
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

    /// <summary>
    /// Provides a string representation of the queue's current state.
    /// </summary>
    /// <returns>A string showing the elements currently in the queue.</returns>
    public string GetRepresentation() {
        var output = "[" + string.Join(", ", _queue) + "]";
        return output;
    }
}
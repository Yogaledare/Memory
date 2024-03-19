using LanguageExt;
using LanguageExt.Common;

namespace Memory.Utils.CollectionHandlers;

/// <summary>
/// Handles operations for a list collection, allowing items to be added, removed, and represented as a string.
/// </summary>
public class ListHandler : ICollectionHandler {
    /// <summary>
    /// Gets the name of the collection handler, "List".
    /// </summary>
    public string Name { get; } = "List";

    /// <summary>
    /// Indicates whether the collection behaves like a stack or queue. For ListHandler, this is always false.
    /// </summary>
    public bool IsStackOrQueue { get; } = false;

    private readonly List<string> _list = new List<string>();

    /// <summary>
    /// Adds an item to the list.
    /// </summary>
    /// <param name="item">The item to add to the list.</param>
    public void Add(string item) {
        _list.Add(item);
    }

    /// <summary>
    /// Removes an item from the list, if present.
    /// </summary>
    /// <param name="maybeItem">An optional value indicating which item to remove from the list.</param>
    /// <returns>A Result indicating the success or failure of the remove operation, including the item removed or an error message.</returns>
    /// <remarks>
    /// If the item specified is not found in the list, an error is returned.
    /// </remarks>
    public Result<string> Remove(Option<string> maybeItem) {
        return maybeItem.Match(
            Some: item => {
                var didRemove = _list.Remove(item);

                if (!didRemove) {
                    var error = new InvalidOperationException($"Could not find {item}");
                    return new Result<string>(error);
                }

                return item;
            },
            None: () => {
                var error = new ArgumentException("Needs a non-null/empty item to remove");
                return new Result<string>(error);
            }
        );
    }

    /// <summary>
    /// Generates and returns a string representation of the current state of the list, including item count and capacity.
    /// </summary>
    /// <returns>A string that represents the current state of the list.</returns>
    public string GetRepresentation() {
        return $"Count: {_list.Count}, Capacity: {_list.Capacity}";
    }
}
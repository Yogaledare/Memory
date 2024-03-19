using LanguageExt;
using LanguageExt.Common;

namespace Memory.Utils.CollectionHandlers;

/// <summary>
/// Defines a contract for collection handlers that manage and manipulate collections.
/// </summary>
public interface ICollectionHandler {
    /// <summary>
    /// Gets the name of the collection.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Indicates whether the collection behaves like a stack or queue, affecting how items are removed.
    /// </summary>
    bool IsStackOrQueue { get; }

    /// <summary>
    /// Adds an item to the collection.
    /// </summary>
    /// <param name="item">The item to add to the collection.</param>
    void Add(string item);

    /// <summary>
    /// Removes an item from the collection, optionally using a provided value to determine which item to remove.
    /// </summary>
    /// <param name="maybeItem">An optional value indicating which item to remove from the collection, if applicable.</param>
    /// <returns>A Result containing a message about the removal operation, or an error if the operation fails.</returns>
    Result<string> Remove(Option<string> maybeItem);

    /// <summary>
    /// Generates and returns a string representation of the current state of the collection.
    /// </summary>
    /// <returns>A string that represents the current state of the collection.</returns>
    string GetRepresentation();
}

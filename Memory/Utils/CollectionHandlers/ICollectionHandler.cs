using LanguageExt;
using LanguageExt.Common;

namespace Memory.Utils.CollectionHandlers;

public interface ICollectionHandler {
    string Name { get; }

    bool IsStackOrQueue { get; }

    void Add(string item);

    Result<string> Remove(Option<string> maybeItem);

    string GetRepresentation();
}

using LanguageExt;
using LanguageExt.Common;

namespace Memory.Utils.CollectionHandlers;

public interface ICollectionHandler {
    MenuConf MenuConf { get; }
    void Add(string item);
    Result<string> Remove(Option<string> maybeItem);
    string GetRepresentation();
}

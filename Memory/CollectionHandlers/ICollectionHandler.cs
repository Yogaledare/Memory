using LanguageExt;
using LanguageExt.Common;

namespace Memory;

partial class Program {
    public interface ICollectionHandler {
        MenuConf MenuConf { get; }
        void Add(string item);
        Result<string> Remove(Option<string> maybeItem); 
        string GetRepresentation();
    }
}
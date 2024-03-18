using LanguageExt;
using LanguageExt.Common;

namespace Memory.Utils.CollectionHandlers;

public class ListHandler : ICollectionHandler {

    public string Name { get; } = "List";
    public bool IsStackOrQueue { get; } = false; 

    private readonly List<string> _list = new List<string>();

    public void Add(string item) {
        _list.Add(item);
    }

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

    public string GetRepresentation() {
        return $"Count: {_list.Count}, Capacity: {_list.Capacity}";
    }
}



//
//             
// if (string.IsNullOrEmpty(item)) {
//     var error = new ArgumentException("Needs a non-null or empty item to remove");
//     return new Result<string>(error);
// }
//
//             
//
// if (!didRemove) {
//     var error = new InvalidOperationException($"Could not find {item}");
//     return new Result<string>(error);
// }
//
// return item;
//

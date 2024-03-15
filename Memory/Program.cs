using System;
using System.Reflection.Metadata.Ecma335;
using LanguageExt.Common;

namespace SkalProj_Datastrukturer_Minne {
    class Program {
        /// <summary>
        /// The main method, vill handle the menues for the program
        /// </summary>
        /// <param name="args"></param>
        static void Main() {
            ExamineList();
        }


        private static void MainLoop() {
            while (true) {
                Console.WriteLine(
                    "Please navigate through the menu by inputting the number \n(1, 2, 3 ,4, 0) of your choice"
                    + "\n1. Examine a List"
                    + "\n2. Examine a Queue"
                    + "\n3. Examine a Stack"
                    + "\n4. CheckParenthesis"
                    + "\n0. Exit the application");
                char input = ' '; //Creates the character input to be used with the switch-case below.
                try {
                    input = Console.ReadLine()![0]; //Tries to set input to the first char in an input line
                }
                catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
                {
                    Console.Clear();
                    Console.WriteLine("Please enter some input!");
                }

                switch (input) {
                    case '1':
                        ExamineList();
                        break;
                    case '2':
                        ExamineQueue();
                        break;
                    case '3':
                        ExamineStack();
                        break;
                    case '4':
                        CheckParanthesis();
                        break;
                    /*
                     * Extend the menu to include the recursive
                     * and iterative exercises.
                     */
                    case '0':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4)");
                        break;
                }
            }
        }

        /// <summary>
        /// Examines the datastructure List
        /// </summary>
        static void ExamineList() {
            /*
             * Loop this method untill the user inputs something to exit to main menue.
             * Create a switch statement with cases '+' and '-'
             * '+': Add the rest of the input to the list (The user could write +Adam and "Adam" would be added to the list)
             * '-': Remove the rest of the input from the list (The user could write -Adam and "Adam" would be removed from the list)
             * In both cases, look at the count and capacity of the list
             * As a default case, tell them to use only + or -
             * Below you can see some inspirational code to begin working.
             */


            Console.WriteLine("Enter +input to add, -input to remove, or q to exit");
            // Console.WriteLine();
            // Console.WriteLine("Available commands:");
            // Console.WriteLine("+input to add");
            // Console.WriteLine("-input to remove");
            // Console.WriteLine("q to quit");
            Console.WriteLine();

            List<string> theList = new List<string>();

            while (true) {
                var input = Console.ReadLine();
                if (input is null) continue;
                char nav = input[0];
                string value = input.Substring(1);

                switch (nav) {
                    case '+':
                        theList.Add(value);
                        Console.WriteLine($"Added {value} to the list");
                        Console.WriteLine($"Count: {theList.Count}, Capacity: {theList.Capacity}");
                        break;
                    case '-':
                        var success = theList.Remove(value);
                        var outputString = success
                            ? $"Removed one {value} from the list"
                            : $"Could not find a {value} in the list";
                        Console.WriteLine(outputString);
                        Console.WriteLine($"Count: {theList.Count}, Capacity: {theList.Capacity}");
                        break;
                    case 'q':
                        Console.WriteLine("Leaving list examiner...");
                        return;
                    default:
                        Console.WriteLine("+input or -input or q only");
                        break;
                }
            }
        }


        private interface IQueueLike<T> {
            void Add(T item);
            T Remove();
        }


        /// <summary>
        /// Examines the datastructure Queue
        /// </summary>
        static void ExamineQueue() {
            /*
             * Loop this method untill the user inputs something to exit to main menue.
             * Create a switch with cases to enqueue items or dequeue items
             * Make sure to look at the queue after Enqueueing and Dequeueing to see how it behaves
             */
            
            Console.WriteLine("Enter +input to add, - to remove, or q to exit");
            Console.WriteLine();

            var queue = new Queue<string>();

            while (true) {
                var input = Console.ReadLine();
                if (input is null) continue;
                char nav = input[0];
                string value = input.Substring(1);
                
                switch (nav) {
                    case '+':
                        // queue.Enqueue();
                        break;
                    case '-':
                        break;
                    case 'q':
                        return;
                    default:
                        break;
                }
                
                
            }
        }

        /// <summary>
        /// Examines the datastructure Stack
        /// </summary>
        static void ExamineStack() {
            /*
             * Loop this method until the user inputs something to exit to main menue.
             * Create a switch with cases to push or pop items
             * Make sure to look at the stack after pushing and and poping to see how it behaves
             */
        }

        static void CheckParanthesis() {
            /*
             * Use this method to check if the paranthesis in a string is Correct or incorrect.
             * Example of correct: (()), {}, [({})],  List<int> list = new List<int>() { 1, 2, 3, 4 };
             * Example of incorrect: (()]), [), {[()}],  List<int> list = new List<int>() { 1, 2, 3, 4 );
             */
        }

        
        


        private record Menu(string AddName, string RemoveName, string QuitName, string CollectionName, string? RunPresetName = null); 


        private interface ICollectionHandler {
            Menu Menu { get; }
            void Add(string item);
            Result<string> Remove(string? item = default);
            string GetRepresentation();
        }


        private class ListHandler : ICollectionHandler {
            public Menu Menu { get; } = new Menu("+", "-", "q", "list", "l");

            private readonly List<string?> _list = new List<string?>();


            public void Add(string? item) {
                _list.Add(item);
            }
        
            public Result<string> Remove(string? item) {

                if (string.IsNullOrEmpty(item)) {
                    var error = "Null or empty input";
                    return new Result<string>(error); 
                }
                
                var didRemove = _list.Remove(item);

                if (!didRemove) {
                    var error = $"Could not find {item}";
                    return new Result<string>(error); 
                }

                return item; 
            }
            
        
            public string GetRepresentation() {
                return $"Count: {_list.Count}, Capacity: {_list.Capacity}";
            }
        }





    }
}




// private class QueueHandler<T> : ICollectionHandler<T> {
//     private readonly Queue<T?> _queue = new Queue<T?>();
//
//     public void Add(T? item) {
//         _queue.Enqueue(item);
//     }
//
//     public void Remove(T? item) {
//         _queue.Dequeue();
//     }
//
//     public string GetRepresentation() {
//         throw new NotImplementedException();
//     }
// }


// private interface IMenuItems {
//     string AddName { get; }
//     string RemoveName { get; }
// }







        // private static void DisplayCollectionMenu() {
        //     Console.WriteLine();
        //     Console.WriteLine("Available commands: +string or -string to add or remove");
        //     Console.WriteLine("+string to add");
        //     Console.WriteLine("-string to remove");
        //     Console.WriteLine("q to quit");
        // }

        // private static void RunCollectionLoop(ICollection<string> collection,
        //     Action<ICollection<string>, string> addAction, Action<ICollection<string>, string> removeAction,
        //     Func<ICollection<string>, string> getRepresentation) {
        //     DisplayCollectionMenu();
        //
        //     while (true) {
        //         var input = Console.ReadLine();
        //         if (input is null) continue;
        //         char nav = input[0];
        //         string value = input.Substring(1);
        //
        //         switch (nav) {
        //             case '+':
        //                 addAction(collection, value);
        //                 // addAction() collection.adda(value);
        //                 Console.WriteLine($"Added {value} to {collection.GetType()}");
        //                 var representation = getRepresentation(collection); 
        //                 if (collection is List<string> list) {
        //                     Console.WriteLine($"Count: {list.Count}, Capacity: {list.Capacity}");
        //                 }
        //
        //                 break;
        //             case '-':
        //                 removeAction(collection, value);
        //                 Console.WriteLine($"Tried removing one {value} from list");
        //                 Console.WriteLine($"Count: {theList.Count}, Capacity: {theList.Capacity}");
        //                 break;
        //             case 'q':
        //                 Console.WriteLine("Leaving list examiner...");
        //                 return;
        //             default:
        //                 Console.WriteLine("input +string or -string or q only");
        //                 break;
        //         }
        //     }
        // }



// string input = Console.ReadLine();


//switch(nav){...}

//
//
// var list = new List<string>(100);
// Console.WriteLine(list.Capacity);
// Console.WriteLine($"Count: {list.Count}, Capacity: {list.Capacity}");
//
// list.Add("hello");
// Console.WriteLine($"Count: {list.Count}, Capacity: {list.Capacity}");
//
//
// list.Add("hello");
// list.Add("hello");
// list.Add("hello");
// list.Add("hello");
//
//
// Console.WriteLine($"Count: {list.Count}, Capacity: {list.Capacity}");
//
//
// list.Add("hello");
// list.Add("hello");
// list.Add("hello");
// list.Add("hello");
// list.Add("hello");
// list.Add("hello");
// list.Add("hello");
//
// Console.WriteLine($"Count: {list.Count}, Capacity: {list.Capacity}");
//
//
// list.Add("hello");
// list.Add("hello");
//
// list.Add("hello");
// // list.Add("hello");
// list.Add("hello");
//
// list.Add("hello");
// list.Add("hello");
// list.Add("hello");            
// list.Add("hello");
// list.Add("hello");
// list.Add("hello");            
// list.Add("hello");
// list.Add("hello");
// list.Add("hello");            
// list.Add("hello");
// list.Add("hello");
// list.Add("hello");            
// list.Add("hello");
// list.Add("hello");
// list.Add("hello");            
// list.Add("hello");
//
//
// Console.WriteLine($"Count: {list.Count}, Capacity: {list.Capacity}");
//
// list.RemoveAll(s => s == "hello"); 
//
// Console.WriteLine($"Count: {list.Count}, Capacity: {list.Capacity}");
//
// Console.WriteLine();
// var array = new string[10];
//
// array[0] = "hej";
// // Console.WriteLine(array[0]);
// array[1] = "kalle"; 
//
// for (int i = 0; i < array.Length; i++) {
//     Console.WriteLine( array[i]);
// }
//
// var ingetNamn = array[2];
//
//
// Console.WriteLine();
//
// foreach (var s in array) {
//     Console.WriteLine(s);
// }
//
//
//
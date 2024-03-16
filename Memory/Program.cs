using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using LanguageExt;
using LanguageExt.ClassInstances.Const;
using LanguageExt.Common;
using Memory.CollectionExaminer.Handlers;
using static LanguageExt.Prelude;

namespace Memory {
    partial class Program {
        /// <summary>
        /// The main method, vill handle the menues for the program
        /// </summary>
        /// <param name="args"></param>
        static void Main() {
            // ExamineStack();


            // var rev = ReverseText("12345");
            // Console.WriteLine(rev);

            MainLoop();
        }


        private static void MainLoop() {
            while (true) {
                Console.WriteLine(
                    "Please navigate through the menu by inputting the number \n(1, 2, 3, 4, 0) of your choice"
                    + "\n1. Examine a List"
                    + "\n2. Examine a Queue"
                    + "\n3. Examine a Stack"
                    + "\n4. CheckParenthesis"
                    + "\n0. Exit the application");
                Console.WriteLine();
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

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Examines the datastructure List
        /// </summary>
        static void ExamineList() {
            var listHandler = new ListHandler();
            ExamineCollection(listHandler);
        }


        /// <summary>
        /// Examines the datastructure Queue
        /// </summary>
        static void ExamineQueue() {
            var queueHandler = new QueueHandler();
            ExamineCollection(queueHandler);
        }

        /// <summary>
        /// Examines the datastructure Stack
        /// </summary>
        static void ExamineStack() {
            var stackHandler = new StackHandler();
            ExamineCollection(stackHandler);
        }


        static void CheckParanthesis() {
            /*
             * Use this method to check if the paranthesis in a string is Correct or incorrect.
             * Example of correct: (()), {}, [({})],  List<int> list = new List<int>() { 1, 2, 3, 4 };
             * Example of incorrect: (()]), [), {[()}],  List<int> list = new List<int>() { 1, 2, 3, 4 );
             */
            Console.WriteLine("Input a string to check if it has balanced brackets: ");
            var input = Console.ReadLine();
            var balanced = HasStringBalancedBrackets(input ?? "");
            var insert = balanced ? "balanced" : "unbalanced";
            Console.WriteLine($"The string has {insert} brackets");
        }


        private static bool HasStringBalancedBrackets(string input) {
            var openers = new System.Collections.Generic.HashSet<char>() {
                '(',
                '[',
                '{',
            };

            Dictionary<char, char> closers = new Dictionary<char, char>() {
                {')', '('},
                {']', '['},
                {'}', '{'},
            };

            var bracketsStack = new Stack<char>();

            foreach (var c in input) {
                if (openers.Contains(c)) {
                    bracketsStack.Push(c);
                    continue;
                }

                if (closers.ContainsKey(c)) {
                    var couldPop = bracketsStack.TryPop(out var popped);

                    if (!couldPop) {
                        return false;
                    }

                    if (popped != closers[c]) {
                        return false;
                    }
                }
            }

            return bracketsStack.Count <= 0;
        }


        private static string ReverseText(string input) {
            StringBuilder stringBuilder = new StringBuilder();

            var stack = new Stack<char>();

            foreach (var c in input) {
                stack.Push(c);
            }

            while (true) {
                var couldPop = stack.TryPop(out var c);

                if (!couldPop) {
                    break;
                }

                stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }


        private static void ExamineCollection(ICollectionHandler collectionHandler) {
            PrintMenu(collectionHandler.MenuConf);

            while (true) {
                var commandResult = AskForCommand();

                var shouldBreakLoop = commandResult.Match(
                    Succ: command => {
                        var shouldBreak = ProcessCommand(collectionHandler, command);
                        return shouldBreak;
                    },
                    Fail: exception => {
                        Console.WriteLine(exception.Message);
                        return false;
                    }
                );

                if (shouldBreakLoop) {
                    break;
                }
            }
        }


        private static void PrintMenu(MenuConf menuConf) {
            Console.WriteLine();
            Console.WriteLine("Available commands:");
            Console.WriteLine($"+input to add to {menuConf.CollectionName}");
            var removeCommand = menuConf.RemoveTakesArgument ? "-input" : "-";
            Console.WriteLine($"{removeCommand} to remove from {menuConf.CollectionName}");

            if (menuConf.GivePresetOption) {
                Console.WriteLine($"l to run preset input sequence (ICA queue)");
            }

            Console.WriteLine($"q to quit");
            Console.WriteLine();
        }


        private record Command(char Nav, Option<string> MaybeValue);


        private static Result<Command> AskForCommand() {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) {
                var error = new InvalidOperationException("Input is null or empty.");
                return new Result<Command>(error);
            }

            var nav = input[0];
            var value = input[1..];
            var maybeValue = string.IsNullOrEmpty(value) ? Option<string>.None : Option<string>.Some(value);
            return new Command(nav, maybeValue);
        }

        // char nav, Option<string> maybeValue
        private static bool ProcessCommand(ICollectionHandler collectionHandler, Command command) {
            var nav = command.Nav;
            var maybeValue = command.MaybeValue;

            switch (nav) {
                case '+':
                    AddAction(collectionHandler, maybeValue);
                    break;
                case '-':
                    RemoveAction(collectionHandler, maybeValue);
                    break;
                case 'l':
                    if (!collectionHandler.MenuConf.GivePresetOption) {
                        Console.WriteLine("Bad input. Try again.");
                        break;
                    }

                    ICAExampleAction(collectionHandler);
                    break;
                case 'q':
                    Console.WriteLine($"Leaving {collectionHandler.MenuConf.CollectionName} examiner...");
                    return true;
                default:
                    Console.WriteLine("Bad input. Try again.");
                    break;
            }

            return false;
        }


        private static void AddAction(ICollectionHandler collectionHandler, Option<string> maybeValue) {
            var performed = maybeValue.Match(
                Some: value => {
                    collectionHandler.Add(value);
                    Console.WriteLine($"Added {value} to {collectionHandler.MenuConf.CollectionName}");
                    var statusUpdate = collectionHandler.GetRepresentation();
                    Console.WriteLine(statusUpdate);
                    return true;
                },
                None: () => {
                    Console.WriteLine("Could not add. Value needed!");
                    return false;
                }
            );
        }


        private static void RemoveAction(ICollectionHandler collectionHandler, Option<string> maybeValue) {
            var removalResult = collectionHandler.Remove(maybeValue);

            var result = removalResult.Match(
                Succ: removedItem => $"Removed {removedItem} from {collectionHandler.MenuConf.CollectionName}",
                Fail: err => $"Could not remove: {err.Message}");

            Console.WriteLine(result);
            var statusUpdate = collectionHandler.GetRepresentation();
            Console.WriteLine(statusUpdate);
        }


        private static void ICAExampleAction(ICollectionHandler collectionHandler) {
            AddAction(collectionHandler, "Kalle");
            AddAction(collectionHandler, "Greta");
            RemoveAction(collectionHandler, Option<string>.None);
            AddAction(collectionHandler, "Stina");
            RemoveAction(collectionHandler, Option<string>.None);
            AddAction(collectionHandler, "Olle");
        }
    }
}


//                 
// var input = Console.ReadLine();
// if (input is null) continue;
// var nav = input[0];
// var value = input.Substring(1);
//
// if (ProcessCommand(collectionHandler, nav, value)) return;
//
//
//
// // if (commandResult.IsFaulted) {
//     var  commandResult.Match(
//         Succ: command => true; 
//         
//         )
// }

// commandResult.Match(
//     Succ: command => {
//         Console.WriteLine("hello"); 
//     },
//     Fail: exception => {
//         Console.WriteLine("bello");
//     }
//
// ); 
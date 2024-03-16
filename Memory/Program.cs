﻿using System.Text;
using Memory.CollectionExaminer.Handlers;
using static Memory.CollectionExaminer.CollectionExaminer; 

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


        // char nav, Option<string> maybeValue
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
using System.Text;
using Memory.CollectionExaminer;
using Memory.CollectionExaminer.Handlers;
using static Memory.CollectionExaminer.CollectionExaminer;
using static Memory.CollectionExaminer.EvenOddUtils;

namespace Memory {
    partial class Program {
        /// <summary>
        /// The main method, vill handle the menues for the program
        /// </summary>
        /// <param name="args"></param>
        static void Main() {
            // MainLoop();
            // var fib = EvenOddUtils.IterativeFibonacci(18);

            // Console.WriteLine(fib);
        }



        
        

        private static void MainLoop() {
            while (true) {
                Console.WriteLine(
                    "Please navigate through the menu by inputting the number \n(1, 2, 3, 4, 0) of your choice"
                    + "\n1. Examine a List"
                    + "\n2. Examine a Queue"
                    + "\n3. Examine a Stack"
                    + "\n4. CheckParenthesis"
                    + "\n5. Reverse a String"
                    + "\n6. Fibonacci"
                    + "\n7. Even"
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
            Console.WriteLine("Input a string to check if it has balanced brackets: ");
            var input = Console.ReadLine();
            var balanced = StringUtils.HasBalancedBrackets(input ?? "");
            var insert = balanced ? "balanced" : "unbalanced";
            Console.WriteLine($"The string has {insert} brackets");
        }

        


    }
}


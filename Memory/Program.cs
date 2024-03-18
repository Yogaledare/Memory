using Memory.Utils.CollectionHandlers;
using static Memory.Utils.CollectionExaminer;
using static Memory.Utils.EvenOddUtils;
using static Memory.Utils.StringUtils;
using static IOValidation.IOValidator; 

namespace Memory {
    partial class Program {
        /// <summary>
        /// The main method, vill handle the menues for the program
        /// </summary>
        /// <param name="args"></param>
        static void Main() {
            MainLoop();
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
                    case '5':
                        ReverseString();
                        break;
                    case '6':
                        Fibonacci();
                        break;
                    case '7':
                        Even();
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
            const string prompt = "Input a string to check if it has balanced brackets: ";
            var input = RetrieveInput(prompt, ValidatePlainString);
            var balanced = HasBalancedBrackets(input);
            var insert = balanced ? "balanced" : "unbalanced";
            Console.WriteLine($"The string has {insert} brackets");
        }


        private static void ReverseString() {
            const string prompt = "Input a string to reverse: ";
            var input = RetrieveInput(prompt, ValidatePlainString);
            var reversed = ReverseText(input ?? "");
            Console.WriteLine($"Reversed: {reversed}");
        }


        private static void Fibonacci() {
            const string prompt = "Input positive integer n for (n:th) Fibonacci number: ";
            var input = RetrieveInput(prompt, ValidateNumber); 
            var recursiveFibonacci = RecursiveFibonacci(input);
            var iterativeFibonacci = IterativeFibonacci(input);
            var output = $"{input}:th Fibonacci number: {recursiveFibonacci} (recursive), {iterativeFibonacci} (iterative)";
            Console.WriteLine(output);
        }


        private static void Even() {
            const string prompt = "Input positive integer n for (n:th) even number: ";
            var input = RetrieveInput(prompt, ValidateNumber);
            var recursiveEven = RecursiveEven(input);
            var iterativeEven = IterativeEven(input);
            var output = $"{input}:th even number: {recursiveEven} (recursive), {iterativeEven} (iterative)";
            Console.WriteLine(output);
        }
    }
}



            
            
// Console.WriteLine("Input positive integer n for (n:th) Fibonacci number: ");
// var input = Console.ReadLine();
// if (string.IsNullOrEmpty(input)) {
//     Console.WriteLine("wrong input");
//     return;
// }
//
// var n = int.Parse(input);
//
// if (n < 0) {
//     Console.WriteLine("wrong input");
//     return;
// }



            
            
// Console.WriteLine("Input a string to reverse: ");
// var input = Console.ReadLine();



// Console.WriteLine("Input a string to check if it has balanced brackets: ");
// var input = Console.ReadLine();
// var balanced = HasBalancedBrackets(input ?? "");

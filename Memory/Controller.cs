using IOValidation;
using Memory.Utils;
using Memory.Utils.CollectionHandlers;

namespace Memory;

public class Controller {
    
    /// <summary>
    /// The main menu
    /// </summary>
    public static void MainMenu() {
        while (true) {
            Console.WriteLine(
                "Please navigate through the menu by inputting the number \n(1, 2, 3, 4, 5, 6, 7, 0) of your choice"
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
                    Controller.ExamineList();
                    break;
                case '2':
                    Controller.ExamineQueue();
                    break;
                case '3':
                    Controller.ExamineStack();
                    break;
                case '4':
                    Controller.CheckParanthesis();
                    break;
                case '5':
                    Controller.ReverseString();
                    break;
                case '6':
                    Controller.Fibonacci();
                    break;
                case '7':
                    Controller.Even();
                    break;
                case '0':
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4. 5. 6. 7)");
                    break;
            }

            Console.WriteLine();
        }
    }
    
    /// <summary>
    /// Examines the datastructure List
    /// </summary>
    public static void ExamineList() {
        var listHandler = new ListHandler();
        CollectionExaminer.ExamineCollection(listHandler);
    }

    /// <summary>
    /// Examines the datastructure Queue
    /// </summary>
    public static void ExamineQueue() {
        var queueHandler = new QueueHandler();
        CollectionExaminer.ExamineCollection(queueHandler);
    }

    /// <summary>
    /// Examines the datastructure Stack
    /// </summary>
    public static void ExamineStack() {
        var stackHandler = new StackHandler();
        CollectionExaminer.ExamineCollection(stackHandler);
    }

    /// <summary>
    /// Checks if the provided string has balanced parentheses.
    /// </summary>
    public static void CheckParanthesis() {
        const string prompt = "Input a string to check if it has balanced brackets: ";
        var input = IOValidator.RetrieveInput(prompt, IOValidator.ValidatePlainString);
        var balanced = StringUtils.HasBalancedBrackets(input);
        var insert = balanced ? "balanced" : "unbalanced";
        Console.WriteLine($"The string has {insert} brackets");
    }

    /// <summary>
    /// Prompts the user for a string and prints its reverse to the console.
    /// </summary>
    /// <remarks>
    /// This method asks the user to input a string, reverses the entered string, 
    /// and displays the reversed string. It validates the input to ensure it's not null or empty.
    /// </remarks>
    public static void ReverseString() {
        const string prompt = "Input a string to reverse: ";
        var input = IOValidator.RetrieveInput(prompt, IOValidator.ValidatePlainString);
        var reversed = StringUtils.ReverseText(input ?? "");
        Console.WriteLine($"Reversed: {reversed}");
    }

    /// <summary>
    /// Prompts the user for an integer and prints the nth Fibonacci number, both recursively and iteratively calculated.
    /// </summary>
    /// <remarks>
    /// This method requests a positive integer from the user and then calculates the corresponding Fibonacci number 
    /// in two ways: recursively and iteratively. It displays both results.
    /// The method validates the input to ensure it's a positive integer.
    /// </remarks>
    public static void Fibonacci() {
        const string prompt = "Input positive integer n for (n:th) Fibonacci number: ";
        var input = IOValidator.RetrieveInput(prompt, IOValidator.ValidateNumber);
        var recursiveFibonacci = EvenOddUtils.RecursiveFibonacci(input);
        var iterativeFibonacci = EvenOddUtils.IterativeFibonacci(input);
        var output =
            $"{input}:th Fibonacci number: {recursiveFibonacci} (recursive), {iterativeFibonacci} (iterative)";
        Console.WriteLine(output);
    }

    /// <summary>
    /// Prompts the user for an integer and prints the nth even number, calculated both recursively and iteratively.
    /// </summary>
    /// <remarks>
    /// After prompting for a positive integer, this method calculates the nth even number 
    /// using both a recursive approach and an iterative approach. Results of both calculations are displayed.
    /// Input is validated to ensure it represents a positive integer.
    /// </remarks>
    public static void Even() {
        const string prompt = "Input positive integer n for (n:th) even number: ";
        var input = IOValidator.RetrieveInput(prompt, IOValidator.ValidatePositiveInt);
        var recursiveEven = EvenOddUtils.RecursiveEven(input);
        var iterativeEven = EvenOddUtils.IterativeEven(input);
        var output = $"{input}:th even number: {recursiveEven} (recursive), {iterativeEven} (iterative)";
        Console.WriteLine(output);
    }

}
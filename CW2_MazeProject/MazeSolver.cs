using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Author: Jordan McCann
/// Student ID: 23571144
/// Date (last updated): 09/01/2019
/// File: MazeSolver.cs
/// </summary>

namespace CW2_MazeProject
{
    class LinkedNode
    {
        public char val;
        public LinkedNode next;
        public LinkedNode prev;
        public LinkedNode top;
        public LinkedNode bottom;

        //Default Constructor
        public LinkedNode()
        {
            val = ' ';
            next = null;
            prev = null;
            top = null;
            bottom = null;

        }
        //Constructor with parameter
        public LinkedNode(char value)
        {
            val = value;
            next = null;
            prev = null;
            top = null;
            bottom = null;
        }

        // STACK TRAVERSAL
        public static Stack<LinkedNode> final_path = new Stack<LinkedNode>(); // Stack to keep track of the path taken from start to end

        /// <summary>
        /// Maze Traversal Function that traverses through the maze and finds an exit,
        /// this traversal will backtrack and take another available path if the user arrives at a dead end
        /// </summary>
        /// <param name="row">Row's of the maze board for displaying</param>
        /// <param name="col">Columns of the maze board for displaying</param>
        /// <param name="startX">Start position X coordinate</param>
        /// <param name="startY">Start position Y coordinate</param>
        /// <param name="Maze">Array containing the maze data</param>
        /// <returns>STATUS: The final result showing whether or not the exit of the maze was found </returns>
        public static bool StackTraverseMaze(int row, int col, int startX, int startY, LinkedNode[,] Maze)
        {
            bool STATUS = false;

            // Stack to progress through the maze
            Stack<LinkedNode> path = new Stack<LinkedNode>();

            // Push the starting node onto the path stack
            path.Push(Maze[startX, startY]);

            // While the path stack is not empty - While there are available steps
            while (path.Count != 0)
            {
                LinkedNode currentNode = path.Pop(); // Pop the stack to access the current node

                // If current node is valid but not the starting node
                if (currentNode.val != 's' && currentNode.val == '0' && currentNode.val != 'e')
                    currentNode.val = 'v';

                // Show maze to the user each iteration
                ShowMaze(row, col, Maze);

                // If current node is the exit
                if (currentNode.val == 'e')
                {
                    STATUS = true; // SUCCESS
                    break; // Exit the loop
                }

                // Check neighbors for valid square 
                // - Try catch used to avoid null reference exception incase the program needs to assess a neighbor that might be null

                // Check top neighbor
                try
                {
                    // If valid or the exit
                    if (currentNode.top.val == '0' || currentNode.top.val == 'e')
                    {
                        path.Push(currentNode.top); // Push neighbor - move to valid square
                        final_path.Push(currentNode.top); // Push - has been visited
                        continue; // Progress to Next iteration
                    }
                }
                catch (NullReferenceException nre) { }  // Handle if neighbor that is being accessed is null

                // Check below neighbor
                try
                {
                    // If valid or the exit
                    if (currentNode.bottom.val == '0' || currentNode.bottom.val == 'e')
                    {
                        path.Push(currentNode.bottom); // Push neighbor - move to valid square
                        final_path.Push(currentNode.bottom); // Push - has been visited
                        continue; // Progress to Next iteration
                    }
                }
                catch (NullReferenceException nre) { } // Handle if neighbor that is being accessed is null

                // Check left neighbor
                try
                {
                    // If valid or the exit
                    if (currentNode.prev.val == '0' || currentNode.prev.val == 'e')
                    {
                        path.Push(currentNode.prev); // Push neighbor - move to valid square
                        final_path.Push(currentNode.prev); // Push - has been visited
                        continue; // Progress to Next iteration
                    }
                }
                catch (NullReferenceException nre) { }  // Handle if neighbor that is being accessed is null

                // Check right neighbor
                try
                {
                    // If valid or the exit
                    if (currentNode.next.val == '0' || currentNode.next.val == 'e')
                    {
                        path.Push(currentNode.next); // Push neighbor - move to valid square
                        final_path.Push(currentNode.next); // Push - has been visited
                        continue; // Progress to Next iteration
                    }
                }
                catch (NullReferenceException nre) { }  // Handle if neighbor that is being accessed is null

                // BACKTRACK IF CURRENT NODE HAS NO AVAILABLE '0' / valid steps
                // If the current node is at a deadend - then check for visited squares

                // Check below
                if (currentNode.bottom.val == 'v')
                {
                    // Push, move to that node
                    path.Push(currentNode.bottom);
                    final_path.Pop(); // Pop as the previously stored final node is no longer necessary
                    continue;// Progress to Next iteration
                }

                // Check above
                if (currentNode.top.val == 'v')
                {
                    // Push, move to that node
                    path.Push(currentNode.top);
                    final_path.Pop(); // Pop as the previously stored final node is no longer necessary
                    continue;// Progress to Next iteration
                }

                // Check Left
                if (currentNode.prev.val == 'v')
                {
                    // Push, move to that node
                    path.Push(currentNode.prev);
                    final_path.Pop(); // Pop as the previously stored final node is no longer necessary
                    continue;
                }

                // Check right
                if (currentNode.next.val == 'v')
                {
                    // Push, move to that node
                    path.Push(currentNode.next);
                    final_path.Pop(); // Pop as the previously stored final node is no longer necessary
                    continue;
                }

            }
            return STATUS; // Return result of the traversal
        }


        /// <summary>
        /// Prompts the user for input to continue the program, 
        /// Used to display the maze board to the user.
        /// </summary>
        /// <param name="row">Row's of the maze board for displaying</param>
        /// <param name="col">Column's of the maze board for displaying</param>
        /// <param name="node">Array holding the Maze data</param>
        public static void ShowMaze(int row, int col, LinkedNode[,] node)
        {
            // Format and prompt the user for input to continue the program
            Console.WriteLine();
            Console.WriteLine("Press enter to continue...");
            Console.WriteLine();
            Console.ReadKey();

            // For every row
            for (int i = 0; i < row; i++)
            {
                // For every column
                for (int j = 0; j < col; j++)
                    Console.Write(node[i, j].val + " "); // Print each section of the maze board to the user
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays the final path taken from start to finish in the maze
        /// </summary>
        /// <param name="row">Row's of the maze board for displaying</param>
        /// <param name="col">Column's of the maze board for displaying</param>
        /// <param name="node">Array holding the Maze data</param>
        public static void ShowFinalPath(int row, int col, LinkedNode[,] node)
        {
            // While the final path stack is not empty
            while(final_path.Count != 0)
            {
                // Pop and access the current node / section of the board that was traversed
                LinkedNode curr = final_path.Pop();

                // Change the value from V for visited to F for found
                if (curr.val == 'v')
                    curr.val = 'F';
            }

            // Display the maze and prompt the user
            ShowMaze(row, col, node);
            Console.WriteLine("Here is the path taken! (labelled F for Found)");
        }
    }

    class MazeSolver
    {
        static void Main(string[] args)
        {
            //path to the file. This file you could read from the command line
            // Takes a commandline argument for a file - files must be in the same directory as the executable
            Console.Write("Please enter a maze file: ");
            string path = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            //First line holds row and column information
            string[] line = lines[0].Split(' ');
            int row = Convert.ToInt32(line[0]);
            int col = Convert.ToInt32(line[1]);
            //holding 0s 1s and starting point 's' and exit point 'o'
            char[,] maze_board = new char[row, col];

            for (int i = 1; i < lines.Length; i++)
            {
                //String to char array
                char[] arr = lines[i].ToCharArray();

                for (int j = 0; j < arr.Length; j++)
                    maze_board[i - 1, j] = arr[j];
            }
            //Display the maze now

            Console.WriteLine("Here is the maze you have chosen...");
            Console.WriteLine();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    Console.Write(maze_board[i, j] + " ");
                Console.WriteLine();
                //Console.WriteLine();
            }

            //An array of nodes to create a four-way linked list for traversal of the maze
            LinkedNode[,] node = new LinkedNode[row, col];

            //Holds the start coords of the maze entrance
            int startX = -1, startY = -1;

            //Nested for loops that index and populate a 2d dimensional array which will be the maze
            for (int i = 0; i < lines.Length - 1; i++)
            {
                //Converts the maze file to an array of characters
                char[] arr = lines[i + 1].ToCharArray();
                for (int j = 0; j < arr.Length; j++)
                {
                    //Sets the array of nodes to be the number of tiles in the maze
                    node[i, j] = new LinkedNode(arr[j]);
                    //If the start of the maze is found, store the array coordinates
                    if (node[i, j].val == 's' || node[i, j].val == 'o')
                    {
                        startX = i;
                        startY = j;
                    }
                    //Checks that the left side of the maze tile is inside the array bounds
                    if (j - 1 >= 0)
                    {
                        //creates a link between the previous and next nodes
                        node[i, j].prev = node[i, j - 1];
                        node[i, j - 1].next = node[i, j];
                    }
                    //Does the same but with the top and bottom tiles
                    if (i - 1 >= 0)
                    {
                        node[i, j].top = node[i - 1, j];
                        node[i - 1, j].bottom = node[i, j];
                    }
                }
            }


            // Run the stack traversal of the maze and recieve the final outcome
            bool result = LinkedNode.StackTraverseMaze(row, col, startX, startY, node);

            // If the final result is true - success
            if (result == true)
            {
                // Prompt the user
                Console.WriteLine("Congrats, Maze is solved");
                // Show the final path taken from start to exit
                LinkedNode.ShowFinalPath(row, col, node);
            }
            // If false - fail
            else
                Console.WriteLine("UH OHHHH!!! The maze has not been solved..."); // Prompt the user
        }
    }
}

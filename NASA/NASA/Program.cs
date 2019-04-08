using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASA
{
    class Program
    {
        static char[] delimiter = new char[] { ',' };
        static char[] validHeadings = new char[] { 'N', 'E', 'S', 'W' };
        static char[] validTurnMoveInstructions = new char[] { 'L', 'R', 'M' };
        const string North = "N";
        const string East = "E";
        const string South = "S";
        const string West = "W";

        static void Main(string[] args)
        {
            do {
                int[] explorationGridSize = GetExplorationGridSize();
                string[] rover1CurrentPositionAndHeading = GetRoverCurrentPositionAndHeading("1");
                char[] rover1TurnMoveInstructions = GetRoverTurnMoveInstructions("1");
                string[] rover2CurrentPositionAndHeading = GetRoverCurrentPositionAndHeading("2");
                char[] rover2TurnMoveInstructions = GetRoverTurnMoveInstructions("2");

                string rover1result = NavigateRover(explorationGridSize, rover1CurrentPositionAndHeading, rover1TurnMoveInstructions);
                string rover2result = NavigateRover(explorationGridSize, rover2CurrentPositionAndHeading, rover2TurnMoveInstructions);

                Console.WriteLine(rover1result);
                Console.WriteLine(rover2result);
            } while (true);
        }

        private static string NavigateRover(int[] ExplorationGridSize, string[] RoverCurrentPositionAndHeading, char[] RoverTurnMoveInstructions)
        {
            foreach (char instruction in RoverTurnMoveInstructions)
            {
                switch (instruction.ToString())
                {
                    case "L":
                    case "R":
                        TurnRover(ref RoverCurrentPositionAndHeading, instruction.ToString());
                        break;
                    case "M":
                        MoveRover(ref RoverCurrentPositionAndHeading, instruction.ToString());
                        break;
                }
            }

            return String.Join(",", RoverCurrentPositionAndHeading);
        }

        private static void MoveRover(ref string[] RoverCurrentPositionAndHeading, string Instruction)
        {
            int roverPositionX = int.Parse(RoverCurrentPositionAndHeading[0]);
            int roverPositionY = int.Parse(RoverCurrentPositionAndHeading[1]);

            switch (RoverCurrentPositionAndHeading[2])
            {
                case North:
                    roverPositionY++;
                    break;
                case East:
                    roverPositionX++;
                    break;
                case South:
                    roverPositionY--;
                    break;
                case West:
                    roverPositionX--;
                    break;
            }

            RoverCurrentPositionAndHeading[0] = roverPositionX.ToString();
            RoverCurrentPositionAndHeading[1] = roverPositionY.ToString();
        }

        private static void TurnRover(ref string[] RoverCurrentPositionAndHeading, string instruction)
        {
            string currentHeading = RoverCurrentPositionAndHeading[2];
            switch (instruction)
            {
                case "L":
                    if (currentHeading == North)
                        currentHeading = West;
                    else if (currentHeading == East)
                        currentHeading = North;
                    else if (currentHeading == South)
                        currentHeading = East;
                    else if (currentHeading == West)
                        currentHeading = South;
                    break;
                case "R":
                    if (currentHeading == North)
                        currentHeading = East;
                    else if (currentHeading == East)
                        currentHeading = South;
                    else if (currentHeading == South)
                        currentHeading = West;
                    else if (currentHeading == West)
                        currentHeading = North;
                    break;
            }
            RoverCurrentPositionAndHeading[2] = currentHeading;
        }

        private static char[] GetRoverTurnMoveInstructions(string RoverId)
        {
            char[] turnMoveInstructionsArray;
            bool validInstructions;

            do
            {
                Console.Write(String.Format("Enter rover {0} turn/move instructions ({1}): ", RoverId, String.Join(",", validTurnMoveInstructions)));
                turnMoveInstructionsArray = Console.ReadLine().Trim().ToUpper().ToCharArray();

                validInstructions = true;
                foreach (var instruction in turnMoveInstructionsArray)
                {
                    if (!validTurnMoveInstructions.Contains(instruction))
                    {
                        validInstructions = false;
                        Console.WriteLine(String.Format("Error: Invalid turn/move instruction \"{0}\" detected. Please enter the following values only: {1}.", instruction, String.Join(",", validTurnMoveInstructions)));
                        break;
                    }
                }

            } while (!validInstructions);

            return turnMoveInstructionsArray;
        }

        private static string[] GetRoverCurrentPositionAndHeading(string RoverId)
        {
            string roverPositionAndHeadingInput;
            string[] roverPositionAndHeadingArray;
            bool validPositionAndHeading = false;
            int dummy;

            do {
                Console.Write(String.Format("Enter rover {0} current position and heading (x,y,z): ", RoverId));
                roverPositionAndHeadingInput = Console.ReadLine().Trim().ToUpper();

                roverPositionAndHeadingArray = roverPositionAndHeadingInput.Split(delimiter);

                if (roverPositionAndHeadingArray.Count() != 3
                    || (!int.TryParse(roverPositionAndHeadingArray[0], out dummy) || !int.TryParse(roverPositionAndHeadingArray[1], out dummy)
                    || roverPositionAndHeadingArray[2].IndexOfAny(validHeadings) == -1))
                {
                    Console.WriteLine("Error: Invalid coordinates and/or heading. Enter two numbers and a compass heading delimited by a comma. EG 5,4,N");
                }
                else
                {
                    validPositionAndHeading = true;
                }

            } while (!validPositionAndHeading);

            return roverPositionAndHeadingArray;
        }

        private static int[] GetExplorationGridSize()
        {
            string gridSizeInput;
            string[] gridSizeArray;
            int[] gridSize = new int[2];
            bool validCoordinates = false;

            do {
                Console.Write("Enter Grid Size (x,y): ");
                gridSizeInput = Console.ReadLine();

                gridSizeArray = gridSizeInput.Split(delimiter);

                if (gridSizeArray.Count() != 2 || (!int.TryParse(gridSizeArray[0], out gridSize[0]) || !int.TryParse(gridSizeArray[1], out gridSize[1])))
                    Console.WriteLine("Error: Invalid coordinates. Enter two numbers delimited by a comma. EG. 12,2");
                else
                    validCoordinates = true;

            } while (!validCoordinates);

            return gridSize;
        }
    }
}

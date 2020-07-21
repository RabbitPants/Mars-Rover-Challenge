using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarsRover
{
    public class Rover
    {
        public int? StartPosX;
        public int EndPosX;
        public int? StartPosY;
        public int EndPosY;
        public int? StartDirection;     //N = 90, W = 180, S = 270, E = 0
        public int EndDirection;
        public string EndDirectionLetter;
        public string RoverInstruction;

        public Rover() { }

        public Rover(int? _startX, int? _startY, int? _startDirection, string _instruction) {
            StartPosX = _startX;
            StartPosY = _startY;
            StartDirection = _startDirection;
            RoverInstruction = _instruction;
        }

        public Rover(int _startX, int _endX, int _startY, int _endY, int _startDirection, int _endDirection,
            string _endDirectionLetter, string _instruction) {
            StartPosX = _startX;
            EndPosX = _endX;
            StartPosY = _startY;
            EndPosY = _endY;
            StartDirection = _startDirection;
            EndDirection = _endDirection;
            EndDirectionLetter = _endDirectionLetter;
            RoverInstruction = _instruction;
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n");
            Console.WriteLine(@"

      ___           ___           ___           ___                 ___           ___           ___           ___           ___     
     /\__\         /\  \         /\  \         /\  \               /\  \         /\  \         /\__\         /\  \         /\  \    
    /::|  |       /::\  \       /::\  \       /::\  \             /::\  \       /::\  \       /:/  /        /::\  \       /::\  \   
   /:|:|  |      /:/\:\  \     /:/\:\  \     /:/\ \  \           /:/\:\  \     /:/\:\  \     /:/  /        /:/\:\  \     /:/\:\  \  
  /:/|:|__|__   /::\~\:\  \   /::\~\:\  \   _\:\~\ \  \         /::\~\:\  \   /:/  \:\  \   /:/__/  ___   /::\~\:\  \   /::\~\:\  \ 
 /:/ |::::\__\ /:/\:\ \:\__\ /:/\:\ \:\__\ /\ \:\ \ \__\       /:/\:\ \:\__\ /:/__/ \:\__\  |:|  | /\__\ /:/\:\ \:\__\ /:/\:\ \:\__\
 \/__/~~/:/  / \/__\:\/:/  / \/_|::\/:/  / \:\ \:\ \/__/       \/_|::\/:/  / \:\  \ /:/  /  |:|  |/:/  / \:\~\:\ \/__/ \/_|::\/:/  /
       /:/  /       \::/  /     |:|::/  /   \:\ \:\__\            |:|::/  /   \:\  /:/  /   |:|__/:/  /   \:\ \:\__\      |:|::/  / 
      /:/  /        /:/  /      |:|\/__/     \:\/:/  /            |:|\/__/     \:\/:/  /     \::::/__/     \:\ \/__/      |:|\/__/  
     /:/  /        /:/  /       |:|  |        \::/  /             |:|  |        \::/  /       ~~~~          \:\__\        |:|  |    
     \/__/         \/__/         \|__|         \/__/               \|__|         \/__/                       \/__/         \|__|    

            ");
            Console.WriteLine();
            Console.WriteLine("******************************************** Welcome to Mars Rover Explorer ********************************************\n");
            Console.WriteLine("Rover Positions:\n");
            Rover roverN = new Rover();
            roverN.ReadRoverFile();
        }

        /*
         * Reads the instruction file containing the commands for each rover
         */
        public void ReadRoverFile()
        {
            string directory = Path.GetFullPath("Rover_Instructions.txt");
            StreamReader myReader = new StreamReader(directory);
            string line = "";
            string[] values;
            int plateauLine = 0;
            int plateauX = 0;
            int plateauY = 0;
            int count = 0;
            List<Rover> RoverList = new List<Rover>();

            while (line != null)
            {
                line = myReader.ReadLine();

                //Sets the size of the plateau
                if (plateauLine == 0)
                {
                    values = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    plateauX = Convert.ToInt16(values[0]);
                    plateauY = Convert.ToInt16(values[1]);
                    plateauLine++;
                }
                //Sets starting position and movement instructions
                else
                {
                    if (line != null && line != "")
                    {
                        values = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        //Determines if line is a starting position
                        if(values.Length > 1)
                        {
                            count = 1;
                            StartPosX = Convert.ToInt16(values[0]);
                            StartPosY = Convert.ToInt16(values[1]);
                            switch (values[2])
                            {
                                case "E":
                                    StartDirection = 0;
                                    break;
                                case "N":
                                    StartDirection = 90;
                                    break;
                                case "W":
                                    StartDirection = 180;
                                    break;
                                case "S":
                                    StartDirection = 270;
                                    break;
                                default:
                                    StartDirection = 90;
                                    break;
                            }
                        }
                        //Determines if line is a movement instruction
                        else
                        {
                            count = 2;
                            RoverInstruction = values[0].Trim();
                        }

                        if (StartPosX != null && StartPosY != null && StartDirection != null && RoverInstruction != null && count == 2)
                        {
                            Rover rover = new Rover(StartPosX, StartPosY, StartDirection, RoverInstruction);
                            RoverList.Add(rover);
                        }
                    }
                }
            }

            myReader.Close();
            ActionRoverCoordinates(RoverList, plateauX, plateauY);

            Console.ReadLine();
        }

        /*
         * Sets object properties and calculates coordinates
         */
        public void ActionRoverCoordinates(List<Rover> rovers, int plateauX, int plateauY)
        {
            foreach(var rover in rovers)
            {
                int degree = Convert.ToInt16(rover.StartDirection);
                int xCoord = Convert.ToInt16(rover.StartPosX);
                int yCoord = Convert.ToInt16(rover.StartPosY);
                char[] instructions = rover.RoverInstruction.ToCharArray();
                for(var i = 0; i < instructions.Length; i++)
                {
                    switch (instructions[i])
                    {
                        case 'L':
                        case 'l':
                            degree += 90;
                            degree = degree > 270 ? 0 : degree;
                            rover.EndDirection = degree;
                            rover.EndDirectionLetter = degree == 0 ? "E" : degree == 90 ? "N" : degree == 180 ? "W" : "S";
                            break;
                        case 'R':
                        case 'r':
                            degree -= 90;
                            degree = degree < 0 ? 270 : degree;
                            rover.EndDirection = degree;
                            rover.EndDirectionLetter = degree == 0 ? "E" : degree == 90 ? "N" : degree == 180 ? "W" : "S";
                            break;
                        case 'M':
                        case 'm':
                            switch (degree)
                            {
                                case 0:
                                    xCoord += 1;
                                    rover.EndDirection = 0;
                                    rover.EndDirectionLetter = "E";
                                    break;
                                case 90:
                                    yCoord += 1;
                                    rover.EndDirection = 90;
                                    rover.EndDirectionLetter = "N";
                                    break;
                                case 180:
                                    xCoord -= 1;
                                    rover.EndDirection = 180;
                                    rover.EndDirectionLetter = "E";
                                    break;
                                case 270:
                                    yCoord -= 1;
                                    rover.EndDirection = 270;
                                    rover.EndDirectionLetter = "S";
                                    break;
                                default:
                                    rover.EndDirection = Convert.ToInt16(rover.StartDirection);
                                    rover.EndDirectionLetter = rover.EndDirection == 0 ? "E" : rover.EndDirection == 90 ? "N" :
                                        rover.EndDirection == 180 ? "W" : "S";
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
                rover.EndPosX = xCoord;
                rover.EndPosY = yCoord;
            }
            DrawGridOutput(rovers, plateauX, plateauY);
        }

        /*
         * Output grid to console window
         */
        public void DrawGridOutput(List<Rover> rovers, int plateauX, int plateauY)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string roverSummary = "";
            int count = 0;
            for(int y = plateauY; y >= 0; y--)
            {
                for(int xTop = 0; xTop <= plateauX; xTop++)
                {
                    Console.Write(" ┌───┐ ");
                }
                Console.WriteLine();
                for(int xMid = 0; xMid <= plateauX; xMid++)
                {
                    string gridString = " │   │ ";
                    var E = rovers.Any(x => x.EndPosX == xMid && x.EndPosY == y && x.EndDirection == 0) ? gridString = " │ › │ " : null;
                    var N = rovers.Any(x => x.EndPosX == xMid && x.EndPosY == y && x.EndDirection == 90) ? gridString = " │ ˄ │ " : null;
                    var W = rovers.Any(x => x.EndPosX == xMid && x.EndPosY == y && x.EndDirection == 180) ? gridString = " │ ‹ │ " : null;
                    var S = rovers.Any(x => x.EndPosX == xMid && x.EndPosY == y && x.EndDirection == 270) ? gridString = " │ ˅ │ " : null;

                    Console.Write(gridString);
                }
                Console.WriteLine();
                for(int xBot = 0; xBot <= plateauX; xBot++)
                {
                    Console.Write(" └───┘ ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            foreach (Rover rover in rovers)
            {
                count++;
                if (rover.EndPosX <= plateauX && rover.EndPosY <= plateauY)
                {
                    roverSummary += "Rover " + count + " Coordinates: " + rover.EndPosX + " " + rover.EndPosY +
                    " " + rover.EndDirectionLetter + ". \n";
                }
                else
                {
                    roverSummary += "Rover " + count + " is out of bounds. \n";
                }
            }
            Console.WriteLine(roverSummary);
        }
    }
}

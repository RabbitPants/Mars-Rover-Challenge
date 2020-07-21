Installation:

Open a console window change the directory to where the repo is located. Then run the following command:
dotnet publish -c Debug -r win10-x64
Navigate to the ~\MarsRover\MarsRover\bin\Debug\netcoreapp3.1\ folder and double click MarsRover.exe
Ensure the Rover_Instructions.txt file contains the rover instructions, as well as the required values for each rover (See assumptions).

Assumptions:

1.The first line in the 'Rover_Instructions.txt' file will always be the Plateau's size.
2.The Plateau's x- and y-coordinates will always be separated by a space (" ").
3.The next (not empty or null) line in the 'Rover_Instructions.txt' file will always be the starting position and direction of the rover.
4.The starting point x- and y-coordinates and direction will always be separated by a space (" ") and the direction will always be one character.
5.The starting point line must contain 3 values, one for X, Y and direction.
5.The next (not empty or null) line must contain a string of movement instructions containing the characters 'L', 'R', or 'M'.
6.Any subsequent (not empty or null) lines after the movement instructions, will apply to an additional Rover's starting position followed by its movement instructions.
7.The 'Rover_Instructions.txt' file must always be in the following directory: ~\MarsRover\bin\Debug\netcoreapp3.1\
8.Rovers cannot move out of the plateau or its signal will be lost and will vanish from the grid.

Sources:

https://www.csharp-console-examples.com/methods/draw-a-rectangle-in-console-application-using-static-method/
https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener?redirectedfrom=MSDN&view=netcore-3.1 DID NOT USE
https://docs.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2019
https://dzone.com/articles/generate-an-exe-for-net-core-console-apps-net-core

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;
using System.Collections.Generic;

namespace RoverTest
{
    [TestClass]
    public class RoverTest
    {
        [TestMethod]
        public void ActionRoverCoordinatesTest()
        {
            int plateauX = 5;
            int plateauY = 5;
            int StartPosX = 0;
            int ExpectedEndPosX = 3;
            int? StartPosY = 0;
            int ExpectedEndPosY = 3;
            int? StartDirection = 90;     //N = 90, W = 180, S = 270, E = 0
            //int ExpectedEndDirection = 0;
            //string ExpectedEndDirectionLetter = "E";
            string RoverInstruction = "RMLMRMLMR";
            Rover TestRover = new Rover(StartPosX, StartPosY, StartDirection, RoverInstruction);
            List<Rover> TestRovers = new List<Rover>();

            TestRovers.Add(TestRover);

            TestRover.ActionRoverCoordinates(TestRovers, plateauX, plateauY);

            //Assert
            Assert.AreEqual(ExpectedEndPosX, TestRover.EndPosX, 1, "TestRover X-Coordinates do not match");
            Assert.AreEqual(ExpectedEndPosY, TestRover.EndPosY, 1, "TestRover Y-Coordinates do not match");
            Assert.IsTrue(TestRover.EndPosX < plateauX && TestRover.EndPosX >= 0, "TestRover out of bounds on the X-Coordinate");
            Assert.IsTrue(TestRover.EndPosY < plateauY && TestRover.EndPosY >= 0, "TestRover out of bounds on the Y-Coordinate");
        }
    }
}

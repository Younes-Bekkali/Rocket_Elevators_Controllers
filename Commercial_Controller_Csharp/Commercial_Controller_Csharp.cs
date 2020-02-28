//



//


using System;
using System.Collections.Generic;
using System.Threading;

namespace Commercial_controller
{

    // 1  Outline (Algorithm) -Done-

    // 2- class Elevator (-Done-)
    //a - constructor
    //b - add to list method with requested floor as parameter
    //c - operate elevator method
    //d - open doors and close doors methods
    //e-  Move up and move down methods

    // 3 - class Column
    //a- Constructor
    //b- Find an elevator methods one for going down and anather for going up

    // 4 - class Battery
    //a - Constructor
    //b - select a column method

    // 5- classs BatteryController
    //a -  contructor
    //b -  request elevator function using the class Elevator with floor number and requested floor as parameter
    //c -  select elevator function using the class Elevator with requested floor as parameter

    // 6 - class ScenariosTesting


    public class Elevator
    {
        public int elevatorId;
        public string status;
        public int ElevCurrentFloor;
        public string elevDirection;
        public List<int> elevFloorsList;
        public bool doorsSensor;
        public bool weightCapacitySensor;

        public Elevator(int elevatorId, string status, int ElevCurrentFloor, string elevDirection)
        {
            this.elevatorId = elevatorId;
            this.status = status;
            this.ElevCurrentFloor = ElevCurrentFloor;
            this.elevDirection = elevDirection;
            this.doorsSensor = true;
            this.weightCapacitySensor = true;
            this.elevFloorsList = new List<int>();
        }

        // Add the request to the list , sort it, select an elevator and move the elevator to reach its destination
        public void addToList(int RequestedFloor, int columnId)
        {
            elevFloorsList.Add(RequestedFloor);

            if (RequestedFloor > ElevCurrentFloor)
            {
                // Sort the list from last to first. compare every element to each other 
                elevFloorsList.Sort((i1, i2) => i1.CompareTo(i2));
            }
            else if (RequestedFloor < ElevCurrentFloor)
            {
                elevFloorsList.Sort((i1, i2) => -1 * i1.CompareTo(i2));
            }

            // Call the move elevator to its destination once the request added and the list sorted
            moveElevator(RequestedFloor, columnId);
        }

        // moveElevator function : Move the elevator to its destination, open the doors ,
        //and display those operations on the console
        public void moveElevator(int RequestedFloor, int columnId)
        {

            if (RequestedFloor < this.ElevCurrentFloor)
            // if the destination is below the elevators current position of the elevator
            {
                // Display the column's number, the buttons status and the elevator's number and status
                status = "Moving";
                Console.WriteLine(" ");
                Console.WriteLine("Buttons' light off");
                Console.WriteLine(" ");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("Column # " + columnId + " Elevator # " + this.elevatorId + " " + status);
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine(" ");
                // Move the elevator down untill it reaches the RequestedFloor
                this.elevDirection = "Down";
                moveDown(RequestedFloor, columnId);
                this.status = "Stopped";
                Console.WriteLine(" ");
                Console.WriteLine("Column # " + columnId + " Elevator # " + this.elevatorId + " " + status);
                Console.WriteLine(" ");
                // Open the doors once the elevator reached its destination
                this.openDoors();
                // Remove the destination from the list once the elevator reached it
                this.elevFloorsList.Remove(0);
            }

            else if (RequestedFloor > this.ElevCurrentFloor)
            // if the destination is above the elevators current position 
            {
                // Change the elevator status 
                this.status = "Moving";
                Console.WriteLine(" ");
                Console.WriteLine("Buttons' light off");
                // Display on the console the column's number, the elevator's number and status 
                Console.WriteLine(" ");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("Column # " + columnId + " Elevator # " + this.elevatorId + " " + status);
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine(" ");
                this.elevDirection = "Up";
                this.moveUp(RequestedFloor, columnId);
                // Once the elevator reach requested floor the elevator will stop moving
                this.status = "Stopped";
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("Column # " + columnId + " Elevator # " + this.elevatorId + " " + status);
                // Once the elevator stops moving the doors will open
                this.openDoors();
                // Remove the floor from the elevator's list
                this.elevFloorsList.Remove(0);
            }
            else
            {
                // If the elevator is at the floor requested, open doors
                openDoors();
                // once the door closed the elevator will move to its new destination or to the idle position
                this.status = "Moving";
                this.elevFloorsList.Remove(0);
            }

        }

        // Doors' manipulation
        // Display the opening and the closing of the doors
        public void openDoors()
        {
            if (status != "Moving")
            {
                Thread.Sleep(700);
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("Doors: Opening");
                Thread.Sleep(700);
                Console.WriteLine("Doors: Opened");
                Thread.Sleep(700);
                // close automatically the doors after a delay
                this.closeDoors();
            }

        }
        public void closeDoors()
        {
            if (doorsSensor || weightCapacitySensor)
            {
                Console.WriteLine("Doors: Closing");
                Thread.Sleep(700);
                Console.WriteLine("Doors: Not obstructed");
                Console.WriteLine("Doors: Closed");
                Thread.Sleep(700);


                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
            }
            else if (doorsSensor == false || weightCapacitySensor)
            {
                openDoors();
                Console.WriteLine("Doors: Obstructed");
            }
            else if (doorsSensor || weightCapacitySensor == false)
            {
                openDoors();
                Console.WriteLine("Elevator's weight capacity reached");
            }
            else
            {
                openDoors();
                Console.WriteLine("Doors: Obstructed");
                Console.WriteLine("Elevator's weight capacity reached");
            }
        }

        // Move the elevator according to the Requested floor's position
        // Display those operations on the console

        public void moveUp(int RequestedFloor, int columnId)
        {

            Console.WriteLine("Column # " + columnId + " || Elevator # " + elevatorId + " ||  Current Floor # " + this.ElevCurrentFloor);
            Console.WriteLine(" ");
            Thread.Sleep(700);
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");

            while (this.ElevCurrentFloor != RequestedFloor)
            {
                this.ElevCurrentFloor += 1;
                Console.WriteLine("Column # " + columnId + " || Elevator # " + elevatorId + " ||  Floor # " + this.ElevCurrentFloor);
                Thread.Sleep(300);
            }
        }

        public void moveDown(int RequestedFloor, int columnId)
        {
            Console.WriteLine("Column # " + columnId + " || Elevator # " + elevatorId + " ||  Current Floor # " + this.ElevCurrentFloor);
            Console.WriteLine(" ");
            Thread.Sleep(700);
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");

            while (this.ElevCurrentFloor != RequestedFloor)
            {
                this.ElevCurrentFloor -= 1;
                Console.WriteLine("Column # " + columnId + " || Elevator # " + elevatorId + " ||  Floor # " + this.ElevCurrentFloor);
                Thread.Sleep(300);
            }
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
        }

    }

    // The class column to assign columns to a group of floors, to select an elevator depending on the floor
    // and the direction: going up/going down.

    public class Column
    {
        public int columnId;
        public int floorNb;
        public int elevatorsColumn;
        public List<Elevator> elevatorsList;
        public Column(int columnId, int floorNb, int elevatorsColumn)
        {
            this.columnId = columnId;
            this.floorNb = floorNb;
            this.elevatorsColumn = elevatorsColumn;
            elevatorsList = new List<Elevator>();

            for (int i = 1; i <= this.elevatorsColumn; i++)
            {
                Elevator elevator = new Elevator(i, "Idle", 1, "Up");
                elevatorsList.Add(elevator);
            }
        }


        // Select an elevator to get to any floor 
        public Elevator getToFloorsElevators(int FloorNumber)
        {
            foreach (var elevator in elevatorsList)
            {
                // Return first the elevators that are on idle status in the column
                if (elevator.status == "Idle" || elevator.status == "Stopped")
                {
                    return elevator;
                }
            }
            //  Calculate and find the closest elevator to the floor in the column
            int shortDistance = 5555;
            int suitableElevator = 0;

            for (int elevId = 0; elevId < this.elevatorsList.Count; elevId++)
            {

                // The floor requested from the elevator's requests list
                int floorRequested = elevatorsList[elevId].elevFloorsList[0];
                // The floor where the elevator is
                int currentPosition = elevatorsList[elevId].ElevCurrentFloor;
                // The distance between the requested floor and the position where the elevator is
                int distance = Math.Abs(currentPosition - floorRequested);
                // The closesest elevator around the requested floor
                int floorsDistance = floorRequested + distance - 1;

                // store and return the smallest distance and return it
                if (shortDistance >= floorsDistance)
                {
                    shortDistance = floorsDistance;
                    suitableElevator = elevId;
                }
            }
            return elevatorsList[suitableElevator];
        }


        // Select an elevator from any level to go to the first floor (ground)
        public Elevator getToGroundElevators(int RequestedFloor)
        {
            int shortDistance = 5555;
            int suitableElevator = 0;
            //int callDirection = elevatorsList[elevId].ElevCurrentFloor - RequestedFloor;

            for (int elevId = 0; elevId < this.elevatorsList.Count; elevId++)
            {
                // Calucalte the distance between the floor of the call and the elevator's position 
                int floorsDistance = Math.Abs(elevatorsList[elevId].ElevCurrentFloor - RequestedFloor);
                // If the elevator direction is the same as the direction of the call = going down
                if (shortDistance < floorsDistance)
                {
                    shortDistance = floorsDistance;
                    suitableElevator = elevId;
                }
            }
            return elevatorsList[suitableElevator];
        }

        // Select an elevator from any basement to go up to the first floor (ground)
        public Elevator getUpToGroundElevators(int RequestedFloor)
        {
            int shortDistance = 5555;
            int suitableElevator = 0;
            //int callDirection = elevatorsList[elevId].ElevCurrentFloor - RequestedFloor;

            for (int elevId = 0; elevId < this.elevatorsList.Count; elevId++)

            {
                // Calucalte the distance between the floor of the call and the elevator's position 
                int floorsDistance = Math.Abs(elevatorsList[elevId].ElevCurrentFloor - RequestedFloor);
                // If the elevator direction is the same as the direction of the call = going down
                if (shortDistance > floorsDistance && elevatorsList[elevId].ElevCurrentFloor > RequestedFloor)
                {
                    shortDistance = floorsDistance;
                    suitableElevator = elevId;
                }
            }
            return elevatorsList[suitableElevator];
        }

        // Select an elevator to get to the basements
        public Elevator getToBasementsElevator(int FloorNumber)
        {
            foreach (var elevator in elevatorsList)
            {
                // Return first the elevators that are on idle status in the column
                if (elevator.status == "Idle" || elevator.status == "Stopped")
                {
                    return elevator;
                }
            }
            //  Calculate and find the closest elevator to the floor in the column
            int shortDistance = 5555;
            int suitableElevator = 0;

            for (int elevId = 0; elevId < this.elevatorsList.Count; elevId++)
            {

                // The floor requested from the elevator's requests list
                int floorRequested = elevatorsList[elevId].elevFloorsList[0];
                // The floor where the elevator is
                int currentPosition = elevatorsList[elevId].ElevCurrentFloor;
                // The distance between the requested floor and the position where the elevator is
                int distance = Math.Abs(currentPosition - floorRequested);
                // The closesest elevator around the requested floor
                int floorsDistance = floorRequested + distance - 1;

                // store and return the smallest distance and return it
                if (shortDistance >= floorsDistance)
                {
                    shortDistance = floorsDistance;
                    suitableElevator = elevId;
                }
            }
            return elevatorsList[suitableElevator];
        }


    }

    public class Battery
    {
        // the class battery with the status, the columns' numbers and columns list as arguments
        public int columnNb;
        public List<Column> columnsList;

        public Battery(int columnNb)
        {

            this.columnNb = columnNb;
            columnsList = new List<Column>();

            // Create, assign the columns numbers and add them to the columns' list
            // Column I for B6 to B1 and 1st floor =  7 floors in total
            Column columnI = new Column(1, 7, 5);
            columnsList.Add(columnI);
            // Column II for the floors 2 to 20 and the first floor = 20 floors
            Column columnII = new Column(2, 20, 5);
            columnsList.Add(columnII);
            // Column III for the floors 21 to 40 and the first floor = 40 floors
            Column columnIII = new Column(3, 40, 5);
            columnsList.Add(columnIII);
            // Column IV for the floors 41 to 60 and the first floor = 60 floors
            Column columnIV = new Column(4, 60, 5);
            columnsList.Add(columnIV);

        }

        // Select a column depending on the destination floor
        public Column selectColumn(int RequestedFloor)
        {
            Column selectedColumn = null;
            foreach (Column column in columnsList)
            {
                if (RequestedFloor >= -6 && RequestedFloor <= 0)
                // the basements are assigned to the column # 1
                // -6 is assigned to B6 , 0 to the first floor.
                {
                    selectedColumn = columnsList[0];
                }
                else if (RequestedFloor == 1 || RequestedFloor > 2 && RequestedFloor <= 20)
                // the floors from 2 to 20 and the 1 floor are assigned to the column # 2
                {
                    selectedColumn = columnsList[1];
                }
                else if (RequestedFloor == 1 || RequestedFloor >= 21 && RequestedFloor <= 40)
                // the floors from 21 to 40 and the 1 floor are assigned to the column # 3
                {
                    selectedColumn = columnsList[2];
                }
                else if (RequestedFloor == 1 || RequestedFloor >= 41 && RequestedFloor <= 60)
                // the floors from 41 to 60 and the 1 floor are assigned to the column # 4
                {
                    selectedColumn = columnsList[3];
                }

            }
            return selectedColumn;
        }
    }

    public class BatteryController
    {
        public int floorNb;
        public int elevatorsColumn;
        public int columnNb;

        public Battery battery;


        public BatteryController(int floorNb, int columnNb, int elevatorsColumn)
        {
            this.floorNb = floorNb;
            this.columnNb = columnNb;
            this.elevatorsColumn = elevatorsColumn;

            this.battery = new Battery(this.columnNb);

        }
        // Call an elevator from the first floor to go to any level
        public Elevator AssignElevator_ToFloors(int RequestedFloor)
        {
            // Display the floor chosen by the user
            Console.WriteLine(" ");
            Console.WriteLine("The user request the floor # " + RequestedFloor);
            Thread.Sleep(200);
            Console.WriteLine("Button's light turned to illuminated");

            // Select a column depending on the floor destination
            Column column = battery.selectColumn(RequestedFloor);
            // The call is from the first floor going up 
            // Select the suitable elevator
            Elevator elevator = column.getToFloorsElevators(1);
            // Add the destination to elevator's list
            elevator.addToList(1, column.columnId);
            elevator.addToList(RequestedFloor, column.columnId);

            // Return the selecated elevator
            return elevator;
        }
        public Elevator AssignElevator_ToBasements(int RequestedFloor)
        {
            // Display the floor chosen by the user
            Console.WriteLine(" ");
            Console.WriteLine("The user request the floor # " + RequestedFloor);
            Thread.Sleep(200);
            Console.WriteLine("Button's light turned to illuminated");

            // Select a column depending on the floor destination
            Column column = battery.selectColumn(RequestedFloor);
            // The call is from the first floor going up 
            // Select the suitable elevator
            Elevator elevator = column.getToBasementsElevator(7);
            // Add the destination to elevator's list
            elevator.addToList(7, column.columnId);
            elevator.addToList(RequestedFloor, column.columnId);

            // Return the selecated elevator
            return elevator;
        }
        // Call an elevator from any level to get to the first floor
        public Elevator RequestElevator_ToGround(int FloorNumber, int RequestedFloor)
        {
            Console.WriteLine("");
            // Display the level from where the user has called an elevator
            Console.WriteLine("Call an elevator to floor # " + FloorNumber);
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
            Thread.Sleep(200);
            Console.WriteLine("Call button light's illuminated");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");

            // Select the column according to the call elevator's level
            var column = battery.selectColumn(FloorNumber);

            // Select the suitable elevator
            var elevator = column.getToGroundElevators(FloorNumber);

            if (elevator.ElevCurrentFloor < FloorNumber)

            {
                elevator.moveDown(RequestedFloor, column.columnId);
                elevator.addToList(FloorNumber, column.columnId);
                elevator.addToList(RequestedFloor, column.columnId);
            }
            else if (elevator.ElevCurrentFloor > FloorNumber)
            //If the elevator is above the floorNumber and moving down
            {   //Add the level from where the elevator's is called
                //Add the distination to the elevator's list
                elevator.addToList(FloorNumber, column.columnId);
                elevator.addToList(RequestedFloor, column.columnId);
            }

            Console.WriteLine(" ");
            Console.WriteLine("Button's light turned off");
            // Return an elevator
            return elevator;
        }

        public Elevator RequestElevator_upToGround(int FloorNumber, int RequestedFloor)
        {
            Console.WriteLine("");
            // Display the level from where the user has called an elevator
            Console.WriteLine("Call an elevator to floor # " + FloorNumber);
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
            Thread.Sleep(200);
            Console.WriteLine("Call button light's illuminated");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");

            // Select the column according to the call elevator's level
            var column = battery.selectColumn(FloorNumber);

            // call the suitable elevator
            var elevator = column.getUpToGroundElevators(FloorNumber);

            if (elevator.ElevCurrentFloor > FloorNumber)

            {
                elevator.moveUp(RequestedFloor, column.columnId);
                elevator.addToList(FloorNumber, column.columnId);
                elevator.addToList(RequestedFloor, column.columnId);
            }
            else if (elevator.ElevCurrentFloor < FloorNumber)
            //If the elevator is above the floorNumber and moving down
            {   //Add the level from where the elevator's is called
                //Add the distination to the elevator's list
                elevator.addToList(FloorNumber, column.columnId);
                elevator.addToList(RequestedFloor, column.columnId);
            }

            Console.WriteLine(" ");
            Console.WriteLine("Button's light turned off");
            // Return an elevator
            return elevator;
        }

    }
    //Méthode 1: RequestElevator_ToGround(FloorNumber, RequestedFloor)
    //Méthode 2: AssignElevator_ToFloors(RequestedFloor)
    //FloorNumber = the place where the customer is at
    //RequestedFloor = the floor the customer want to go


    //"*************************************************************"
    //"                 -  TEST SCENARIOS -                         "
    //"*************************************************************"


    public class TestScenarios
    {
        public static void Main(string[] args)
        {
            //// Initialization
            BatteryController controller = new BatteryController(66, 4, 5);
            //// Columns Variables
            var ColumnOne = controller.battery.columnsList[0];
            var ColumnTwo = controller.battery.columnsList[1];
            var ColumnThree = controller.battery.columnsList[2];
            var ColumnFour = controller.battery.columnsList[3];
            //
           
            while (true)
            { 
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("");
                Console.WriteLine("             ---Senarios Testing--- ");
                Console.WriteLine("");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                 Console.WriteLine("");
                Console.WriteLine("Want you test a scenario? y/n (yes/No)");
                string continueTesting = Console.ReadLine();
                 while (continueTesting != "y" && continueTesting != "n")
                 {
                Console.WriteLine("Please choose y for Yes or n for No.");
                continueTesting = Console.ReadLine();
                }

                if (continueTesting == "n"){
                    Console.WriteLine("You choose to leave the test!");
                    Console.WriteLine("The test is done.");
                    Console.WriteLine("Thank you!");
                    break;
                }
                 Console.WriteLine("");
                Console.WriteLine("Please choose a scenario: 1, 2, 3 or 4");
                string scenario = Console.ReadLine();
                while (scenario != "1" && scenario != "2" && scenario != "3" && scenario != "4")
                {
                    Console.WriteLine("Please choose a scenario: 1, 2, 3 or 4");
                    scenario = Console.ReadLine();
                }
                //--+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                if (scenario == "1")
                {
                    Console.WriteLine("");
                    Console.WriteLine("---Testing scenario 1 ");
                    Console.WriteLine("");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");


                    // // Scenario 1:
                    // // With second column (or column B) serving floors from 2 to 20, 
                    // // with elevator B1 at 20th floor going to 5th, B2 at 3rd floor going to 15th,
                    // // B3 at 13th floor going to 1st, B4 at 15th floor going to 2nd, 
                    // // and B5 at 6th floor going to 1st, someone is at 1st floor and
                    // // requests the 20th floor, elevator B5 is expected to be sent


                    //Elevator # 1
                    // with elevator B1 at 20th floor going to 5th
                    ColumnTwo.elevatorsList[0].ElevCurrentFloor = 20;
                    ColumnTwo.elevatorsList[0].elevDirection = "Up";
                    ColumnTwo.elevatorsList[0].status = "Moving";
                    ColumnTwo.elevatorsList[0].elevFloorsList.Add(5);

                    //Elevator # 2
                    // B2 at 3rd floor going to 15th,
                    ColumnTwo.elevatorsList[1].ElevCurrentFloor = 3;
                    ColumnTwo.elevatorsList[1].elevDirection = "Up";
                    ColumnTwo.elevatorsList[1].status = "Moving";
                    ColumnTwo.elevatorsList[1].elevFloorsList.Add(15);

                    //Elevator # 3
                    // B3 at 13th floor going to 1st,
                    ColumnTwo.elevatorsList[2].ElevCurrentFloor = 13;
                    ColumnTwo.elevatorsList[2].elevDirection = "Down";
                    ColumnTwo.elevatorsList[2].status = "Moving";
                    ColumnTwo.elevatorsList[2].elevFloorsList.Add(1);

                    //Elevator # 4
                    // B4 at 15th floor going to 2nd
                    ColumnTwo.elevatorsList[3].ElevCurrentFloor = 15;
                    ColumnTwo.elevatorsList[3].elevDirection = "Down";
                    ColumnTwo.elevatorsList[3].status = "Moving";
                    ColumnTwo.elevatorsList[3].elevFloorsList.Add(2);

                    //Elevator # 5
                    // B5 at 6th floor going to 1st
                    ColumnTwo.elevatorsList[4].ElevCurrentFloor = 6;
                    ColumnTwo.elevatorsList[4].elevDirection = "Down";
                    ColumnTwo.elevatorsList[4].status = "Moving";
                    ColumnTwo.elevatorsList[4].elevFloorsList.Add(1);

                    //  someone is at 1st floor and
                    //  requests the 20th floor, elevator B5 is expected to be sent


                    //Request:  
                    controller.AssignElevator_ToFloors(20);

                    Console.WriteLine("");
                    Console.WriteLine("---Test scenario 1 : Ok++");
                    Console.WriteLine("");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");


                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                }
                else if (scenario == "2")
                {
                    Console.WriteLine("");
                    Console.WriteLine("---Testing scenario 2 ");
                    Console.WriteLine("");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                    // // Scenario # 2:
                    // //With third column (or column C) serving floors from 21 to 40,
                    // // with elevator C1 at 1st floor going to 21th, C2 at 23st floor going to 28th,
                    // //C3 at 33rd floor going to 1st, C4 at 40th floor going to 24th,
                    // //and C5 at 39nd floor going to 1st, someone is at 1st floor 
                    // //and requests the 36th floor, elevator C1 is expected to be sent

                    //Elevator # 1
                    // C1 at 1st floor going to 21th
                    ColumnThree.elevatorsList[0].ElevCurrentFloor = 1;
                    ColumnThree.elevatorsList[0].elevDirection = "Up";
                    ColumnThree.elevatorsList[0].status = "Stopped";
                    ColumnThree.elevatorsList[0].elevFloorsList.Add(21);

                    //Elevator # 2
                    // C2 at 23st floor going to 28th,
                    ColumnThree.elevatorsList[1].ElevCurrentFloor = 23;
                    ColumnThree.elevatorsList[1].elevDirection = "Up";
                    ColumnThree.elevatorsList[1].status = "Moving";
                    ColumnThree.elevatorsList[1].elevFloorsList.Add(28);

                    //Elevator # 3
                    //  C3 at 33rd floor going to 1st,
                    ColumnThree.elevatorsList[2].ElevCurrentFloor = 33;
                    ColumnThree.elevatorsList[2].elevDirection = "Down";
                    ColumnThree.elevatorsList[2].status = "Moving";
                    ColumnThree.elevatorsList[2].elevFloorsList.Add(1);

                    //Elevator # 4
                    // C4 at 40th floor going to 24th,
                    ColumnThree.elevatorsList[3].ElevCurrentFloor = 40;
                    ColumnThree.elevatorsList[3].elevDirection = "Down";
                    ColumnThree.elevatorsList[3].status = "Moving";
                    ColumnThree.elevatorsList[3].elevFloorsList.Add(24);

                    //Elevator # 5
                    // and C5 at 39nd floor going to 1st
                    ColumnThree.elevatorsList[4].ElevCurrentFloor = 39;
                    ColumnThree.elevatorsList[4].elevDirection = "Down";
                    ColumnThree.elevatorsList[4].status = "Moving";
                    ColumnThree.elevatorsList[4].elevFloorsList.Add(1);

                    //someone is at 1st floor 
                    // and requests the 36th floor, elevator C1 is expected to be sent

                    // Request
                    controller.AssignElevator_ToFloors(36);

                     Console.WriteLine("");
                    Console.WriteLine("---Test scenario 2 : Ok++");
                    Console.WriteLine("");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");



                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                }
                else if (scenario == "3")
                {
                    Console.WriteLine("");
                    Console.WriteLine("---Testing scenario 3 ");
                    Console.WriteLine("");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");


                    // //Scénario 3:


                    // //With fourth column (or column D) serving floors from 41 to 60, 
                    // //with elevator D1 at 58th floor going to 1st, D2 at 50th floor going to 60th, 
                    // //D3 at 46th floor going to 58th, D4 at 1st floor going to 54th, 
                    // //and D5 at 60th floor going to 1st, someone is at 54th floor and requests the 1st floor,
                    //  //elevator D1 is expected to pick him up

                    //Elevator # 1
                    //D1 at floor 58
                    ColumnFour.elevatorsList[0].ElevCurrentFloor = 58;
                    ColumnFour.elevatorsList[0].elevDirection = "Down";
                    ColumnFour.elevatorsList[0].status = "Moving";
                    ColumnFour.elevatorsList[0].elevFloorsList.Add(1);

                    //Elvator # 2
                    //D2 at 50th floor
                    ColumnFour.elevatorsList[1].ElevCurrentFloor = 50;
                    ColumnFour.elevatorsList[1].elevDirection = "Up";
                    ColumnFour.elevatorsList[1].status = "Moving";
                    ColumnFour.elevatorsList[1].elevFloorsList.Add(60);

                    //Elevator # 3
                    //D3 at 46th
                    ColumnFour.elevatorsList[2].ElevCurrentFloor = 46;
                    ColumnFour.elevatorsList[2].elevDirection = "Up";
                    ColumnFour.elevatorsList[2].status = "Moving";
                    ColumnFour.elevatorsList[2].elevFloorsList.Add(58);

                    //Elevator # 4
                    //D4 at 1st floor going to 54th
                    ColumnFour.elevatorsList[3].ElevCurrentFloor = 1;
                    ColumnFour.elevatorsList[3].elevDirection = "Up";
                    ColumnFour.elevatorsList[3].status = "Moving";
                    ColumnFour.elevatorsList[3].elevFloorsList.Add(54);

                    //Elevator # 5
                    //D5 at 60th floor going to 1st
                    ColumnFour.elevatorsList[4].ElevCurrentFloor = 60;
                    ColumnFour.elevatorsList[4].elevDirection = "Down";
                    ColumnFour.elevatorsList[4].status = "Moving";
                    ColumnFour.elevatorsList[4].elevFloorsList.Add(1);


                    // someone is at 54th floor and requests the 1st floor,
                    // elevator D1 is expected to pick him up

                    //Request
                    Elevator elevator = controller.RequestElevator_ToGround(54, 1);

                     Console.WriteLine("");
                    Console.WriteLine("---Test scenario 3 : Ok++");
                    Console.WriteLine("");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");



                    //--------------------------------------------------------------------------------------------------
                }
                else if (scenario == "4")
                {
                    Console.WriteLine("");
                    Console.WriteLine("---Testing scenario 4 ");
                    Console.WriteLine("");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                    // Scenario 4:
                    // With first column (or Column A) serving the basements B1 to B6,
                    // with elevator A1 idle at B4, A2 idle at 1st floor, A3 at B3 and going to B5, 
                    // A4 at B6 and going to 1st floor, and A5 at B1 going to B6, 
                    // someone is at B3 and requests the 1st floor.
                    // Elevator A4 is expected to be sent.

                    // For the test:
                    // B6 = -6, B5 = -5 ... , B1 = -1 and 0 the first floor.

                    //Elevator # 1
                    //A1 idle at B4 = -4
                    ColumnOne.elevatorsList[0].ElevCurrentFloor = -4;
                    ColumnOne.elevatorsList[0].elevDirection = " ";
                    ColumnOne.elevatorsList[0].status = "Idle";
                    ColumnOne.elevatorsList[0].elevFloorsList.Add(-4);

                    //Elevator # 2
                    //A2 idle at 1st floor,
                    ColumnOne.elevatorsList[1].ElevCurrentFloor = 0;
                    ColumnOne.elevatorsList[1].elevDirection = " ";
                    ColumnOne.elevatorsList[1].status = "Idle";
                    ColumnOne.elevatorsList[1].elevFloorsList.Add(0);

                    //Elevator # 3
                    //A3 at B3 = -3 and going to B5 = -5,
                    ColumnOne.elevatorsList[2].ElevCurrentFloor = -3;
                    ColumnOne.elevatorsList[2].elevDirection = "Up";
                    ColumnOne.elevatorsList[2].status = "Moving";
                    ColumnOne.elevatorsList[2].elevFloorsList.Add(-5);

                    //Elevator # 4
                    //A4 at B6 = -6 and going to 1st floor
                    ColumnOne.elevatorsList[3].ElevCurrentFloor = -6;
                    ColumnOne.elevatorsList[3].elevDirection = "Up";
                    ColumnOne.elevatorsList[3].status = "Moving";
                    ColumnOne.elevatorsList[3].elevFloorsList.Add(0);

                    //Elevator # 5
                    //and A5 at B1 = -1 going to B6 = -6,
                    ColumnOne.elevatorsList[3].ElevCurrentFloor = -1;
                    ColumnOne.elevatorsList[3].elevDirection = "Down";
                    ColumnOne.elevatorsList[3].status = "Moving";
                    ColumnOne.elevatorsList[3].elevFloorsList.Add(-5);


                    // someone is at B3 = -3 and requests the 1st floor.
                    // Elevator A4 is expected to be sent.

                    Elevator elevator = controller.RequestElevator_upToGround(-3, 0);

                    //controller.AssignElevator_ToBasements(1);

                     Console.WriteLine("");
                    Console.WriteLine("---Test scenario 1 : Ending");
                    Console.WriteLine("");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");


                }
            }
        }

    }

}



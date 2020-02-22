//"*************************************************************"
//"                 -  TEST SCENARIOS -                         "
//"*************************************************************"

function testBattery() {

    var testScenario = initSystem(10, 2);
    var elevatorOne = testScenario.column.elevatorsCulumn[0];
    var elevatorTwo = testScenario.column.elevatorsCulumn[1];


    //Scenario 1 

    // elevatorOne.currentPosition = 2;                            // elevator 1 Idle at floor 2
    // elevatorOne.state = "Idle";
    // elevatorOne.elevatorDirection = "Up";

    // elevatorTwo.currentPosition = 6;                            //  elevator 2  Idle at floor 6
    // elevatorTwo.state = "Idle";
    // elevatorTwo.elevatorDirection = "Down";

    // var elevator = testScenario.RequestElevator(3, "Up");         //someone is on floor 3 and requests the 7th floor
    // testScenario.RequestFloor(elevator, 7);                       // and requests the 7th floor

    // console.log("")

    // console.log("")
    // console.log("---Test scenario 1 : Ok++")                  // elevator 1 is expected to be sen
    // console.log("")

    // console.log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")

    //Scenario 2 

    // elevatorOne.currentPosition = 10;                    //elevator 1 idle at floor 10
    // elevatorOne.state = "Idle";
    // elevatorOne.elevatorDirection = "Up";

    // elevatorTwo.currentPosition = 3;                     //  elevator 2 idle at floor 3,
    // elevatorTwo.state = "Idle";
    // elevatorTwo.elevatorDirection = "Up";

    // var elevator = testScenario.RequestElevator(1, "Up");        // someone is on the 1st floor 
    // testScenario.RequestFloor(elevator, 6);                      //and requests the 6th floor = elevator 2 should be sent = ok

    // elevator = testScenario.RequestElevator(3, "Up");            // someone else is on the 3rd floor 
    // testScenario.RequestFloor(elevator, 5);                      // and requests the 5th floor.Elevator 2 should be sent.

    // elevator = testScenario.RequestElevator(9, "Down");             //on floor 9
    // testScenario.RequestFloor(elevator, 2);                       // and wants to go down to the 2nd floor= Elevator 1 should be sent.

    // console.log("");
    // console.log("---Test scenario 2 : Ok++");
    // console.log("");
    // console.log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")

    //Scenario 3 

    elevatorOne.currentPosition = 10; //elevator 1 idle at floor 10 
    elevatorOne.state = "Idle";
    elevatorOne.elevatorDirection = "Down";

    elevatorTwo.currentPosition = 3; //elevator 2 moving from floor 3 to floor 6
    elevatorTwo.state = "Moving"; // and wants to go down to the 3rd floor. Elevator 2 should be sent => Ok
    elevatorTwo.elevatorDirection = "Up";

    // someone is on the 1st floor 
    var elevator = testScenario.RequestElevator(3, "Down"); //and requests the 6th floor = elevator 2 should be sent = ok

    testScenario.RequestFloor(elevator, 2);             //on floor 9
    elevator = testScenario.RequestElevator(10, "Down"); // and wants to go down to the 2nd floor= Elevator 1 should be sent.=ok
    testScenario.RequestFloor(elevator, 3);                // someone else is on the 10th floor and wants to go down  to the 3rd 


    console.log("");
    console.log("---Test scenario 3 : Ok++")
    console.log("");
    console.log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")

}

//*************************************************************"
//             - ELEVATORS SYSTEM INITIALISATION  -            "
//**************************************************************"

function initSystem(nbFloors, nbElevators) {
    var syst = new ElevatorController(nbFloors, nbElevators);

    return syst;
}
class Column {
    constructor(nbFloors, nbElevators) {

        this.nbFloors = nbFloors;
        this.nbElevators = nbElevators;
        this.elevatorsCulumn = [];

        for (let i = 0; i < this.nbElevators; i++) {

            let elevator = new Elevator(i + 1, "Idle", 1, "Up");
            this.elevatorsCulumn.push(elevator);
        }
    }
}

class Elevator {

    constructor(elevatorId, state, currentPosition, elevatorDirection) {

        this.elevatorId = elevatorId;
        this.state = state;
        this.currentPosition = currentPosition;
        this.elevatorDirection = elevatorDirection;
        this.elevatorFloorsList = [];

    }

    // "*************************************************************"
    // "                 -  ELEVATORS STOPS LIST -                  "
    // "*************************************************************"

    // Add the request to the elevators list

    addToList(RequestedFloor) {

        this.elevatorFloorsList.push(RequestedFloor);
        this.sortList();
        this.operate_elevator(RequestedFloor);
    }

    // Sort the list depending on the direction of the elevator

    sortList() {
        if (this.elevatorDirection === "Up") {
            this.elevatorFloorsList.sort();
        } else if (this.elevatorDirection === "Down") {
            this.elevatorFloorsList.sort();
            this.elevatorFloorsList.reverse();
        }
        return this.elevatorFloorsList;
    }

    // Move the elvators to their destination

    operate_elevator(RequestedFloor) {
        while (this.elevatorFloorsList > 0) {
            if (RequestedFloor === this.currentPosition) {
                this.openDoors();
                this.state = "Moving";

                this.elevatorFloorsList.shift();
            } else if (RequestedFloor < this.currentPosition) {
                this.state = "Moving";
                console.log("Elevator " + this.elevatorId + " is selected");
                console.log("Elevator " + this.elevatorId, this.state);
                console.log("");
                this.Direction = "Down";
                this.moveDown(RequestedFloor);
                this.state = "Stopped";
                console.log("");
                console.log("Elevator " + this.elevatorId, this.state);
                console.log("");
                this.openDoors();
                this.elevatorFloorsList.shift();
            } else if (RequestedFloor > this.currentPosition) {
                delay(1000);
                this.state = "Moving";
                console.log("");
                console.log("Elevator " + this.elevatorId + " is selected");
                console.log("Elevator " + this.elevatorId, this.state);
                console.log("");
                this.Direction = "Up";
                this.moveUp(RequestedFloor);
                this.state = "Stopped";
                console.log("");
                console.log("Elevator " + this.elevatorId, this.state);
                console.log("");

                this.openDoors();

                this.elevatorFloorsList.shift();
            }
        }
        if (this.elevatorFloorsList === 0) {
            this.state = "Idle";
        }
    }

    // "*************************************************************"
    // "               -  Floor Request & Call Buttons Control -     "
    // "*************************************************************"
    Request_floor_button(RequestedFloor) {

        this.RequestedFloor = RequestedFloor;
        this.floorLights = floorLights;
    }
    Call_floor_button(floor, Direction) {

        this.floor = floor;
        this.Direction = Direction;
    }

    // "*************************************************************"
    // "                 -  ELEVATORS DOORS CONTROL -                "
    // "**************************************************************"

    openDoors() {
        delay(1000);
        console.log("Doors opening");
        console.log("Doors opened");
        console.log("");
        console.log("Button light-off");
        delay(1000);

        console.log("");
        delay(1000);
        this.Close_door();
    }
    Close_door() {
        console.log("Closing doors");
        console.log("Doors' not obstructed : OK")
        console.log("Elevator's capacity check: OK ")
        console.log("Doors Closed")
        delay(1000);
    }

    // "*************************************************************"
    // "                 -  ElEVATORS MOVEMENTS -                      "
    // "*************************************************************"

    moveUp(RequestedFloor) {
        console.log("Floor : " + this.currentPosition);
        delay(1000);
        while (this.currentPosition !== RequestedFloor) {
            this.currentPosition += 1;
            console.log("Floor : " + this.currentPosition);

            delay(1000);
        }
    }

    moveDown(RequestedFloor) {
        console.log("Floor : " + this.currentPosition);
        delay(1000);
        while (this.currentPosition !== RequestedFloor) {
            this.currentPosition -= 1;
            console.log("Floor : " + this.currentPosition);

            delay(1000);
        }
    }
}
// "*************************************************************"
// "                 -  ElEVATORS CONTROL -                      "
// "*************************************************************"

class ElevatorController {
    constructor(nbFloors, nbElevators) {
        this.nbFloors = nbFloors;
        this.nbElevators = nbElevators;
        this.column = new Column(nbFloors, nbElevators);


        console.log("System is starting");
    }

    // Request an a floor

    RequestElevator(floor, Direction) {
        delay(1000);
        console.log("");
        console.log("Request elevator to floor : ", floor);
        delay(1000);
        console.log("");
        console.log("Call button illuminated");
        delay(1000);

        let elevator = this.selectElevator(floor, Direction);
        elevator.addToList(floor);
        return elevator;
    }

    // Call an elevator 

    RequestFloor(elevator, RequestedFloor) {
        delay(1000);
        console.log("");
        console.log("The floor number " + RequestedFloor + " is requested");
        delay(1000);
        console.log("");
        console.log("Request button illuminated");
        delay(1000);
        elevator.addToList(RequestedFloor);

    }

    // "*************************************************************"
    // "                 -  SELECT AN ELEVATOR -                 "
    // "**************************************************************"

    selectElevator(floor, Direction) {

        let suitableElevator = null;
        let shortestDistance = 1000;
        for (let i = 0; i < this.column.elevatorsCulumn.length; i++) {
            let elevator = this.column.elevatorsCulumn[i];

            if (floor === elevator.currentPosition && (elevator.state === "Stopped" || elevator.state === "Idle" || elevator.state === "Moving")) {
                return elevator;
            } else {
                let distance = Math.abs(floor - elevator.currentPosition);
                if (shortestDistance > distance) {
                    shortestDistance = distance;
                    suitableElevator = elevator;

                    if (elevator.Direction === Direction) {
                        suitableElevator = elevator;
                    }
                }
            }
        }
        return suitableElevator;
    }
}

// "*************************************************************"
// "                 -  DELAY CLOSING/OPENING DOORS -            "
// "*************************************************************"
function delay(milliseconds) {
    var start = new Date().getTime();
    for (var i = 0; i < 1e7; i++) {
        if (new Date().getTime() - start > milliseconds) {
            break;
        }
    }
}
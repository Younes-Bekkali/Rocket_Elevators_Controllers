console.log("Test");
console.log("Test2");

class Column {
    constructor() {
    this.nbFloors = nbFloors;
    this.nbElevators = nbElevators;
    }
    MoveUp(){};

    MoveDown(){};

    RequestElevator (RequestedFloor, Direction){} //Method 1 :  must return the chosen elevator and move the elevators in its treatment.

    RequestFloor (Elevator, RequestedFloor){} // Method 2 : must move the elevators in its treatment.

    
}
console.log("The floor where the person is: ");
console.log("the direction in which he wants to go (Up or Down)");
console.log("The elevator used is: ");
console.log("The requested floor is: ");

package main

import (
	"fmt"
	"sort"
	"time"
)

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



// Elevator struct (class for elevators)
type Elevator struct {
	elevatorID        		int
	ElevCurrentFloor 		int
	elevFloorsList      	[]int
	elevStatus    			string
	elevDirection 			string
	doorsSensor       		bool
	weightCapacitySensor	bool
	column             		Column
}

// ElevConstructor the constructor to create new elevators
// the elevators are idle ate the first floor

func ElevConstructor() *Elevator {
	elevator := new(Elevator)
	elevator.ElevCurrentFloor= 1
	elevator.elevFloorsList = []int{}
	elevator.elevStatus = "idle"
	elevator.elevDirection = "Down"
	elevator.doorsSensor = true
	elevator.weightCapacitySensor = true
	return elevator
}

// Column the struct for columns
type Column struct {
	columnID               int
	elevatorsColumn		   int
	elevatorsList          []Elevator
}

// ColumnConstr the constructor to create new columns with a pointor to the struct Column

func ColumnConstr(elevatorsColumn int) *Column {
	column := new(Column)
	column.elevatorsColumn  = 5
	for index := 0; index < column.elevatorsColumn	; index++ {
		elevator := ElevConstructor()
		column.elevatorsList = append(column.elevatorsList, *elevator)
	}
	return column
}

// Battery the struct for Batteries
type Battery struct {
	columnNb 	  int
	columnsList   []Column
}

// BatteryConstr the constructor to create new columns with a pointor to the struct Battery

func BatteryConstr(columnNb int) *Battery {
	battery := new(Battery)
	battery.columnNb = 4

	for index := 0; index < battery.columnNb; index++ {
		column := ColumnConstr(index)
		battery.columnsList = append(battery.columnsList, *column)
	}
	return battery
}

// BatteryController is the struct for controller
type BatteryController struct {
	battryNb 		int
	battery       	[]Battery
	columnNb        int
	userDirection   string
}

// ControllerConstr is the constructor of the struct (class)

func ControllerConstr(battryNb int) BatteryController {
	
	controller := new(BatteryController)
	controller.battryNb = 1

	for index := 0; index < battryNb; index++ {
		battery := BatteryConstr(index)
		controller.battery = append(controller.battery, *battery)
	}
	return *controller
}

// Select an elevator from any level to go to the first floor (ground)
func (controller *BatteryController) RequestElevator(FloorNumber int, RequestedFloor int) Elevator {
	fmt.Println("Request elevator to floor : ", FloorNumber)
	time.Sleep(1200 * time.Millisecond)
	fmt.Println("Button's light turned to illuminated")
	// Select a column depending on the floor destination
	var column = controller.battery[0].selectColumn(FloorNumber)
	// The call is from the first floor going up 
    // Select the suitable elevator

	var elevator = column.selectElevator(RequestedFloor, "Down")
	// Add the destination to elevator's list
	elevator.addToList(FloorNumber)
	elevator.addToList(RequestedFloor)
	return elevator
}

// Call an elevator from the first floor to go to any level
func (controller *BatteryController) AssignElevator(RequestedFloor int) Elevator {
	fmt.Println("The user request the floor # ", RequestedFloor)
	time.Sleep(2 * time.Millisecond)
	fmt.Println("Button's light turned to illuminated")
	// Select a column depending on the floor destination
	column := controller.battery[0].selectColumn(RequestedFloor)
	// The call is from the first floor going Up 
    // Select the suitable elevator
	var elevator = column.selectElevator(RequestedFloor, "Up")
	 // Return the selecated elevator
	elevator.addToList(1)
	elevator.addToList(RequestedFloor)
	
	return elevator
}

 // Select a column depending on the destination floor
func (b Battery) selectColumn(RequestedFloor int) Column { // not sure about *
	if (RequestedFloor > -6 && RequestedFloor <= 0) || (RequestedFloor == 1 ) {
		// the basements are assigned to the column # 1
        // -6 is assigned to B6 , 0 to the first floor.
		return b.columnsList[0]
	} else if (RequestedFloor >= 2 && RequestedFloor <= 20 ) || (RequestedFloor == 1 ){
		 // the floors from 2 to 20 and the 1 floor are assigned to the column # 2
		return b.columnsList[1]
	} else if RequestedFloor >= 21 && RequestedFloor <= 40 || (RequestedFloor == 1 ) {
		// the floors from 21 to 40 and the 1 floor are assigned to the column # 3
		return b.columnsList[2]
	} else if RequestedFloor >= 41 && RequestedFloor <= 60 || (RequestedFloor == 1 ){
		// the floors from 41 to 60 and the 1 floor are assigned to the column # 4
		return b.columnsList[2] // for testing , erreur
	}
	return b.columnsList[3] // not workin yet
}

 // Select an elevator to get to the floors 

func (c *Column) selectElevator(RequestedFloor int, userDirection string) Elevator {
	var suitableElevator = c.elevatorsList[1]
	for _, e := range c.elevatorsList {
		// the best elevator, not working yet
		// calcul distance to do, will serve to calculate the shortest distance
		if RequestedFloor < e.ElevCurrentFloor && e.elevDirection == "Down" && userDirection == "Down" {
			suitableElevator = e
		} else if e.elevStatus == "idle" {
			suitableElevator = e
		} 
	}
	return suitableElevator
}

// Add the request to the list , sort it, select an elevator and operate the elevator to reach its destination
func (e *Elevator) addToList(RequestedFloor int) {
	e.elevFloorsList = append(e.elevFloorsList, RequestedFloor)
	
	if RequestedFloor > e.ElevCurrentFloor{
		// Sort the list from last to first. compare every element to each other 
		sort.Ints(e.elevFloorsList)
	} else if RequestedFloor < e.ElevCurrentFloor{
		sort.Sort(sort.Reverse(sort.IntSlice(e.elevFloorsList)))
	}
	// Call the move elevator to its destination once the request added and the list sorted
	e.operateElevator(RequestedFloor)
}

// moveElevator function : Move the elevator to its destination, open the doors ,
//and display those operations on the console
func (e *Elevator) operateElevator(RequestedFloor int) {
	
	 // If the elevator is at the floor requested, open doors
	if RequestedFloor == e.ElevCurrentFloor{
		e.openDoors()
		// once the door closed the elevator will move to its new destination or to the idle position
	} else if RequestedFloor > e.ElevCurrentFloor{
		e.elevStatus = "Moving"
		 // if the destination is below the elevators current position of the elevator
		fmt.Println("")
		fmt.Println("Buttons' light off")
		fmt.Println(" ")
		fmt.Println("+++++++++++++++++++++++++++++++++++++++++++++++++++")
		fmt.Println("")
		fmt.Println("Door is Opening")
		e.moveUp(RequestedFloor)
		e.elevStatus = "Stopped"
		// Open the doors once the elevator reached its destination
		e.openDoors()
		// Remove the destination from the list once the elevator reached it-todo
		e.elevStatus = "Moving"
	} else if RequestedFloor < e.ElevCurrentFloor{
		e.elevStatus = "Moving"
		fmt.Println("")
		fmt.Println("Buttons' light off")
		fmt.Println(" ")
		fmt.Println("+++++++++++++++++++++++++++++++++++++++++++++++++++")
		fmt.Println("")
		fmt.Println("Door is Opening")
		e.moveDown(RequestedFloor)
		e.elevStatus = "Stopped"
		// Open the doors once the elevator reached its destination
		e.openDoors()
		// Remove the destination from the list once the elevator reached it-todo
		e.elevStatus = "Moving"
	} 
}

// Doors' manipulation
// Display the opening and the closing of the doors
func (e *Elevator) openDoors() {
	fmt.Println("+++++++++++++++++++++++++++++++++++++++++++++++++++")
	fmt.Println("Doors: Opening")
	time.Sleep(3 * time.Second)
	fmt.Println("Doors: Opened")
	time.Sleep(3 * time.Second)
	
	e.closeDoors()
}
func (e *Elevator) closeDoors() {
	if e.doorsSensor == true && e.weightCapacitySensor == true{
		fmt.Println("Doors: Closing")
		time.Sleep(2 * time.Second)
		fmt.Println("Doors: Not obstructed")
		time.Sleep(1 * time.Second)
		fmt.Println("Doors: Closed")
		fmt.Println("+++++++++++++++++++++++++++++++++++++++++++++++++++")
		time.Sleep(2 * time.Second)
	} else if e.doorsSensor == false || e.weightCapacitySensor == false{
		e.openDoors()
		fmt.Println("Doors: Obstructed")
		fmt.Println("Elevator's weight capacity reached")
	}
}

// Move the elevator according to the Requested floor's position
// Display those operations on the console

func (e Elevator) moveUp(RequestedFloor int) {
	fmt.Println("Column # ", e.column.columnID, " || Elevator # " , e.elevatorID , " || Current Floor :", e.ElevCurrentFloor)

	for RequestedFloor > e.ElevCurrentFloor{
		e.ElevCurrentFloor ++
		if RequestedFloor == e.ElevCurrentFloor{
			
			time.Sleep(1 * time.Second)
			fmt.Println("+++++++++++++++++++++++++++++++++++++++++++++++++++")
			fmt.Println("Column # ", e.column.columnID, " || Elevator # " , e.elevatorID , " ||  Floor # ", e.ElevCurrentFloor)
		
		}
		time.Sleep(1 * time.Second)
		fmt.Println("Column # ", e.column.columnID, " || Elevator # ", e.elevatorID ,  " ||  Floor # ", e.ElevCurrentFloor)
	
	}
}

func (e *Elevator) moveDown(RequestedFloor int) {
	fmt.Println("Column # ", e.column.columnID, " || Elevator # ", e.elevatorID , " ||  Floor # ", e.ElevCurrentFloor)

	for RequestedFloor < e.ElevCurrentFloor{
		e.ElevCurrentFloor --
		if RequestedFloor == e.ElevCurrentFloor{
			time.Sleep(1 * time.Second)
			fmt.Println("---------------------------------------------------")
			fmt.Println("Column # ", e.column.columnID, " || Elevator # ", e.elevatorID , " Arrived at destination floor : ", e.ElevCurrentFloor)
		}
		time.Sleep(300 * time.Millisecond)
		fmt.Println("Column # ", e.column.columnID, " || Elevator # ", e.elevatorID ," ||  Floor # ", e.ElevCurrentFloor)
	}
}
    //Méthode 1: RequestElevator_ToGround(FloorNumber, RequestedFloor)
    //Méthode 2: AssignElevator_ToFloors(RequestedFloor)
    //FloorNumber = the place where the customer is at
	//RequestedFloor = the floor the customer want to go
	
// the main function to test the senarios
func main() {
	// for testing
	// not working yet
	controller := ControllerConstr(1)
	controller.AssignElevator(60)
	controller.RequestElevator(33, 60)


	// controller := ControllerConstr(1)
	// controller.AssignElevator(6)
	// controller.RequestElevator(3, 16)

	// controller := ControllerConstr(5)
	// controller.AssignElevator(3)
	// controller.RequestElevator(5, 26)


	// controller := ControllerConstr(-6)
	// controller.AssignElevator(10)
	// controller.RequestElevator(3, 1)

	// controller := ControllerConstr(5)
	// controller.AssignElevator(13)
	// controller.RequestElevator(5, 26)

}


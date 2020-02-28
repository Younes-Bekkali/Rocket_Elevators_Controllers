package main



import (
	"fmt"
	"sort"
	"time"
)

// public class Elevator
// public int elevatorId;
// public string status;
// public int ElevCurrentFloor;
// public string elevDirection;
// public bool doorsSensor; 
// public bool weightCapacitySensor;
// public int FloorDisplay; 
// public List<int> elevFloorsList;


// // Elevator class
// type Elevator struct {

// 	// elvatorId: the elevator's number
// 	elevatorID int
// 	//status: the elevator's status: Moving, Idle, or Stopped
// 	status string
// 	//ElevCurrentFloor: the current position of the elevator
// 	ElevCurrentFloor int
// 	//elevDirection: the direction of movement of the elevator: Up or Down
// 	elevDirection string
// 	//doorsSensor: the door's obstruction sensor: True or false
// 	doorsSensor bool
// 	//weightCapacitySensor: the elevator capacoty weight controller, True or False
// 	weightCapacitySensor bool
// 	// FloorDisplay
// 	FloorDisplay int
// 	//elevFloorsList: the elevator's list of requests and calls
// 	elevFloorsList [] int
	
// }

// // Sort the list from last to first. compare every element to each other 
// func (elev Elevator) addToList(RequestedFloor int, columnID int)(){

// 	elev.elevFloorsList = append(elev.elevFloorsList, RequestedFloor)
	
// 	if RequestedFloor > elev.ElevCurrentFloor{
// 		Sort.Ints(elev.elevFloorsList) 
// 	}else if RequestedFloor < elev.ElevCurrentFloor{ 
// 		sort.Sort(sort.Reverse(sort.IntSlice(elev.elevFloorsList)))
// 	}
// 	moveElevator(RequestedFloor, columnID)
// }

// // moveElevator function : Move the elevator to its destination, open the doors ,
// //and display those operations on the console
// func (elev Elevator) moveElevator( RequestedFloor int, columnID int){

// 	if RequestedFloor < elev.ElevCurrentFloor {
// 	// if the destination is below the elevators current position of the elevator

// 	// Display the column's number, the buttons status and the elevator's number and status
// 		elev.status = "Moving";
// 		fmt.Println("Buttons' light off");
// 		fmt.Println(" ");
// 		fmt.Println("+++++++++++++++++++++++++++++++++++++++++++++++++++");
// 		fmt.Println("Column # " + columnID + " Elevator # " + elev.elevatorID + " " + elev.status);
// 		fmt.Println("+++++++++++++++++++++++++++++++++++++++++++++++++++");
// 		fmt.Println(" ");
// 	// Move the elevator down untill it reaches the RequestedFloor
// 		elev.elevDirection = "Down";
// 		moveDown(RequestedFloor, columnID);
// 		elev.status = "Stopped";
// 		fmt.Println(" ");
// 		fmt.Println("Column # " + columnID + " Elevator # " + elev.elevatorID + " " + elev.status);
// 		fmt.Println(" ");
// 	// Open the doors once the elevator reached its destination
// 		elev.openDoors();                
// 	// Remove the destination from the list once the elevator reached it
// 		elev.elevFloorsList.Remove(0); 

// 	} else if RequestedFloor > elev.ElevCurrentFloor {
// 	// if the destination is above the elevators current position 

// 	// Change the elevator status 
// 		elev.status = "Moving";
// 		fmt.Println(" ");
// 		fmt.Println("Buttons' light off");
// 	// Display on the console the column's number, the elevator's number and status 
// 		fmt.Println(" ");
// 		fmt.Println("+++++++++++++++++++++++++++++++++++++++++++++++++++");
// 		fmt.Println("Column # " + columnID + " Elevator # " + elev.elevatorId + " " + elev.status);
// 		fmt.Println("+++++++++++++++++++++++++++++++++++++++++++++++++++");
// 		fmt.Println(" ");
// 		elev.elevDirection = "Up";
// 		elev.moveUp(RequestedFloor, columnID);
// 	// Once the elevator reach requested floor the elevator will stop moving
// 		elev.status = "Stopped";
// 		fmt.Println("+++++++++++++++++++++++++++++++++++++++++++++++++++");
// 		fmt.Println("Column # " + columnID + " Elevator # " + elev.elevatorID + " " + elev.status);
// 	// Once the elevator stops moving the doors will open
// 		elev.openDoors();
// 	// Remove the dloor from the elevator list
// 		elev.elevFloorsList.Remove(0);
// 	} else {
// 	// If the elevator is at the floor requested, open doors
// 		openDoors();
// 	// once the door closed the elevator will move to its new destination or to the idle position
// 		elev.status = "Moving";
// 		elev.elevFloorsList.Remove(0);
// 	}

// 	}

// 		// Doors' manipulation
//         // Display the opening and the closing of the doors
// func (elev Elevator) openDoors() { 

// 		if elev.status != "Moving" {
// 		time.Sleep(1000 * time.Millisecond)
// 		}

//            fmt.Println("+++++++++++++++++++++++++++++++++++++++++++++++++++")
//            fmt.Println("Doors: Opening")
//             time.Sleep(1000 * time.Millisecond)

//            fmt.Println("Doors: Opened")
//             time.Sleep(1000 * time.Millisecond)

//         // close automatically the doors after a delay
//             this.closeDoors()

          
           
//         }






////Exemple

/* 
Affiche des informations sur un personnage

@return: void
*/
func (p Personnage) Affichage() { // déclaration de ma méthode Affichage() liée à ma structure Personnage
    fmt.Println("--------------------------------------------------")
    fmt.Println("Vie du personnage", p.Nom, ":", p.Vie)
    fmt.Println("Puissance du personnage", p.Nom, ":", p.Puissance)

    if p.Mort {
        fmt.Println("Vie du personnage", p.Nom, "est mort")
    } else {
        fmt.Println("Vie du personnage", p.Nom, "est vivant")
    }

    fmt.Println("\nLe personnage", p.Nom, "possède dans son inventaire :", p.Vie)

    for _, item := range p.Inventaire {
        fmt.Println("-", item)
    }
}

func main() {

	magicien := Personnage{ // Instanciation de la classe Personnage
		Nom:        "magix",
		Vie:        100,
		Puissance:  20,
		Mort:       false,
		Inventaire: [3]string{"potions", "poisons", "bâton"},
	}

	magicien.Affichage()
}
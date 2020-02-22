import time

# Initiate the system and creating new columns
class columnController:
    def __init__(self, nbFloors, nbElevators):
        self.nbFloors = nbFloors
        self.nbElevators = nbElevators
        self.column = Column(nbFloors, nbElevators)
       

# Call an elevator 
    def RequestElevator(self, floor, direction):
        time.sleep(1)
        print("")
        print("Request elevator to floor : ", floor)
        time.sleep(1)
        print("")
        print("Call button illuminated")
        time.sleep(1)
        elevator = self.selectElevator(floor, direction)
        elevator.addToList(floor)
        return elevator
# Request an a floor
    def RequestFloor(self, elevator, RequestedFloor):
        time.sleep(1)
        print("")
        print("Requested floor to ", RequestedFloor)
        time.sleep(1)
        print("")
        print("Request button illuminated")
        time.sleep(1)
        elevator.addToList(RequestedFloor)

# Select the suitable elevator dependind on the direction, the status and level of the elevator
    def selectElevator(self, floor, direction):
        suitableElevator = None
        shortestDistance = 1000
        for elevator in (self.column.elevatorsColumn):
            if (floor == elevator.elevatorCurrentFloor and (elevator.elevatorStatus == "Stopped" or elevator.elevatorStatus == "Idle" or elevator.elevatorStatus == "Moving")):
                return elevator
            else:
                distance = abs(floor - elevator.elevatorCurrentFloor)
                if shortestDistance > distance:
                    shortestDistance = distance
                    suitableElevator = elevator

                elif elevator.direction == direction:
                    suitableElevator = elevator

        return suitableElevator

# Column's class allows to add more elevators
class Column:
    def __init__(self, nbFloors, nbElevators):
        self.nbFloors = nbFloors
        self.nbElevators = nbElevators
        self.elevatorsColumn = []
        for i in range(nbElevators):
            elevator = Elevator(i+1, "Idle", 1, "Up")
            self.elevatorsColumn.append(elevator)
# Elevator's class creats the elevators and operate them            
class Elevator:
    def __init__(self, elevatorId, elevatorStatus, elevatorCurrentFloor, elevatorDirection):
        self.elevatorId = elevatorId
        self.elevatorStatus = elevatorStatus
        self.elevatorCurrentFloor = elevatorCurrentFloor
        self.elevatorDirection = elevatorDirection
        self.elevatorsFloorsList = []
# sort and add the level requested to the list of requestes
    def addToList(self, RequestedFloor):
        self.elevatorsFloorsList.append(RequestedFloor)
        self.sort()
        self.moveElevator(RequestedFloor)


    def sort(self):
        if self.elevatorDirection == "Up":
            self.elevatorsFloorsList.sort()
        elif self.elevatorDirection == "Down":
            self.elevatorsFloorsList.sort()
            self.elevatorsFloorsList.reverse()
        return self.elevatorsFloorsList

# Moving the elevator to the level destination
    def moveElevator(self, RequestedFloor):
        while (len(self.elevatorsFloorsList) > 0):
            if ((RequestedFloor == self.elevatorCurrentFloor)):
                self.openDoors()
                self.elevatorStatus = "Moving"
                self.elevatorsFloorsList.pop()
            elif (RequestedFloor < self.elevatorCurrentFloor):

                self.elevatorStatus = "Moving"
                print("")
                print("Elevator number ", self.elevatorId,"is ", self.elevatorStatus)
                print("")
                self.direction = "Down"
                self.moveDown(RequestedFloor)
                self.elevatorStatus = "Stopped"
                print("")
                print("Elevator number ", self.elevatorId,"is ", self.elevatorStatus)
                print("")
                self.openDoors()
                self.elevatorsFloorsList.pop()
                
            elif (RequestedFloor > self.elevatorCurrentFloor):

                time.sleep(1)
                self.elevatorStatus = "Moving"
                print("")
                print("Elevator number ", self.elevatorId,"is ", self.elevatorStatus)
                print("")
                self.direction = "Up"
                self.moveUp(RequestedFloor)
                self.elevatorStatus = "Stopped"
                print("")
                print("Elevator number", self.elevatorId,"is ", self.elevatorStatus)
                print("")

                self.openDoors()

                self.elevatorsFloorsList.pop()

        if self.elevatorsFloorsList == 0:
            self.elevatorStatus = "Idle"

# Doors Manipulation

    def openDoors(self):
        time.sleep(1)
        print("Open Door")
        print("")
        print("Button deactivated")
        time.sleep(1)
        print("")
        time.sleep(1)
        self.closeDoor()

    def closeDoor(self):
        print("Closing Doors")
        print("Doors not obstructed : OK")
        print("Elevator's capacity check: OK ")
        print("Doors Closed")
        time.sleep(1)

# Moving the elevator depending on the equested direction

    def moveUp(self, RequestedFloor):
        print("Floor : ", self.elevatorCurrentFloor)
        time.sleep(1)
        while(self.elevatorCurrentFloor != RequestedFloor):
            self.elevatorCurrentFloor += 1
            print("Floor : ", self.elevatorCurrentFloor)
            time.sleep(1)

    def moveDown(self, RequestedFloor):
        print("Floor : ", self.elevatorCurrentFloor)
        time.sleep(1)
        while(self.elevatorCurrentFloor != RequestedFloor):
            self.elevatorCurrentFloor -= 1
            print("Floor : ", self.elevatorCurrentFloor)

            time.sleep(1)


class Call_button:
    def __init__(self, floor, direction):
        self.floor = floor
        self.direction = direction
      

class Floor_button:
    def __init__(self, RequestedFloor):
        self.RequestedFloor = RequestedFloor


"*************************************************************"
"                 -  TEST SCENARIOS -                         "
"*************************************************************"
# Testing on different scenario

# Scenario 1 

controllerOne = columnController(10, 2)

elevatorNumOne = controllerOne.column.elevatorsColumn[0]
elevatorNumTwo = controllerOne.column.elevatorsColumn[1]

elevatorNumOne.elevatorCurrentFloor = 2
elevatorNumOne.elevatorStatus = "Idle"
elevatorNumOne.elevatorDirection = "Up"

elevatorNumTwo.elevatorCurrentFloor = 6
elevatorNumTwo.elevatorStatus = "Idle"
elevatorNumTwo.elevatorDirection = "Down"

print("")
print("---Test scenario 1 starts---")
print("")
elevator = controllerOne.RequestElevator( 5, "Up")
controllerOne.RequestFloor(elevator, 7)
print("")
print("End test scenario 1 : OK++---")
print("")


# Scenario 2 

controllerTwo = columnController(10, 2)

elevatorNumOne = controllerTwo.column.elevatorsColumn[0]
elevatorNumTwo = controllerTwo.column.elevatorsColumn[1]

elevatorNumOne.elevatorCurrentFloor = 10
elevatorNumOne.elevatorStatus = "Idle"
elevatorNumOne.elevatorDirection = "Down"
elevatorNumTwo.elevatorCurrentFloor = 3
elevatorNumTwo.elevatorStatus = "Idle"
elevatorNumTwo.elevatorDirection = "Down"

print("")
print("---Test scenario 2 starts---")
print("")


elevator = controllerTwo.RequestElevator(1, "Up")
controllerTwo.RequestFloor(elevator, 6)
elevator = controllerTwo.RequestElevator(3, "Up")
controllerTwo.RequestFloor(elevator, 5)
elevator = controllerTwo.RequestElevator(9, "Down")
controllerTwo.RequestFloor(elevator, 2)
print("")
print("End test scenario 2 : OK++ ")
print("")


print("+++++++++++++++++++++++++++++++++++++++")

# Scenario 3 

controllerThree = columnController(10, 2)

elevatorNumOne = controllerThree.column.elevatorsColumn[0]
elevatorNumTwo = controllerThree.column.elevatorsColumn[1]

elevatorNumOne.elevatorCurrentFloor = 10
elevatorNumOne.elevatorStatus = "Idle"
elevatorNumOne.elevatorDirection = "Down"

elevatorNumTwo.elevatorCurrentFloor = 3
elevatorNumTwo.elevatorStatus = "Moving"
elevatorNumTwo.elevatorDirection = "Down"



print("+++++++++++++++++++++++++++++++++++++++")

print("")
print("---Test scenario 3 starts---")
print("")

elevator = controllerThree.RequestElevator(10, "Down")
controllerThree.RequestFloor(elevator, 3)

elevator = controllerThree.RequestElevator(3, "Down")
controllerThree.RequestFloor(elevator, 2)

print("")
print("End test scenario 3 : OK++")
print("")

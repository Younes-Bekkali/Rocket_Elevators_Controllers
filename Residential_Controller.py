import time


class columnController:
    def __init__(self, nbFloors, nbElevators):
        self.nbFloors = nbFloors
        self.nbElevators = nbElevators
        self.column = Column(nbFloors, nbElevators)
       


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

    def RequestFloor(self, elevator, RequestedFloor):
        time.sleep(1)
        print("")
        print("Requested floor to ", RequestedFloor)
        time.sleep(1)
        print("")
        print("Request button illuminated")
        time.sleep(1)
        elevator.addToList(RequestedFloor)


    def selectElevator(self, floor, direction):
        bestElevator = None
        shortest_distance = 1000
        for elevator in (self.column.elevatorsColumn):
            if (floor == elevator.elevatorCurrentFloor and (elevator.elevatorStatus == "Stopped" or elevator.elevatorStatus == "Idle" or elevator.elevatorStatus == "Moving")):
                return elevator
            else:
                ref_distance = abs(floor - elevator.elevatorCurrentFloor)
                if shortest_distance > ref_distance:
                    shortest_distance = ref_distance
                    bestElevator = elevator

                elif elevator.direction == direction:
                    bestElevator = elevator

        return bestElevator


class Column:
    def __init__(self, nbFloors, nbElevators):
        self.nbFloors = nbFloors
        self.nbElevators = nbElevators
        self.elevatorsColumn = []
        for i in range(nbElevators):
            elevator = Elevator(i+1, "Idle", 1, "Up")
            self.elevatorsColumn.append(elevator)
            
class Elevator:
    def __init__(self, elevatorId, elevatorStatus, elevatorCurrentFloor, elevatorDirection):
        self.elevatorId = elevatorId
        self.elevatorStatus = elevatorStatus
        self.elevatorCurrentFloor = elevatorCurrentFloor
        self.elevatorDirection = elevatorDirection
        self.floor_list = []

    def addToList(self, RequestedFloor):
        self.floor_list.append(RequestedFloor)
        self.sort()
        self.operate_elevator(RequestedFloor)


    def sort(self):
        if self.elevatorDirection == "Up":
            self.floor_list.sort()
        elif self.elevatorDirection == "Down":
            self.floor_list.sort()
            self.floor_list.reverse()
        return self.floor_list


    def operate_elevator(self, RequestedFloor):
        while (len(self.floor_list) > 0):
            if ((RequestedFloor == self.elevatorCurrentFloor)):
                self.openDoors()
                self.elevatorStatus = "Moving"
                self.floor_list.pop()
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
                self.floor_list.pop()
                
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

                self.floor_list.pop()

        if self.floor_list == 0:
            self.elevatorStatus = "Idle"



    def openDoors(self):
        time.sleep(1)
        print("Open Door")
        print("")
        print("Button deactivated")
        time.sleep(1)
        print("")
        time.sleep(1)
        self.Close_door()

    def Close_door(self):
        print("Close door")
        print("Doors not obstructed")
        print("Elevator's capacity check: OK ")
        print("Doors Closed")
        time.sleep(1)



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
        self.light = False


class Floor_button:
    def __init__(self, RequestedFloor):
        self.RequestedFloor = RequestedFloor


"*************************************************************"
"                 -  TEST SCENARIOS -                         "
"*************************************************************"


# Scenario 1 

controllerOne = columnController(10, 2)

elevatorNumOne = controllerOne.column.elevatorsColumn[0]
elevatorNumTwo = controllerOne.column.elevatorsColumn[1]

elevatorNumOne.elevatorCurrentFloor = 2
elevatorNumOne.elevatorStatus = "Moving"
elevatorNumOne.elevatorDirection = "Down"

elevatorNumTwo.elevatorCurrentFloor = 6
elevatorNumTwo.elevatorStatus = "Moving"
elevatorNumTwo.elevatorDirection = "Down"

print("")
print("---Test scenario 1 starts---")
print("")
elevator = controllerOne.RequestElevator(5, "Up")
controllerOne.RequestFloor(elevator, 7)
print("")
print("---The end of the test scenario 1---")
print("")


# Scenario 2 

controllerTwo = columnController(10, 2)

elevatorNumOne = controllerTwo.column.elevatorsColumn[0]
elevatorNumTwo = controllerTwo.column.elevatorsColumn[1]

elevatorNumOne.elevatorCurrentFloor = 10
elevatorNumOne.elevatorStatus = "Moving"
elevatorNumOne.elevatorDirection = "Down"
elevatorNumTwo.elevatorCurrentFloor = 3
elevatorNumTwo.elevatorStatus = "Moving"
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
print("The end of the test scenario 2 ")
print("")


print("+++++++++++++++++++++++++++++++++++++++")

# Scenario 3 

controllerThree = columnController(10, 2)

elevatorNumOne = controllerThree.column.elevatorsColumn[0]
elevatorNumTwo = controllerThree.column.elevatorsColumn[1]

elevatorNumOne.elevatorCurrentFloor = 10
elevatorNumOne.elevatorStatus = "Moving"
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
print("The end of the test scenario 3")
print("")

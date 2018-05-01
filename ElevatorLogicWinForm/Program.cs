using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElevatorLogicWinForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    
    public enum ElevatorDirectionEnum
    {
        Idle = 0,
        GoingUp,
        GoingDown,
        Maintenence
    }

    public struct ElevatorStatus
    {
        public ElevatorDirectionEnum currentElevatorDirection;
        public int currentElevatorFloor;
    }

    public class FloorControls
    {
        static int HighestFloor = 10;
        static int LowestFloor = 0;

        bool UpButton;
        bool DownButton;
        int Priority;

        int FloorNum;

        public int FloorNumber { get => FloorNum; set => FloorNum = value; }


        public FloorControls()
        {
            UpButton = DownButton = false;
            FloorNumber = 0;
            Priority = 0;
        }


        public void UpButtonPressed(ElevatorStatus currentElevatorStatus)
        {
            if (FloorNumber != HighestFloor)
            {
                UpButton = true;
                Priority = CalculatePriority(currentElevatorStatus);
            } //-- On Highest floor, there is no up button. Hence no else block is reqd
        }

        public void DownButtonPressed(ElevatorStatus currentElevatorStatus)
        {
            if (FloorNumber != LowestFloor)
            {
                DownButton = true;
                Priority = CalculatePriority(currentElevatorStatus);
            } //-- On lowest floor, there is no down button. Hence no else block is reqd
        }

        public bool DoesUpButtonRequireService()
        {
            return UpButton;
        }

        public bool DoesDownButtonRequireService()
        {
            return DownButton;
        }

        public int GetPriority()
        {
            return Priority;
        }
        public void UpButtonServiced()
        {
            UpButton = false;
            Priority = 0;
            // Open elevator door
            // Get destination floor number
            // Close Elevator door
        }

        public void DownButtonServiced()
        {
            DownButton = false;
            Priority = 0;
            // Open elevator door
            // Get destination floor number
            // Close Elevator door
        }

        int CalculatePriority(ElevatorStatus currentElevatorStatus)
        {
            int currentElevatorPos = currentElevatorStatus.currentElevatorFloor;
            ElevatorDirectionEnum currentElevatorDir = currentElevatorStatus.currentElevatorDirection;
            int Multiplier = 0;
            int RelativeWeight = HighestFloor - Math.Abs(FloorNumber - currentElevatorPos);  // Weight of difference between elevator's current floor and this floor.
            int direction_hint = FloorNumber - currentElevatorPos;

            /*
            You can be in one of 4 cases:
            1. Elevator is in maintenence mode (so priority is of no use and it is returned as 0)
            2. Elevator is currently idle, so it will service your request next  (priority will be medium : say P2)
            3. Elevator is currently moving towards your floor. 
                a. You want to move in same direction as elevator is currently moving. Ideally we should make the elevator stop at your floor. (priority will be highest : say P3)
                b. You want to move in opposite direction as elevator is moving.  (priority will be medium : say P2)
            4. Elevator is currently moving away from your floor.
                a. You want to move in same direction as elevator is currently moving. You request will not be service until we reach case 2 or 3 (priority will be lowest : say P1)
                b. You want to move in opposite direction as elevator is moving. You request will not be service until we reach case 2 or 3 (priority will be lowest : say P1)
                */

            if (currentElevatorDir == ElevatorDirectionEnum.Maintenence)
            {
                // Case 1: Elevator is in maintenence mode, prioirty is 0.                
                goto ExitPoint;
            }

            if (currentElevatorDir == ElevatorDirectionEnum.Idle)
            {
                // Case 2: Elevator is currently idle. It will service your request next.
                Multiplier = 2;
                goto ExitPoint;
            }

            if (((direction_hint > 0) && (currentElevatorDir == ElevatorDirectionEnum.GoingUp)) ||
                ((direction_hint < 0) && (currentElevatorDir == ElevatorDirectionEnum.GoingDown)))
            {   // This means the Elevator is moving towards your floor:  Case 3
                if (UpButton && (currentElevatorDir == ElevatorDirectionEnum.GoingUp))
                {
                    Multiplier = 3;
                }
                else if (DownButton && (currentElevatorDir == ElevatorDirectionEnum.GoingDown))
                {
                    Multiplier = 3;
                }
                else
                {
                    Multiplier = 2;
                }
            }
            else
            {
                // Elevator is currently moving away from your floor: Case 4
                Multiplier = 1;
            }
        ExitPoint:
            return (RelativeWeight * Multiplier);
        }

    }

    public class ElevatorClass
    {
        public ElevatorStatus currStatus;
        public FloorControls[] AllFloorControls;

        public ElevatorClass()
        {
            currStatus = new ElevatorStatus();
            AllFloorControls = new FloorControls[10];
            currStatus.currentElevatorDirection = ElevatorDirectionEnum.Idle;
            currStatus.currentElevatorFloor = 0;
            for (int i = 1; i < 10; i++)
            {
                AllFloorControls[i] = new FloorControls();
                AllFloorControls[i].FloorNumber = i;
            }
        }

        public void UpButtonPressed(int Floor)
        {
            if (Floor < 10)
            {
                AllFloorControls[Floor].UpButtonPressed(currStatus);
            }
        }

        public void DownButtonPressed(int Floor)
        {
            if (Floor < 10)
            {
                AllFloorControls[Floor].UpButtonPressed(currStatus);
            }
        }

        public void Move()
        {
            if (AllFloorControls[currStatus.currentElevatorFloor].DoesUpButtonRequireService())
            {

            }

            //-- Add weights of all floors  (if floor is above currentElevatorFloor, multiply with -1)
            for (int i = 0; i < currStatus.currentElevatorFloor; i++)
            {
                if (i > currStatus.currentElevatorFloor)
                {
                    // AllFloorControls[Floor].GetPriority();
                }

            }
        }
        public void MoveUp()
        {
            for (var i = currStatus.currentElevatorFloor; i <= 10; i++) // Go to requested floor as per the weight calculated
            {

            }
        }
        public void MoveDown()
        {
            for (var i = currStatus.currentElevatorFloor; i >= 10; i++) // Go to requested floor as per the weight calculated
            {

            }
        }

        public void StopLift(int currentElevatorFloor)
        {

        }
    }
    

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ElevatorLogicWinForm
{
    public partial class Form1 : Form
    {
        ElevatorClass myElevator;
        Label[] myFloors;

        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            myElevator = new ElevatorClass();
            System.Threading.Thread ElevatorThread = new Thread(StartElevatorThread);
            InitializeComponent();
            ElevatorThread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //myElevator.currStatus.currentElevatorDirection = ElevatorDirectionEnum.Idle;         


        }

        void StartElevatorThread()
        {
                        
            while (true)
            {
                Thread.Sleep(1000);
                int UpSum = 0, DownSum = 0;
                int nearestUp = 10, nearestDown = 0;

                for (int i = 0; i < 11; i++)
                {
                    if (i < myElevator.currStatus.currentElevatorFloor)
                    {
                        DownSum += myElevator.AllFloorControls[i].GetPriority();

                        if (myElevator.AllFloorControls[i].DoesDownButtonRequireService()
                                && nearestDown > i)
                        {
                            nearestDown = i;
                        }
                    }
                    else if (i > myElevator.currStatus.currentElevatorFloor)
                    {
                        UpSum += myElevator.AllFloorControls[i].GetPriority();

                        if (myElevator.AllFloorControls[i].DoesUpButtonRequireService()
                                && nearestUp < i)
                        {
                            nearestUp = i;
                        }
                    }
                }

                if (UpSum > DownSum)
                {
                    myElevator.currStatus.currentElevatorDirection = ElevatorDirectionEnum.GoingUp;
                    myElevator.currStatus.currentElevatorFloor = nearestUp;
                    MessageTxt.Text = "Elevator is moving towards " + nearestUp;
                    myElevator.AllFloorControls[nearestUp].UpButtonServiced();
                }
                else if (DownSum > UpSum)
                {
                    myElevator.currStatus.currentElevatorDirection = ElevatorDirectionEnum.GoingDown;
                    myElevator.currStatus.currentElevatorFloor = nearestDown;
                    MessageTxt.Text = "Elevator is moving towards " + nearestDown;
                    myElevator.AllFloorControls[nearestUp].DownButtonServiced();
                }
                else
                {
                    myElevator.currStatus.currentElevatorDirection = ElevatorDirectionEnum.Idle;
                }                

                if(myElevator.currStatus.currentElevatorDirection == ElevatorDirectionEnum.Maintenence)
                {
                    break;
                }
            }
        }

 

        private void btnUp_Click(object sender, EventArgs e)
        {
            ElevatorDirectionEnum myDirection = myElevator.currStatus.currentElevatorDirection;
            int myFloor = myElevator.currStatus.currentElevatorFloor;
            string s = ((sender as Button).Tag).ToString();
            MessageTxt.Text = "The Up button on " + s + " floor button was pressed";
            myElevator.UpButtonPressed(Convert.ToInt32(s));

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            ElevatorDirectionEnum myDirection = myElevator.currStatus.currentElevatorDirection;
            int myFloor = myElevator.currStatus.currentElevatorFloor;
            string s = ((sender as Button).Tag).ToString();
            MessageTxt.Text = "The Down button on " + s + " floor button was pressed";
            myElevator.DownButtonPressed(Convert.ToInt32(s));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myElevator.SetMaintenceMode();
            MessageTxt.Text = "The elevator thread has exited.";
        }
    }
}

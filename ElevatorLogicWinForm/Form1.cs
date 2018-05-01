using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElevatorLogicWinForm
{
    public partial class Form1 : Form
    {
        ElevatorClass myElevator;
        Label[] myFloors;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myElevator = new ElevatorClass();
            myFloors = new Label[10];

            myElevator.currStatus.currentElevatorFloor = 0;
            myElevator.currStatus.currentElevatorDirection = ElevatorDirectionEnum.Idle;
            MessageTxt.Text = "The Elevator is currently in " + myElevator.currStatus.currentElevatorFloor;

            for (int i = 0; i < 10; i++)
            {
                myFloors[i] = new Label();
                myFloors[i].BackColor = System.Drawing.Color.Gray;
            }
        }



      

        public string ThreadFn()
        {
            do
            {
                int c = myElevator.currStatus.currentElevatorFloor;
                for (int i = 0; i < 10; i++)
                {
                    myFloors[i].BackColor = System.Drawing.Color.Gray;
                }

                myFloors[c].BackColor = System.Drawing.Color.Green;

            } while (true);
        }

 

        private void btnUp_Click(object sender, EventArgs e)
        {
            ElevatorDirectionEnum myDirection = myElevator.currStatus.currentElevatorDirection;
            int myFloor = myElevator.currStatus.currentElevatorFloor;
            string s = ((sender as Button).Tag).ToString();
            MessageTxt.Text = "The " + s + " floor button was pressed";
            myElevator.UpButtonPressed(Convert.ToInt32(s));
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            ElevatorDirectionEnum myDirection = myElevator.currStatus.currentElevatorDirection;
            int myFloor = myElevator.currStatus.currentElevatorFloor;
            string s = ((sender as Button).Tag).ToString();
            MessageTxt.Text = "The " + s + " floor button was pressed";
            myElevator.DownButtonPressed(Convert.ToInt32(s));
        }
    }
}

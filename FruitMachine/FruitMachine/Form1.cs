using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FruitMachine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Decimal Credit = 1;
        List<string> Fruits = new List<string>() { "Cherry", "Bell","Lemon","Orange","Star","Skull"};
        List<Image> FruitImages = new List<Image>() {Properties.Resources.Cherry, Properties.Resources.Bell, Properties.Resources.Lemon, Properties.Resources.Orange, Properties.Resources.Star, Properties.Resources.Skull };
        List<string> UserFruits = new List<string>() { "","",""};
        Random rnd = new Random();

        private void Start() 
        {
            for (int x = 0; x < 3; x++)
            {
                int randomchoice = rnd.Next(0, 6);
                UserFruits[x] = Fruits[randomchoice];
                ((PictureBox)this.Controls["pictureBox" + (x+1).ToString()]).BackgroundImage = FruitImages[randomchoice];
            }
            Credit = decimal.Subtract(Credit, Convert.ToDecimal(0.2));
            CheckRoll();
        }
        private void CheckRoll() 
        {
            btnCashOut.Enabled = true;
            if(UserFruits[0] == UserFruits[1] && UserFruits[0]== UserFruits[2]) 
            {
                if (UserFruits[0] == "Bell") 
                {
                    Credit = decimal.Add(Credit, Convert.ToDecimal(5));
                }else if(UserFruits[0] == "Skull") 
                {
                    EndGame("You are broke lol");
                }
                else
                {
                    Credit = decimal.Add(Credit, Convert.ToDecimal(1));
                }       
            }else if (UserFruits[0]== UserFruits[1] || UserFruits[0] == UserFruits[2])
            {
                if(UserFruits[0] == "Skull") 
                {
                    Credit = decimal.Subtract(Credit, Convert.ToDecimal(1));
                }
                else 
                {
                    Credit = decimal.Add(Credit, Convert.ToDecimal(0.5));
                }
            }else if (UserFruits[1] == UserFruits[2]) 
            {
                if (UserFruits[1] == "Skull")
                {
                    Credit = decimal.Subtract(Credit, Convert.ToDecimal(1));
                }
                else
                {
                    Credit = decimal.Add(Credit, Convert.ToDecimal(0.5));
                }
            }
            LBLmoney.Text = Credit.ToString() + "0";
            if (Credit <= 0) 
            {
                EndGame("You are broke lol");
            }
        }
        private void EndGame(string message) 
        {
            MessageBox.Show(message);
            Environment.Exit(0);
        }

        private void btnRoll_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void btnCashOut_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Well Done , You won £"+Credit.ToString()+"0");
            Environment.Exit(0);
        }
    }
}

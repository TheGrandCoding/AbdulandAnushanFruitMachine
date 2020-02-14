using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        List<int> FruitsCount = new List<int>() { 0,0,0,0,0,0};
        List<Image> FruitImages = new List<Image>() {Properties.Resources.Cherry, Properties.Resources.Bell, Properties.Resources.Lemon, Properties.Resources.Orange, Properties.Resources.Star, Properties.Resources.Skull };
        List<string> UserFruits = new List<string>() { "","",""};
        Random rnd = new Random();
        int twosame = 0;
        int threesame = 0;
        int threeskulls  = 0;
        int threebells = 0;
        int nosame = 0;
        bool stop = false;

        private void Start() 
        {
            for (int x = 0; x < 3; x++)
            {
                int randomchoice = rnd.Next(0, 6);
                UserFruits[x] = Fruits[randomchoice];
               // ((PictureBox)this.Controls["pictureBox" + (x+1).ToString()]).BackgroundImage = FruitImages[randomchoice];
                FruitsCount[randomchoice]++;
            }
            Credit = decimal.Subtract(Credit, Convert.ToDecimal(0.2));
            CheckRoll();
        }
        private void CheckRoll() 
        {
            this.Invoke(new MethodInvoker(delegate
            {
                btnStopRoll.Enabled = true;
            }));
            if(UserFruits[0] == UserFruits[1] && UserFruits[0]== UserFruits[2]) 
            {
                if (UserFruits[0] == "Bell") 
                {
                    Credit = decimal.Add(Credit, Convert.ToDecimal(5));
                    threebells++;
                }else if(UserFruits[0] == "Skull") 
                {
                    threeskulls++;
                    //EndGame("You are broke lol");
                }
                else
                {
                    Credit = decimal.Add(Credit, Convert.ToDecimal(1));
                }
                threesame++;
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
                twosame++;
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
                twosame++;
            }
            else
            {
                nosame++;
            }
            this.Invoke(new MethodInvoker(delegate
            {
                LBLmoney.Text = Credit.ToString() + "0";
            }));
            
            //if (Credit <= 0) 
            //{
            //    EndGame("You are broke lol");
            //}
        }
        private void EndGame(string message) 
        {
            MessageBox.Show(message);
            Environment.Exit(0);
        }

        private void btnRoll_Click(object sender, EventArgs e)
        {
            btnRoll.Enabled = false;
            Thread h = new Thread(Hi);
            h.Start();
        }
        private void Hi()
        {
            FruitsCount = new List<int>() { 0, 0, 0, 0, 0, 0 };
            threesame = 0;
            twosame = 0;
            nosame = 0;
            threebells = 0;
            threeskulls = 0;
            stop = false;
            for (int i = 0; i < Convert.ToInt32(TxtNum.Text); i++)
            {
                if (stop == true)
                {
                    break;
                }
                Start();
            }
            MessageBox.Show("Threesame:" + threesame + "\n Twosame:" + twosame + "\n Nosame:" + nosame + "\n Threebells:" + threebells + "\n ThreeSkulls:" + threeskulls);
            MessageBox.Show("\n Cherry: " + FruitsCount[0] + "\n Bell:" + FruitsCount[1] + "\n Lemon: " + FruitsCount[2] + "\n Orange: " + FruitsCount[3] + "\n Star:" + FruitsCount[4] + "\n Skull: " + FruitsCount[5]);
            this.Invoke(new MethodInvoker(delegate
            {
                btnRoll.Enabled = true;
            }));

        }

        private void btnStopRoll_Click(object sender, EventArgs e)
        {
            stop = true;
            //MessageBox.Show("Well Done , You won £"+Credit.ToString()+"0");
            //Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

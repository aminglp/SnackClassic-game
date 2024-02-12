using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnackClassic
{
    public partial class Form1 : Form
    {
        private List<Circle> Snack=new List<Circle>();
        private Circle food = new Circle();

        int maxWidth;
        int maxHeight;

        int score;
        int hightScore;

        Random random=new Random();

        bool goLeft,goRight,goDown,goUp;

        public Form1()
        {
            InitializeComponent();
            new Settings();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left && Settings.Direction != "right")
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && Settings.Direction != "left")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Down && Settings.Direction != "up")
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.Up && Settings.Direction != "down")
            {
                goUp = true;
            }



        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left )
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right )
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up )
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down )
            {
                goDown = false;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            txtGameover.Enabled = false;
            txtGameover.Visible = false;
            Restart();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {

            if (goLeft)
            {
                Settings.Direction = "left";
            }
            if (goRight)
            {
                Settings.Direction = "right";
            }
            if (goDown)
            {
                Settings.Direction = "down";
            }
            if (goUp)
            {
                Settings.Direction = "up";
            }
            //end of direction

            for (int i = Snack.Count -1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.Direction)
                    {
                        case "left":
                            Snack[i].X--;
                            break;
                        case "right":
                            Snack[i].X++;
                            break;
                        case "down":
                            Snack[i].Y++;
                            break;
                        case "up":
                            Snack[i].Y--;
                            break;

                    }


                    if (Snack[i].X < 0)
                    {
                        Snack[i].X = maxWidth;
                    }
                    if (Snack[i].X > maxWidth)
                    {
                        Snack[i].X = 0;
                    }
                    if (Snack[i].Y < 0)
                    {
                        Snack[i].Y = maxHeight;
                    }
                    if (Snack[i].Y > maxHeight)
                    {
                        Snack[i].Y = 0;
                    }
                    if (Snack[i].X == food.X && Snack[i].Y == food.Y)
                    {
                        EatFood();

                    }

                    for (int j = 1; j < Snack.Count ; j++)
                    {
                        if (Snack[i].X == Snack[j].X && Snack[i].Y == Snack[j].Y)
                        {

                            GameOver();
                        }
                    }
                }
                else
                {
                    Snack[i].X = Snack[i - 1].X;
                    Snack[i].Y = Snack[i - 1].Y;
                }
            }
            pictureBox1.Invalidate();


        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Brush snakeColour;

            for (int i = 0; i< Snack.Count ; i++)
            {
                if(i == 0)
                {
                    snakeColour = Brushes.Black;

                }
                else
                {
                    snakeColour = Brushes.DarkGreen;
                }

                graphics.FillEllipse(snakeColour,new Rectangle 
                    (
                        Snack[i].X*Settings.Width,
                        Snack[i].Y * Settings.Height,
                        Settings.Width,Settings.Height 
                    ));
            }


            graphics.FillEllipse(Brushes.DarkRed, new Rectangle
                (
                    food.X * Settings.Width,
                    food.Y * Settings.Height,
                    Settings.Width, Settings.Height
                ));

        }



        private void Restart()
        {
            maxHeight = pictureBox1.Width / Settings.Width - 1 ;
            maxWidth = pictureBox1.Height / Settings.Height - 1;

            Snack.Clear();

            btnStart.Enabled = false;
            txtGameover.Enabled = false;
            score = 0;
            txtScore.Text = "score" + score;

            Circle head= new Circle { X = 10,Y = 5 };
            Snack.Add(head);

            for (int i = 0; i < 3; i++)
            {
                Circle body = new Circle();
                Snack.Add(body);
            }

            food= new Circle { X = random.Next(2,maxWidth)   ,Y = random.Next(2, maxHeight) };
            gameTimer.Start();
        }

        private void EatFood()
        {
            score += 1;
            txtScore.Text = "Score: " + score;
            Circle body = new Circle()
            {
                X = Snack[Snack.Count - 1].X,
                Y = Snack[Snack.Count - 1].Y
            };
            Snack.Add(body);
            gameTimer.Interval -= 1;
            food = new Circle { X = random.Next(2, maxWidth), Y = random.Next(2, maxHeight) };
        }

        private void GameOver()
        {
            gameTimer.Stop();
            btnStart.Enabled= true;
           

            if (score > hightScore)
            {
                hightScore = score;
                txtHighScore.Text = "High score:" + Environment.NewLine + hightScore;
                txtHighScore.ForeColor= Color.Maroon;
                txtHighScore.TextAlign= ContentAlignment.MiddleLeft;
                txtGameover.Visible= true;
                txtGameover.Text = "GameOver";
                txtGameover.BackColor= Color.Maroon;
            }
            gameTimer.Interval = 65;

        }


    }
}

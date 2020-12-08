using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics_Lab8
{
    public partial class Form1 : Form
    {
        const int mapWidth = 20;
        const int mapHeight = 30;

        public int[,] map = new int[mapHeight, mapWidth];
        public int dirX = 0;
        public int dirY = 0;
        public int platformX;
        public int platformY;
        public int ballX;
        public int ballY;
        public int score;

        public Image arcanoidSet;

        public Label scoreLabel;
        public Button button1;
        public Form1()
        {
            InitializeComponent();

            scoreLabel = new Label();
            scoreLabel.Location = new Point((mapWidth) * 20 + 1, 50);
            /*button1 = new Button();
            button1.Location = new Point((mapWidth) * 20 + 1, 100);*/
            scoreLabel.Text = "Score: " + score;
            //button1.Text = "Start";
            this.Controls.Add(scoreLabel);
           // this.Controls.Add(button1);
            timer1.Tick += new EventHandler(update);
            this.KeyUp += new KeyEventHandler(inputCheck);
           // button1.Click += new EventHandler(start);
            Init();
        }
        /*private void start(object sender, EventArgs e) 
        {
             timer1.Start();
        }*/
        private void inputCheck(object sender, KeyEventArgs e)
        {
            map[platformY, platformX] = 0;
            map[platformY, platformX + 1] = 0;
            map[platformY, platformX + 2] = 0;
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (platformX + 1 < mapWidth - 2)
                        platformX++;
                    break;
                case Keys.Left:
                    if (platformX > 0)
                        platformX--;
                    break;
            }
            map[platformY, platformX] = 9;
            map[platformY, platformX + 1] = 99;
            map[platformY, platformX + 2] = 999;
        }
        private void update(object sender, EventArgs e)
        {
            if (ballY + dirY > mapHeight - 1)
            {
                Init();
            }


            map[ballY, ballX] = 0;
            if (!IsCollide())
                ballX += dirX;
            if (!IsCollide())
                ballY += dirY;
            map[ballY, ballX] = 8;

            map[platformY, platformX] = 9;
            map[platformY, platformX + 1] = 99;
            map[platformY, platformX + 2] = 999;

            Invalidate();
        }

        public void GeneratePlatforms()
        {
            Random r = new Random();
            for (int i = 0; i < mapHeight / 3; i++)
            {
                for (int j = 0; j < mapWidth; j += 2)
                {
                    int currPlatform = r.Next(1, 5);
                    map[i, j] = currPlatform;
                    map[i, j + 1] = currPlatform + currPlatform * 10;
                }
            }
        }

        public bool IsCollide()
        {
            int c = 0;
            bool isColliding = false;
            if (ballX + dirX > mapWidth - 1 || ballX + dirX < 0)
            {
                dirX *= -1;
                isColliding = true;
            }
            if (ballY + dirY < 0)
            {
                dirY *= -1;
                isColliding = true;
            }

            if (map[ballY + dirY, ballX] != 0)
            {
                bool addScore = false;
                isColliding = true;

                if (map[ballY + dirY, ballX] > 10 && map[ballY + dirY, ballX] < 99)
                {
                    c = map[ballY + dirY, ballX];
                    map[ballY + dirY, ballX] = 0;
                    map[ballY + dirY, ballX - 1] = 0;
                    addScore = true;
                }
                else if (map[ballY + dirY, ballX] < 9)
                {
                    c = map[ballY + dirY, ballX];
                    map[ballY + dirY, ballX] = 0;
                    map[ballY + dirY, ballX + 1] = 0;
                    addScore = true;
                }
                if (addScore)
                {
                    if (c == 11 || c == 1)
                    {
                        score += 10;
                    }
                    if (c ==22 || c == 2)
                    {
                        score += 20;
                    }
                    if (c == 33 || c == 3)
                    {
                        score += 30;
                    }
                    if (c == 44 || c == 4)
                    {
                        score += 40;
                    }
                    if (c == 55 || c == 5)
                    {
                        score += 50;
                    }
                }
                dirY *= -1;
            }
            if (map[ballY, ballX + dirX] != 0)
            {
                bool addScore = false;
                isColliding = true;

                if (map[ballY, ballX + dirX] > 10 && map[ballY + dirY, ballX] < 99)
                {
                    c = map[ballY, ballX + dirX];
                    map[ballY, ballX + dirX] = 0;
                    map[ballY, ballX + dirX - 1] = 0;
                    addScore = true;
                }
                else if (map[ballY, ballX + dirX] < 9)
                {
                    c = map[ballY, ballX + dirX];
                    map[ballY, ballX + dirX] = 0;
                    map[ballY, ballX + dirX + 1] = 0;
                    addScore = true;
                }
                if (addScore)
                {
                    if (c == 11 || c == 1)
                    {
                        score += 10;
                    }
                    if (c == 22 || c == 2)
                    {
                        score += 20;
                    }
                    if (c == 33 || c == 3)
                    {
                        score += 30;
                    }
                    if (c == 44 || c == 4)
                    {
                        score += 40;
                    }
                    if (c == 55 || c == 5)
                    {
                        score += 50;
                    }
                }
                dirX *= -1;
            }
            scoreLabel.Text = "Score: " + score;

            return isColliding;
        }
       
        public void DrawArea(Graphics g)
        {
            g.DrawRectangle(Pens.Black, 0, 0, mapWidth * 20, mapHeight * 20);
        }

        public void Init()
        {
            this.Width = (mapWidth+5) * 20;
            this.Height = (mapHeight + 2) * 20;

            arcanoidSet = new Bitmap("C:\\Users\\MSI\\Desktop\\arcanoid.png");
            timer1.Interval = 40;

            score = 0;
            
            scoreLabel.Text = "Score: "+score;

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    map[i, j] = 0;
                }
            }

            platformX = (mapWidth-1)/2;
            platformY = mapHeight-1;

            map[platformY, platformX] = 9;
            map[platformY, platformX + 1] = 99;
            map[platformY, platformX + 2] = 999;

            ballY = platformY - 1;
            ballX = platformX + 1;

            map[ballY, ballX] = 8;

            dirX = 1;
            dirY = -1;

            GeneratePlatforms();

            timer1.Start();
        }

        public void DrawMap(Graphics g)
        {
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if (map[i, j] == 9)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(60, 20)), 398, 17, 150, 50, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 8)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(20, 20)), 806, 548, 73, 73, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 1)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 20, 16, 170, 59, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 2)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 20, 16 + 77 * (map[i, j] - 1), 170, 59, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 3)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 20, 16 + 77 * (map[i, j] - 1), 170, 59, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 4)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 20, 16 + 77 * (map[i, j] - 1), 170, 59, GraphicsUnit.Pixel);
                    }
                    if (map[i, j] == 5)
                    {
                        g.DrawImage(arcanoidSet, new Rectangle(new Point(j * 20, i * 20), new Size(40, 20)), 20, 16 + 77 * (map[i, j] - 1), 170, 59, GraphicsUnit.Pixel);
                    }
                }
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            DrawArea(e.Graphics);
            DrawMap(e.Graphics);
        }
    }
}

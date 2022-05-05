using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_20212202_JPV4PC.Logic
{
    internal class GameLogic : IGameModel
    {
        public double Distance { get; set; }
        System.Drawing.Size area;
        public List<RoadMark> RoadMarks { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public event EventHandler Changed;
        public List<Car> Cars { get; set; }
        public int PlayerCarWidth { get; set; }
        public int PlayerCarHeight { get; set; }

        //Coin Feature
        public List<Coin> Coins { get; set; }
        public int CoinTimer { get; set; }
        public bool vanCoin { get; set; }

        // Bullet feature 
        public List<Bullet> Bullets { get; set; }
        public int BulletCounter { get; set; }

        //Protein feature
        public List<Protein> Proteins { get; set; }
        public int ProteinTimer { get; set; }
        public bool vanProtein { get; set; }

        //Laser feature
        public List<Laser> Lasers { get; set; }

        //Score feature
        public double Score { get; set; }

        //Game over event 
        public event EventHandler GameOver;

        public void SetupSizes(System.Drawing.Size area)
        {
            this.area = area;
            Cars = new List<Car>();
            RoadMarks = new List<RoadMark>();

            PlayerCarWidth = 100;
            PlayerCarHeight = 100;

            Coins = new List<Coin>();
            CoinTimer = 0;
            vanCoin = false;

            Bullets = new List<Bullet>();
            BulletCounter = 0;


            Proteins = new List<Protein>();
            ProteinTimer = 0;
            vanProtein = false;

            Lasers = new List<Laser>();

            Score = 0;

            if (area.Width > 500)
            {
                for (int i = 0; i < 10; i++)
                {
                    Cars.Add(new Car(new System.Windows.Size(area.Width, area.Height)));
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    Cars.Add(new Car(new System.Windows.Size(area.Width, area.Height)));
                }
            }
            RoadMarks.Add(new RoadMark(new System.Drawing.Point(area.Width / 2, 0),
                new System.Windows.Vector(0, 60)));

            Coins.Add(new Coin(new System.Drawing.Point(Randomizer(20, area.Width - 20), 0),
                new System.Windows.Vector(0, 50)));

            Bullets.Add(new Bullet(new System.Drawing.Point(Randomizer(20, area.Width - 20), 0),
                new System.Windows.Vector(0, 50)));
            Proteins.Add(new Protein(new System.Drawing.Point(Randomizer(20, area.Width - 20), 0),
                new System.Windows.Vector(0, 50)));
        }
        public GameLogic(string name, string Color)
        {
            this.Name = name;
            this.Color = Color;
        }

        static Random r = new Random();

        private int Randomizer(int min, int max)
        {
            int rnd = 0;
            do
            {
                rnd = r.Next(min, max + 1);
            } while (rnd == 0);
            return rnd;
        }
        public enum Controls
        {
            Left, Right, Shoot
        }
        public void Control(Controls control)
        {
            switch (control)
            {
                case Controls.Left:
                    if (Distance > -area.Width / 2 + 70)
                    {
                        Distance -= 40;
                    }

                    break;
                case Controls.Right:
                    if (Distance < area.Width / 2 - PlayerCarWidth / 2)
                    {
                        Distance += 40;
                    }
                    break;
                case Controls.Shoot:
                    NewShoot();
                    break;
                default:
                    break;
            }
            Changed?.Invoke(this, null);
        }

        private void NewShoot()
        {
            if (BulletCounter > 0)
            {
                Lasers.Add(new Laser(new System.Drawing.Point(area.Width / 2 + (int)Distance, area.Height - 70),
                new System.Windows.Vector(0, -120)));
                BulletCounter--;
            }
        }
        public void TimeStep()
        {
            if (CoinTimer > 0)
            {
                CoinTimer--;
            }
            else
            {
                vanCoin = false;
            }

            if (ProteinTimer > 0 && vanProtein)
            {
                ProteinTimer--;
                PlayerCarHeight = 200;
                PlayerCarWidth = 200;
            }
            else
            {
                vanProtein = false;
                PlayerCarHeight = 100;
                PlayerCarWidth = 100;
            }

            Score = Math.Round(Score + 0.1, 2);

            for (int i = 0; i < Lasers.Count; i++)
            {
                bool inside = Lasers[i].Move(area);
                if (!inside)
                {
                    Lasers.RemoveAt(i);
                }
            }

            for (int i = 0; i < RoadMarks.Count; i++)
            {
                bool inside = RoadMarks[i].Move(area);
                if (!inside)
                {
                    RoadMarks.RemoveAt(i);
                    RoadMarks.Add(new RoadMark(new System.Drawing.Point(area.Width / 2, 0),
                new System.Windows.Vector(0, 60)));
                }
            }
            for (int i = 0; i < Bullets.Count; i++)
            {
                bool inside = Bullets[i].Move(area);
                if (!inside)
                {
                    Bullets.RemoveAt(i);
                }
            }

            //Moving opposite cars
            for (int i = 0; i < Cars.Count; i++)
            {
                bool inside = Cars[i].Move(new System.Drawing.Size((int)area.Width, (int)area.Height));
                if (!inside)
                {
                    Cars.RemoveAt(i);
                    Cars.Add(new Car(new System.Windows.Size(area.Width, area.Height)));

                    if (vanCoin == true)
                    {
                        foreach (var item in Cars)
                        {
                            item.Speed = new System.Windows.Vector(0, 40);
                        }
                    }
                }
                else
                {
                    Rect CarRect = new Rect(Cars[i].Center.X, Cars[i].Center.Y, 70, 70);
                    Rect PlayerRect = new Rect((area.Width / 2 - 50) + (int)(Distance), area.Height - 110, PlayerCarWidth, PlayerCarHeight);
                    if (CarRect.IntersectsWith(PlayerRect) && CoinTimer == 0)
                    {

                        Cars.RemoveAt(i);
                        Cars.Add(new Car(new System.Windows.Size(area.Width, area.Height)));
                        Cars.Clear();
                        GameOver?.Invoke(this, null);


                    }

                    for (int j = 0; j < Lasers.Count; j++)
                    {
                        Rect laserRect = new Rect(Lasers[j].Center.X, Lasers[j].Center.Y, 40, 40);
                        if (laserRect.IntersectsWith(CarRect))
                        {
                            Cars.RemoveAt(j);
                            Cars.Add(new Car(new System.Windows.Size(area.Width, area.Height)));
                            if (vanCoin == true)
                            {
                                foreach (var item in Cars)
                                {
                                    item.Speed = new System.Windows.Vector(0, 40);
                                }
                            }
                            Lasers.Remove(Lasers[j]);
                        }
                    }
                }

            }

            //Moving cars
            for (int i = 0; i < Coins.Count; i++)
            {
                bool inside = Coins[i].Move(area);

                if (!inside)
                {
                    Coins.RemoveAt(i);
                }
                else
                {
                    Rect CoinRect = new Rect(Coins[i].Center.X, Coins[i].Center.Y, 60, 100);
                    Rect PlayerRect = new Rect((area.Width / 2 - 50) + (int)(Distance), area.Height - 110, 100, 100);
                    if (CoinRect.IntersectsWith(PlayerRect))
                    {
                        Coins.RemoveAt(i);
                        vanCoin = true;
                        CoinTimer = Randomizer(50, 100);
                    }
                }
            }
            // Bullets moving
            for (int i = 0; i < Bullets.Count; i++)
            {
                bool inside = Bullets[i].Move(area);

                if (!inside)
                {
                    Bullets.RemoveAt(i);
                }
                else
                {
                    Rect BulletRect = new Rect(Bullets[i].Center.X, Bullets[i].Center.Y, 60, 100);
                    Rect PlayerRect = new Rect((area.Width / 2 - 50) + (int)(Distance), area.Height - 110, 100, 100);
                    if (BulletRect.IntersectsWith(PlayerRect))
                    {
                        Bullets.RemoveAt(i);
                        BulletCounter += 10;
                    }
                }
            }
            //protein
            for (int i = 0; i < Proteins.Count; i++)
            {
                bool inside = Proteins[i].Move(area);

                if (!inside)
                {
                    Proteins.RemoveAt(i);
                }
                else
                {
                    Rect ProteinRect = new Rect(Proteins[i].Center.X, Proteins[i].Center.Y, 60, 100);
                    Rect PlayerRect = new Rect((area.Width / 2 - 50) + (int)(Distance), area.Height - 110, 100, 100);
                    if (ProteinRect.IntersectsWith(PlayerRect))
                    {
                        Proteins.RemoveAt(i);
                        vanProtein = true;
                        ProteinTimer = Randomizer(10, 40);

                    }


                }
            }
            //coins creation
            int r = Randomizer(0, 100);
            if (r < 3)
            {
                Coins.Add(new Coin(new System.Drawing.Point(Randomizer(20, area.Width - 20), 0), new System.Windows.Vector(0, 20)));
            }
            
            // protein creation
            int r2 = Randomizer(0, 100);
            if (r2 < 2)
            {
                Proteins.Add(new Protein(new System.Drawing.Point(Randomizer(20, area.Width - 20), 0), new System.Windows.Vector(0, 20)));
            }

            //  Bullets creation
            int r3 = Randomizer(0, 100);
            if (r3 < 4)
            {
                Bullets.Add(new Bullet(new System.Drawing.Point(Randomizer(20, area.Width - 20), 0), new System.Windows.Vector(0, 20)));
            }

            

            Changed?.Invoke(this, null);
        }
    }
}

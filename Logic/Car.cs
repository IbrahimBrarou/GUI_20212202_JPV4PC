using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_20212202_JPV4PC.Logic
{
    internal class Car
    {
        public System.Drawing.Point Center { get; set; }
        public Vector Speed { get; set; }
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
        public Car(Size area)
        {
            int vel = r.Next(0, 2); //0..3
            switch (vel)
            {
                case 0:
                    //left
                    Center = new System.Drawing.Point
                        (r.Next(50, (int)area.Width / 2 + 50), 0);

                    Speed = new Vector(0,
                        Randomizer(5, 20));
                    break;
                case 1:
                    //right
                    Center = new System.Drawing.Point(r.Next(-50 + (int)area.Width / 2, (int)area.Width - 50), 0);
                    Speed = new Vector(0, Randomizer(5, 20));
                    break;
                default:
                    break;
            }

        }
        public bool Move(System.Drawing.Size area)
        {
            System.Drawing.Point newCenter =
                new System.Drawing.Point(Center.X + (int)Speed.X,
                Center.Y + (int)Speed.Y);
            if (newCenter.X >= 0 &&
                newCenter.X <= area.Width &&
                newCenter.Y >= 0 &&
                newCenter.Y <= area.Height
                )
            {
                Center = newCenter;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

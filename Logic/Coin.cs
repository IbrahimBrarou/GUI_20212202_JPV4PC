using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_20212202_JPV4PC.Logic
{
    internal class Coin
    {
        public System.Drawing.Point Center { get; set; }
        public Vector Speed { get; set; }

        public Coin(System.Drawing.Point center, Vector speed)
        {
            Center = center;
            Speed = speed;
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

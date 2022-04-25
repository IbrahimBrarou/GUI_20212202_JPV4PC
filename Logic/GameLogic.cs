using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_JPV4PC.Logic
{
    internal class GameLogic : IGameModel
    {
        System.Drawing.Size area;
        public List<RoadMark> RoadMarks { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public event EventHandler Changed;
        public List<Car> Cars { get; set; }

        public void SetupSizes(System.Drawing.Size area)
        {
            this.area = area;
            Cars = new List<Car>();
            RoadMarks = new List<RoadMark>();
            RoadMarks.Add(new RoadMark(new System.Drawing.Point(area.Width / 2, 0),
                new System.Windows.Vector(0, 60)));
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
        }
        public GameLogic(string name, string Color)
        {
            this.Name = name;
            this.Color = Color;
        }
        public void TimeStep()
        {
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

            for (int i = 0; i < Cars.Count; i++)
            {
                bool inside = Cars[i].Move(new System.Drawing.Size((int)area.Width, (int)area.Height));
                if (!inside)
                {
                    Cars.RemoveAt(i);
                    Cars.Add(new Car(new System.Windows.Size(area.Width, area.Height)));

                    foreach (var item in Cars)
                    {
                        item.Speed = new System.Windows.Vector(0, 40);
                    }


                }
                Changed?.Invoke(this, null);
            }
        }
    }
}

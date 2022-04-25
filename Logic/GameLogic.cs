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
        public void SetupSizes(System.Drawing.Size area)
        {
            this.area = area;
            RoadMarks = new List<RoadMark>();
            RoadMarks.Add(new RoadMark(new System.Drawing.Point(area.Width / 2, 0),
                new System.Windows.Vector(0, 60)));
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
            Changed?.Invoke(this, null);
        }
    }
}

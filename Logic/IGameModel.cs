using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_JPV4PC.Logic
{
    internal interface IGameModel
    {
        double Distance { get; set; }
        List<RoadMark> RoadMarks { get; set; }
        string Name { get; set; }
        string Color { get; set; }
        event EventHandler Changed;
        List<Car> Cars { get; set; }
        int PlayerCarWidth { get; set; }
        int PlayerCarHeight { get; set; }
    }
}

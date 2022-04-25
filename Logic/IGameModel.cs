using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_JPV4PC.Logic
{
    internal interface IGameModel
    {
        List<RoadMark> RoadMarks { get; set; }
        string Name { get; set; }
        string Color { get; set; }
        event EventHandler Changed;

    }
}

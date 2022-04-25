using GUI_20212202_JPV4PC.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GUI_20212202_JPV4PC.Renderer
{
    class Display : FrameworkElement
    {
        Size area;
        IGameModel model;
        public void SetupSizes(Size area)
        {
            this.area = area;
            this.InvalidateVisual();
        }
        public void SetupModel(IGameModel model)
        {
            this.model = model;
            this.model.Changed += (sender, eventargs) => this.InvalidateVisual();
        }
        public Brush RoadBrush
        {
            get
            {
                return Brushes.Black;


            }
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (area.Width > 0 && area.Height > 0 && model != null)
            {
                drawingContext.DrawRectangle(RoadBrush, null, new Rect(0, 0, area.Width, area.Height));
                foreach (var item in model.RoadMarks)
                {
                    drawingContext.DrawRectangle(Brushes.White, null, new Rect(item.Center.X, item.Center.Y, 20, 100));
                }
            }
        }

    }
}

using GUI_20212202_JPV4PC.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        public Brush CarBrush
        {
            get
            {

                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "BlueCar.png"), UriKind.RelativeOrAbsolute)));
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

                foreach (var item in model.Cars)
                {
                    drawingContext.DrawRectangle(CarBrush, null, new Rect(item.Center.X, item.Center.Y, 100, 130));
                }





            }
        }

    }
}

using GUI_20212202_JPV4PC.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public Brush PlayerCarBrush
        {
            get
            {
                if (model.Color == "Yellow")
                {
                    return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "YellowCar.png"), UriKind.RelativeOrAbsolute)));
                }
                else if (model.Color == "Green")
                {
                    return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "GreenCar.png"), UriKind.RelativeOrAbsolute)));
                }
                else
                {
                    return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "RedCar.png"), UriKind.RelativeOrAbsolute)));
                }
            }
        }
        public Brush CoinBrush
        {
            get
            {

                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Dollar Coin.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush BulletBrush
        {
            get
            {

                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Bullet.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (area.Width > 0 && area.Height > 0 && model != null)
            {
                if (model.vanCoin == true)
                {
                    drawingContext.DrawRectangle(Brushes.Gray, null, new Rect(0, 0, area.Width, area.Height));
                }
                else
                {
                    drawingContext.DrawRectangle(RoadBrush, null, new Rect(0, 0, area.Width, area.Height));
                }
                foreach (var item in model.RoadMarks)
                {
                    drawingContext.DrawRectangle(Brushes.White, null, new Rect(item.Center.X, item.Center.Y, 20, 100));
                }

                drawingContext.PushTransform(new TranslateTransform(model.Distance, 0));
                drawingContext.DrawRectangle(PlayerCarBrush, null, new Rect(area.Width / 2 - 50, area.Height - 110, model.PlayerCarWidth, model.PlayerCarHeight));
                drawingContext.Pop();

                foreach (var item in model.Cars)
                {
                    drawingContext.DrawRectangle(CarBrush, null, new Rect(item.Center.X, item.Center.Y, 100, 130));
                }
                if (model.vanCoin)
                {
                    drawingContext.DrawText(new FormattedText("Coin Timer :" + model.CoinTimer, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 22, Brushes.White, VisualTreeHelper.GetDpi(this).PixelsPerDip), new Point(area.Width - 200, 0));
                }
                if (model.BulletCounter > 0)
                {
                    drawingContext.DrawText(new FormattedText("Bullets :" + model.BulletCounter, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 22, Brushes.White, VisualTreeHelper.GetDpi(this).PixelsPerDip), new Point(0, 50));
                }

                foreach (var item in model.Coins)
                {
                    drawingContext.DrawRectangle(CoinBrush, null, new Rect(item.Center.X, item.Center.Y, 30, 30));
                }
                foreach (var item in model.Bullets)
                {
                    drawingContext.DrawRectangle(BulletBrush, null, new Rect(item.Center.X, item.Center.Y, 40, 40));
                }


            }
        }

    }
}

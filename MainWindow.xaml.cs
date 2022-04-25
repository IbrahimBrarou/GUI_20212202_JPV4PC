using GUI_20212202_JPV4PC.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GUI_20212202_JPV4PC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameLogic logic;
        string name;
        string lvl;
        string carColor;
        public MainWindow(string name, string lvl, string color)
        {
            InitializeComponent();
            this.name = name;
            this.lvl = lvl;
            this.carColor = color;
            logic = new GameLogic(name, color);
            display.SetupModel(logic);

            DispatcherTimer dt = new DispatcherTimer();

            if (lvl == "Easy")
            {
                dt.Interval = TimeSpan.FromMilliseconds(100);
            }
            else if (lvl == "Medium")
            {
                dt.Interval = TimeSpan.FromMilliseconds(70);
            }
            else
            {
                dt.Interval = TimeSpan.FromMilliseconds(50);
            }

            dt.Tick += Dt_Tick;
            dt.Start();
        }
        private void Dt_Tick(object sender, EventArgs e)
        {
            logic.TimeStep();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.SetupSizes(new Size(grid.ActualWidth, grid.ActualHeight));
            logic.SetupSizes(new System.Drawing.Size((int)grid.ActualWidth, (int)grid.ActualHeight));
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.SetupSizes(new Size(grid.ActualWidth, grid.ActualHeight));

            logic.SetupSizes(new System.Drawing.Size((int)grid.ActualWidth, (int)grid.ActualHeight));
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                logic.Control(GameLogic.Controls.Left);
            }
            else if (e.Key == Key.Right)
            {
                logic.Control(GameLogic.Controls.Right);
            }
            else if (e.Key == Key.Space)
            {
                logic.Control(GameLogic.Controls.Shoot);
            }
        }
    }
}

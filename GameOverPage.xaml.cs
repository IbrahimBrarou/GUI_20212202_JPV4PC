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
using System.Windows.Shapes;

namespace GUI_20212202_JPV4PC
{
    /// <summary>
    /// Interaction logic for GameOverPage.xaml
    /// </summary>
    public partial class GameOverPage : Window
    {
        public string name;
        public double score;
        public GameOverPage(double score, string name)
        {
            InitializeComponent();
            this.score = score;
            this.name = name;
            this.DataContext = new playerrr
            {
                Name = name,
                Score = score
            };

        }
        private void Replay(object sender, RoutedEventArgs e)
        {
            new StartPage().Show();
            this.Close();
        }
    }
    public class playerrr
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private double score;

        public double Score
        {
            get { return score; }
            set { score = value; }
        }
    }
}

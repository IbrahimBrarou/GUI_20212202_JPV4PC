using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_20212202_JPV4PC.ViewModel
{
    public class StartPageWindowViewModel: ObservableRecipient
    {
        private string carColor;
        public string CarColor
        {
            get => carColor;
            set
            {
                SetProperty(ref carColor, value);
            }
        }
        private string lvl;
        public string Lvl
        {
            get => lvl;
            set
            {
                SetProperty(ref lvl, value);
            }
        }
        private string name;

        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
            }
        }
        public RelayCommand StartCommand { get; set; }

        public StartPageWindowViewModel()
        {
            StartCommand = new RelayCommand(OpenGameWindow);
        }
        private void OpenGameWindow()
        {
            if (name != null)
            {
                new MainWindow(name, lvl, carColor).Show();
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.Title == "StartPage")
                    {
                        item.Close();
                    }


                }
            }


        }
    }
}

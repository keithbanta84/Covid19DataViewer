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

namespace CovidDataViewer
{
    /// <summary>
    /// Interaction logic for LocationDetailedWindow.xaml
    /// </summary>
    public partial class LocationDetailedWindow : Window
    {
        public long MaxBarSize = 0;

        public LocationDetailedWindow(LocationVM locationVM)
        {
            InitializeComponent();
            this.DataContext = locationVM;
            MaxBarSize = locationVM.TotalConfirmedForLocation;
        }
    }
}

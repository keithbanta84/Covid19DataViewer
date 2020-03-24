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

namespace CovidDataViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel vm = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void Button_ReadFiles_Click(object sender, RoutedEventArgs e)
        {
            vm.ReadDataFromAllCsv(vm.FileNames.ToList());
            foreach (var location in vm.LocationData)
            {
                location.RefreshTotalsByLocation();
            }
            vm.FileNames.Clear();
            
        }



        private void Button_OpenLocationWindow_Click(object sender, RoutedEventArgs e)
        {
            LocationDetailedWindow window = new LocationDetailedWindow((LocationVM)DataGrid_LocationData.SelectedItem);
            window.Show();
        }

        private void ListView_Files_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }

        private void ListView_Files_PreviewDrop(object sender, DragEventArgs e)
        {
            vm.FileNames.Clear();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                vm.FileNames.Add(file);
            }
            e.Handled = true;//This is file being dropped not copied text so we handle it
        }
    }
}

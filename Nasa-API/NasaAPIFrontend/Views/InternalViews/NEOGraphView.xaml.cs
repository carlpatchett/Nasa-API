using NasaAPICore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace NasaAPIFrontend.Views.InternalViews
{
    /// <summary>
    /// Interaction logic for NEOGraphView.xaml
    /// </summary>
    public partial class NEOGraphView : UserControl
    {
        private NEOViewer mViewer;

        public NEOGraphView(NEOViewer viewer)
        {
            InitializeComponent();

            mViewer = viewer;
        }

        public ObservableCollection<string> GraphByValues = new ObservableCollection<string>() { "Date" };
    }
}

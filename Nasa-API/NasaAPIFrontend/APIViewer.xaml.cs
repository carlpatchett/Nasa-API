using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace NasaAPIFrontend
{
    /// <summary>
    /// Interaction logic for APIViewer.xaml
    /// </summary>
    public partial class APIViewer : Window, INotifyPropertyChanged
    {
        private UserControl mCurrentControl;

        private NEOViewer mNEOViewer = new NEOViewer();

        public event PropertyChangedEventHandler PropertyChanged;

        public APIViewer()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public UserControl CurrentControl
        {
            get
            {
                return mCurrentControl;
            }
            set
            {
                mCurrentControl = value;

                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.CurrentControl)));
            }
        }

        private void NEOViewBtn_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentControl = mNEOViewer;
        }
    }
}

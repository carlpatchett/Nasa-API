using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using NasaAPICore;
using NasaAPIFrontend.Views.InternalViews;

namespace NasaAPIFrontend
{
    /// <summary>
    /// Interaction logic for NEOViewer.xaml
    /// </summary>
    public partial class NEOViewer : UserControl, INotifyPropertyChanged
    {
        private NEOTreeView mNEOTreeView;
        private NEOGraphView mNEOGraphView;
        private Dispatcher mDispatcher;
        private UserControl mInternalView;

        public event PropertyChangedEventHandler PropertyChanged;

        public NEOViewer()
        {
            InitializeComponent();

            this.APIHub = new APIHub();

            this.DataContext = this;

            mNEOTreeView = new NEOTreeView(this);
            mNEOGraphView = new NEOGraphView(this);
            mDispatcher = Dispatcher.CurrentDispatcher;

            this.InternalView = mNEOTreeView;
        }

        public UserControl InternalView
        {
            get
            {
                return mInternalView;
            }
            set
            {
                mInternalView = value;

                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.InternalView)));
            }
        }

        public APIHub APIHub { get; set; }

        private void TreeViewBtn_Click(object sender, RoutedEventArgs e)
        {
            this.InternalView.Content = mNEOTreeView;
        }

        private void GraphViewBtn_Click(object sender, RoutedEventArgs e)
        {
            this.InternalView.Content = mNEOGraphView;
        }
    }
}

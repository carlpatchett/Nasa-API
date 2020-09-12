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

namespace NasaAPIFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<HierarchicalNode> mRootNodes;
        private Dispatcher mDispatcher;

        public MainWindow()
        {
            InitializeComponent();

            mDispatcher = Dispatcher.CurrentDispatcher;

            this.APIHub = new APIHub();

            this.DataContext = this;

            mRootNodes = new ObservableCollection<HierarchicalNode>();

            this.NEOTreeView.ItemsSource = this.RootNodes;

            this.EndDatePicker.SelectedDate = DateTime.UtcNow;

            this.OrderByCombobox.ItemsSource = this.PropertyValues;
        }

        public APIHub APIHub { get; set; }

        public bool OrderByDescending { get; set; }

        public IEnumerable<HierarchicalNode> RootNodes { get { return mRootNodes; } }

        public ObservableCollection<string> PropertyValues = new ObservableCollection<string>() { "Absolute Magnitude", 
                                                                                                  "Estimated Diameter", 
                                                                                                  "Potentially Hazardous",
                                                                                                  "Miss Distance" };

        private async void RetrieveNEOsBtn_Click(object sender, RoutedEventArgs e)
        {
            var neoRequest = await this.APIHub.APIRequestHub.PerformAPIRequestNEO(this.StartDatePicker.SelectedDate.Value, this.EndDatePicker.SelectedDate.Value);
            var neos = this.APIHub.APIParserHub.ParseNEOs(neoRequest);

            // Order the query if we've selected something to OrderBy
            IEnumerable<NearEarthObject> query = null;
            if (this.OrderByCombobox.SelectedItem != null)
            {
                switch (this.OrderByCombobox.SelectedItem.ToString())
                {
                    case "Absolute Magnitude":
                        if (this.OrderByDescending)
                        {
                            query = neos.OrderByDescending(neo => neo.AbsoluteMagnitude);
                        }
                        else
                        {
                            query = neos.OrderBy(neo => neo.AbsoluteMagnitude);
                        }
                        break;

                    case "Estimated Diameter":
                        if (this.OrderByDescending)
                        {
                            query = neos.OrderByDescending(neo => neo.EstimatedDiameter.KilometersMax);
                        }
                        else
                        {
                            query = neos.OrderBy(neo => neo.EstimatedDiameter.KilometersMax);
                        }
                        break;

                    case "Potentially Hazardous":
                        if (this.OrderByDescending)
                        {
                            query = neos.OrderByDescending(neo => neo.PotentiallyHazardous);
                        }
                        else
                        {
                            query = neos.OrderBy(neo => neo.PotentiallyHazardous);
                        }
                        break;

                    case "Miss Distance":
                        if(this.OrderByDescending)
                        {
                            query = neos.OrderByDescending(neo => neo.CloseApproachData.MissDistance.Kilometers);
                        }
                        else
                        {
                            query = neos.OrderBy(neo => neo.CloseApproachData.MissDistance.Kilometers);
                        }
                        break;
                }
            }

            if (query == null)
            {
                query = neos;
            }

            foreach (var neo in query)
            {
                if (mRootNodes.FirstOrDefault(node => String.Equals(node.Name, neo.Name)) != null)
                {
                    // Skip if we already contain it.
                    continue;
                }

                mRootNodes.Add(this.ParseNEOIntoNode(neo));
            }

            foreach (var node in mRootNodes)
            {
                if (neos.FirstOrDefault(neo => String.Equals(neo.Name, node.Name)) == null)
                {
                    // Remove if current list contains it.
                    mRootNodes.Remove(node);
                }
            }
        }

        private HierarchicalNode ParseNEOIntoNode(NearEarthObject neo)
        {
            // Top Level Name
            var root = new HierarchicalNode()
            {
                Name = neo.Name
            };

            // Name Property
            root.Children.Add(new HierarchicalNode()
            {
                Name = "Name",
                Value = neo.Name
            });

            // Id Property
            root.Children.Add(new HierarchicalNode()
            {
                Name = "Id",
                Value = neo.Id
            });

            // Nasa JPL URL Property
            root.Children.Add(new HierarchicalNode()
            {
                Name = "Nasa JPL URL",
                Value = neo.NasaJplUrl
            });

            // Absolute Magnitude Property
            root.Children.Add(new HierarchicalNode()
            {
                Name = "Absolute Magnitude",
                Value = neo.AbsoluteMagnitude.ToString()
            });

            // Estimated Diameter Property
            var estimatedDiameterNode = new HierarchicalNode()
            {
                Name = "Estimated Diameter"
            };

            estimatedDiameterNode.Children.Add(new HierarchicalNode()
            {
                Name = "Kilometers Max",
                Value = neo.EstimatedDiameter.KilometersMax.ToString()
            });

            estimatedDiameterNode.Children.Add(new HierarchicalNode()
            {
                Name = "Kilometers Min",
                Value = neo.EstimatedDiameter.KilometersMin.ToString()
            });

            estimatedDiameterNode.Children.Add(new HierarchicalNode()
            {
                Name = "Meters Max",
                Value = neo.EstimatedDiameter.MetersMax.ToString()
            });

            estimatedDiameterNode.Children.Add(new HierarchicalNode()
            {
                Name = "Meters Min",
                Value = neo.EstimatedDiameter.MetersMin.ToString()
            });

            estimatedDiameterNode.Children.Add(new HierarchicalNode()
            {
                Name = "Miles Max",
                Value = neo.EstimatedDiameter.MilesMax.ToString()
            });

            estimatedDiameterNode.Children.Add(new HierarchicalNode()
            {
                Name = "Miles Min",
                Value = neo.EstimatedDiameter.MilesMin.ToString()
            });

            estimatedDiameterNode.Children.Add(new HierarchicalNode()
            {
                Name = "Feet Max",
                Value = neo.EstimatedDiameter.FeetMax.ToString()
            });

            estimatedDiameterNode.Children.Add(new HierarchicalNode()
            {
                Name = "Feet Min",
                Value = neo.EstimatedDiameter.FeetMin.ToString()
            });

            root.Children.Add(estimatedDiameterNode);

            // Potentially Hazardous Property
            root.Children.Add(new HierarchicalNode()
            {
                Name = "Potentially Hazardous",
                Value = neo.PotentiallyHazardous.ToString()
            });

            // Close Approch Data Property
            var closeApproachDataNode = new HierarchicalNode()
            {
                Name = "Close Approach Data"
            };

            closeApproachDataNode.Children.Add(new HierarchicalNode()
            {
                Name = "Close Approach Date",
                Value = neo.CloseApproachData.CloseApproachDate.ToString("yyyy-MM-dd")
            });

            closeApproachDataNode.Children.Add(new HierarchicalNode()
            {
                Name = "Epoch Date Close",
                Value = neo.CloseApproachData.EpochDateClose.ToString()
            });

            // Relative Velocity Property
            var relativeVelocityNode = new HierarchicalNode()
            {
                Name = "Relative Velocity"
            };

            relativeVelocityNode.Children.Add(new HierarchicalNode()
            {
                Name = "Kilometers Per Second",
                Value = neo.CloseApproachData.RelativeVelocity.KilometersPerSecond.ToString()
            });

            relativeVelocityNode.Children.Add(new HierarchicalNode()
            {
                Name = "Kilometers Per Hour",
                Value = neo.CloseApproachData.RelativeVelocity.KilometersPerHour.ToString()
            });

            relativeVelocityNode.Children.Add(new HierarchicalNode()
            {
                Name = "Miles Per Hour",
                Value = neo.CloseApproachData.RelativeVelocity.MilesPerHour.ToString()
            });

            closeApproachDataNode.Children.Add(relativeVelocityNode);

            // Miss Distance Property
            var missDistanceNode = new HierarchicalNode()
            {
                Name = "Miss Distance"
            };

            missDistanceNode.Children.Add(new HierarchicalNode()
            {
                Name = "Astronomical",
                Value = neo.CloseApproachData.MissDistance.Astronomical.ToString()
            });

            missDistanceNode.Children.Add(new HierarchicalNode()
            {
                Name = "Lunar",
                Value = neo.CloseApproachData.MissDistance.Lunar.ToString()
            });

            missDistanceNode.Children.Add(new HierarchicalNode()
            {
                Name = "Kilometers",
                Value = neo.CloseApproachData.MissDistance.Kilometers.ToString()
            });

            missDistanceNode.Children.Add(new HierarchicalNode()
            {
                Name = "Miles",
                Value = neo.CloseApproachData.MissDistance.Miles.ToString()
            });

            closeApproachDataNode.Children.Add(missDistanceNode);

            // Orbiting Body Property
            closeApproachDataNode.Children.Add(new HierarchicalNode()
            {
                Name = "Orbiting Body",
                Value = neo.CloseApproachData.OrbitingBody
            });

            root.Children.Add(closeApproachDataNode);

            // Is Sentry Object Property
            root.Children.Add(new HierarchicalNode()
            {
                Name = "Is Sentry Object",
                Value = neo.IsSentryObject.ToString()
            });

            return root;

        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.EndDatePicker.DisplayDateEnd = this.StartDatePicker.SelectedDate.Value.AddDays(7.0);
            this.EndDatePicker.DisplayDateStart = this.StartDatePicker.SelectedDate.Value;

            this.RetrieveNEOsBtn.IsEnabled = this.EndDatePicker.SelectedDate.HasValue && this.StartDatePicker.SelectedDate.HasValue;
        }

        private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.StartDatePicker.DisplayDateStart = this.EndDatePicker.SelectedDate.Value.AddDays(-7.0);
            this.StartDatePicker.DisplayDateEnd = this.EndDatePicker.SelectedDate.Value;

            this.RetrieveNEOsBtn.IsEnabled = this.EndDatePicker.SelectedDate.HasValue && this.StartDatePicker.SelectedDate.HasValue;
        }
    }
}

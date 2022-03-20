using LiveCharts;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MetricsApp
{
    /// <summary>
    /// Interaction logic for MaterialCards.xaml
    /// </summary>
    public partial class MaterialCards : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ChartValues<int> LineSeriesValue { get; set; }

        public MaterialCards()
        {
            InitializeComponent();
            LineSeriesValue = new ChartValues<int>();
            DataContext = this;
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateOnСlick(object sender, RoutedEventArgs e)
        {
            //TimePowerChart.Update(true);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MetricsApp.Models;

namespace MetricsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _agentId = "";

        public MainWindow()
        {
            InitializeComponent();
            Metrics.Visibility = Visibility.Collapsed;
        }

        private async void CpuMetric_Button(object sender, RoutedEventArgs e)
        {
            SetBackgroundGray(Metrics.Children);

            Button metricButton = (Button)sender;
            metricButton.Background = new SolidColorBrush(Colors.Green);

            (TimeSpan fromTime, TimeSpan toTime) = GetTime();
            var collection = await MetricsManagerClient<Metric>.GetValues($"metrics/cpu/agent/{_agentId}/from/{fromTime}/to/{toTime}");
            SetChartValues(collection);
        }

        private async void RamMetric_Button(object sender, RoutedEventArgs e)
        {
            SetBackgroundGray(Metrics.Children);

            Button metricButton = (Button)sender;
            metricButton.Background = new SolidColorBrush(Colors.Green);

            (TimeSpan fromTime, TimeSpan toTime) = GetTime();
            var collection = await MetricsManagerClient<Metric>.GetValues($"metrics/ram/agent/{_agentId}/from/{fromTime}/to/{toTime}");
            SetChartValues(collection);
        }

        private async void HddMetric_Button(object sender, RoutedEventArgs e)
        {
            SetBackgroundGray(Metrics.Children);
            
            Button metricButton = (Button)sender;
            metricButton.Background = new SolidColorBrush(Colors.Green);

            (TimeSpan fromTime, TimeSpan toTime) = GetTime();
            var collection = await MetricsManagerClient<Metric>.GetValues($"metrics/hdd/agent/{_agentId}/from/{fromTime}/to/{toTime}");
            SetChartValues(collection);
        }

        private async void NetworkMetric_Button(object sender, RoutedEventArgs e)
        {
            SetBackgroundGray(Metrics.Children);

            Button metricButton = (Button)sender;
            metricButton.Background = new SolidColorBrush(Colors.Green);

            (TimeSpan fromTime, TimeSpan toTime) = GetTime();
            var collection = await MetricsManagerClient<Metric>.GetValues($"metrics/network/agent/{_agentId}/from/{fromTime}/to/{toTime}");
            SetChartValues(collection);
        }
        private async void DotNetMetric_Button(object sender, RoutedEventArgs e)
        {
            SetBackgroundGray(Metrics.Children);

            Button metricButton = (Button)sender;
            metricButton.Background = new SolidColorBrush(Colors.Green);

            (TimeSpan fromTime, TimeSpan toTime) = GetTime();
            var collection = await MetricsManagerClient<Metric>.GetValues($"metrics/dotnet/agent/{_agentId}/from/{fromTime}/to/{toTime}");
            SetChartValues(collection);
        }

        private void AgentButton_Click(object sender, RoutedEventArgs e)
        {
            Button agentButton = (Button)sender;

            if (_agentId.Equals(agentButton.Name))
            {
                return;
            }

            SetBackgroundGray(AgentsNames.Children);
            SetBackgroundGray(Metrics.Children);
            
            agentButton.Background = new SolidColorBrush(Colors.Green);
            _agentId = agentButton.Name.Substring(1);
            Metrics.Visibility = Visibility.Visible;
        }

        private async void Update_Button(object sender, RoutedEventArgs e)
        {
            var agentsCollection = await MetricsManagerClient<Models.Agent>.GetValues("Agents/registered");

            try
            {
                foreach (Agent agent in agentsCollection)
                {
                    Button agentButton = new Button
                    {
                        Content = agent.Name,
                        Name = "_"+agent.Id.ToString(),
                        Height = 20,
                        Margin = new Thickness(10, 10, 10, 10),
                        Background = new SolidColorBrush(Colors.Gray)
                    };
                    agentButton.Click += AgentButton_Click;

                    AgentsNames.Children.Add(agentButton);
                }
            }
            catch (Exception ex)
            {
                string es = ex.Message;
            }

        }

        private void SetBackgroundGray(UIElementCollection collection)
        {
            foreach (Button element in collection)
            {
                element.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        private void SetChartValues(IEnumerable<Metric> collection)
        {
            try
            {
                Chart.LineSeriesValue.Clear();

                int i = 0;
                foreach (Metric metric in collection)
                {
                    i++;
                    Chart.LineSeriesValue.Add(metric.Value);
                }

            }
            catch (Exception ex)
            {
                string es = ex.Message;
            }
        }

        private (TimeSpan fromTime, TimeSpan toTime) GetTime()
        {
            var sec = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            TimeSpan toTime = TimeSpan.FromSeconds(sec);
            TimeSpan fromTime = TimeSpan.FromSeconds(sec - (5 * 60));

            return (fromTime, toTime);
        }

    }
}

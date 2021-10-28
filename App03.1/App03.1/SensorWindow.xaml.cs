using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;

namespace App03._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class SensorWindow : Window
    {
        public delegate void Start();
        public SensorController Controller { get; set; } = new SensorController();
        public SensorWindow()
        {
            InitializeComponent();

            sensorsList.ItemsSource = Controller.CollectionSensors.Sensors;
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if(sensorsList.SelectedItem is Sensor sensor)
            {
                Controller.RemoveSensor(sensor);

                sensorGrid.DataContext = null;
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            sensorsList.IsEnabled = false;
           
            Start start = new Start(MeasurementStart);

            start.BeginInvoke(null, null);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddSensor addSensor = new AddSensor();

            if(addSensor.ShowDialog()==true)
            {
                Controller.AddSensor(addSensor.Sensor);
            }
        }

        private void exitMune_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void JSONLoad_Click(object sender, RoutedEventArgs e)
        {
            IFactory factory = new JSONFactory();
            Load("json files (*.json)|*.json", factory);
        }

        private void XMLLoad_Click(object sender, RoutedEventArgs e)
        {
            IFactory factory = new XMLFactory();
            Load("xml files (*.xml)|*.xml", factory);
        }

        private void Load(string filter, IFactory factory)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IRepository repository = factory.CreateRepository();
                Controller.LoadFromFile(openFileDialog.FileName, repository);
            }
        }

        private void sensorsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sensorsList.SelectedItem is Sensor sensor)
            {
                sensorGrid.DataContext = sensor;
            }
        }

        private void switchModeButton_Click(object sender, RoutedEventArgs e)
        {
            sensorsList.IsEnabled = false;

            Start start = new Start(SwitchStart);

            start.BeginInvoke(null, null);
        }

        private void MeasurementStart()
        {
            Sensor sensor = Dispatcher.Invoke(() => sensorsList.SelectedItem as Sensor);
            
            Controller.SequentialSwitchingModes(sensor);
            
            Dispatcher.Invoke(() => sensorsList.IsEnabled = true);
        }

        private void SwitchStart()
        {
            Sensor sensor = Dispatcher.Invoke(() => sensorsList.SelectedItem as Sensor);

            Controller.SwitchingModes(sensor);

            Dispatcher.Invoke(() => sensorsList.IsEnabled = true);
        }
    }
}

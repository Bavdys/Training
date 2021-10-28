using System;
using System.Linq;
using System.Windows;

namespace App03._1
{
    /// <summary>
    /// Interaction logic for AddSensor.xaml
    /// </summary>
    public partial class AddSensor : Window
    {
        public Sensor Sensor { get; private set; }
        public AddSensor()
        {
            InitializeComponent();
            sensorComboBox.ItemsSource= Enum.GetValues(typeof(VariousSensor)).Cast<VariousSensor>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            VariousSensor variousState = (VariousSensor)sensorComboBox.SelectedItem;
            MeasurementInterval measurementInterval = new MeasurementInterval(int.Parse(minTextBox.Text), int.Parse(maxTextBox.Text));
            int measuredValue = int.Parse(measureTextBox.Text);

            Sensor = new Sensor(variousState, measurementInterval, measuredValue);

            this.DialogResult = true;
        }
    }
}

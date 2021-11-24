using App03._1.Identifier;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App03._1
{
    public class Sensor: INotifyPropertyChanged
    {
        private Mode _mode;
        private MeasurementInterval _measurementInterval;
        private VariousSensor _variousSensor;
        private VariousState _variousState;
        private int _currentValue;
        private int _measuredValue;


        public Sensor(VariousSensor variousSensor, MeasurementInterval measurementInterval, int measuredValue)
        {
            Id = GeneratorIdentifier.GetInstance().GetGuid();
            Various = variousSensor;
            MeasurementInterval = measurementInterval;
            MeasuredValue = measuredValue;
            CurrentValue = MeasurementInterval.Min;
            VariousState = VariousState.Simple;
            _mode = new Mode();
        }

        public Guid Id { get; }
        public VariousSensor Various 
        {
            get
            {
                return _variousSensor;
            }
            set
            {
                _variousSensor = value;
                OnPropertyChanged("VariousSensor");
            }
        }
        public VariousState VariousState
        {
            get
            {
                return _variousState;
            }
            set
            {
                _variousState = value;
                OnPropertyChanged("VariousState");
            }
        }
        public MeasurementInterval MeasurementInterval 
        {
            get
            {
                return _measurementInterval;
            }
            set
            {
                _measurementInterval = value;
                OnPropertyChanged("MeasurementInterval");
            }
        }
        public int MeasuredValue 
        {
            get
            {
                return _measuredValue;
            }
            set
            {
                _measuredValue = value;
                OnPropertyChanged("MeasuredValue");
            }
        }
        public int CurrentValue
        {
            get 
            { 
                return _currentValue; 
            }
            set
            {
                _currentValue = value; 
                OnPropertyChanged("CurrentValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public void SequentialSwitchingModes()
        {
            _mode.State = ModeSimple.GetInstance();

            for (int i = 0; i < 4; i++)
            {
                _mode.Switch(this);
            }
        }
        public void SwitchingModes()
        {
            _mode.Switch(this);
        }
    } 
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace TestDateTimePicker
{
    public class MainWindowVM : NotifyPropertyChangedBase
    {
        public MainWindowVM()
        {
            Date = new DateTime(1956, 9, 23, 11, 48, 3);

            Date = Date.AddHours(5);

            Input = new UIsCommand(SetSize);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Date = Date.AddSeconds(1);
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => Set(ref _date, value);
        }

        private int _fontSize = 12;

        public int FontSizeing 
        { 
            get => _fontSize; 
            set => Set(ref _fontSize, value);
        }

        public ICommand Input { get; }

        private void SetSize(object param)
        {
            FontSizeing++;
        }
    }

    class UIsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Action<object> f_Execute;
        private readonly Func<object, bool> f_CanExecute;

        public UIsCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            f_Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            f_CanExecute = CanExecute;
        }

        public bool CanExecute(object parameter)
        {
            return f_CanExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            f_Execute(parameter);
        }
    }
}

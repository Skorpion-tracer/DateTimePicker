using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TestDateTimePicker.UserControls
{
    /// <summary>
    /// Логика взаимодействия для DateTimePicker.xaml
    /// </summary>
    public partial class DateTimePicker : UserControl
    {
        public DateTime DateChanged
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set {
                SetValue(SelectedDateProperty, value);
                UpdateItems();
            }
        }

        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register("DateChanged", typeof(DateTime), typeof(DateTimePicker), new FrameworkPropertyMetadata(DateTime.Now,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(SelectedDateChanged)));

        private static void SelectedDateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DateTimePicker picker = (DateTimePicker)sender;
            picker.DateChanged = (DateTime)args.NewValue;
        }

        #region Конструктор
        public DateTimePicker()
        {
            InitializeComponent();

            Years = GetYears();
            Months = GetMonths();
            Days = GetDays(DateChanged);
            Hours = GetHours();
            Minutes = GetTimeUnits();
            Seconds = GetTimeUnits();

            CBDays.ItemsSource = Days;
            CBDays.SelectedItem = ActiveDay;

            CBMonths.ItemsSource = Months;
            CBMonths.SelectedItem = ActiveMonth;

            CBYears.ItemsSource = Years;
            CBYears.SelectedItem = ActiveYear;

            CBHours.ItemsSource = Hours;
            CBHours.SelectedItem = ActiveHour;

            CBMinutes.ItemsSource = Minutes;
            CBMinutes.SelectedItem = ActiveMinute;

            CBSeconds.ItemsSource = Seconds;
            CBSeconds.SelectedItem = ActiveSecond;
        }
        #endregion

        #region Поля
        private List<int> _days = new();
        private List<int> _months = new();
        private List<int> _years = new();
        private List<int> _hours = new();
        private List<int> _minutes = new();
        private List<int> _seconds = new();

        private int _activeDay;
        private int _activeMonth;
        private int _activeYear;
        private int _activeHour;
        private int _activeMinute;
        private int _activeSecond;
        #endregion

        #region Свойства
        public List<int> Days
        {
            get => _days;
            set => _days = value;
        }

        public List<int> Months
        {
            get => _months;
            set => _months = value;
        }

        public List<int> Years
        {
            get => _years;
            set =>_years = value;
        }

        public List<int> Hours
        {
            get => _hours;
            set => _hours = value;
        }

        public List<int> Minutes
        {
            get => _minutes;
            set => _minutes = value;
        }

        public List<int> Seconds
        {
            get => _seconds;
            set => _seconds = value;
        }

        public int ActiveDay
        {
            get => _activeDay;
            set => _activeDay = value;
        }

        public int ActiveMonth
        {
            get => _activeMonth;
            set => _activeMonth = value;
        }

        public int ActiveYear
        {
            get => _activeYear;
            set => _activeYear = value;
        }

        public int ActiveHour
        {
            get => _activeHour;
            set => _activeHour = value;
        }

        public int ActiveMinute
        {
            get => _activeMinute;
            set => _activeMinute = value;
        }

        public int ActiveSecond
        {
            get => _activeSecond;
            set => _activeSecond = value;
        }
        #endregion

        private List<int> GetDays(DateTime dateTime)
        {
            List<int> days = new List<int>();

            int countDays = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            for (int i = 1; i <= countDays; i++)
            {
                days.Add(i);
            }

            return days;
        }

        private List<int> GetMonths()
        {
            List<int> months = new List<int>();

            int countMonth = 12;

            for (int i = 1; i <= countMonth; i++)
            {
                months.Add(i);
            }

            return months;
        }

        private List<int> GetYears()
        {
            int firsYear = DateTime.Now.AddYears(-70).Year;
            int lastYear = DateTime.Now.AddYears(30).Year;

            List<int> years = new List<int>();

            for (int i = firsYear; i <= lastYear; i++)
            {
                years.Add(i);
            }

            return years;
        }

        private List<int> GetTimeUnits()
        {
            int units = 60;

            List<int> items = new List<int>();

            for (int i = 0; i < units; i++)
            {
                items.Add(i);
            }

            return items;
        }

        private List<int> GetHours()
        {
            int units = 24;

            List<int> hours = new List<int>();

            for (int i = 0; i < units; i++)
            {
                hours.Add(i);
            }

            return hours;
        }

        private DateTime GetDate(int year, int month, int day, int hour, int minute, int second)
        {
            if (year != 0 &&
                month != 0 &&
                day != 0)
            {
                if (day > DateTime.DaysInMonth(year, month))
                {
                    day = DateTime.DaysInMonth(year, month);

                    DateTime newDate = new DateTime(year, month, day, hour, minute, second);
                    return newDate;
                }

                return new DateTime(year, month, day, hour, minute, second);
            }
            else
            {
                return DateTime.Now;
            }
        }

        private void UpdateSelectDate()
        {
            DateChanged = GetDate(ActiveYear, ActiveMonth, ActiveDay, ActiveHour, ActiveMinute, ActiveSecond);
        }

        private void UpdateItems()
        {
            ActiveYear = DateChanged.Year;
            ActiveMonth = DateChanged.Month;
            ActiveDay = DateChanged.Day;
            ActiveHour = DateChanged.Hour;
            ActiveMinute = DateChanged.Minute;
            ActiveSecond = DateChanged.Second;

            CBDays.SelectedItem = ActiveDay;
            CBMonths.SelectedItem = ActiveMonth;
            CBYears.SelectedItem = ActiveYear;
            CBHours.SelectedItem = ActiveHour;
            CBMinutes.SelectedItem = ActiveMinute;
            CBSeconds.SelectedItem = ActiveSecond;
        }

        private void ComboBox_SelectionDay(object sender, SelectionChangedEventArgs e)
        {
            ActiveDay = (int)CBDays.SelectedItem;
            UpdateSelectDate();
        }

        private void ComboBox_SelectionMonth(object sender, SelectionChangedEventArgs e)
        {
            ActiveMonth = (int)CBMonths.SelectedItem;
            UpdateSelectDate();

            Days = GetDays(DateChanged);
            CBDays.ItemsSource = Days;
        }

        private void ComboBox_SelectionYear(object sender, SelectionChangedEventArgs e)
        {
            ActiveYear = (int)CBYears.SelectedItem;
            UpdateSelectDate();

            Days = GetDays(DateChanged);
            CBDays.ItemsSource = Days;
        }

        private void ComboBox_SelectionHour(object sender, SelectionChangedEventArgs e)
        {
            ActiveHour = (int)CBHours.SelectedItem;
            UpdateSelectDate();
        }

        private void ComboBox_SelectionMinute(object sender, SelectionChangedEventArgs e)
        {
            ActiveMinute = (int)CBMinutes.SelectedItem;
            UpdateSelectDate();
        }

        private void ComboBox_SelectionSecond(object sender, SelectionChangedEventArgs e)
        {
            ActiveSecond = (int)CBSeconds.SelectedItem;
            UpdateSelectDate();
        }
    }
}

using System;
using System.Collections.Generic;
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

namespace TestDateTimePicker.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ShowNumberControl.xaml
    /// </summary>
    public partial class ShowNumberControl : UserControl
    {
        public int CurrentNumber
        {
            get { return (int)GetValue(CurrentNumberProperty); }
            set { SetValue(CurrentNumberProperty, value); }
        }
       
        public static readonly DependencyProperty CurrentNumberProperty =
            DependencyProperty.Register("CurrentNumber", typeof(int), typeof(ShowNumberControl), new UIPropertyMetadata(100, 
                new PropertyChangedCallback(CurrentNumberChanged)),
                new ValidateValueCallback(ValidateCurrentNumber));

        private static void CurrentNumberChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            ShowNumberControl c = (ShowNumberControl)depObj;

            Label theLabel = c.numberDisplay;

            theLabel.Content = args.NewValue.ToString();
        }

        public static bool ValidateCurrentNumber(object value)
        {
            return Convert.ToInt32(value) >= 0 && Convert.ToInt32(value) <= 500;
        }

        public ShowNumberControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentNumber++;
        }

        //private int _currNumber = 0;
        //public int CurrentNumber
        //{
        //    get => _currNumber;
        //    set {
        //        _currNumber = value;
        //        numberDisplay.Content = CurrentNumber.ToString();
        //    }
        //}
    }
}

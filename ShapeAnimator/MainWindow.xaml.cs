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
using System.Windows.Threading;

namespace ShapeAnimator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShapeAnimationEngine engine = new ShapeAnimationEngine();

        int x = 0;

        public MainWindow()
        {
            InitializeComponent();
        }
         
        private void RedColor(object sender, RoutedEventArgs e)
        {
            x = 0;
        }

        private void GreenColor(object sender, RoutedEventArgs e)
        {
            x = 1;
        }

        private void BlueColor(object sender, RoutedEventArgs e)
        {
            x = 2;
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(MyCanvas);
            engine.AddCircle(p, 100);
            Repaint(); // Better approach: Draw just one circle
        }

        private void Repaint()
        {
            MyCanvas.Children.Clear();
            foreach (Circle c in engine.circles)
            {

               DrawCircle(c);
            }
        }

        

        private void DrawCircle(Circle c)
        {
            Ellipse e = new Ellipse();
            if (x == 0)
            {
               
                
                    e.Width = c.Radius;
                    e.Height = c.Radius;
                    e.Stroke = Brushes.Red;
                    e.StrokeThickness = 1;
                
                e.SetValue(Canvas.LeftProperty, c.Center.X - 50);
                e.SetValue(Canvas.TopProperty, c.Center.Y - 50);

                MyCanvas.Children.Add(e);
            }else if (x == 1)
            {

                e.Width = c.Radius;
                e.Height = c.Radius;
                e.Stroke = Brushes.Green;
                e.StrokeThickness = 1;

                e.SetValue(Canvas.LeftProperty, c.Center.X - 50);
                e.SetValue(Canvas.TopProperty, c.Center.Y - 50);

                MyCanvas.Children.Add(e);
            }
            else if(x==2)
            {
                e.Width = c.Radius;
                e.Height = c.Radius;
                e.Stroke = Brushes.Blue;
                e.StrokeThickness = 1;

                e.SetValue(Canvas.LeftProperty, c.Center.X - 50);
                e.SetValue(Canvas.TopProperty, c.Center.Y - 50);

                MyCanvas.Children.Add(e);
            }
 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Step();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Step();
        }

        private void Step()
        {
            foreach (Circle c in engine.circles)
            {
                c.Center.X = c.Center.X + c.deltaX;
                c.Center.Y = c.Center.Y + c.deltaY;
                // Right wall and left wall
                if (c.Center.X + c.Radius > MyCanvas.Width + 30 || c.Center.X + c.Radius <150)
                {
                    c.deltaX = -c.deltaX;
                }
                 // Top and down wall
                if (c.Center.Y + c.Radius > MyCanvas.Height +50 || c.Center.Y + c.Radius < 150)
                {
                    c.deltaY = -c.deltaY;
                }

                // Left, Top and Bottom walls

              


            }
            Repaint();
        }
    }
}

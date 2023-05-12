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
using System.Windows.Shapes;



namespace TrickedKnowledgeHub
{
    /// <summary>
    /// Interaction logic for Create_exercise_window.xaml
    /// </summary>
    public partial class Create_exercise_window : Page
    {
        public Create_exercise_window()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (string.IsNullOrEmpty(tb.Text))
                    tb.Text = "Jeg står her når der er tomt.";
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            // this.Visibility = Visibility.Collapsed;
            MainWindow mainWindow = new();

            // Find the Frame control in the MainWindow using its name
            Frame frameExercise = (Frame)mainWindow.FindName("FrameExercise");

            Grid gridleft = (Grid)mainWindow.FindName("Blackoutleft");

            Rectangle recleft = (Rectangle)mainWindow.FindName("Overlayleft");
            // Set the Visibility property of the Frame control to Collapsed
            frameExercise.Visibility = Visibility.Collapsed;

            gridleft.Visibility = Visibility.Collapsed;
            recleft.Visibility = Visibility.Collapsed;

        }

        private void Create_Exercise(object sender, RoutedEventArgs e)
        {
            string message = "Øvelsen er hermed gemt :)";
            string title = "Gemt Øvelse";
            MessageBox.Show(message, title);
           
        }


    }
}

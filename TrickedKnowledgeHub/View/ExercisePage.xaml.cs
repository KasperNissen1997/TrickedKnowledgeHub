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

namespace TrickedKnowledgeHub.View
{
    /// <summary>
    /// Interaction logic for ExercisePage.xaml
    /// </summary>
    public partial class ExercisePage : Page
    {
        public ExercisePage()
        {
            InitializeComponent();
        }

        private void Frame_Collapsed_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);

            // Find the Frame control in the MainWindow using its name
            Frame frameExercise = (Frame)mainWindow.FindName("FrameExercise");

            // Set the Visibility property of the Frame control to Collapsed
            frameExercise.Visibility = Visibility.Collapsed;    
        }
    }
}

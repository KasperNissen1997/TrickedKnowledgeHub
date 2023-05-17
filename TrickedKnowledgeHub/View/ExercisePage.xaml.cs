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
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.ViewModel.Domain;

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
            Frame frameExercise = mainWindow.FrameExercise;
            Grid grid = mainWindow.Blackout;

            Rectangle rec = mainWindow.overlayBlack;
            // Set the Visibility property of the Frame control to Collapsed
            frameExercise.Visibility = Visibility.Collapsed;

            grid.Visibility = Visibility.Collapsed;
            rec.Visibility = Visibility.Collapsed;

        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);

            var selectedExercise = DataContext as Exercise;

            UpdatePage updatePage = new(selectedExercise);

            Frame frameExercise = mainWindow.FrameExercise;

            updatePage.Content = selectedExercise;

            frameExercise.Content = updatePage;

            frameExercise.Visibility = Visibility.Visible;

            //Im so confused
        }
    }
}

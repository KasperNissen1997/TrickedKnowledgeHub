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
using TrickedKnowledgeHub.Model.Persistence;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.ViewModel;
using Microsoft.Identity.Client;
using System.Runtime.ConstrainedExecution;
using TrickedKnowledgeHub.Command.CreateExerciseWindowCommand;
using System.Windows.Media.Media3D;

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

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClose();
        }

        private void Create_Exercise(object sender, RoutedEventArgs e)
        {
            string message = "Øvelsen er hermed gemt :)";
            string title = "Gemt Øvelse";
            MessageBox.Show(message, title);
            CreateExerciseCommand cem = new();
            cem.Execute((CreateExerciseWindowViewVM)DataContext);
            FrameClose();
        }

        private void FrameClose()
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

            if (DataContext is CreateExerciseWindowViewVM ViewModel)
            {
                FocusPoint_ComboBox.SelectedItem = null;
                LearningObjective_ComboBox.SelectedItem = null;
                Game_ComboBox.SelectedItem = null;
                Rating_ComboBox.SelectedItem = null;
                Description_TextBox.Text = null;
                Title_TextBox.Text = null;

                ViewModel.CreateExerciseViewReset();
            }
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CreateExerciseWindowViewVM ViewModel)
            {
                FocusPoint_ComboBox.SelectedItem = null;
                LearningObjective_ComboBox.SelectedItem = null;
                Game_ComboBox.SelectedItem = null;
                Rating_ComboBox.SelectedItem = null;
                Description_TextBox.Text = null;
                Title_TextBox.Text = null;

                ViewModel.CreateExerciseViewReset();


            }
        }
    }
}

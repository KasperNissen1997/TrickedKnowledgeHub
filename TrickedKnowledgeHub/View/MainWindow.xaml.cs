using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.Windows.Input;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Persistence;
using TrickedKnowledgeHub.View;
using TrickedKnowledgeHub.ViewModel;
using TrickedKnowledgeHub.ViewModel.Domain;

namespace TrickedKnowledgeHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ExercisePage ExercisePage { get; set; } = new();
        MainWindowViewVM vm = new();
        Create_exercise_window create_Exercise = new();

        private List<ExerciseVM> ExistingExerciseVMs = new();

        public MainWindow()
        {

            InitializeComponent();

            DataContext = vm;
            ExercisePage.DataContext = vm.ExercisePageVM;

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(DBUpdate);
            bw.RunWorkerAsync();
        }
        
        public void DBUpdate(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker) sender;
            while (!worker.CancellationPending)                                 //While worker isn't pending on cancellation
            {
                //Get information
                List<ExerciseVM> knownExerciseVMs = vm.ExerciseVMs.ToList();    //Get collection of known exercises

                ExistingExerciseVMs.Clear();                                    //Clear existing exercise list
                RepositoryManager.ExerciseRepository.Reset();                   //Reset Exercise list in repo (Clear then Load)

                foreach (Exercise exercise in 
                    RepositoryManager.ExerciseRepository.RetrieveAll())         //For each exercise in Exercise list
                        ExistingExerciseVMs.Add(new(exercise));                 //Add that exercise to the list supposed to contain existing exercises

                //Prepare information
                List<ExerciseVM> exercisesToAdd = new();                        //New list to contain exercises to add

                foreach (ExerciseVM existingExercise in ExistingExerciseVMs)    //For each existing exercise
                    if (!knownExerciseVMs.Contains(existingExercise))           //If knownExerciseVms doesn't contain an existing Exercise
                        exercisesToAdd.Add(existingExercise);                   //Add that existing Exercise to exercisesToAdd list

                //Add information
                foreach (ExerciseVM exerciseToAdd in exercisesToAdd)            //Foreach Exercise to add in exercisesToAdd list
                    vm.ExerciseVMs.Add(exerciseToAdd);                          //Add that Exercise to ExerciseVms collection

                //Correct
                List<ExerciseVM> exercisesToRemove = new();                     //New list to conatin Exercises to remove

                foreach (ExerciseVM knownExercise in knownExerciseVMs)          //For each known Exercise
                    if (!ExistingExerciseVMs.Contains(knownExercise))           //If  ExistingExerciseVMs doesn't contain that known Exercise, meaning that Exercise has been deleted
                        exercisesToRemove.Add(knownExercise);                   //Add that known Exercise to exercisesToRemove list

                foreach (ExerciseVM exerciseToRemove in exercisesToRemove)      //For each Exercise to remove
                    vm.ExerciseVMs.Remove(exerciseToRemove);                    //Remove that Exercise  from ExerciseVMs
                
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
        }

        private void FeedListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            FrameExercise.Content = ExercisePage;
            FrameExercise.Visibility = Visibility.Visible;
            FeedListBox.SelectedIndex = -1;
            Blackout.Visibility = Visibility.Visible;
            overlayBlack.Visibility = Visibility.Visible;

        }
        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // this makes it possible when click outside the frame it closes the window
            if (!FrameExercise.IsMouseOver)
            {
                FrameExercise.Visibility = Visibility.Collapsed;
                Blackout.Visibility = Visibility.Collapsed;
                overlayBlack.Visibility = Visibility.Collapsed;

            }
        }

        private void Create_Exercise_Click(object sender, RoutedEventArgs e)
        {

            FrameExercise.Content = create_Exercise;
            create_Exercise.DataContext = vm.CreateExerciseWindowVM;
            // this sets the selcteditem to -1 as the listboxitems that are visible starts at 0
            // this makes it possible to select the same exercise over and over again
            FeedListBox.SelectedIndex = -1;

            FrameExercise.Visibility = Visibility.Visible;
            Blackout.Visibility = Visibility.Visible;
            overlayBlack.Visibility = Visibility.Visible;

        }
    }
}




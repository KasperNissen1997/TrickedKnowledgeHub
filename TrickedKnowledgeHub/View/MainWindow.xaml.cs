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
using TrickedKnowledgeHub.Model.Repo;
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
            while (!worker.CancellationPending)
            {
                List<ExerciseVM> knownExerciseVMs = vm.ExerciseVMs.ToList();

                ExistingExerciseVMs.Clear();
                RepositoryManager.ExerciseRepository.Reset();

                foreach (Exercise exercise in RepositoryManager.ExerciseRepository.RetrieveAll())
                    ExistingExerciseVMs.Add(new(exercise));

                List<ExerciseVM> exercisesToAdd = new();

                foreach (ExerciseVM existingExercise in ExistingExerciseVMs)
                    if (!knownExerciseVMs.Contains(existingExercise))
                        exercisesToAdd.Add(existingExercise);

                foreach (ExerciseVM exerciseToAdd in exercisesToAdd)
                    vm.ExerciseVMs.Add(exerciseToAdd);

                List<ExerciseVM> exercisesToRemove = new();

                foreach (ExerciseVM knownExercise in knownExerciseVMs)
                    if (!ExistingExerciseVMs.Contains(knownExercise))
                        exercisesToRemove.Add(knownExercise);

                foreach (ExerciseVM exerciseToRemove in exercisesToRemove)
                    vm.ExerciseVMs.Remove(exerciseToRemove);
                
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




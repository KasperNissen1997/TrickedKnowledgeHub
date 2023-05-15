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
        public MainWindow()
        {

            InitializeComponent();

            DataContext = vm;

            ExercisePage.DataContext = vm.ExercisePageVM;


            // DBUpdate();
        }

        

        public async void DBUpdate()
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            MainWindowViewVM mainWindowViewVM = (MainWindowViewVM)DataContext;

            //ObservableCollection<ExerciseVM> exerciseVMs = new ObservableCollection<ExerciseVM>();
            List<Exercise> temp = new List<Exercise>();


            while (await timer.WaitForNextTickAsync())
            {
                List<Exercise> exercises = RepositoryManager.ExerciseRepository.RetrieveAll();

                if (!exercises.SequenceEqual(temp))
                {
                    mainWindowViewVM.ExerciseVMs.Clear();
                    foreach (var exercise in exercises)
                    {
                        mainWindowViewVM.ExerciseVMs.Add(new ExerciseVM(exercise));
                    }

                    //mainWindowViewVM.ExerciseVMs = exerciseVMs;
                    temp = exercises;
                }
                else
                {
                    RepositoryManager.ExerciseRepository.Reset();
                }
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


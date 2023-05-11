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
        public ExercisePage ExercisePage { get; set; } =new();

        public MainWindow()
        {
            MainWindowViewVM vm = new();

            InitializeComponent();

            DataContext = vm;

            ExercisePage.DataContext = vm.ExercisePageVM;
            FrameExercise.Content = ExercisePage;

            DBUpdate();
        }


        public async void DBUpdate()
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            MainWindowViewVM mainWindowViewVM = (MainWindowViewVM)DataContext;

            ObservableCollection<ExerciseVM> exerciseVMs = new ObservableCollection<ExerciseVM>();
            List<Exercise> temp = new List<Exercise>();


            while (await timer.WaitForNextTickAsync())
            {
                List<Exercise> exercises = RepositoryManager.ExerciseRepository.RetrieveAll();

                if (!exercises.SequenceEqual(temp))
                {
                    exerciseVMs.Clear();
                    foreach (var exercise in exercises)
                    {
                        exerciseVMs.Add(new ExerciseVM(exercise));
                    }

                    mainWindowViewVM.ExerciseVMs = exerciseVMs;
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
            FrameExercise.Visibility = Visibility.Visible;
            FeedListBox.SelectedIndex= -1;
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!FrameExercise.IsMouseOver)
            {
                FrameExercise.Visibility = Visibility.Collapsed;
            }
        }

    }
}


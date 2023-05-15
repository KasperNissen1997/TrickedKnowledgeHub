using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.ViewModel;
using TrickedKnowledgeHub.ViewModel.Domain;

namespace TrickedKnowledgeHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewVM();

            DBUpdate();

            //Filter();
        }

        

        public async void DBUpdate()
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            MainWindowViewVM mainWindowViewVM = (MainWindowViewVM)DataContext;
            
            ObservableCollection<ExerciseVM> exerciseVMs;

            while (await timer.WaitForNextTickAsync())
            {
                exerciseVMs = new ObservableCollection<ExerciseVM>();
                RepositoryManager.ExerciseRepository.Reset();
                List<Exercise> exercises = RepositoryManager.ExerciseRepository.RetrieveAll();

                foreach (var exercise in exercises)
                {
                    exerciseVMs.Add(new ExerciseVM(exercise));
                }

                mainWindowViewVM.ExerciseVMs = exerciseVMs;
            }
        }

        public async void Filter()
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            MainWindowViewVM mainWindowViewVM = (MainWindowViewVM)DataContext;

            ObservableCollection<ExerciseVM> exerciseVMs;

            while (await timer.WaitForNextTickAsync())
            {
                if (mainWindowViewVM.SelectedGameFilter != null)
                {
                    exerciseVMs = new ObservableCollection<ExerciseVM>();
                    RepositoryManager.ExerciseRepository.Reset();
                    List<Exercise> exercises = RepositoryManager.ExerciseRepository.RetrieveAll();

                    // Get the selected game from the GameCombo, and add a null check
                    GameVM selectedGame = mainWindowViewVM.SelectedGameFilter;
                    if (mainWindowViewVM.SelectedGameFilter != null)
                    {

                        foreach (var exercise in exercises)
                        {
                            // Only add exercises that match the selected game
                            if (selectedGame != null && exercise.Game != null && exercise.Game.Equals(selectedGame) && mainWindowViewVM.SelectedGameFilter != null)
                            {
                                //Make it so that only the exercises with the selected game will be shown
                                exerciseVMs.Add(new ExerciseVM(exercise));
                            }
                        }

                        if (mainWindowViewVM.SelectedLearningObjectiveFilter != null)
                        {
                            foreach (var exercise in exercises)
                            {
                                if (exercise.FocusPoint.Parent.Equals(mainWindowViewVM.SelectedLearningObjectiveFilter) && mainWindowViewVM.SelectedLearningObjectiveFilter != null)
                                {

                                }
                            }


                            if (mainWindowViewVM.SelectedFocusPointFilter != null)
                            {

                                foreach (var exercise in exercises)
                                {
                                    if (exercise.FocusPoint.Equals(mainWindowViewVM.SelectedFocusPointFilter) && mainWindowViewVM.SelectedFocusPointFilter != null)
                                    {

                                    }
                                }

                                if (mainWindowViewVM.SelectedRatingFilter != null)
                                {
                                    foreach (var exercise in exercises)
                                    {
                                        if (exercise.Rating.Equals(mainWindowViewVM.SelectedRatingFilter) && mainWindowViewVM.SelectedRatingFilter != null)
                                        {

                                        }
                                    }


                                }
                            }
                        }
                    }

                    mainWindowViewVM.ExerciseVMs = exerciseVMs;
                }
            }
        }
    }
}

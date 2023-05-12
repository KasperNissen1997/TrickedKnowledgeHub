﻿using System;
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
        }

        

        public async void DBUpdate()
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
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
                if (mainWindowViewVM.SelectedGame != null)
                {
                    exerciseVMs = new ObservableCollection<ExerciseVM>();
                    RepositoryManager.ExerciseRepository.Reset();
                    List<Exercise> exercises = RepositoryManager.ExerciseRepository.RetrieveAll();

                    // Get the selected game from the GameCombo, and add a null check
                    GameVM selectedGame = mainWindowViewVM.SelectedGame;
                    if (mainWindowViewVM.selectedGame != null)
                    {

                        foreach (var exercise in exercises)
                        {
                            // Only add exercises that match the selected game
                            if (exercise.Game == selectedGame)
                            {
                                exerciseVMs.Add(new ExerciseVM(exercise));
                            }
                        }

                        if (mainWindowViewVM.SelectedLearningObjective != null)
                        {



                            if (mainWindowViewVM.SelectedFocusPoint != null)
                            {

                                if (mainWindowViewVM.SelectedRating != null)
                                {



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

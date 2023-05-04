﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickedKnowledgeHub.Command.MainWindowCommand;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.ViewModel.Domain;
using TrickedKnowledgeHub.Model;

namespace TrickedKnowledgeHub.ViewModel
{
    public class MainWindowViewVM
    {
        public EmployeeVM ActiveUser { get; set; }
        public OpenCreateExerciseViewCmd OpenCreateExerciseViewCmd { get; set; } = new OpenCreateExerciseViewCmd();

        public DownloadMaterialCommand DownloadMaterialCommand { get; set; }

        private List<ExerciseVM> _exerciseVM;
        public List<ExerciseVM> ExerciseVMs
        {
            get { return _exerciseVM; }
            set { _exerciseVM = value; }
        }

        private ExerciseVM _selectedExerciseVM;
        public ExerciseVM SelectedExerciseVM
        {
            get
            {
                return _selectedExerciseVM;
            }

            set
            {
                SelectedExerciseVM = value;
            }
        }

        public MainWindowViewVM()
        {
            //You need to enter the email of a active user in the database, so be sure the email is in the database.
            ActiveUser = new(RepositoryManager.EmployeeRepository.Retrieve("nikolai@gmail.com"));

            ExerciseVMs = new();
            foreach (Exercise exercise in RepositoryManager.ExerciseRepository.RetrieveAll())
                ExerciseVMs.Add(new ExerciseVM(exercise));

        }
    }
}

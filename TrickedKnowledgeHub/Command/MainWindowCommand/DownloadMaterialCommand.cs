﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Repo;
using TrickedKnowledgeHub.ViewModel;
using Microsoft.Win32;

namespace TrickedKnowledgeHub.Command.MainWindowCommand
{
    public class DownloadMaterialCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewModel.Domain.ExerciseVM vm) //If parameter is the VM
            {
                SaveFileDialog saveFileDialog = new(); //New dialog

                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Initial directory equals the specified folder path
                                                                                                                         //given from GetFolderPath method which returns a string of a path

                saveFileDialog.DefaultExt = ".docx";

                if (saveFileDialog.ShowDialog() == true)
                {
                    byte[] material = RepositoryManager.ExerciseRepository.GetMaterial(vm.Source.ExerciseID); //Material equals material returned from GetMaterial method using the selected exercise ID from ExerciseVM

                    string filePath = saveFileDialog.FileName + "";

                    File.WriteAllBytes(filePath, material); //Creates file on specified path and name, using specified byte
                }
            }
        }
    }
}

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
using TrickedKnowledgeHub.ViewModel;

namespace TrickedKnowledgeHub.View
{
    /// <summary>
    /// Interaction logic for UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Page
    {
        UpdatePageVM vm = new();
        public UpdatePage(Exercise selectedExercise)
        {
            InitializeComponent();

            // Set the data context of the UpdatePage to the selected exercise
            DataContext = selectedExercise;
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e) //TODO: Make this into a command in the future
        {
            if (DataContext is ExercisePage view)
            {
                //vm.Id
                vm.Title = Title_TextBox.Text;
                vm.Description = Description_TextBox.Text;
                vm.Update();
            }
        }
    }
}

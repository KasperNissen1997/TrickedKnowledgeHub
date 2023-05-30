using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TrickedKnowledgeHub.Model.Persistence;
using TrickedKnowledgeHub.ViewModel;

namespace TrickedKnowledgeHub.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public LoginViewModel VM;

        public Login()
        {
            InitializeComponent();

            VM = new LoginViewModel();
            DataContext = VM;
        }

        private void bntLogin_Click(object sender, RoutedEventArgs e)
        {
            Employee employee;

            try
            {
                employee = RepositoryManager.EmployeeRepository.Retrieve(VM.Username);
            }
            catch (Exception)
            {
                Trace.WriteLine($"Error finding employee with username {VM.Username}");
                return;
            }

            if (employee.Password.Equals(VM.Password))
            {
                Trace.WriteLine("Login succesfull!");

                MainWindow mainWindow = new();

                if (mainWindow.DataContext is MainWindowViewVM mainWindowVM)
                    mainWindowVM.ActiveUser = new(employee);

                mainWindow.Show();
                Close();
            }
        }
    }
}

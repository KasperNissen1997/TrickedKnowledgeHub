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
using TrickedKnowledgeHub.ViewModel;

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
        }

        private void Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                
                Debugger.Log(0, "Info", "Application closed\n");
            }
            Application.Current.Shutdown();
        }
    }
}

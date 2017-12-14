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

namespace _0SimpleWebChat
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
     
        public MainWindow()
        {
            InitializeComponent();           
        }

        private void btnsubmit_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            TalkerWindow talkerWindow = new TalkerWindow(userName);
            talkerWindow.Show();
            this.Close();
        }
    }
}

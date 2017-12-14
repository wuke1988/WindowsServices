using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _0SimpleWebChat
{
    /// <summary>
    /// TalkerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TalkerWindow : Window
    {
        private string userName;
        private Talker talker;
        public TalkerWindow(string userName)
        {
            InitializeComponent();
            this.userName = userName;

            InitialTalker();

        }
        ~TalkerWindow()
        {
            talker.Dispose();
        }
           
        private void InitialTalker()
        {
            lblUserName.Content += userName;

            talker = new Talker();
            talker.PortNumberBrReady += Talker_PortNumberBrReady;
            talker.ClientConnected += Talker_ClientConnected;
            talker.MessageRecieved += Talker_MessageRecieved;
        }

        private void Talker_MessageRecieved(string message)
        {
            txtMessageRecord.Dispatcher.Invoke(
                new Action<string>(r => txtMessageRecord.Text += message),
                new object[] { message});
        }

        private void Talker_ClientConnected(System.Net.IPEndPoint endPoint)
        {
            txtMessageRecord.Dispatcher.Invoke(
                new Action<IPEndPoint>(
                    r=> {
                        txtMessageRecord.Text += $"System[{DateTime.Now.ToString()}]:\n远程主机{r.ToString()}连接到本地\n";
                    }),new object[] { endPoint });
        }

        private void Talker_PortNumberBrReady(int port)
        {
            lblPort.Dispatcher.Invoke(
                new Action<int> (
                    r=> {
                        lblPort.Content += r.ToString(); } ),
                new object[] { port });
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            IPAddress iPAddress;
            try
            {
                iPAddress = IPAddress.Parse(txtIpAddress.Text.Trim());
                int port = int.Parse(txtPort.Text.Trim());
                if (talker.ConnectByIp(iPAddress, port))
                {
                    btnConnect.IsEnabled = false;
                    txtMessageRecord.Text += $"[System{DateTime.Now.ToString()}]:\n已成功连接到远程\n";
                }
                else
                {
                    MessageBox.Show("远程主机不存在，或者拒绝连接！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            talker.SignOut();
            btnConnect.IsEnabled = true;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Message message = new Message(userName, txtMessageSend.Text,DateTime.Now);
            if (talker.SendMessage(message))
            {
                txtMessageSend.Text = null;
                txtMessageRecord.Text += message.ToString();
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtMessageSend.Text = null;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            talker.Dispose();
            this.Close(); 
        }
    }
}

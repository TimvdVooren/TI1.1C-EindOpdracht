using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EindopdrachtRickEnTim
{
    public partial class UsernameInput : Form
    {
        public string Username { get; set; }
        private Thread clientThread;
        private AppChat appChat;
        private ChatClient chatClient;

        public UsernameInput()
        {
            InitializeComponent();
            clientThread = new Thread(StartClient);
            clientThread.Start();
        }

        private void StartClient()
        {
            chatClient = new ChatClient(this);
            while (true)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usernameText.Text != String.Empty && usernameText.Text.Length > 2)
            {
                Username = usernameText.Text;
                chatClient.SendUserName(Username);
                while (chatClient.usernameState == null) { }
                if (chatClient.usernameState == "OK")
                {
                    appChat = new AppChat(chatClient);
                    appChat.Show();
                    appChat.Username = Username;
                    chatClient.appChat = appChat;
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("This username was already taken");
                    chatClient.usernameState = null;
                }

            }
            else
            {
                MessageBox.Show("Username must be longer than 2 letters");
            }
        }
    }
}

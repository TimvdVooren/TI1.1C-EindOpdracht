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
    public partial class AppChat : Form
    {
        private string Username;
        private ChatClient chatClient;
        private bool sendUsername;
        private UsernameInput input;

        public AppChat()
        {
            InitializeComponent();
            input = new UsernameInput();
            input.ShowDialog();
            sendUsername = false;
            Thread clientThread = new Thread(StartClient);
            clientThread.Start();           
        }

        private void StartClient()
        {
            chatClient = new ChatClient();
            while(!sendUsername)
            {
                sendUsername = !input.Visible; 
            }
            this.Username = input.Username;
            chatClient.SendUserName(Username);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if(sendTextBox.Text != String.Empty)
            {
                receiveTextBox.Text = receiveTextBox.Text + "\r\n" + Username + ": " + sendTextBox.Text;
            }
            sendTextBox.Text = receiveTextBox.Text;
        }

        private void AppChat_Load(object sender, EventArgs e)
        {
        }

        private void addFriend_Click(object sender, EventArgs e)
        {
            input.UsernameLabel.Text = "Enter your friend's username below:";
            input.UsernameTextBox.Text = "";
            input.AddFriend = true;
            input.Visible = true;
            chatClient.AddFriend(input.Username);
        }
    }
}

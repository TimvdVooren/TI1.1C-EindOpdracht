using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private Thread clientThread;
        private string displayed;

        public AppChat()
        {
            InitializeComponent();
            input = new UsernameInput();
            input.ShowDialog();
            sendUsername = false;
            clientThread = new Thread(StartClient);
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
            string lastmessage = "";
            while (true)
            {
                if (chatClient != null)
                {
                    string[] receivedMessage = Regex.Split(chatClient.lastMessage, "\r\n"); ;
                    if (lastmessage != receivedMessage[0])
                    {
                        receiveTextBox.Invoke(new Action(() => receiveTextBox.Text += "\r\n" + receivedMessage[1] + ": " + receivedMessage[0]));
                        displayed = "\r\n" + receivedMessage[1] + ": " + receivedMessage[0];
                        lastmessage = receivedMessage[0];
                    }
                }
            }

        }

        //private void displayNewMessage()
        //{
        //    string lastmessage = "";            
        //    if (chatClient != null)
        //    {
        //        string[] receivedMessage = Regex.Split(chatClient.lastMessage, "\r\n"); ;
        //        if (lastmessage != receivedMessage[0])
        //        {
        //            receiveTextBox.Text += "\r\n" + receivedMessage[1] + ": " + receivedMessage[0];
        //            lastmessage = receivedMessage[0];
        //        }
        //    }
            
        //}



        private void sendButton_Click(object sender, EventArgs e)
        {
            if(sendTextBox.Text != String.Empty)
            {
                chatClient.SendMessage(sendTextBox.Text);
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

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
        private UsernameInput input;
        private Thread clientThread;
        private string displayed;
        private AddFriendInput friendInput;

        public AppChat()
        {
            InitializeComponent();
            input = new UsernameInput();
            input.ShowDialog();
            clientThread = new Thread(StartClient);
            clientThread.Start();
        }

        private void StartClient()
        {
            chatClient = new ChatClient();
            while(input.Visible){}

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
            friendInput = new AddFriendInput();
            Thread addFriendThread = new Thread(AddFriend);
            addFriendThread.Start();
        }

        private void AddFriend()
        {
            while (friendInput.Visible) {}
            chatClient.AddFriend(friendInput.FriendName);
            friendInput.Dispose();
        }
    }
}

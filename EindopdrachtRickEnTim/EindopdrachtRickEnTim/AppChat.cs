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
        private string currentFriend;

        public AppChat()
        {
            InitializeComponent();
            input = new UsernameInput();
            input.ShowDialog();
            sendTextBox.Enabled = false;
            clientThread = new Thread(StartClient);
            clientThread.Start();
            currentFriend = "NoFriend";
        }

        private void StartClient()
        {
            chatClient = new ChatClient(this);
            while(input.Visible){}

            this.Username = input.Username;
            chatClient.SendUserName(Username);
            string lastmessage = "";
            while (true)
            {
                //if (chatClient != null)
                //{
                //    string[] receivedMessage = Regex.Split(chatClient.lastMessage, "\r\n"); ;
                //    if (lastmessage != receivedMessage[0])
                //    {
                //        receiveTextBox.Invoke(new Action(() => receiveTextBox.Text += "\r\n" + receivedMessage[1] + ": " + receivedMessage[0]));
                //        displayed = "\r\n" + receivedMessage[1] + ": " + receivedMessage[0];
                //        lastmessage = receivedMessage[0];
                //    }
                //}
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
                chatClient.SendMessage(sendTextBox.Text, (string)friendList.Items[friendList.SelectedIndex]);
                receiveTextBox.Text = receiveTextBox.Text + "\r\n" + Username + ": " + sendTextBox.Text;
            }
            sendTextBox.Text = "";
        }

        public void AddMessage(string message)
        {
            chatClient.GetChat(currentFriend);
        }

        public void SetFriendList(List<string> people)
        {
            foreach(string person in people)
            {
                if(person != Username)
                    friendList.Invoke(new Action(() => friendList.Items.Add(person)));
            }
        }

        public void SetChat(string chat)
        {
            receiveTextBox.Invoke(new Action(() => receiveTextBox.Text = chat));
        }

        private void addFriend_Click(object sender, EventArgs e)
        {
            friendInput = new AddFriendInput();
            friendInput.Show();
            //Thread addFriendThread = new Thread(AddFriend);
            //addFriendThread.Start();
        }

        //private void AddFriend()
        //{
        //    while (friendInput.Visible) {}
        //    chatClient.AddFriend(friendInput.FriendName);
        //    friendInput.Dispose();
        //}

        private void friendList_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentFriend = (string) friendList.Items[friendList.SelectedIndex];
            chatClient.GetChat(currentFriend);
            sendTextBox.Enabled = true;
        }
    }
}

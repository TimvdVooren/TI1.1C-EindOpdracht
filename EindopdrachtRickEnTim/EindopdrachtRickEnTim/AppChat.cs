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
        private AddFriendInput friendInput;
        public string currentFriend;

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
            while (true)
            {

            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if(sendTextBox.Text != String.Empty)
            {
                chatClient.SendMessage(sendTextBox.Text, (string)friendList.Items[friendList.SelectedIndex]);
                receiveTextBox.Text = receiveTextBox.Text + Username + ": " + sendTextBox.Text + "\r\n";
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
                if(person != Username && !friendList.Items.Contains(person))
                    friendList.Invoke(new Action(() => friendList.Items.Add(person)));
            }
        }

        public void SetChat(string[] chatLines)
        {
            string chat = "";
            for(int i = 2; i < chatLines.Length; i++)
            {
                string message = chatLines[i];
                if(message != "load_chat" && message != "")
                    chat = chat + message + "\r\n";
            }
            receiveTextBox.Invoke(new Action(() => receiveTextBox.Text = chat));
        }

        private void addFriend_Click(object sender, EventArgs e)
        {
            friendInput = new AddFriendInput();
            friendInput.Show();
            //Thread addFriendThread = new Thread(AddFriend);
            //addFriendThread.Start();
        }

        private void friendList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = friendList.SelectedIndex;
            if(index >= 0)
                currentFriend = (string) friendList.Items[friendList.SelectedIndex];
            chatClient.GetChat(currentFriend);
            sendTextBox.Enabled = true;
        }
    }
}

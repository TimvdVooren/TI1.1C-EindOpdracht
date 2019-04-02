using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EindopdrachtRickEnTim
{
    public partial class AppChat : Form
    {
        public string Username;
        public ChatClient chatClient;
        public string currentFriend;

        public AppChat(ChatClient chatClient)
        {
            InitializeComponent();
            sendTextBox.Enabled = false;
            currentFriend = "NoFriend";
            this.chatClient = chatClient;
        }

        public void SetVisible(bool state)
        {
            Invoke(new Action(() => this.Visible = state));
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

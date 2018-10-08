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

        public AppChat()
        {
            InitializeComponent();
            Thread clientThread = new Thread(StartClient);
            UsernameInput input = new UsernameInput(chatClient);
            input.ShowDialog();
            this.Username = input.Username;
        }

        private void StartClient()
        {
            chatClient = new ChatClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(sendTextBox.Text != String.Empty)
            {
                receiveTextBox.Text = receiveTextBox.Text + "\r\n" + Username + ": " + sendTextBox.Text;
            }
            sendTextBox.Text = "";
        }

        private void AppChat_Load(object sender, EventArgs e)
        {
        }
    }
}

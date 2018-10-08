using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EindopdrachtRickEnTim
{
    public partial class AppChat : Form
    {
        private string Username;

        public AppChat()
        {
            InitializeComponent();
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
            UsernameInput input = new UsernameInput();
            input.ShowDialog();
            this.Username = input.Username;
        }
    }
}

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
    public partial class AddFriendInput : Form
    {
        public string FriendName { get; set; }

        public AddFriendInput()
        {
            FriendName = "";
            InitializeComponent();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (usernameText.Text != String.Empty && usernameText.Text.Length > 2)
            {
                FriendName = usernameText.Text;
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Username must be longer than 2 letters");
            }
        }
    }
}

﻿using System;
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
    public partial class UsernameInput : Form
    {
        public string Username { get; set; }

        public UsernameInput()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usernameText.Text != String.Empty && usernameText.Text.Length > 2)
            {
                Username = usernameText.Text;
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Your username must be longer than 2 letters");
            }
        }
    }
}
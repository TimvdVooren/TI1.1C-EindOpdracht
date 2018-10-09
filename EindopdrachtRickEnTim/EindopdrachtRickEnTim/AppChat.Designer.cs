namespace EindopdrachtRickEnTim
{
    partial class AppChat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.friendList = new System.Windows.Forms.ListBox();
            this.sendTextBox = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.addFriend = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.receiveTextBox = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // friendList
            // 
            this.friendList.Dock = System.Windows.Forms.DockStyle.Left;
            this.friendList.FormattingEnabled = true;
            this.friendList.ItemHeight = 16;
            this.friendList.Location = new System.Drawing.Point(0, 0);
            this.friendList.Name = "friendList";
            this.friendList.Size = new System.Drawing.Size(178, 450);
            this.friendList.TabIndex = 0;
            // 
            // sendTextBox
            // 
            this.sendTextBox.Location = new System.Drawing.Point(184, 3);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.sendTextBox.Size = new System.Drawing.Size(772, 75);
            this.sendTextBox.TabIndex = 1;
            this.sendTextBox.Text = "";
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(962, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 75);
            this.button1.TabIndex = 2;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.addFriend);
            this.flowLayoutPanel1.Controls.Add(this.sendTextBox);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 450);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1042, 78);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // addFriend
            // 
            this.addFriend.Location = new System.Drawing.Point(3, 3);
            this.addFriend.Name = "addFriend";
            this.addFriend.Size = new System.Drawing.Size(175, 75);
            this.addFriend.TabIndex = 5;
            this.addFriend.Text = "Add a friend";
            this.addFriend.UseVisualStyleBackColor = true;
            this.addFriend.Click += new System.EventHandler(this.addFriend_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // receiveTextBox
            // 
            this.receiveTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receiveTextBox.Location = new System.Drawing.Point(178, 0);
            this.receiveTextBox.Name = "receiveTextBox";
            this.receiveTextBox.ReadOnly = true;
            this.receiveTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.receiveTextBox.Size = new System.Drawing.Size(864, 450);
            this.receiveTextBox.TabIndex = 4;
            this.receiveTextBox.Text = "";
            // 
            // AppChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 528);
            this.Controls.Add(this.receiveTextBox);
            this.Controls.Add(this.friendList);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppChat";
            this.Text = "AppChat";
            this.Load += new System.EventHandler(this.AppChat_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox friendList;
        private System.Windows.Forms.RichTextBox sendTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RichTextBox receiveTextBox;
        private System.Windows.Forms.Button addFriend;
        private System.Windows.Forms.Button button2;
    }
}


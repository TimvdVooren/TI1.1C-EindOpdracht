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
            this.receiveTextBox = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // friendList
            // 
            this.friendList.BackColor = System.Drawing.Color.LightSeaGreen;
            this.friendList.Dock = System.Windows.Forms.DockStyle.Left;
            this.friendList.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 15F);
            this.friendList.ForeColor = System.Drawing.Color.Blue;
            this.friendList.FormattingEnabled = true;
            this.friendList.ItemHeight = 25;
            this.friendList.Location = new System.Drawing.Point(0, 0);
            this.friendList.Margin = new System.Windows.Forms.Padding(2);
            this.friendList.Name = "friendList";
            this.friendList.Size = new System.Drawing.Size(134, 429);
            this.friendList.TabIndex = 0;
            this.friendList.SelectedIndexChanged += new System.EventHandler(this.friendList_SelectedIndexChanged);
            // 
            // sendTextBox
            // 
            this.sendTextBox.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 12F);
            this.sendTextBox.ForeColor = System.Drawing.Color.Blue;
            this.sendTextBox.Location = new System.Drawing.Point(2, 2);
            this.sendTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.sendTextBox.Size = new System.Drawing.Size(577, 62);
            this.sendTextBox.TabIndex = 1;
            this.sendTextBox.Text = "";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 13F);
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(583, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 62);
            this.button1.TabIndex = 2;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.sendTextBox);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(134, 366);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(648, 63);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // receiveTextBox
            // 
            this.receiveTextBox.BackColor = System.Drawing.Color.Aquamarine;
            this.receiveTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receiveTextBox.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 12F);
            this.receiveTextBox.ForeColor = System.Drawing.Color.Blue;
            this.receiveTextBox.Location = new System.Drawing.Point(134, 0);
            this.receiveTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.receiveTextBox.Name = "receiveTextBox";
            this.receiveTextBox.ReadOnly = true;
            this.receiveTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.receiveTextBox.Size = new System.Drawing.Size(648, 366);
            this.receiveTextBox.TabIndex = 4;
            this.receiveTextBox.Text = "";
            // 
            // AppChat
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 429);
            this.Controls.Add(this.receiveTextBox);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.friendList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppChat";
            this.Text = "AppChat";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox friendList;
        private System.Windows.Forms.RichTextBox sendTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RichTextBox receiveTextBox;
    }
}


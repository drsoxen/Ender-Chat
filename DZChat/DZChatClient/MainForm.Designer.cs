namespace DZChatClient
{
    partial class MainForm
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
            this.SendBox = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ClientServerStatus = new System.Windows.Forms.Label();
            this.MainMessageTextBox = new System.Windows.Forms.RichTextBox();
            this.StaticStatusText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBox_Users = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // SendBox
            // 
            this.SendBox.AllowDrop = true;
            this.SendBox.Location = new System.Drawing.Point(72, 240);
            this.SendBox.Name = "SendBox";
            this.SendBox.Size = new System.Drawing.Size(178, 20);
            this.SendBox.TabIndex = 0;
            this.SendBox.TextChanged += new System.EventHandler(this.SendBox_TextChanged);
            this.SendBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.SendBox_DragDrop);
            this.SendBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.SendBox_DragEnter);
            this.SendBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendBox_KeyDown);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(256, 199);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 61);
            this.SendButton.TabIndex = 1;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            this.SendButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendButton_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Client Chat";
            // 
            // ClientServerStatus
            // 
            this.ClientServerStatus.AutoSize = true;
            this.ClientServerStatus.Location = new System.Drawing.Point(61, 199);
            this.ClientServerStatus.Name = "ClientServerStatus";
            this.ClientServerStatus.Size = new System.Drawing.Size(37, 13);
            this.ClientServerStatus.TabIndex = 5;
            this.ClientServerStatus.Text = "Status";
            // 
            // MainMessageTextBox
            // 
            this.MainMessageTextBox.Location = new System.Drawing.Point(11, 13);
            this.MainMessageTextBox.Name = "MainMessageTextBox";
            this.MainMessageTextBox.Size = new System.Drawing.Size(316, 180);
            this.MainMessageTextBox.TabIndex = 6;
            this.MainMessageTextBox.Text = "";
            // 
            // StaticStatusText
            // 
            this.StaticStatusText.AutoSize = true;
            this.StaticStatusText.Location = new System.Drawing.Point(12, 199);
            this.StaticStatusText.Name = "StaticStatusText";
            this.StaticStatusText.Size = new System.Drawing.Size(43, 13);
            this.StaticStatusText.TabIndex = 7;
            this.StaticStatusText.Text = "Status: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(348, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Users";
            // 
            // TextBox_Users
            // 
            this.TextBox_Users.Location = new System.Drawing.Point(351, 30);
            this.TextBox_Users.Name = "TextBox_Users";
            this.TextBox_Users.Size = new System.Drawing.Size(104, 226);
            this.TextBox_Users.TabIndex = 10;
            this.TextBox_Users.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 272);
            this.Controls.Add(this.TextBox_Users);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StaticStatusText);
            this.Controls.Add(this.MainMessageTextBox);
            this.Controls.Add(this.ClientServerStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.SendBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "EnderChat";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SendBox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ClientServerStatus;
        private System.Windows.Forms.RichTextBox MainMessageTextBox;
        private System.Windows.Forms.Label StaticStatusText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox TextBox_Users;
    }
}


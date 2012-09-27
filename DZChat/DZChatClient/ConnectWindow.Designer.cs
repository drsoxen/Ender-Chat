namespace DZChatClient
{
    partial class ConnectWindow
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
            this.ConnectButton = new System.Windows.Forms.Button();
            this.TextBox_UserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBox_Port = new System.Windows.Forms.TextBox();
            this.TextBox_Endpoint = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Button_StartServer = new System.Windows.Forms.Button();
            this.Radio_Multicast = new System.Windows.Forms.RadioButton();
            this.Radio_TCP = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(12, 186);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(167, 54);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "Client Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // TextBox_UserName
            // 
            this.TextBox_UserName.Location = new System.Drawing.Point(72, 80);
            this.TextBox_UserName.Name = "TextBox_UserName";
            this.TextBox_UserName.Size = new System.Drawing.Size(197, 20);
            this.TextBox_UserName.TabIndex = 0;
            this.TextBox_UserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IPandPortTextBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "UserName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // TextBox_Port
            // 
            this.TextBox_Port.Location = new System.Drawing.Point(72, 142);
            this.TextBox_Port.Name = "TextBox_Port";
            this.TextBox_Port.Size = new System.Drawing.Size(85, 20);
            this.TextBox_Port.TabIndex = 1;
            this.TextBox_Port.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_Port_KeyDown);
            // 
            // TextBox_Endpoint
            // 
            this.TextBox_Endpoint.Location = new System.Drawing.Point(72, 110);
            this.TextBox_Endpoint.Name = "TextBox_Endpoint";
            this.TextBox_Endpoint.Size = new System.Drawing.Size(140, 20);
            this.TextBox_Endpoint.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Endpoint";
            // 
            // Button_StartServer
            // 
            this.Button_StartServer.Location = new System.Drawing.Point(185, 186);
            this.Button_StartServer.Name = "Button_StartServer";
            this.Button_StartServer.Size = new System.Drawing.Size(167, 54);
            this.Button_StartServer.TabIndex = 5;
            this.Button_StartServer.Text = "Start Server";
            this.Button_StartServer.UseVisualStyleBackColor = true;
            this.Button_StartServer.Click += new System.EventHandler(this.Button_StartServer_Click);
            // 
            // Radio_Multicast
            // 
            this.Radio_Multicast.AutoSize = true;
            this.Radio_Multicast.Location = new System.Drawing.Point(12, 13);
            this.Radio_Multicast.Name = "Radio_Multicast";
            this.Radio_Multicast.Size = new System.Drawing.Size(68, 17);
            this.Radio_Multicast.TabIndex = 6;
            this.Radio_Multicast.TabStop = true;
            this.Radio_Multicast.Text = "MultiCast";
            this.Radio_Multicast.UseVisualStyleBackColor = true;
            this.Radio_Multicast.CheckedChanged += new System.EventHandler(this.Radio_Multicast_CheckedChanged);
            // 
            // Radio_TCP
            // 
            this.Radio_TCP.AutoSize = true;
            this.Radio_TCP.Location = new System.Drawing.Point(12, 36);
            this.Radio_TCP.Name = "Radio_TCP";
            this.Radio_TCP.Size = new System.Drawing.Size(72, 17);
            this.Radio_TCP.TabIndex = 7;
            this.Radio_TCP.TabStop = true;
            this.Radio_TCP.Text = "TCPClient";
            this.Radio_TCP.UseVisualStyleBackColor = true;
            this.Radio_TCP.CheckedChanged += new System.EventHandler(this.Radio_TCP_CheckedChanged);
            // 
            // ConnectWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 250);
            this.Controls.Add(this.Radio_TCP);
            this.Controls.Add(this.Radio_Multicast);
            this.Controls.Add(this.Button_StartServer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBox_Endpoint);
            this.Controls.Add(this.TextBox_Port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBox_UserName);
            this.Controls.Add(this.ConnectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ConnectWindow";
            this.Text = "ConnectWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox TextBox_UserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBox_Port;
        private System.Windows.Forms.TextBox TextBox_Endpoint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Button_StartServer;
        private System.Windows.Forms.RadioButton Radio_Multicast;
        private System.Windows.Forms.RadioButton Radio_TCP;
    }
}
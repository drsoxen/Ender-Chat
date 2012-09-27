namespace UDPServer
{
    partial class UDPServer
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
            this.UDPClientMessages = new System.Windows.Forms.ListBox();
            this.StartServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UDPClientMessages
            // 
            this.UDPClientMessages.FormattingEnabled = true;
            this.UDPClientMessages.Location = new System.Drawing.Point(12, 12);
            this.UDPClientMessages.Name = "UDPClientMessages";
            this.UDPClientMessages.Size = new System.Drawing.Size(260, 186);
            this.UDPClientMessages.TabIndex = 0;
            // 
            // StartServer
            // 
            this.StartServer.Location = new System.Drawing.Point(12, 204);
            this.StartServer.Name = "StartServer";
            this.StartServer.Size = new System.Drawing.Size(260, 46);
            this.StartServer.TabIndex = 1;
            this.StartServer.Text = "Start Server";
            this.StartServer.UseVisualStyleBackColor = true;
            this.StartServer.Click += new System.EventHandler(this.StartServer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.StartServer);
            this.Controls.Add(this.UDPClientMessages);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox UDPClientMessages;
        private System.Windows.Forms.Button StartServer;
    }
}


namespace UDPClient
{
    partial class UDPClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.RemoteIP = new System.Windows.Forms.TextBox();
            this.SendDataButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Remote Host IP Address ";
            // 
            // RemoteIP
            // 
            this.RemoteIP.Location = new System.Drawing.Point(12, 25);
            this.RemoteIP.Name = "RemoteIP";
            this.RemoteIP.Size = new System.Drawing.Size(291, 20);
            this.RemoteIP.TabIndex = 1;
            // 
            // SendDataButton
            // 
            this.SendDataButton.Location = new System.Drawing.Point(15, 51);
            this.SendDataButton.Name = "SendDataButton";
            this.SendDataButton.Size = new System.Drawing.Size(75, 23);
            this.SendDataButton.TabIndex = 2;
            this.SendDataButton.Text = "Send Data";
            this.SendDataButton.UseVisualStyleBackColor = true;
            this.SendDataButton.Click += new System.EventHandler(this.SendDataButton_Click);
            // 
            // UDPClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 84);
            this.Controls.Add(this.SendDataButton);
            this.Controls.Add(this.RemoteIP);
            this.Controls.Add(this.label1);
            this.Name = "UDPClient";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox RemoteIP;
        private System.Windows.Forms.Button SendDataButton;
    }
}


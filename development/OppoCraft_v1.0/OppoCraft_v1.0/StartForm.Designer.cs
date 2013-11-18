namespace OppoCraft
{
    partial class StartForm
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
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.IPAddr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConnectionStatus = new System.Windows.Forms.Label();
            this.serverStatus = new System.Windows.Forms.Label();
            this.StartSrvBtn = new System.Windows.Forms.Button();
            this.ConnectedClients = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(281, 64);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(75, 23);
            this.ConnectBtn.TabIndex = 0;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // IPAddr
            // 
            this.IPAddr.Location = new System.Drawing.Point(144, 66);
            this.IPAddr.Name = "IPAddr";
            this.IPAddr.Size = new System.Drawing.Size(131, 20);
            this.IPAddr.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server IP address:";
            // 
            // ConnectionStatus
            // 
            this.ConnectionStatus.AutoSize = true;
            this.ConnectionStatus.Location = new System.Drawing.Point(152, 99);
            this.ConnectionStatus.Name = "ConnectionStatus";
            this.ConnectionStatus.Size = new System.Drawing.Size(0, 13);
            this.ConnectionStatus.TabIndex = 3;
            // 
            // serverStatus
            // 
            this.serverStatus.AutoSize = true;
            this.serverStatus.Location = new System.Drawing.Point(253, 17);
            this.serverStatus.Name = "serverStatus";
            this.serverStatus.Size = new System.Drawing.Size(0, 13);
            this.serverStatus.TabIndex = 5;
            // 
            // StartSrvBtn
            // 
            this.StartSrvBtn.Location = new System.Drawing.Point(155, 12);
            this.StartSrvBtn.Name = "StartSrvBtn";
            this.StartSrvBtn.Size = new System.Drawing.Size(75, 23);
            this.StartSrvBtn.TabIndex = 4;
            this.StartSrvBtn.Text = "Start Server";
            this.StartSrvBtn.UseVisualStyleBackColor = true;
            this.StartSrvBtn.Click += new System.EventHandler(this.StartSrvBtn_Click);
            // 
            // ConnectedClients
            // 
            this.ConnectedClients.AutoSize = true;
            this.ConnectedClients.Location = new System.Drawing.Point(144, 125);
            this.ConnectedClients.Name = "ConnectedClients";
            this.ConnectedClients.Size = new System.Drawing.Size(0, 13);
            this.ConnectedClients.TabIndex = 7;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 271);
            this.Controls.Add(this.ConnectedClients);
            this.Controls.Add(this.serverStatus);
            this.Controls.Add(this.StartSrvBtn);
            this.Controls.Add(this.ConnectionStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IPAddr);
            this.Controls.Add(this.ConnectBtn);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.TextBox IPAddr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ConnectionStatus;
        private System.Windows.Forms.Label serverStatus;
        private System.Windows.Forms.Button StartSrvBtn;
        private System.Windows.Forms.Label ConnectedClients;
    }
}
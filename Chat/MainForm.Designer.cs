namespace Chat
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
            this.messages = new System.Windows.Forms.ListBox();
            this.sysMessages = new System.Windows.Forms.ListBox();
            this.textMessage = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.connect = new System.Windows.Forms.Button();
            this.connectionStatus = new System.Windows.Forms.Label();
            this.disconnect = new System.Windows.Forms.Button();
            this.userName = new System.Windows.Forms.Label();
            this.userChoose = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.user2Messages = new System.Windows.Forms.ListBox();
            this.user3Messages = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // messages
            // 
            this.messages.FormattingEnabled = true;
            this.messages.HorizontalScrollbar = true;
            this.messages.ItemHeight = 16;
            this.messages.Location = new System.Drawing.Point(40, 46);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(387, 244);
            this.messages.TabIndex = 0;
            // 
            // sysMessages
            // 
            this.sysMessages.FormattingEnabled = true;
            this.sysMessages.HorizontalScrollbar = true;
            this.sysMessages.ItemHeight = 16;
            this.sysMessages.Location = new System.Drawing.Point(448, 46);
            this.sysMessages.Name = "sysMessages";
            this.sysMessages.Size = new System.Drawing.Size(356, 356);
            this.sysMessages.TabIndex = 1;
            // 
            // textMessage
            // 
            this.textMessage.Location = new System.Drawing.Point(40, 325);
            this.textMessage.Name = "textMessage";
            this.textMessage.Size = new System.Drawing.Size(286, 22);
            this.textMessage.TabIndex = 2;
            // 
            // send
            // 
            this.send.Enabled = false;
            this.send.Location = new System.Drawing.Point(332, 324);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(95, 23);
            this.send.TabIndex = 3;
            this.send.Text = "Отправить";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(40, 379);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(113, 23);
            this.connect.TabIndex = 4;
            this.connect.Text = "Подключиться";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // connectionStatus
            // 
            this.connectionStatus.AutoSize = true;
            this.connectionStatus.Location = new System.Drawing.Point(282, 385);
            this.connectionStatus.Name = "connectionStatus";
            this.connectionStatus.Size = new System.Drawing.Size(109, 16);
            this.connectionStatus.TabIndex = 5;
            this.connectionStatus.Text = "Не подключено";
            // 
            // disconnect
            // 
            this.disconnect.Enabled = false;
            this.disconnect.Location = new System.Drawing.Point(160, 378);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(105, 23);
            this.disconnect.TabIndex = 6;
            this.disconnect.Text = "Отключиться";
            this.disconnect.UseVisualStyleBackColor = true;
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // userName
            // 
            this.userName.AutoSize = true;
            this.userName.Location = new System.Drawing.Point(445, 19);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(37, 16);
            this.userName.TabIndex = 7;
            this.userName.Text = "label";
            // 
            // userChoose
            // 
            this.userChoose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userChoose.Enabled = false;
            this.userChoose.FormattingEnabled = true;
            this.userChoose.Location = new System.Drawing.Point(124, 16);
            this.userChoose.Name = "userChoose";
            this.userChoose.Size = new System.Drawing.Size(195, 24);
            this.userChoose.TabIndex = 9;
            this.userChoose.SelectedIndexChanged += new System.EventHandler(this.userChoose_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Сообщение";
            // 
            // user2Messages
            // 
            this.user2Messages.FormattingEnabled = true;
            this.user2Messages.ItemHeight = 16;
            this.user2Messages.Location = new System.Drawing.Point(40, 46);
            this.user2Messages.Name = "user2Messages";
            this.user2Messages.Size = new System.Drawing.Size(387, 244);
            this.user2Messages.TabIndex = 11;
            this.user2Messages.Visible = false;
            // 
            // user3Messages
            // 
            this.user3Messages.FormattingEnabled = true;
            this.user3Messages.ItemHeight = 16;
            this.user3Messages.Location = new System.Drawing.Point(40, 46);
            this.user3Messages.Name = "user3Messages";
            this.user3Messages.Size = new System.Drawing.Size(387, 244);
            this.user3Messages.TabIndex = 12;
            this.user3Messages.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 450);
            this.Controls.Add(this.user3Messages);
            this.Controls.Add(this.user2Messages);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userChoose);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.disconnect);
            this.Controls.Add(this.connectionStatus);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.send);
            this.Controls.Add(this.textMessage);
            this.Controls.Add(this.sysMessages);
            this.Controls.Add(this.messages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Чат";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox messages;
        private System.Windows.Forms.ListBox sysMessages;
        private System.Windows.Forms.TextBox textMessage;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.Label connectionStatus;
        private System.Windows.Forms.Button disconnect;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.ComboBox userChoose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox user2Messages;
        private System.Windows.Forms.ListBox user3Messages;
    }
}
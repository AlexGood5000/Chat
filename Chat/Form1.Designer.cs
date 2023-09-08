namespace Chat
{
    partial class Login
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.backPortChoose = new System.Windows.Forms.ComboBox();
            this.frontPortChoose = new System.Windows.Forms.ComboBox();
            this.nickname = new System.Windows.Forms.TextBox();
            this.enter = new System.Windows.Forms.Button();
            this.connectionAwaiting = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backPortChoose
            // 
            this.backPortChoose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.backPortChoose.FormattingEnabled = true;
            this.backPortChoose.Location = new System.Drawing.Point(33, 155);
            this.backPortChoose.Name = "backPortChoose";
            this.backPortChoose.Size = new System.Drawing.Size(108, 24);
            this.backPortChoose.TabIndex = 0;
            // 
            // frontPortChoose
            // 
            this.frontPortChoose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.frontPortChoose.FormattingEnabled = true;
            this.frontPortChoose.Location = new System.Drawing.Point(260, 155);
            this.frontPortChoose.Name = "frontPortChoose";
            this.frontPortChoose.Size = new System.Drawing.Size(108, 24);
            this.frontPortChoose.TabIndex = 1;
            // 
            // nickname
            // 
            this.nickname.Location = new System.Drawing.Point(143, 84);
            this.nickname.Name = "nickname";
            this.nickname.Size = new System.Drawing.Size(127, 22);
            this.nickname.TabIndex = 2;
            // 
            // enter
            // 
            this.enter.Location = new System.Drawing.Point(130, 225);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(150, 23);
            this.enter.TabIndex = 3;
            this.enter.Text = "Войти в систему";
            this.enter.UseVisualStyleBackColor = true;
            this.enter.Click += new System.EventHandler(this.enter_Click);
            // 
            // connectionAwaiting
            // 
            this.connectionAwaiting.AutoSize = true;
            this.connectionAwaiting.Location = new System.Drawing.Point(127, 276);
            this.connectionAwaiting.Name = "connectionAwaiting";
            this.connectionAwaiting.Size = new System.Drawing.Size(44, 16);
            this.connectionAwaiting.TabIndex = 4;
            this.connectionAwaiting.Text = "label1";
            this.connectionAwaiting.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Введите имя:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Входной порт:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Выходной порт:";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 341);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.connectionAwaiting);
            this.Controls.Add(this.enter);
            this.Controls.Add(this.nickname);
            this.Controls.Add(this.frontPortChoose);
            this.Controls.Add(this.backPortChoose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.Text = "Вход в систему";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox backPortChoose;
        private System.Windows.Forms.ComboBox frontPortChoose;
        private System.Windows.Forms.TextBox nickname;
        private System.Windows.Forms.Button enter;
        private System.Windows.Forms.Label connectionAwaiting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}


namespace chat_client
{
    partial class MainControl
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.onlineListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.generalChat = new System.Windows.Forms.ListBox();
            this.messageText = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.General = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.General.SuspendLayout();
            this.SuspendLayout();
            // 
            // onlineListBox
            // 
            this.onlineListBox.FormattingEnabled = true;
            this.onlineListBox.Location = new System.Drawing.Point(6, 16);
            this.onlineListBox.Name = "onlineListBox";
            this.onlineListBox.Size = new System.Drawing.Size(120, 342);
            this.onlineListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Users Online";
            // 
            // generalChat
            // 
            this.generalChat.FormattingEnabled = true;
            this.generalChat.Location = new System.Drawing.Point(6, 6);
            this.generalChat.Name = "generalChat";
            this.generalChat.Size = new System.Drawing.Size(512, 251);
            this.generalChat.TabIndex = 2;
            // 
            // messageText
            // 
            this.messageText.Location = new System.Drawing.Point(132, 312);
            this.messageText.Multiline = true;
            this.messageText.Name = "messageText";
            this.messageText.Size = new System.Drawing.Size(451, 46);
            this.messageText.TabIndex = 3;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(589, 312);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 46);
            this.sendButton.TabIndex = 4;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.General);
            this.tabControl1.Location = new System.Drawing.Point(132, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(532, 290);
            this.tabControl1.TabIndex = 5;
            // 
            // General
            // 
            this.General.Controls.Add(this.generalChat);
            this.General.Location = new System.Drawing.Point(4, 22);
            this.General.Name = "General";
            this.General.Padding = new System.Windows.Forms.Padding(3);
            this.General.Size = new System.Drawing.Size(524, 264);
            this.General.TabIndex = 0;
            this.General.Text = "General Chat";
            this.General.UseVisualStyleBackColor = true;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messageText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.onlineListBox);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(672, 369);
            this.tabControl1.ResumeLayout(false);
            this.General.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox onlineListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox generalChat;
        private System.Windows.Forms.TextBox messageText;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage General;
    }
}

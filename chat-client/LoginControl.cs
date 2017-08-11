using System;
using System.Windows.Forms;

namespace chat_client
{
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();
            //Subscribe to textboxe click event
            user_name.Click += new EventHandler(Click_Handler);
        }

        // Clear textboxes on click
        private void Click_Handler(object sender, System.EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.Text.Equals("Nickname"))
                textbox.Clear();
        }

        public Button LoginButton
        {
            get { return loginButton; }
        }

        public string StatusLabelText
        {
            get { return statusLabel.Text; }
            set { statusLabel.Text = value; }
        }

        public string UserName
        {
            get { return user_name.Text; }
        }
    }
}

using System.Windows.Forms;

namespace chat_client
{
    public partial class MainControl : UserControl
    {   
        public MainControl()
        {
            InitializeComponent();
        }

        public ListBox OnlineListBox
        {
            get { return onlineListBox; }
        }

        public ListBox GeneralChat
        {
            get { return generalChat; }
        }

        public TabControl TabBox
        {
            get { return tabControl1; }
        }

        public Button SendButton
        {
            get { return sendButton; }
        }

        public TextBox MessageText
        {
            get { return messageText; }
        }
    }
}

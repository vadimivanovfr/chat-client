using System;
using System.Drawing;
using System.Windows.Forms;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json.Linq;
using static chat_client.Network;

namespace chat_client
{
    public partial class Form1 : Form
    {
        //Socket socket = Network.socket;
        LoginControl loginControl = new LoginControl(); // Login Screen
        MainControl mainControl = new MainControl(); // Chat screen
        MessageType message_type = MessageType.General;

        public Form1()
        {
            InitializeComponent();
            InitializeEvents();
            EventsSubscribe();
            loginControl.Location = new Point((ClientSize.Width - loginControl.Width) / 2, 
                (ClientSize.Height - loginControl.Height) / 2); // Centering the login screen
            Controls.Add(loginControl);

            // Creating a context menu for onlineListBox
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("Private Message");
            cms.Items[0].Tag = 0;
            cms.ItemClicked += new ToolStripItemClickedEventHandler(Online_Click);
            mainControl.OnlineListBox.ContextMenuStrip = cms;
        }

        // Subscribing to Form events
        private void EventsSubscribe()
        {
            loginControl.LoginButton.Click += new System.EventHandler(LoginButton_Click);
            mainControl.SendButton.Click += new System.EventHandler(Send_Button);
            mainControl.MessageText.KeyUp += new KeyEventHandler(SendMessage);
            mainControl.TabBox.Selecting += new TabControlCancelEventHandler(TabBox_Selecting);
        }

        // Initializing socket events
        private void InitializeEvents()
        {
            // On Connect
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                Invoke((MethodInvoker)delegate {
                    loginControl.StatusLabelText = "Server: Online";
                    loginControl.LoginButton.Enabled = true;
                });
            });

            // When server sent a message about successful login
            socket.On("login", (result) =>
            {
                if ((bool)result == true)
                {
                    Invoke((MethodInvoker)delegate {
                        Controls.Remove(loginControl);
                        Size = new Size(680, 400);
                        Controls.Add(mainControl);
                        socket.Emit("load"); // Requesting chat data (Users online, last 10 messages)
                    });
                }
            });

            // When server sent chat data
            socket.On("load", (result) =>
            {
                Invoke((MethodInvoker)delegate {
                    JArray msg = (JArray)result; // msg[0] - users, msg[1] - messages
                    JObject users = (JObject)msg[0];

                    // Adding users to the Online List
                    foreach (var user in users)
                    {
                        user_list.Add(new User { id = user.Key, name = user.Value.ToString() });
                        mainControl.OnlineListBox.Items.Add(user.Value.ToString());
                    }

                    // Adding messages to the General Chat
                    var message_list = (JArray)msg[1];
                    foreach (var message in message_list)
                    {
                        mainControl.GeneralChat.Items.Add(message[0] + ": " + message[1]);
                    }
                });
            });

            // When new user connected
            socket.On("add_user", (user) =>
            {
                Invoke((MethodInvoker)delegate {
                    JArray user_array = (JArray)user;
                    user_list.Add(new User { id = user_array[0].ToString(), name = user_array[1].ToString() });
                    mainControl.OnlineListBox.Items.Add(user_array[1].ToString());
                });
            });

            // When user disconnected
            socket.On("remove_user", (user_id) =>
            {
                Invoke((MethodInvoker)delegate {
                    User user = user_list.Find(a => a.id == user_id.ToString());
                    mainControl.OnlineListBox.Items.Remove(user.name);
                    user_list.Remove(user);
                });
            });

            // When user sent a message
            socket.On("message", (message) =>
            {
                Invoke((MethodInvoker)delegate {
                    JArray msg = (JArray)message; // msg[0] - Socket ID, msg[1] - Message, msg[2] - Message type
                    User user = user_list.Find(a => a.id == msg[0].ToString()); // Getting user data

                    if ((int)msg[2] == (int)MessageType.General) // If message type is general
                        mainControl.GeneralChat.Items.Add(user.name + ": " + msg[1].ToString());
                    else // If private message
                    {
                        // If private conversation tab with the user is already exist then switch on it
                        foreach (TabPage a in mainControl.TabBox.TabPages)
                        {
                            if (a.Tag != null && a.Tag.ToString() == user.id)
                            {
                                mainControl.TabBox.SelectedTab = a;
                                ListBox listbox = (ListBox)a.Controls[0];
                                listbox.Items.Add(user.name + ": " + msg[1].ToString());
                                return;
                            }
                        }
                        // If not then create it
                        TabPage myTabPage = new TabPage(user.name);
                        ListBox pm = new ListBox() { Size = mainControl.GeneralChat.Size, Location = mainControl.GeneralChat.Location };
                        pm.Items.Add(user.name + ": " + msg[1].ToString());
                        myTabPage.BackColor = Color.White;
                        myTabPage.Controls.Add(pm);
                        myTabPage.Tag = user.id;
                        mainControl.TabBox.TabPages.Add(myTabPage);
                        mainControl.TabBox.SelectedTab = myTabPage;
                    }
                });
            });
        }

        // Sending login data
        private void LoginButton_Click(object sender, EventArgs e)
        {
            socket.Emit("login", loginControl.UserName);
        }

        // Sending message
        private void Send_Button(object sender, EventArgs e)
        {
            string target = String.Empty;
            if (message_type == MessageType.PM)
            {
                target = user_list.Find(a => a.id == mainControl.TabBox.SelectedTab.Tag.ToString()).id;
                ListBox listbox = (ListBox)mainControl.TabBox.SelectedTab.Controls[0];
                listbox.Items.Add("Me: " + mainControl.MessageText.Text);
            }
            socket.Emit("message", message_type, mainControl.MessageText.Text, target);
            mainControl.MessageText.Clear();
        }

        // Context menu click
        private void Online_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            switch (item.Tag)
            {
                case 0: // Private Message
                    foreach (TabPage a in mainControl.TabBox.TabPages)
                    {
                        if (a.Tag == mainControl.OnlineListBox.SelectedItem)
                        {
                            mainControl.TabBox.SelectedTab = a;
                            return;
                        }
                    }
                    TabPage myTabPage = new TabPage(mainControl.OnlineListBox.SelectedItem.ToString());
                    ListBox pm = new ListBox() {
                        Size = mainControl.GeneralChat.Size,
                        Location = mainControl.GeneralChat.Location
                    };
                    myTabPage.BackColor = Color.White;
                    myTabPage.Controls.Add(pm);
                    myTabPage.Tag = user_list.Find(a => a.name == mainControl.OnlineListBox.SelectedItem.ToString()).id;
                    mainControl.TabBox.TabPages.Add(myTabPage);
                    mainControl.TabBox.SelectedTab = myTabPage;
                    break;
            }
        }

        // Sending message with Enter key
        private void SendMessage(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Send_Button(sender, e);
            }
        }

        // Switching message type
        private void TabBox_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // If selected Tab is General Chat then message type is General otherwise PM
            if (e.TabPage == mainControl.TabBox.GetControl(0))
                message_type = MessageType.General;
            else
                message_type = MessageType.PM;
        }
    }
}

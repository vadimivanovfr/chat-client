using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace chat_client
{
    public class Network
    {
        public static Socket socket = IO.Socket("http://artschool4.me:3000"); // Create connection
        public static List<User> user_list = new List<User>(); // List of Users

        public enum MessageType // Message types: General Chat and PM (Private Message)
        {
            General,
            PM
        }
    }

    public class User
    {
        public string id; // Socket ID
        public string name; // Nickname
    }
}

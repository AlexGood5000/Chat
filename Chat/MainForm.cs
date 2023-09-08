using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    internal partial class MainForm : Form
    {
        internal Connection connection;

        internal MainForm(Connection connection, string name)
        {
            InitializeComponent();
            this.connection = connection;
            connection.GetComponents(sysMessages, messages, user2Messages, user3Messages, connect, disconnect, send, connectionStatus, userChoose);
            userName.Text = "Текущий пользователь \"" + name + "\"";
            textMessage.Text = "";
        }

        private void connect_Click(object sender, EventArgs e)
        {
            sysMessages.Items.Add("[" + DateTime.Now + "] Инициализация подключения");
            connection.Connect();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            sysMessages.Items.Add("[" + DateTime.Now.ToString() + "] Соединение установлено");
            connection.Rts();
        }

        private void send_Click(object sender, EventArgs e)
        {
            if (textMessage.Text != "")
            {
                connection.Send(textMessage.Text, userChoose.SelectedIndex);
                textMessage.Text = "";
            }
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            sysMessages.Items.Add("[" + DateTime.Now + "] Инициализация отключения");
            connection.Disconnect();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            connection.ClosePorts();
            Environment.Exit(0);
        }

        private void userChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (userChoose.SelectedIndex == 0)
            {
                messages.Visible = true;
                user2Messages.Visible = false;
                user3Messages.Visible = false;
            }
            else if (userChoose.SelectedIndex == 1)
            {
                messages.Visible = false;
                user2Messages.Visible = true;
                user3Messages.Visible = false;
            }
            else if (userChoose.SelectedIndex == 2)
            {
                messages.Visible = false;
                user2Messages.Visible = false;
                user3Messages.Visible = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Disconnect();
        }
    }
}

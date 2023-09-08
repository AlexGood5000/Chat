using System;
using System.IO.Ports;
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
    internal partial class Login : Form
    {
        private Connection connection;
        private int countTick;
        private Timer timer;
        private bool enterPressed;

        internal Login()
        {
            InitializeComponent();
            enterPressed = false;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (SerialPort.GetPortNames().Length == 0)
            {
                MessageBox.Show("Нет доступных портов для подключения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            backPortChoose.Items.AddRange(SerialPort.GetPortNames());
            backPortChoose.Text = backPortChoose.Items[0].ToString();
            frontPortChoose.Items.AddRange(SerialPort.GetPortNames());
            frontPortChoose.Text = frontPortChoose.Items[1].ToString();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            countTick++;
            if (countTick == 60)
            {
                MessageBox.Show("Время ожидания подключения истекло!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                enterPressed = false;
                connection.ClosePorts();
                connectionAwaiting.Text = "";
                timer.Stop();
            }
            else if (countTick < 60 && connection.backPort.DsrHolding == true && connection.frontPort.DsrHolding == true)
            {
                MainForm mf = new MainForm(connection, nickname.Text);
                mf.Show();
                timer.Stop();
                this.Visible = false;
            }

        }

        private void enter_Click(object sender, EventArgs e)
        {
            if (backPortChoose.SelectedIndex == frontPortChoose.SelectedIndex)
                MessageBox.Show("Выберите разные порты!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (nickname.Text == "")
                MessageBox.Show("Введите имя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                connection = new Connection(nickname.Text);
                connection.ClosePorts();
                try
                {
                    connection.OpenPorts(backPortChoose.Text, frontPortChoose.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно открыть порт: " + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                enterPressed = true;
                connectionAwaiting.Text = "Ожидание соединения...";
                connectionAwaiting.Visible = true;
                connection.Dtr();
                countTick = 0;
                timer = new Timer();
                timer.Interval = 1000;
                timer.Enabled = true;
                timer.Tick += timer_Tick;
                timer.Start();
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (enterPressed)
                connection.ClosePorts();
        }
    }
}

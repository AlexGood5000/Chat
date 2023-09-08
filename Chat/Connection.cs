using System;
using System.IO.Ports;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    internal class Connection
    {
        internal SerialPort backPort { get; }
        internal SerialPort frontPort { get; }

        private User me, user2, user3;
        private bool connected, names, uplinkSend;
        private int ackRecievedCount, gotNames, ackNeeded;
        private ListBox sysMessages, messages, user2Messages, user3Messages;
        private Button connect, disconnect, send;
        private Label connectionStatus;
        private ComboBox userChoose;
        private List<byte> previousFrame;

        internal Connection(string name)
        {
            backPort = new SerialPort();
            frontPort = new SerialPort();
            me = new User();
            user2 = new User();
            user3 = new User();
            me.Name = name;
            connected = false;
            gotNames = 0;
            ackRecievedCount = 0;
            uplinkSend = false;
            names = false;
        }

        internal void GetComponents(ListBox sysMessages, ListBox messages, ListBox user2Messages, ListBox user3Messages, Button connect, Button disconnect, Button send, Label connectionStatus, ComboBox userChoose)
        {
            this.sysMessages = sysMessages;
            this.messages = messages;
            this.user2Messages = user2Messages;
            this.user3Messages = user3Messages;
            this.connect = connect;
            this.disconnect = disconnect;
            this.send = send;
            this.connectionStatus = connectionStatus;
            this.userChoose = userChoose;
        }

        internal void Rts()
        {
            backPort.RtsEnable = true;
            frontPort.RtsEnable = true;
        }

        internal void Dtr()
        {
            backPort.DtrEnable = true;
            frontPort.DtrEnable = true;
        }

        internal void ClosePorts()
        {
            backPort.RtsEnable = false;
            frontPort.RtsEnable = false;
            backPort.DtrEnable = false;
            frontPort.DtrEnable = false;
            backPort.Close();
            frontPort.Close();
        }

        internal void OpenPorts(string backPortName, string frontPortName)
        {
            backPort.PortName = backPortName;
            backPort.BaudRate = 9600;
            backPort.DataBits = 8;
            backPort.Parity = Parity.None;
            backPort.StopBits = StopBits.One;
            backPort.ReadTimeout = 1000;
            backPort.WriteTimeout = 1000;
            backPort.Open();
            frontPort.PortName = frontPortName;
            frontPort.BaudRate = 9600;
            frontPort.DataBits = 8;
            frontPort.Parity = Parity.None;
            frontPort.StopBits = StopBits.One;
            frontPort.ReadTimeout = 1000;
            frontPort.WriteTimeout = 1000;
            frontPort.Open();
            backPort.DataReceived += new SerialDataReceivedEventHandler(backport_DataRecieved);
        }

        internal void Connect()
        {
            if (backPort.CtsHolding == true && frontPort.CtsHolding == true && !connected)
            {
                me.Address = 0x01;
                string linkdata = "0x02|0x03|";
                List<byte> coded_linkdata = new List<byte>(), link_to_send = new List<byte>() { 0xFF, 0x01, 0x7F, 0x01, 0x00 };
                coded_linkdata = Check.CodingStr(linkdata);
                link_to_send[2] = me.Address;
                link_to_send[4] = (byte)coded_linkdata.Count;
                for (int i = 0; i < coded_linkdata.Count; i++)
                    link_to_send.Add(coded_linkdata[i]);
                link_to_send.Add(0xFF);
                ackNeeded = 2;
                frontPort.Write(link_to_send.ToArray(), 0, link_to_send.Count);
                userChoose.Invoke(new EventHandler(delegate { userChoose.Items.Add("Всем"); }));
                sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Ожидание подключения..."); }));
            }
            else
                sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Сбой подключения"); }));
        }

        internal void Send(string informdata, int address_to)
        {
            if (backPort.CtsHolding == true && frontPort.CtsHolding == true)
            {
                List<byte> coded_informdata = new List<byte>(), inform_to_send = new List<byte>() { 0xFF, 0x00, 0x00, 0x00, 0x00 };
                coded_informdata = Check.CodingStr(informdata);
                switch (address_to)
                {
                    case 1:
                        inform_to_send[1] = user2.Address;
                        break;
                    case 2:
                        inform_to_send[1] = user3.Address;
                        break;
                    case 0:
                        inform_to_send[1] = 0x7F;
                        break;

                }
                inform_to_send[2] = me.Address;
                inform_to_send[4] = (byte)coded_informdata.Count;
                for (int i = 0; i < coded_informdata.Count; i++)
                    inform_to_send.Add(coded_informdata[i]);
                inform_to_send.Add(0xFF);
                if (address_to == 0)
                    ackNeeded = 2;
                else
                    ackNeeded = 1;
                frontPort.Write(inform_to_send.ToArray(), 0, inform_to_send.Count);
                previousFrame = inform_to_send;
                if (connected && names)
                {
                    sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Сообщение отправлено"); }));
                    switch (address_to)
                    {
                        case 0:
                            messages.Invoke(new EventHandler(delegate { messages.Items.Add("[" + me.Name + "]: " + informdata); }));
                            break;
                        case 1:
                            user2Messages.Invoke(new EventHandler(delegate { user2Messages.Items.Add("[" + me.Name + "]: " + informdata); }));
                            break;
                        case 2:
                            user3Messages.Invoke(new EventHandler(delegate { user3Messages.Items.Add("[" + me.Name + "]: " + informdata); }));
                            break;
                    }
                }
            }
            else
                sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Сбой отправки сообщения"); }));
        }

        internal void Disconnect()
        {
            if (backPort.CtsHolding == true && frontPort.CtsHolding == true && connected)
            {
                List<byte> uplink_to_send = new List<byte>() { 0xFF, 0x7F, 0x00, 0x02, 0xFF };
                uplink_to_send[2] = me.Address;
                uplinkSend = true;
                ackNeeded = 2;
                frontPort.Write(uplink_to_send.ToArray(), 0, uplink_to_send.Count);
                sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Ожидание отключения..."); }));
            }
            else
                sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Сбой отключения"); }));
        }

        private void ChangeComponents(string status, bool permission)
        {
            connectionStatus.Invoke(new EventHandler(delegate { connectionStatus.Text = status; }));
            disconnect.Invoke(new EventHandler(delegate { disconnect.Enabled = permission; }));
            send.Invoke(new EventHandler(delegate { send.Enabled = permission; }));
            connect.Invoke(new EventHandler(delegate { connect.Enabled = !permission; }));
            userChoose.Invoke(new EventHandler(delegate { userChoose.Enabled = permission; }));
            userChoose.Invoke(new EventHandler(delegate { userChoose.Text = ""; }));
        }

        private void backport_DataRecieved(object sender, SerialDataReceivedEventArgs e)
        {
            List<byte> frame = new List<byte>();
            byte temp = (byte)backPort.ReadByte();
            if (temp == 0xFF)
            {
                frame.Add(temp);
                do
                {
                    temp = (byte)backPort.ReadByte();
                    frame.Add(temp);
                }
                while (temp != 0xFF);
                temp = frame[3];
                switch (temp)
                {
                    case 0x01: //link-кадр
                        if (frame[2] != me.Address)
                        {
                            List<byte> data = new List<byte>(), new_frame = new List<byte>(), ack_to_send = new List<byte>() { 0xFF, 0x00, 0x00, 0x03, 0xFF };
                            for (int i = 5; i < 5 + frame[4]; i++)
                                data.Add(frame[i]);
                            string decoded = Check.DeCodingStr(data.ToArray());
                            if (decoded == "")
                                break;
                            me.Address = (byte)Convert.ToUInt32(decoded.Split('|')[0], 16);
                            decoded = decoded.Replace(decoded.Split('|')[0] + "|", "");
                            data = Check.CodingStr(decoded);
                            for (int i = 0; i < 4; i++)
                                new_frame.Add(frame[i]);
                            new_frame.Add((byte)data.Count);
                            for (int i = 0; i < data.Count; i++)
                                new_frame.Add(data[i]);
                            new_frame.Add(0xFF);
                            frontPort.Write(new_frame.ToArray(), 0, new_frame.Count);
                            ack_to_send[1] = frame[2];
                            ack_to_send[2] = me.Address;
                            frontPort.Write(ack_to_send.ToArray(), 0, ack_to_send.Count);
                            connected = true;
                            userChoose.Invoke(new EventHandler(delegate { userChoose.Items.Add("Всем"); }));
                            sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Подключение произведено"); }));
                            Send(me.Name, 0);
                        }
                        break;
                    case 0x03: //ack-кадр
                        if (frame[1] == me.Address)
                        {
                            if (!connected)
                            {
                                ackRecievedCount++;
                                if (ackRecievedCount == ackNeeded)
                                {
                                    connected = true;
                                    ackRecievedCount = 0;
                                    sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Подключение произведено"); }));
                                    Send(me.Name, 0);
                                }
                            }
                            else if (!names)
                            {
                                ackRecievedCount++;
                                if (ackRecievedCount == ackNeeded)
                                {
                                    names = true;
                                    ackRecievedCount = 0;
                                    ChangeComponents("Подключено", true);
                                    userChoose.Invoke(new EventHandler(delegate { userChoose.SelectedIndex = 0; }));
                                    sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Тестовая передача выполнена"); }));
                                }
                            }
                            else if (connected && names && !uplinkSend)
                            {
                                ackRecievedCount++;
                                if (frame[2] == user2.Address)
                                    sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Сообщение пользователю \"" + user2.Name + "\" доставлено"); }));
                                else if (frame[2] == user3.Address)
                                    sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Сообщение пользователю \"" + user3.Name + "\" доставлено"); }));
                                if (ackRecievedCount == ackNeeded)
                                    ackRecievedCount = 0;
                            }
                            else if (uplinkSend)
                            {
                                ackRecievedCount++;
                                if (ackRecievedCount == ackNeeded)
                                {
                                    uplinkSend = false;
                                    connected = false;
                                    names = false;
                                    gotNames = 0;
                                    user2 = new User();
                                    user3 = new User();
                                    me.Address = 0;
                                    ackRecievedCount = 0;
                                    messages.Invoke(new EventHandler(delegate { messages.Items.Clear(); }));
                                    user2Messages.Invoke(new EventHandler(delegate { user2Messages.Items.Clear(); }));
                                    user3Messages.Invoke(new EventHandler(delegate { user3Messages.Items.Clear(); }));
                                    ChangeComponents("Не подключено", false);
                                    userChoose.Invoke(new EventHandler(delegate { userChoose.Items.Clear(); }));
                                    sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Отключение произведено"); }));
                                }
                            }    
                        }
                        else
                            frontPort.Write(frame.ToArray(), 0, frame.Count);
                        break;
                    case 0x00: //inform-кадр
                        if (frame[2] != me.Address)
                        {
                            if (frame[1] != me.Address && frame[1] != 0x7F)
                                frontPort.Write(frame.ToArray(), 0, frame.Count);
                            else
                            {
                                List<byte> data = new List<byte>(), ack_to_send = new List<byte>() { 0xFF, 0x00, 0x00, 0x03, 0xFF }, ret_to_send = new List<byte>() { 0xFF, 0x00, 0x00, 0x04, 0xFF };
                                for (int i = 5; i < 5 + frame[4]; i++)
                                    data.Add(frame[i]);
                                string decoded = Check.DeCodingStr(data.ToArray());
                                if (decoded == "")
                                {
                                    ret_to_send[1] = frame[2];
                                    ret_to_send[2] = me.Address;
                                    frontPort.Write(ret_to_send.ToArray(), 0, ret_to_send.Count);
                                    break;
                                }
                                if (gotNames < 2)
                                {
                                    if (user2.Address == 0 && user2.Name == null)
                                    {
                                        user2.Address = frame[2];
                                        user2.Name = decoded;
                                        userChoose.Invoke(new EventHandler(delegate { userChoose.Items.Add("пользователю \"" + user2.Name + "\""); }));
                                    }
                                    else
                                    {
                                        user3.Address = frame[2];
                                        user3.Name = decoded;
                                        userChoose.Invoke(new EventHandler(delegate { userChoose.Items.Add("пользователю \"" + user3.Name + "\""); }));
                                    }
                                    gotNames++;
                                }
                                else
                                {
                                    string sender_name = "";
                                    if (user2.Address == frame[2])
                                        sender_name = user2.Name;
                                    else
                                        sender_name = user3.Name;
                                    if (frame[1] == 0x7F)
                                        messages.Invoke(new EventHandler(delegate { messages.Items.Add("[" + sender_name + "]: " + decoded); }));
                                    else
                                    {
                                        if (frame[2] == user2.Address)
                                            user2Messages.Invoke(new EventHandler(delegate { user2Messages.Items.Add("[" + sender_name + "]: " + decoded); }));
                                        else
                                            user3Messages.Invoke(new EventHandler(delegate { user3Messages.Items.Add("[" + sender_name + "]: " + decoded); }));
                                    }
                                    sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Сообщение получено"); }));
                                }
                                if (frame[1] == 0x7F)
                                {
                                    frontPort.Write(frame.ToArray(), 0, frame.Count);
                                }
                                ack_to_send[1] = frame[2];
                                ack_to_send[2] = me.Address;
                                frontPort.Write(ack_to_send.ToArray(), 0, ack_to_send.Count);
                            }
                        }
                        break;
                    case 0x02: //uplink-кадр
                        if (frame[2] != me.Address && connected == true)
                        {
                            List<byte> ack_to_send = new List<byte>() { 0xFF, 0x00, 0x00, 0x03, 0xFF };
                            frontPort.Write(frame.ToArray(), 0, frame.Count);
                            ack_to_send[1] = frame[2];
                            ack_to_send[2] = me.Address;
                            frontPort.Write(ack_to_send.ToArray(), 0, ack_to_send.Count);
                            connected = false;
                            names = false;
                            gotNames = 0;
                            user2 = new User();
                            user3 = new User();
                            me.Address = 0;
                            messages.Invoke(new EventHandler(delegate { messages.Items.Clear(); }));
                            user2Messages.Invoke(new EventHandler(delegate { user2Messages.Items.Clear(); }));
                            user3Messages.Invoke(new EventHandler(delegate { user3Messages.Items.Clear(); }));
                            ChangeComponents("Не подключено", false);
                            userChoose.Invoke(new EventHandler(delegate { userChoose.Items.Clear(); }));
                            sysMessages.Invoke(new EventHandler(delegate { sysMessages.Items.Add("[" + DateTime.Now + "] Отключение произведено"); }));
                        }
                        break;
                    case 0x04: //ret-кадр
                        if (frame[1] == me.Address)
                        {
                            frontPort.Write(previousFrame.ToArray(), 0, previousFrame.Count);
                        }
                        else
                            frontPort.Write(frame.ToArray(), 0, frame.Count);
                        break;
                }
            }
        }
    }
}

/*
 *   Message.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2020 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Fuel_Rats_IRC_Helper
{
    class Message
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        private List<MessagePart> _MessagePart;
        private Settings _Settings;

        public Message()
        {
            _MessagePart = new List<MessagePart>();
            _Settings = new Settings();
        }

        //
        // Summary:
        //   Adds a message part to the beginning of the message
        //
        // Parameters:
        //   messagePartName:
        //     name of the message part to be able to identify it later
        //
        //   messagePartText:
        //     text of the message part
        //
        // Returns:
        //   Error code:
        //     0: Everything went ok
        public int Prepend(string messagePartName, string messagePartText)
        {
            _MessagePart.Insert(0, new MessagePart(messagePartName, messagePartText));
            return 0;
        }

        //
        // Summary:
        //   Adds a message part to the end of the message
        //
        // Parameters:
        //   messagePartName:
        //     name of the message part to be able to identify it later
        //
        //   messagePartText:
        //     text of the message part
        //
        // Returns:
        //   Error code:
        //     0: Everything went ok
        public int Append(string messagePartName, string messagePartText)
        {
            _MessagePart.Add(new MessagePart(messagePartName, messagePartText));
            return 0;
        }

        //
        // Summary:
        //   Replaces the text of a specified message part
        //
        // Parameters:
        //   messagePartName:
        //     name of the message part that identifies it
        //
        //   messagePartText:
        //     new text of the message part
        //
        // Returns:
        //   Error code:
        //     0: Everything went ok
        //     1: There is no message part with that name
        public int Replace(string messagePartName, string messagePartText)
        {
            int messagePartToBeReplaced = -1;

            for (int i = 0; i < _MessagePart.Count; ++i)
            {
                if (_MessagePart.ElementAt(i).Name == messagePartName)
                {
                    messagePartToBeReplaced = i;
                    break;
                }
            }

            if (messagePartToBeReplaced != -1)
            {
                _MessagePart.ElementAt(messagePartToBeReplaced).Text = messagePartText;
            }

            else
            {
                return 1;
            }

            return 0;
        }

        //
        // Summary:
        //   Removes a specified message part
        //
        // Parameters:
        //   messagePartName:
        //     name of the message part that identifies it
        //
        // Returns:
        //   Error code:
        //     0: Everything went ok
        //     1: There is no message part with that name
        public int Remove(string messagePartName)
        {
            int messagePartIndexToBeRemoved = -1;

            for (int i = 0; i < _MessagePart.Count; ++i)
            {
                if (_MessagePart.ElementAt(i).Name == messagePartName)
                {
                    messagePartIndexToBeRemoved = i;
                    break;
                }
            }

            if (messagePartIndexToBeRemoved != -1)
            {
                _MessagePart.RemoveAt(messagePartIndexToBeRemoved);
            }

            else
            {
                return 1;
            }

            return 0;
        }

        //
        // Summary:
        //   Removes all message parts
        //
        // Returns:
        //   Error code:
        //     0: Everything went ok
        public int Clear()
        {
            _MessagePart.Clear();
            return 0;
        }

        //
        // Summary:
        //   Generates the message from the message parts
        //
        // Returns:
        //   Generated message
        public string Generate()
        {
            string message = "";
            for (int i = 0; i < _MessagePart.Count; ++i)
            {
                message += _MessagePart.ElementAt(i).Text;

                if (i != _MessagePart.Count - 1)
                {
                    message += ' ';
                }
            }

            return message;
        }

        //
        // Summary:
        //   Sends the message to the IRC client window and focuses the Elite Dangerous window afterwards
        //
        // Parameters:
        //   message:
        //     message that should be sent to the IRC client
        //
        // Returns:
        //   Error code:
        //     0: Everything went ok
        //     1: Process names of the IRC client and/or Elite Dangerous not specified in preferences
        //     2: IRC client process not found
        //     3: Multiple IRC client processes found
        //     4: IRC helper process not found (How would that even happen? I don't know. :D Why did I put this in here? I dont know. XD)
        //     5: Multiple IRC helper processes found
        //     6: Elite Dangerous process not found
        //     7: Multiple Elite Dangerous processes found
        public int Send(string message)
        {
            string processNameIrcClient = _Settings.Get("processNameIrcClient");
            string processNameIrcHelper = "Fuel Rats IRC Helper";
            string processNameEliteDangerous = _Settings.Get("processNameEliteDangerous");

            if (processNameIrcClient == "" && processNameEliteDangerous == "")
            {
                MessageBox.Show("Your message could not be sent! Go to Settings -> Preferences -> Behaviour to select your IRC client and Elite Dangerous process names.", "Error");
                return 1;
            }

            else if (processNameIrcClient == "")
            {
                MessageBox.Show("Your message could not be sent! Go to Settings -> Preferences -> Behaviour to select your IRC client process name.", "Error");
                return 1;
            }

            else if (processNameEliteDangerous == "")
            {
                MessageBox.Show("Your message could not be sent! Go to Settings -> Preferences -> Behaviour to select your Elite Dangerous process name.", "Error");
                return 1;
            }

            Process[] processIrcClient = Process.GetProcessesByName(processNameIrcClient);
            Process[] processIrcHelper = Process.GetProcessesByName(processNameIrcHelper);
            Process[] processEliteDangerous = Process.GetProcessesByName(processNameEliteDangerous);

            if (processIrcClient.Length == 0)
            {
                MessageBox.Show("Your message could not be sent! There is no process called \"" + processNameIrcClient + "\"! Please open \"" + processNameIrcClient + "\" and try again.", "Error");
                return 2;
            }

            else if (processIrcClient.Length > 1)
            {
                MessageBox.Show("Your message could not be sent! There are more than one processes called \"" + processNameIrcClient + "\"! Please make sure there is only one process called \"" + processNameIrcClient + "\" and try again.", "Error");
                return 3;
            }

            if (processIrcHelper.Length == 0)
            {
                MessageBox.Show("Your message could not be sent! There is no process called \"" + processNameIrcHelper + "\"! Please open \"" + processNameIrcHelper + "\" and try again.", "Error");
                return 4;
            }

            else if (processIrcHelper.Length > 1)
            {
                MessageBox.Show("Your message could not be sent! There are more than one processes called \"" + processNameIrcHelper + "\"! Please make sure there is only one process called \"" + processNameIrcHelper + "\" and try again.", "Error");
                return 5;
            }

            if (processEliteDangerous.Length == 0)
            {
                MessageBox.Show("Your message could not be sent! There is no process called \"" + processNameEliteDangerous + "\"! Please open \"" + processNameEliteDangerous + "\" and try again.", "Error");
                return 6;
            }

            else if (processEliteDangerous.Length > 1)
            {
                MessageBox.Show("Your message could not be sent! There are more than one processes called \"" + processNameEliteDangerous + "\"! Please make sure there is only one process called \"" + processNameEliteDangerous + "\" and try again.", "Error");
                return 7;
            }

            SetForegroundWindow(processIrcClient.ElementAt(0).MainWindowHandle);

            if (_Settings.Get("messageInsertionMode") == "copyPaste")
            {
                Clipboard.SetText(message);
                System.Windows.Forms.SendKeys.SendWait("^{v}");
                Thread.Sleep(10);
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            }

            else
            {

                // The function that actually sends the message interprets some characters as special characters, so we need to escape them by enclosing them in braces.
                string newMessage = "";
                for (int i = 0; i < message.Length; ++i)
                {
                    switch (message.ElementAt(i))
                    {
                        case '+':
                        case '^':
                        case '%':
                        case '~':
                        case '(':
                        case ')':
                        case '[':
                        case ']':
                        case '{':
                        case '}':
                            newMessage += '{';
                            newMessage += message.ElementAt(i);
                            newMessage += '}';
                            break;
                        default:
                            newMessage += message.ElementAt(i);
                            break;
                    }
                }

                System.Windows.Forms.SendKeys.SendWait(newMessage + "{ENTER}");
            }

            SetForegroundWindow(processIrcHelper.ElementAt(0).MainWindowHandle);
            SetForegroundWindow(processEliteDangerous.ElementAt(0).MainWindowHandle);

            return 0;
        }
    }
}

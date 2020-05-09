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
using System.Windows;

namespace Fuel_Rats_IRC_Helper
{
    class Message
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        private List<MessagePart> _MessagePart;

        public Message()
        {
            _MessagePart = new List<MessagePart>();
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
        //   Removes all messages
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
        //   Sends the message to the HexChat window and focuses the Elite Dangerous window afterwards
        //
        // Parameters:
        //   message:
        //     message that should be sent to HexChat
        //
        // Returns:
        //   Error code:
        //     0: Everything went ok
        //     1: There is no process called "HexChat"
        //     2: There are multiple processes called "HexChat"
        public int Send(string message)
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

            Process[] processHexChat = Process.GetProcessesByName("HexChat");
            Process[] processIrcHelper = Process.GetProcessesByName("Fuel Rats IRC Helper");
            Process[] processEliteDangerous = Process.GetProcessesByName("EliteDangerous64");

            if (processHexChat.Length == 1)
            {
                SetForegroundWindow(processHexChat.ElementAt(0).MainWindowHandle);
                System.Windows.Forms.SendKeys.SendWait(newMessage + "{ENTER}");
            }

            else if (processHexChat.Length == 0)
            {
                MessageBox.Show("Could not find a process called \"HexChat\"! Please open HexChat and try again.", "Error");
                return 1;
            }

            else
            {
                MessageBox.Show("There are more than one processes called \"HexChat\"! Please make sure there is only one process called \"HexChat\" and try again.", "Error");
                return 2;
            }

            if (processIrcHelper.Length == 1)
            {
                SetForegroundWindow(processIrcHelper.ElementAt(0).MainWindowHandle);
            }

            if (processEliteDangerous.Length == 1)
            {
                SetForegroundWindow(processEliteDangerous.ElementAt(0).MainWindowHandle);
            }

            return 0;
        }
    }
}

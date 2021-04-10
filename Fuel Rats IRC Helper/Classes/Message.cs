/*
 *   Message.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2021 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using Win32Interop.WinHandles;

namespace Fuel_Rats_IRC_Helper
{
    static class Message
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        private static List<MessagePart> _MessagePart = new List<MessagePart>();

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
        public static int Prepend(string messagePartName, string messagePartText)
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
        public static int Append(string messagePartName, string messagePartText)
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
        public static int Replace(string messagePartName, string messagePartText)
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
        public static int Remove(string messagePartName)
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
        public static int Clear()
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
        public static string Generate()
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
        //     1: Window titles of the IRC client and/or Elite Dangerous not specified in settings
        //     2: IRC client window not found
        //     3: Multiple IRC client windows found
        //     4: IRC helper window not found (How would that even happen? I don't know. :D Why did I put this in here? I don't know. XD)
        //     5: Multiple IRC helper windows found
        //     6: Elite Dangerous window not found
        //     7: Multiple Elite Dangerous windows found
        public static int Send(string message)
        {
            string windowTitleIrcClient = Settings.Get("windowTitleIrcClient");
            string windowTitleIrcHelper = "Fuel Rats - IRC Helper";
            string windowTitleEliteDangerous = Settings.Get("windowTitleEliteDangerous");

            if (windowTitleIrcClient == "" && windowTitleEliteDangerous == "")
            {
                MessageBox.Show("Your message could not be sent! Go to Settings -> Behaviour to specify your IRC client and Elite Dangerous window titles.", "Error");
                return 1;
            }

            else if (windowTitleIrcClient == "")
            {
                MessageBox.Show("Your message could not be sent! Go to Settings -> Behaviour to specify your IRC client window title.", "Error");
                return 1;
            }

            else if (windowTitleEliteDangerous == "")
            {
                MessageBox.Show("Your message could not be sent! Go to Settings -> Behaviour to specify your Elite Dangerous window title.", "Error");
                return 1;
            }

            IEnumerable<WindowHandle> windowIrcClient = TopLevelWindowUtils.FindWindows(wh => wh.GetWindowText().Contains(windowTitleIrcClient));
            IEnumerable<WindowHandle> windowIrcHelper = TopLevelWindowUtils.FindWindows(wh => wh.GetWindowText().Contains(windowTitleIrcHelper));
            IEnumerable<WindowHandle> windowEliteDangerous = TopLevelWindowUtils.FindWindows(wh => wh.GetWindowText().Contains(windowTitleEliteDangerous));
            
            if (windowIrcClient.Count() == 0)
            {
                MessageBox.Show("Your message could not be sent! There is no window whose title contains \"" + windowTitleIrcClient + "\"! Please open your IRC client or go to Settings -> Behaviour to specify your IRC client window title. Then try again.", "Error");
                return 2;
            }

            else if (windowIrcClient.Count() > 1)
            {
                MessageBox.Show("Your message could not be sent! There are more than one windows whose titles contain \"" + windowTitleIrcClient + "\"! Please make sure there is only one window whose title contains \"" + windowTitleIrcClient + "\" or go to Settings -> Behaviour to specify your IRC client window title. Then try again.", "Error");
                return 3;
            }

            if (windowIrcHelper.Count() == 0)
            {
                MessageBox.Show("I don't know how you managed to trigger this error message, but your message could not be sent, because there is no window whose title contains \"" + windowTitleIrcHelper + "\"! Please try again.", "Error");
                return 4;
            }

            else if (windowIrcHelper.Count() > 1)
            {
                MessageBox.Show("Your message could not be sent! There are more than one windows whose titles contain \"" + windowTitleIrcHelper + "\"! Please make sure there is only one window whose title contains \"" + windowTitleIrcHelper + "\" and try again.", "Error");
                return 5;
            }

            if (windowEliteDangerous.Count() == 0)
            {
                MessageBox.Show("Your message could not be sent! There is no window whose title contains \"" + windowTitleEliteDangerous + "\"! Please launch Elite Dangerous or go to Settings -> Behaviour to specify your Elite Dangerous window title. Then try again.", "Error");
                return 6;
            }

            else if (windowEliteDangerous.Count() > 1)
            {
                MessageBox.Show("Your message could not be sent! There are more than one windows whose titles contain \"" + windowTitleEliteDangerous + "\"! Please make sure there is only one instance of the game running or go to Settings -> Behaviour to specify your Elite Dangerous window title. Then try again.", "Error");
                return 7;
            }

            SetForegroundWindow(windowIrcClient.ElementAt(0).RawPtr);
            
            if (message != "")
            {
                if (Settings.Get("messageInsertionMode") == "sendKeys")
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

                else
                {
                    if (Clipboard.ContainsText() == true)
                    {
                        string clipboardBackup = Clipboard.GetText();
                        Clipboard.SetText(message);
                        System.Windows.Forms.SendKeys.SendWait("^{v}");
                        Thread.Sleep(10 + message.Length / 5);
                        System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                        Clipboard.SetText(clipboardBackup);
                    }
                
                    else
                    {
                        IDataObject clipboardBackup = Clipboard.GetDataObject();
                        Clipboard.SetText(message);
                        System.Windows.Forms.SendKeys.SendWait("^{v}");
                        Thread.Sleep(10 + message.Length / 5);
                        System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                        Clipboard.SetDataObject(clipboardBackup);
                    }
                }
            }

            SetForegroundWindow(windowIrcHelper.ElementAt(0).RawPtr);
            SetForegroundWindow(windowEliteDangerous.ElementAt(0).RawPtr);
            
            return 0;
        }
    }
}

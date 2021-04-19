/*
 *   IrcMessage.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2021 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


namespace Fuel_Rats_IRC_Helper
{
    public class IrcMessage
    {
        private long _UnixTimestamp;
        private string _Timestamp;
        private string _SenderNickname;
        private string _Text;

        public IrcMessage(long unixTimestamp, string timestamp, string senderNickname, string text)
        {
            _UnixTimestamp = unixTimestamp;
            _Timestamp = timestamp;
            _SenderNickname = senderNickname;
            _Text = text;
        }

        public IrcMessage() : this(0, "unknown", "unknown", "")
        {

        }

        public long UnixTimestamp
        {
            get { return _UnixTimestamp; }
        }

        public string Timestamp
        {
            get { return _Timestamp; }
        }

        public string SenderNickname
        {
            get { return _SenderNickname; }
        }

        public string Text
        {
            get { return _Text; }
        }
    }
}

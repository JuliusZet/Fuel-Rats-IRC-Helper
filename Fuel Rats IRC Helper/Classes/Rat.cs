/*
 *   Rat.cs
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
    class Rat
    {
        private static int _NextId;
        private int _Id;
        private string _IrcNick;
        private string _CmdrName;

        public Rat(string ircNick, string cmdrName)
        {
            _Id = _NextId++;
            _IrcNick = ircNick;
            _CmdrName = cmdrName;
        }

        public Rat(string ircNick) : this(ircNick, "unknwon")
        {

        }

        public Rat() : this("unknwon", "unknwon")
        {

        }
    }
}

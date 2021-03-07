/*
 *   MessagePart.cs
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
	class MessagePart
    {
		private string _Name;
		private string _Text;

		public MessagePart()
		{
			Name = "";
			Text = "";
		}

		public MessagePart(string name, string text)
		{
			Name = name;
			Text = text;
		}

		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		public string Text
		{
			get { return _Text; }
			set { _Text = value; }
		}
	}
}

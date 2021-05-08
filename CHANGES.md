# Changelog
### Version 1.4.1.0 (2021-05-08)
+ Added IRC connection status indicator to main window
+ Fixed all messages being displayed twice after re-connection
+ Fixed detection of rat un-assignment
+ Updated explanation in Settings how messages are associated to cases
+ Improved performance
### Version 1.4.0.1 (2021-05-07)
+ Slightly improved parsing of information from RATSIGNALs
### Version 1.4.0.0 (2021-05-06)
+ Added IRC client functionality
  + Received messages from the IRC server are processed and associated to cases
  + Messages are now sent directly via the integrated IRC client
+ Added Case list window where all cases that were created during runtime are listed
+ Added Case windows where details and filtered chats are displayed
  + There are also some useful buttons and functions for dispatchers
+ Added option to automatically copy the system name to clipboard when a PC_SIGNAL is received
+ Added distance unit "Mls"
+ Reordered menu bar items
+ Renamed Preferences window to Settings
+ Improved layout
+ Improved readability
+ Improved startup locations of windows
+ Improved performance
+ Fixed a bug when clearing the Syscorr and Navcheck textboxes
+ Fixed a typo in the changelog
### Version 1.3.1.0 (2021-03-11)
+ Added "Reset to default" buttons in Settings -> Behaviour
+ Imporoved some messages (for example "#0 in open" now outputs "#0 client in open")
+ Focusing the Case number textbox will no longer clear it
+ Improved reading of configuration files
### Version 1.3.0.0 (2021-03-07)
+ Focusing windows now uses window titles instead of process names
+ Fixed a bug when sending an empty message
+ Improved usability
  + If a message could not be sent, the message is preserved
+ Updated Copyright claims
### Version 1.2.4.0 (2020-06-20)
+ Improved usability
  + Clear button will focus the Case number textbox
### Version 1.2.3.1 (2020-06-13)
+ Fixed a bug in the clipboard backup functionality
### Version 1.2.3.0 (2020-06-03)
+ Improved the copy-paste message insertion mode so it no longer overwrites your clipboard
+ Improved process drop-down fields in Settings -> Behaviour
### Version 1.2.2.0 (2020-05-29)
+ Improved usability
  + Return key in the Case number textbox will focus the Jump callout textbox
  + Return key in any other textbox will send the message
  + Numpad-plus or -minus keys in the Distance textbox will increase or decrease the unit
+ Fixed typos
### Version 1.2.1.0 (2020-05-17)
+ Added colorization of already sent messages
### Version 1.2.0.0 (2020-05-16)
+ Added the option to choose between two message insertion methods:
  + Write out the message key by key (Default)
  + Copy and paste the message with the clipboard
### Version 1.1.2.0 (2020-05-15)
+ Added drop-down field to select distance unit
+ Improved layout
+ Improved performance
### Version 1.1.1.1 (2020-05-13)
+ Added option to clear drop-down fields in Settings -> Behaviour
+ Fixed settings not being retained after update
### Version 1.1.1.0 (2020-05-13)
+ Added drop-down fields to select processes in Settings -> Behaviour
### Version 1.1.0.0 (2020-05-12)
+ Added Settings window
+ Added support for other IRC clients other than HexChat
+ Fixed a crash when trying to get a non-existing value from the configuration file
### Version 1.0.4.1 (2020-05-10)
+ Improved reading and writing of configuration files
### Version 1.0.4.0 (2020-05-09)
+ Added silent automatic update-check on startup
+ Added new-update notification
+ Added way to manually check for updates
### Version 1.0.3.0 (2020-05-09)
+ Added Changelog window
+ Improved performance
### Version 1.0.2.0 (2020-05-09)
+ Added Clear button
### Version 1.0.1.1 (2020-05-08)
+ Updated information in About window
### Version 1.0.1.0 (2020-05-08)
+ Added information to About window
### Version 1.0.0.1 (2020-05-08)
+ Fixed configuration error in installer
### Version 1.0.0.0 (2020-05-08)
+ Added graphical user interface
+ Added functionality to graphical user interface
+ Added applcation icon
+ Created installer
# SeeloewenMapper

A simple HID to XInput mapper for game controllers. Currently only supports controllers using the DS4 input protocol. Both USB as well as Bluetooth connections are supported.
The main focus of this software is to stay as simple and clean as possible. No unnecessary features, no bloat, no complex design - instead you get simplicity and performance.

<img width="794" height="462" alt="image" src="https://github.com/user-attachments/assets/5275bc45-fc2d-4743-9307-dfcedfd92e71" />

Warning: This software is currently in early stages of development and will thus be unstable and not feature-complete. This also means that things could break or not work as planned. The author of this software is not liable for any problems that might arise as a result of using this software.

If you stumble across any issues, feel free to create an issue on GitHub so it can be fixed as soon as possible.

## Known Issues
* "Ghost connections" where two virtual devices are created and connected. This is just a visual issue though and only one device actually works.
* DS4 v1 controllers connected over bluetooth will not work correctly and produce random inputs

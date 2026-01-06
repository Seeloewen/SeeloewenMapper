# SeeloewenMapper
A simple HID to XInput mapper for game controllers. Currently only supports controllers using the DS4 input protocol.

Warning: This software is currently in early stages of development and will thus be unstable and not feature-complete. This also means that things could break or not work as planned. The author of this software is not liable for any problems that might arise as a result of using this software.

If you stumble across any issues, feel free to create an issue on GitHub so it can be fixed as soon as possible.

## Known Issues
* Poor performance / input delay when a lot of system resources are used
* "Ghost connections" where the controller seems to be successfully detected, but ViGEmBus fails to create a virtual controller

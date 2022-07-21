# AudioStream

This is a simple application that runs in the background and keeps a constant audio stream open on the system. I
created it to resolve annoying popping sounds produced by my Logitech G PRO headset whenever it entered power saving
mode due to lack of open audio streams.

## Installation

Download the latest executable from the [releases page](https://github.com/baileyherbert/audiostream/releases) and
save it anywhere on your system. You can run this executable to start the program.

## Documentation

![AudioStream Tray Icon](https://i.bailey.sh/nXW4rAdBoS.png)

### Automatic startup

Right click the tray icon or open the program's options and enable the "Enable on startup" option. This will create a
registry entry to start the executable when you log in.

### Tray icon

If you would like to disable the tray icon, right click on the icon or open the program's options and disable the
"Enable tray icon" option. The application will continue running in the background.

To open the program's options when the tray icon is disabled, simply run the executable file again. It will detect the
duplicate instance, and open the first instance's settings instead.

## How it works

An empty audio stream is created using NAudio and is kept open until the application is closed. Because your headset
sees an audio stream available, it remains fully active even when no audio is playing, and any problematic sounds
(popping or crackling) related to the onset of power saving mode are resolved.

For this reason, I don't recommend running this on any battery-powered device. Any real difference in overall power
consumption should be pretty negligible, but this could vary by device.

## Credits

Audio icons created by SumberRejeki from Flaticon:
> https://www.flaticon.com/premium-icon/audio-headset_4340355

Powered by NAudio:
> https://github.com/naudio/NAudio

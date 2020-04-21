Pointers:
--------------------

Lower Resolution by 1/2 (1280 x 720 > 640 x 360 > 320 x 180 > 160 x 90 ) to improve rendering performance on default demos. You can use any resolution you want (almost)
but the default demos are made to work with the 1280 x 720 aspect ratio.

Verify rendering performance via "Test"

Verify network performance via live testing using Create / Join. Can test between Unity Editor and Standalone on a single machine if need be.

Resolution and Quality are inversely proportional.

Lowering the Framerate in the default examples' Options menu will reduce the video framerate, this in turn will boost the framerate of the application.

Roadmap:
--------------------
WebGL Audio Transmission
/\
WebGL Mic Transmission
/\
4k
/\
6o fps
/\
Magic Leap
/\
Unity 2019.3
/\
New Video Encoder
/\
Additional Audio Controls

History:
--------------------
3.1.0
Added SendData array to StreamSDKTransporter to allow for custom per receiver data routing without having to write custom code!!!
Added Input and Mouse tracking for any number of users directly into StreamSDK
Added DontDestroyOnLoad, FilterMode, ResampleAudio, and UpdateStreamCamera fields to StreamSDKAdvancedOptions
Added CaptureScreen and DontDestroyOnLoad to Tools to allow for full screen capturing and the ability for keeping necessary GameObjects alive across multiple scenes (both of these were used to allow Swap Fire to run as a cloud game via StreamSDK)
Removed StreamSDKCloudMouse and StreamSDKCloudController
Started removing "Cloud" in general from naming conventions...


--------------------
3.00
Unity 2019 based
Moved StreamSDK to a free-to-start subscription model
Dramatically improved rendering performance (80-100%) via Clone
Added in-app/game audio streaming to capture and transmit all music and sound effects
Removed StreamSDKQualityManager and StreamSDKOptionsManager, they were full of centralized spaghetti logic
Added StreamSDKOptionDropdown and StreamSDKOptionSlider to decentralize get/set options as PlayerPrefs and route logic to StreamSDK or override with other component and method
Removed need for XMLSerializer
Focused on Photon Self-Hosted Server
Dramatically improved Docs
Verified support for PC/Mac, iOS, Android, WebGL, UWP
Unverified (but should work) on Lumin (Magic Leap) and Game Consoles
Removed TNet support (again) for now

--------------------
2.09
Added Cloud Mouse support for controlling mouse driven UI's over the network.
2.08
Backend bug fixes for cleanup with regard to many-to-many resources
Refashioned Options menu with dropdowns instead of text fields
2.07
Updated audio processing to remove choppiness and provide vastly improved stability
Fixed null reference bug in UpdateTextureLocal
2.06
Added many-to-many stream capabilities. Simply assign multiple StreamDisplays on the StreamSDK component and the system handles the rest.
2.05
Fixed compatibility with Unity 2018
Improved efficiency of stream via SendStream()
Updated serialization method to support .NET with UWP (HoloLens support)
Includes IL2CPP dll as well, although this only works between UWP devices for now(Surface > HoloLens for example)

Current support for UWP (HoloLens)
HoloLens > PC UWP (varying results)
HoloLens > PC Sandalone (yes)
HoloLens > PC Unity Editor (no)
HoloLens > Mac Unity Editor (yes)
HoloLens > Mac Standalone (yes)

- 2.04
Fixed memory leak that caused a 75% performance drop when going from the Options menu to a stream.

Added StreamType in order to easily organize and create various streaming experiences such as chat, broadcast, cloudcast, cloudcontrol, and various test modes.

Added Cloudcast and Cloud Control demos.

Added Photon Protocol and Region selection to Options menus in examples.

Solidified Photon example connectivity issues. Will alway connect / join properly so long as the region and protocols align.

Tidied up project a bit...

- 2.03
Fixed bugs that prohibited standard 2-way sharing after broadcast update. Removed Unity 5 support.

- 2.02
Added Broadcast Car Demo and Instant Viewer example to demonstrate how to setup up one-to-many broadcast streaming games for multiple receivers.

- 2.01
Added Car Demo, added a few helper scripts, made changes to core to handle invalid assignments, organized files a bit

- 2.0
This is "Video Chat" 2.0, a major version upgrade from our long time "Video Chat" Unity package. It now features HD and vastly improved performance and is capable of streaming anything Unity can render.
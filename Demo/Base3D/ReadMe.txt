Out of Unity 2019.1, exporting the standard Base3D scene to Mac incurred a rendering bug, whereby the ContentCanvasCamera would cancel previous rendering passes, only showing the Back button and a black screen.

This seems to be a Unity bug.

The Base3DMac scene uses an altered version of the ContentCanvas to workaround the rendering glitch.

We'll keep an eye on this...

In Base3DWebGL notice that there is no AudioListener. Audio can still be heard in WebGL, but adding an AudioListener causes it to crash upon connection to other clients.
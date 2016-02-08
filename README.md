MoverioUnityPlugin
==================

Contact seantron@imaginarycomputer.com for support

PLEASE NOTE, THIS PLUGIN (and the Moverio) WORK BEST WITH UNITY 4.5 - 4.6.

UNITY 5’s new render engine doesn’t provide high enough frame rates for wearable devices.


1.	Locate the Android Manifest (Assets/Plugins/Android/AndroidManifest.xml)
2.	In the Manifest change the    package="com.yourcompanyname.YourAppName" To the Bundle Identifier you have picked in your >PlayerSettings/OtherSettings (must be switched to the Android Platform)
3.  Drop in MoverioController and MoverioCameraRig Prefabs into your scene from MoverioController/Prefabs 
4. Play with the settings on the MoverioController and MoverioCameraRig scripts on the Prefabs to be to your liking. 
5. Compile APK to Moverio! 

//Best Practices 
Sensor - changing the Sensor in the middle of your scene will cause some erratic behavior. It’s best to set the Sensor at the Beginning of the scene, or if you must change, ignore the data for about a second. 
Dealing with UI/Button Press - For Moverio I’ve found it’s best to poll 
Input.GetMouseButtonDown(0) instead of Input.touches. 


Repo for the Moverio Unity Plugin which is a Unity3D Native Android plugin for the Moverio BT-200s.

This plugin helps you build exciting AR/VR Stereoscopic 3D Android games and apps for the Epson Moverio BT-200 Smart Glasses.

The Moverio Unity Plugin includes the following features:

*Camera Rig Prefab that switches seemlessly between Stereoscopic 3D and 2D content with the hooks to activate the Stereoscopic mode on the Moverios (also works with Google Cardboard) 

*Camera Rig also now has Automatic Head Tracking!

*Control over the Moverio's Glasses Brightness

*Native control over Sensor location (Head/Hand Control)

*Native Control over Audio/Display mute 

using UnityEngine;
using System.Collections;



public enum MoverioEventType
{
	Display3DOn,
	Display3DOff,
	DisplayBrightnessChange,
	MuteAudioOn,
	MuteAudioOff,
	MuteDisplayOn,
	MuteDisplayOff,
	SensorHeadTrack,
	SensorHandController
}

public enum MoverioDisplayType
{
	Display3D,
	Display2D
}

public enum MoverioSensorType
{
	SensorHeadTracking,
	SensorHandController
}

[AddComponentMenu("Moverio/MoverioController")]

public class MoverioController : MonoBehaviour {
	
	public delegate void MoverioEvent(MoverioEventType type);
	public static event MoverioEvent OnMoverioStateChange;

	public int InitialScreenBrightness = 20;

	public MoverioDisplayType InitialDisplayMode = MoverioDisplayType.Display2D;

	public MoverioSensorType InitialSensorMode = MoverioSensorType.SensorHeadTracking;

	private AndroidJavaClass _unityPlayer;
	private AndroidJavaObject _currentActivity;

	private static MoverioController _instance;
	public static MoverioController Instance
	{
		get
		{
			if(_instance == null)
			{
				Debug.Log("Please Add MoverioController Prefab To Scene!");
			}
			return _instance;
		}
	}

	void Awake()
	{
		_instance = this;
	}

	bool MoverioDevice = true;

	void Start () 
	{

		CheckDeviceType();

		SetJavaClass();

		SetDefaultSettings();

	}

	void CheckDeviceType()
	{
		if(SystemInfo.deviceModel.Equals("EPSON embt2"))
		{

			AndroidJNI.AttachCurrentThread();

		} else {

			MoverioDevice = false;

		}
	}

	void SetDefaultSettings()
	{
		if(InitialDisplayMode.Equals(MoverioDisplayType.Display3D))
		{
			SetDisplay3D(true);
		} else {
			SetDisplay3D(false);
		}

		if(!InitialScreenBrightness.Equals(20))
		{
			string msg = "";
			msg = SetDisplayBrightness(InitialScreenBrightness);

			Debug.Log(msg);
		}

		if(InitialSensorMode.Equals(MoverioSensorType.SensorHandController))
		{
			SetSensorMode(MoverioSensorType.SensorHandController);
		}
	}

	void SetJavaClass()
	{
#if UNITY_ANDROID && !UNITY_EDITOR

		if(MoverioDevice)
		{
			using(_unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				_currentActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			}

			_currentActivity.Call("SetMoverioDevice");
		}

#endif
	}

	/*
	 * 
	 * SetSensorMode takes either MoverioSensorType.SensorHandController or MoverioSensorType.SensorHeadTracking
	 * 
	 * 
	 */

	public string SetSensorMode(MoverioSensorType sType)
	{
		string msg = "NOT SET";



#if UNITY_ANDROID && !UNITY_EDITOR
		
		if(MoverioDevice)
		{
			int i = 0;

			if(sType.Equals(MoverioSensorType.SensorHandController))
			{
				i = 1;
			}

			msg = _currentActivity.Call<string> ("SetSensorMode", i);
		}
		
#endif

		if(sType.Equals(MoverioSensorType.SensorHandController))
		{
			if(OnMoverioStateChange != null)
			{
				OnMoverioStateChange(MoverioEventType.SensorHandController);
			}
		} else {
			if(OnMoverioStateChange != null)
			{
				OnMoverioStateChange(MoverioEventType.SensorHeadTrack);
			}
		}
		

		return msg;

	}

	/*
	 * 
	 * SetDisplayBrightness takes an int between 0 - 20 
	 * will automatically return an ERROR msg for out of range
	 * 
	 */


	public string SetDisplayBrightness(int brightness)
	{
		string msg = "NOT SET";

#if UNITY_ANDROID && !UNITY_EDITOR

		if(MoverioDevice)
		{
			msg = _currentActivity.Call<string> ("SetDisplayBrightness", brightness);
		}

#endif

		if(OnMoverioStateChange != null)
		{
			OnMoverioStateChange(MoverioEventType.DisplayBrightnessChange);
		}

		return msg;
	}

	/*
	 * 
	 * Gets Current Display Brightness level (an int between 0 - 20)
	 * 
	 */

	public int GetDisplayBrightness()
	{
		int i = -1;

#if UNITY_ANDROID && !UNITY_EDITOR

		if(MoverioDevice)
		{
			i = _currentActivity.Call<int>("GetDisplayBrightness");
		}

#endif
		return i;
	}

	/*
	 * 
	 * Sets 3D Display toggle on/off
	 * 
	 */

	public void SetDisplay3D(bool on)
	{
#if UNITY_ANDROID && !UNITY_EDITOR

		if(MoverioDevice)
		{
			_currentActivity.Call("SetDisplay3D", on);
		}

#endif

		if(OnMoverioStateChange != null)
		{
			MoverioEventType eType;

			if(on)
			{
				eType = MoverioEventType.Display3DOn;
			} else {
				eType = MoverioEventType.Display3DOff;
			}

			OnMoverioStateChange(eType);
		}
	}

	/*
	 * 
	 * Sets 3D Display toggle on/off
	 * 
	 */

	public void MuteAudio(bool mute)
	{

#if UNITY_ANDROID && !UNITY_EDITOR

		if(MoverioDevice)
		{
			_currentActivity.Call ("MuteAudio", mute);	
		}

#endif

		if(OnMoverioStateChange != null)
		{
			MoverioEventType eType;
			
			if(mute)
			{
				eType = MoverioEventType.MuteAudioOn;
			} else {
				eType = MoverioEventType.MuteAudioOff;
			}
			
			OnMoverioStateChange(eType);
		}
	}

	/*
	 * 
	 * Sets 3D Display toggle on/off
	 * 
	 */

	public void MuteDisplay(bool mute)
	{

#if UNITY_ANDROID && !UNITY_EDITOR

		if(MoverioDevice)
		{
			_currentActivity.Call ("MuteDisplay", mute);
		}

#endif

		if(OnMoverioStateChange != null)
		{
			MoverioEventType eType;
			
			if(mute)
			{
				eType = MoverioEventType.MuteDisplayOn;
			} else {
				eType = MoverioEventType.MuteDisplayOff;
			}
			
			OnMoverioStateChange(eType);
		}


	}


}

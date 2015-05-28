using UnityEngine;
using System.Collections;

[AddComponentMenu("Moverio/MoverioCameraController")]

public class MoverioCameraController : MonoBehaviour {

	private static MoverioCameraController _instance;
	public static MoverioCameraController Instance
	{
		get
		{
			if(_instance == null)
			{
				Debug.Log("Please Add MoverioCameraRig Prefab To Scene!");
			}

			return _instance;
		}
	}

	public Camera LeftEyeCam, RightEyeCam, Cam2D;

	public float PupillaryDistance = 0.05f;

	MoverioDisplayType _displayState;

	void Awake()
	{
		_instance = this;
	}

	void Start()
	{
		LeftEyeCam.aspect = RightEyeCam.aspect = Screen.width / Screen.height * 2.0f;
		SetPupillaryDistance(PupillaryDistance);
	}

	public void SetPupillaryDistance(float pDist)
	{
		PupillaryDistance = pDist;

		LeftEyeCam.transform.localPosition = new Vector3(-PupillaryDistance, 0.0f, 0.0f);
		RightEyeCam.transform.localPosition = new Vector3(PupillaryDistance, 0.0f, 0.0f);
	}

	void OnEnable()
	{
		MoverioController.OnMoverioStateChange += HandleOnMoverioStateChange;
	}

	void OnDisable()
	{
		MoverioController.OnMoverioStateChange -= HandleOnMoverioStateChange;
	}

	void HandleOnMoverioStateChange (MoverioEventType type)
	{
		switch(type)
		{
		case MoverioEventType.Display3DOff:
			SetCurrentDisplayType(MoverioDisplayType.Display2D);
			break;
		case MoverioEventType.Display3DOn:
			SetCurrentDisplayType(MoverioDisplayType.Display3D);
			break;
		}

	}

	public MoverioDisplayType GetCurrentDisplayState()
	{
		return _displayState;
	}

	public void SetCurrentDisplayType(MoverioDisplayType type)
	{
		_displayState = type;

		switch(_displayState)
		{
		case MoverioDisplayType.Display2D:
			LeftEyeCam.enabled = RightEyeCam.enabled = false;
			Cam2D.enabled = true;
			break;
		case MoverioDisplayType.Display3D:
			LeftEyeCam.enabled = RightEyeCam.enabled = true;
			Cam2D.enabled = false;
			break;
		}
	}


}

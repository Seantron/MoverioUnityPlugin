using UnityEngine;
using System.Collections;

public class AccelRotator : MonoBehaviour {

	public float Speed = 10.0f;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localEulerAngles += Input.acceleration * Speed; 
	}
}

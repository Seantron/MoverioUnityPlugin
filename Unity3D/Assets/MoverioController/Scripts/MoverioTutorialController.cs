using UnityEngine;
using System.Collections;

public class MoverioTutorialController : MonoBehaviour {

	public GUIText TextView;

	void Start () 
	{
		TextView.text = "Welcome to the Tutorial!";

		StartCoroutine(TutorialSequence());


	}
	
	IEnumerator TutorialSequence()
	{

		yield return new WaitForSeconds(3.0f);

		TextView.text = "Starting Dimmer";
		
		yield return new WaitForSeconds(3.0f);



		MoverioController.Instance.SetDisplayBrightness(10);
		
		TextView.text = "Brightness at 10!";
		
		
		
		yield return new WaitForSeconds(2.0f);

		MoverioController.Instance.SetDisplay3D(true);
		
		TextView.text = "3D Mode on!";
		

		yield return new WaitForSeconds(3.0f);

		MoverioController.Instance.SetDisplay3D(false);
		
		TextView.text = "3D Mode off!";
		
		yield return new WaitForSeconds(3.0f);

		MoverioController.Instance.SetDisplayBrightness(5);
		
		TextView.text = "Brightness at 5!";

		yield return new WaitForSeconds(3.0f);

		MoverioController.Instance.SetDisplayBrightness(15);
		
		TextView.text = "Brightness at 15!";
		
		yield return new WaitForSeconds(3.0f);

		MoverioController.Instance.SetDisplayBrightness(20);
		
		TextView.text = "Brightness at 20!";

		
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Evidence : MonoBehaviour {
	public bool isVisible = true;
	public Camera detectiveCamera;
	public string evidenceDescription = "Fingerprint of Guy LeMan";

	//I intend to get rid of this
	void Start()
	{
		Activate(GameObject.FindObjectOfType<Camera>());
	}
	//Called when player snaps to UV Camera
	public void Activate(Camera _camera)
	{
		detectiveCamera = _camera;
	}
	//if seen by a camera
	void OnBecameVisible () {
		//check if the camera is staring at the evidence;
		InvokeRepeating("CheckLookDirection", 0f, 0.5f);
	}
	//if no longer seen by any camera
	void OnBecameInvisible()
	{
		CancelInvoke("CheckLookDirection");
	}
	void CheckLookDirection()
	{
		//check angle between
		if (Vector3.Angle(detectiveCamera.transform.forward, transform.position - detectiveCamera.transform.position) < 10f)
		{
			//check to make sure there is nothing in between
			RaycastHit _hit;
			if (Physics.Linecast(detectiveCamera.transform.position, transform.position, out _hit) && _hit.transform == transform)
			{
				if (_hit.distance < 7f)
				{
					//check distance
					Examine();
				}
			}
		}
	}
	void Examine()
	{
		Debug.Log(evidenceDescription);
	}
}

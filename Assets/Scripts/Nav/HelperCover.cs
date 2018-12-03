using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperCover : MonoBehaviour {

	public float direction;
	public float height;
	public Vector3 peak;

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position + new Vector3(0, height, 0), transform.position + Quaternion.Euler(0, direction, 0) * Vector3.forward + new Vector3(0, height, 0));
		Gizmos.DrawSphere(transform.position + peak + new Vector3(0, height, 0), 0.5f);
	}
}

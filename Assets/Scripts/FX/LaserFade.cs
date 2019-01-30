using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFade : MonoBehaviour {

    Renderer _rend;

	// Use this for initialization
	void Start () {
        _rend = GetComponent<Renderer>();
        _rend.material.shader = Shader.Find("Dissolve");
        Debug.Log(_rend.material.shader);
    }
	
	// Update is called once per frame
	void Update () {
        float a = Mathf.PingPong(Time.time, 1f);
        _rend.material.SetFloat("Vector1_55D18DBC", a);
    }
}

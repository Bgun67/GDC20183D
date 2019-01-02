using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thermal_Vision : MonoBehaviour
{
	public Camera mainCamera;
	public Shader thermalObject;
	public Shader thermalShader;
	public float intensity;
	public Material material;
	public Texture2D texture;
	// Use this for initialization
	// Creates a private material used to the effect
	void OnEnable()
	{
		mainCamera.SetReplacementShader(thermalShader, "RenderType");
	}
	void OnDisable()
	{
		mainCamera.ResetReplacementShader();
	}

	// Postprocess the image
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material = new Material(thermalObject);
		Graphics.Blit(source, destination, material);
		material.SetFloat("_amplification", intensity);
		material.SetTexture("_MainTex", texture);
	}
	

}

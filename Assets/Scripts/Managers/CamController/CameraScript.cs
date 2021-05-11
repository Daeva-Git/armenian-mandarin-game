using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraScript : MonoBehaviour
{
	public Camera cam;
    public float x_intensity = 20;
    public float y_intensity = 16;
	public bool perlinNoise = true;
	public float _perlin_intensity = 1;
	public float _perlin_speed = 1;
	Vector3 mousePos;
	private float x_rotation;
	private float y_rotation;
	private int perlinOffset = 0;
	
	// Start is called before the first frame update
	private void Start()
	{
		perlinOffset = UnityEngine.Random.Range(0,10000);
	}

	// Update is called once per frame
	public void updateCamera(){
		mousePos = Input.mousePosition;
		x_rotation = - Mathf.Atan(2*(0.5f - mousePos.x / cam.pixelWidth)) * x_intensity;
		y_rotation = Mathf.Atan(2*(0.5f - mousePos.y / cam.pixelHeight)) * y_intensity;

		if(perlinNoise){
			x_rotation += _perlin_intensity * (Mathf.PerlinNoise(Time.time * _perlin_speed + perlinOffset, perlinOffset) - 0.5f);
			y_rotation += _perlin_intensity * (Mathf.PerlinNoise(perlinOffset, Time.time * _perlin_speed + perlinOffset) - 0.5f);
		}

		cam.transform.localRotation = Quaternion.Euler(y_rotation, x_rotation, 0);
	}
}

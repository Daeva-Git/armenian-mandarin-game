using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Camera cam;
	Vector3 mousePos;
	private float x_rotation;
	private float y_rotation;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void FixedUpdate() {
		mousePos = Input.mousePosition;
		x_rotation = - Mathf.Atan(0.5f - mousePos.x / cam.pixelWidth) * 20;
		y_rotation = Mathf.Atan(0.5f - mousePos.y / cam.pixelHeight) * 15;
		cam.transform.localRotation = Quaternion.Euler(y_rotation, x_rotation, 0);
	}
}

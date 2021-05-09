using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
	public GameObject temp_target;
	public Texture2D default_cursor;
	public Texture2D text_bubble_cursor;
	// Start is called before the first frame update
	void Start()
	{

	}

	Ray ray;
	RaycastHit hit;
	Vector3 dir;
	Vector3 look;
	// Update is called once per frame
	void FixedUpdate()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit)){
			look = (hit.point - transform.position).normalized;
		}

		dir = (temp_target.transform.position - transform.position).normalized;
		float dot = Vector3.Dot(dir, look);
		
		if(dot > 0.9985){
			Cursor.SetCursor(text_bubble_cursor, new Vector2(48, 48), CursorMode.Auto);
		}else{
			Cursor.SetCursor(default_cursor, new Vector2(28, 28), CursorMode.Auto);
		}
	}
}

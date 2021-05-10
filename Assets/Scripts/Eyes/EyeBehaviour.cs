using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBehaviour : MonoBehaviour
{
	public GameObject eye1;
	public GameObject eye2;
	public Transform eyelid_prefab;
	private GameObject eyelid;
	private bool blinking = false;
	private bool hidden = true;
	private float blinkCounter = 0;
	float waitTime = 0;

	Ray ray;
	RaycastHit hit;
	Vector3 dir;
	Vector3 look;
	// Start is called before the first frame update
	void Start()
	{	
		eye1 = transform.GetChild(0).gameObject;
		eye2 = transform.GetChild(1).gameObject;
		eyelid = Instantiate(eyelid_prefab, transform, false).gameObject;
		eyelid.transform.localPosition = new Vector3(0, 0, -0.03f);
		eyelid.transform.rotation = transform.localRotation;
		hide();
		
		waitTime = UnityEngine.Random.Range(10f,30f);
		StartCoroutine(Appear(waitTime));
	}

	// Update is called once per frame
	void Update()
	{
		// if(hidden){
			
			
		// 	hidden = false;
		// 	eyelid.transform.localPosition = new Vector3(0, 0, -0.03f);
		// }else{

		// }
	}

	IEnumerator Appear(float waitTimer){
		while(hidden){
			yield return new WaitForSeconds(waitTimer);
			if(isLooking()){
				StartCoroutine(Appear(UnityEngine.Random.Range(10f,30f)));
				break;
			}
			show();
			hidden = false;
			Debug.Log("Shown");
			StartCoroutine(openEyes());
			StartCoroutine(CheckCollision());
		}
//		StartCoroutine(Blink(UnityEngine.Random.Range(2f,6f)));
	}

	IEnumerator CheckCollision(){
		while(!hidden){
			if(isLooking()){
				hidden = true;
				StartCoroutine(hideEyes());
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator openEyes(){
		blinkCounter = 0;
		while(blinkCounter < 0.07f && !hidden){
			eyelid.transform.localPosition = new Vector3(0, blinkCounter, -0.03f);
			blinkCounter += 0.001f;
			yield return null;
		}
		blinkCounter = 0;
//		StartCoroutine(Appear(UnityEngine.Random.Range(3f,5f)));
	}

	IEnumerator hideEyes(){
		blinkCounter = eyelid.transform.localPosition.y + 0.004f;
		while(blinkCounter > 0){
			eyelid.transform.localPosition = new Vector3(0, blinkCounter, -0.03f);
			blinkCounter -= 0.002f;
			yield return null;
		}
		hide();
		StartCoroutine(Appear(UnityEngine.Random.Range(10f,30f)));
	}

	void hide(){
		eye1.transform.GetComponent<MeshRenderer>().enabled = false;
		eye2.transform.GetComponent<MeshRenderer>().enabled = false;
		eyelid.transform.GetComponent<MeshRenderer>().enabled = false;
	}

	void show(){
		eyelid.transform.localPosition = new Vector3(0, 0, -0.03f);
		blinkCounter = 0;
		eye1.transform.GetComponent<MeshRenderer>().enabled = true;
		eye2.transform.GetComponent<MeshRenderer>().enabled = true;
		eyelid.transform.GetComponent<MeshRenderer>().enabled = true;
	}

	bool isLooking(){
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit)){
			look = (hit.point - Camera.main.transform.position).normalized;
		}
		dir = (transform.position - Camera.main.transform.position).normalized;
		float dot = Vector3.Dot(dir, look);
		if (dot > 0.99){
			return true;
		}
		return false;
	}

}

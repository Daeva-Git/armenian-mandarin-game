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
	public bool finished = true;
	float waitTime = 0;
	private int stare_counter = -1;
	public bool boo = false;

	Ray ray;
	RaycastHit hit;
	Vector3 dir;

	Vector3 look;

	private void Start()
	{
		eye1 = transform.GetChild(0).gameObject;
		eye2 = transform.GetChild(1).gameObject;
		eyelid = Instantiate(eyelid_prefab, transform, false).gameObject;
		eyelid.transform.localPosition = new Vector3(0, 0, -0.03f);
		eyelid.transform.rotation = transform.localRotation;
		hide();

		// waitTime = UnityEngine.Random.Range(10f,30f);
		// StartCoroutine(Appear(waitTime));
	}

	public void AppearGlobal()
	{
		finished = false;
		StartCoroutine(Appear(Random.Range(1, 3f)));
	}

	private IEnumerator Appear(float waitTimer)
	{
		while (hidden && !finished)
		{
			yield return new WaitForSeconds(waitTimer);
			if (isLooking())
			{
				StartCoroutine(Appear(UnityEngine.Random.Range(3, 5f)));
				break;
			}

			show();
			hidden = false;
			
			StartCoroutine(openEyes());
			StartCoroutine(CheckCollision());
		}
		// StartCoroutine(Blink(Random.Range(2f,6f)));
	}

	private IEnumerator CheckCollision()
	{
		while (!hidden)
		{
			if(stare_counter > -1)
				stare_counter++;
			if(stare_counter > 20){
				boo = true;
			}
			if (isLooking() || finished == true)
			{
				boo = false;
				hidden = true;
				StartCoroutine(hideEyes());
				stare_counter = -1;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	private IEnumerator openEyes()
	{
		blinkCounter = 0;
		while (blinkCounter < 0.07f && !hidden)
		{
			eyelid.transform.localPosition = new Vector3(0, blinkCounter, -0.03f);
			blinkCounter += 0.001f;
			yield return null;
		}

		blinkCounter = 0;
		stare_counter = 0;
		//StartCoroutine(Appear(Random.Range(3f,5f)));
	}

	private IEnumerator hideEyes()
	{
		blinkCounter = eyelid.transform.localPosition.y + 0.004f;
		while (blinkCounter > 0)
		{
			eyelid.transform.localPosition = new Vector3(0, blinkCounter, -0.03f);
			blinkCounter -= 0.002f;
			yield return null;
		}

		hide();
		boo = false;
		finished = true;
	}

	void hide()
	{
		eye1.transform.GetComponent<MeshRenderer>().enabled = false;
		eye2.transform.GetComponent<MeshRenderer>().enabled = false;
		eyelid.transform.GetComponent<MeshRenderer>().enabled = false;
	}

	private void show()
	{
		eyelid.transform.localPosition = new Vector3(0, 0, -0.03f);
		blinkCounter = 0;
		eye1.transform.GetComponent<MeshRenderer>().enabled = true;
		eye2.transform.GetComponent<MeshRenderer>().enabled = true;
		eyelid.transform.GetComponent<MeshRenderer>().enabled = true;
	}

	private bool isLooking()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			look = (hit.point - Camera.main.transform.position).normalized;
		}

		dir = (transform.position - Camera.main.transform.position).normalized;
		float dot = Vector3.Dot(dir, look);
		if (dot > 0.99)
		{
			return true;
		}

		return false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
	public EyeBehaviour[] rats =  new EyeBehaviour[30];
	private bool doShowRats = false;
	private bool wrking = false;
	private float frequency;
	private int globalCounter = 0;
	// Start is called before the first frame update
	void Start()
	{
	
	}

	// Update is called once per frame
	void Update()
	{
		if(globalCounter > frequency){
			showRat();
			globalCounter = 0;
		}
		if(doShowRats){
			globalCounter++;
		}
		// if(doShowRats && !wrking){
		// 	wrking = true;
		// 	StartCoroutine(showOneRat(frequency));
		// }
	}

	public void showRat(){
		int counter = 0;
		int i = 0;
		while(true && counter < 30){
			counter++;
			i = UnityEngine.Random.Range(0,10);
			if(rats[i].finished == true){
				rats[i].AppearGlobal();
				break;
			}
		}
	}

	public void showRats(float freq){
		frequency = freq * 100;
		doShowRats = true;
//		StartCoroutine(showOneRat(frequency));
	}

	// IEnumerator showOneRat(int frequency){
	// 	while(doShowRats){
	// 		yield return new WaitForSeconds(frequency);
	// 		showRat();
	// 		Debug.Log("Wrking false");
	// 		wrking = false;
	// 		break;
	// 	}
	// }

	public void hideRats(){
		foreach(EyeBehaviour rat in rats){
			rat.finished = true;
		}
	}
}

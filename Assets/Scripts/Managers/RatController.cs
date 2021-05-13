using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RatController : MonoBehaviour
{
	[SerializeField] private List<EyeBehaviour> ratsCollection;

	public bool ScarePlayer => _scarePlayer;
	
	private bool _doShowRats;
	private float _frequency;
	private int _globalCounter;
	private bool _scarePlayer;


	private void Update()
	{
		if (_globalCounter > _frequency)
		{
			ShowRat();
			_globalCounter = 0;
		}

		if (_doShowRats)
		{
			_globalCounter++;
		}

		foreach (var rat in ratsCollection)
		{
			if (rat.scarePlayer)
			{
				_scarePlayer = true;
				break;
			}

			_scarePlayer = false;
		}
	}

	public void ShowRat()
	{
		var counter = 0;
		var ratsCount = ratsCollection.Capacity;
		while (counter < 30)
		{
			counter++;
			var i = Random.Range(0, ratsCount - 1);
			if (ratsCollection[i].finished)
			{
				ratsCollection[i].AppearGlobal();
				break;
			}
		}
	}

	public void ShowRats(float freq)
	{
		_frequency = freq * 100;
		_doShowRats = true;
	}

	public void HideRats()
	{
		foreach (var rat in ratsCollection)
		{
			rat.finished = true;
		}
	}
}

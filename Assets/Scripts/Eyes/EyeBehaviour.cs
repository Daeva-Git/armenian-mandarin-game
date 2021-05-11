using System.Collections;
using UnityEngine;

public class EyeBehaviour : MonoBehaviour
{
	public GameObject eye1;
	public GameObject eye2;
	public Transform eyelidTransform;
	public bool finished = true;
	
	private GameObject _eyelidPrefab;
	private bool _hidden = true;
	private float _blinkCounter;
	private int _stareCounter = -1;
	public bool scarePlayer;
	
	private Ray _ray;
	private RaycastHit _raycastHit;
	private Vector3 _direction;
	private Vector3 _lookingDirection;

	private void Start()
	{
		eye1 = transform.GetChild(0).gameObject;
		eye2 = transform.GetChild(1).gameObject;
		_eyelidPrefab = Instantiate(eyelidTransform, transform, false).gameObject;
		_eyelidPrefab.transform.localPosition = new Vector3(0, 0, -0.03f);
		_eyelidPrefab.transform.rotation = transform.localRotation;
		
		Hide();
	}

	public void AppearGlobal()
	{
		finished = false;
		StartCoroutine(Appear(Random.Range(1, 3f)));
	}

	private IEnumerator Appear(float waitTimer)
	{
		while (_hidden && !finished)
		{
			yield return new WaitForSeconds(waitTimer);
			if (IsLooking())
			{
				StartCoroutine(Appear(Random.Range(3, 5f)));
				break;
			}

			Show();
			_hidden = false;

			StartCoroutine(OpenEyes());
			StartCoroutine(CheckCollision());
		}
	}

	private IEnumerator CheckCollision()
	{
		while (!_hidden)
		{
			if (_stareCounter > -1)
			{
				_stareCounter++;
			}
			if (_stareCounter > 20)
			{
				scarePlayer = true;
			}

			if (IsLooking() || finished)
			{
				scarePlayer = false;
				_hidden = true;
				StartCoroutine(HideEyes());
				_stareCounter = -1;
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	private IEnumerator OpenEyes()
	{
		_blinkCounter = 0;
		while (_blinkCounter < 0.07f && !_hidden)
		{
			_eyelidPrefab.transform.localPosition = new Vector3(0, _blinkCounter, -0.03f);
			_blinkCounter += 0.001f;
			yield return null;
		}

		_blinkCounter = 0;
		_stareCounter = 0;
	}

	private IEnumerator HideEyes()
	{
		_blinkCounter = _eyelidPrefab.transform.localPosition.y + 0.004f;
		while (_blinkCounter > 0)
		{
			_eyelidPrefab.transform.localPosition = new Vector3(0, _blinkCounter, -0.03f);
			_blinkCounter -= 0.002f;
			yield return null;
		}

		Hide();
		scarePlayer = false;
		finished = true;
	}

	private void Hide()
	{
		eye1.SetActive(false);
		eye2.SetActive(false);
		_eyelidPrefab.SetActive(false);
	}

	private void Show()
	{
		_eyelidPrefab.transform.localPosition = new Vector3(0, 0, -0.03f);
		_blinkCounter = 0;
		
		eye1.SetActive(true);
		eye2.SetActive(true);
		_eyelidPrefab.SetActive(true);
	}

	private bool IsLooking()
	{
		var mainCamera = GameManager.Instance.ComponentManager.MainCamera;
		_ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(_ray, out _raycastHit))
		{
			_lookingDirection = (_raycastHit.point - mainCamera.transform.position).normalized;
		}

		_direction = (transform.position - mainCamera.transform.position).normalized;
		var dot = Vector3.Dot(_direction, _lookingDirection);

		return dot > 0.99;
	}
}

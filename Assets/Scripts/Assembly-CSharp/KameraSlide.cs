using UnityEngine;

public class KameraSlide : MonoBehaviour
{
	private float Sol;

	private float Sag;

	private float KameraY;

	private Vector3 baslangic = Vector3.zero;

	private Vector3 sonkonum = Vector3.zero;

	private Vector3 kamera = Vector3.zero;

	private bool LerpHappen;

	private Vector3 Lerp3;

	private void Start()
	{
		Invoke("EnSagaGit", 0.01f);
		Sol = GameObject.Find("SolSinir").transform.position.x;
		Sag = GameObject.Find("SagSinir").transform.position.x;
	}

	private void EnSagaGit()
	{
		int @int = PlayerPrefs.GetInt("GeldiimLevel");
		if (@int > 1)
		{
			string text = @int.ToString();
			GameObject gameObject = GameObject.Find("level_" + text);
			if (gameObject != null)
			{
				Vector3 position = base.transform.position;
				position.x = gameObject.transform.position.x;
				base.transform.position = position;
			}
		}
	}

	private void Update()
	{
		if (Input.touchCount > 0)
		{
			TouchSurukle();
		}
		else
		{
			MouseSurukle();
		}
		if (LerpHappen)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, Lerp3, Time.deltaTime * 3f);
		}
	}

	private void TouchSurukle()
	{
		if (Input.GetTouch(0).phase == TouchPhase.Began)
		{
			baslangic = Input.GetTouch(0).position;
			kamera = base.transform.position;
		}
		if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
		{
			sonkonum = Input.GetTouch(0).position;
			sonkonum.z = (baslangic.z = kamera.z);
			Vector3 vector = Camera.main.ScreenToWorldPoint(sonkonum) - Camera.main.ScreenToWorldPoint(baslangic);
			vector *= -1f;
			Vector3 position = kamera + vector;
			position.y = 0f;
			position.z = -10f;
			if (Sol < position.x && position.x > Sag)
			{
				base.transform.position = position;
			}
		}
		if (Input.GetTouch(0).phase != TouchPhase.Ended)
		{
			return;
		}
		sonkonum = Input.GetTouch(0).position;
		Vector3 vector2 = Camera.main.ScreenToWorldPoint(sonkonum) - Camera.main.ScreenToWorldPoint(baslangic);
		if (Mathf.Abs(vector2.x) > 2f)
		{
			vector2.x = (0f - vector2.x) * 1.5f;
			Vector3 lerp = kamera + vector2;
			lerp.y = 0f;
			lerp.z = -10f;
			if (Sol < lerp.x && lerp.x < Sag)
			{
				LerpHappen = true;
				Lerp3 = lerp;
			}
		}
	}

	private void MouseSurukle()
	{
		if (Input.GetMouseButtonDown(0))
		{
			baslangic = Input.mousePosition;
			kamera = base.transform.position;
			LerpHappen = false;
		}
		if (Input.GetMouseButton(0))
		{
			sonkonum = Input.mousePosition;
			sonkonum.z = (baslangic.z = kamera.z);
			Vector3 vector = Camera.main.ScreenToWorldPoint(sonkonum) - Camera.main.ScreenToWorldPoint(baslangic);
			vector *= -1f;
			Vector3 position = kamera + vector;
			position.y = 0f;
			position.z = -10f;
			if (Sol < position.x && position.x < Sag)
			{
				base.transform.position = position;
			}
		}
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
		sonkonum = Input.mousePosition;
		Vector3 vector2 = Camera.main.ScreenToWorldPoint(sonkonum) - Camera.main.ScreenToWorldPoint(baslangic);
		if (Mathf.Abs(vector2.x) > 2f)
		{
			vector2.x = (0f - vector2.x) * 1.5f;
			Vector3 lerp = kamera + vector2;
			lerp.y = 0f;
			lerp.z = -10f;
			if (Sol < lerp.x && lerp.x < Sag)
			{
				LerpHappen = true;
				Lerp3 = lerp;
			}
		}
	}
}

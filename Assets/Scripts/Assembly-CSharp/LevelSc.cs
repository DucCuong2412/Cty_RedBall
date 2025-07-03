using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSc : MonoBehaviour
{
	private int DokunmaID = -1;

	private Vector3 wp3;

	private Vector2 wp2;

	private float Orjinalboy = 0.9f;

	private bool MouseDn;

	private bool Kilitli = true;

	private GameObject levelkilit;

	private GameObject FontSprt;

	private Vector3 ilkKonum3;

	public void MetinYaz(string metn)
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name == "SpriteFont")
				{
					FontSprt = transform.gameObject;
					FontSprites component = FontSprt.GetComponent<FontSprites>();
					component.MetinDegis(metn);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
	}

	private void Start()
	{
		string text = base.name.Replace("_", string.Empty);
		int @int = PlayerPrefs.GetInt(text + "rb2star");
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		GameObject gameObject3 = null;
		GameObject gameObject4 = null;
		GameObject gameObject5 = null;
		GameObject gameObject6 = null;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name == "LevelKilit")
				{
					levelkilit = transform.gameObject;
				}
				if (transform.name == "YildizOk_1")
				{
					gameObject = transform.gameObject;
				}
				if (transform.name == "YildizOk_2")
				{
					gameObject2 = transform.gameObject;
				}
				if (transform.name == "YildizOk_3")
				{
					gameObject3 = transform.gameObject;
				}
				if (transform.name == "YildizNo_1")
				{
					gameObject4 = transform.gameObject;
				}
				if (transform.name == "YildizNo_2")
				{
					gameObject5 = transform.gameObject;
				}
				if (transform.name == "YildizNo_3")
				{
					gameObject6 = transform.gameObject;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
		if (gameObject != null && gameObject2 != null && gameObject3 != null && gameObject4 != null && gameObject5 != null && gameObject6 != null)
		{
			switch (@int)
			{
			case 3:
				gameObject4.SetActive(false);
				gameObject5.SetActive(false);
				gameObject6.SetActive(false);
				break;
			case 2:
				gameObject3.SetActive(false);
				gameObject4.SetActive(false);
				gameObject5.SetActive(false);
				break;
			case 1:
				gameObject4.SetActive(false);
				gameObject2.SetActive(false);
				gameObject3.SetActive(false);
				break;
			default:
				gameObject.SetActive(false);
				gameObject2.SetActive(false);
				gameObject3.SetActive(false);
				break;
			}
		}
		DokunmaID = -1;
		Orjinalboy = base.transform.localScale.x;
		int int2 = PlayerPrefs.GetInt("sonLevelRB2");
		text = text.Replace("level", string.Empty);
		int2++;
		int num = int.Parse(text);
		if (num <= int2)
		{
			KilitAc();
		}
	}

	public void KilitAc()
	{
		Kilitli = false;
		levelkilit.SetActive(false);
	}

	private void SilikDegil(GameObject go)
	{
		SpriteRenderer component = go.GetComponent<SpriteRenderer>();
		Color white = Color.white;
		white.a = 1f;
		component.color = white;
	}

	private void SilikYap(GameObject go)
	{
		SpriteRenderer component = go.GetComponent<SpriteRenderer>();
		Color grey = Color.grey;
		grey.a = 0.5f;
		component.color = grey;
	}

	private void Update()
	{
		if (Kilitli || !(base.gameObject.GetComponent<Collider2D>() != null))
		{
			return;
		}
		if (Input.touchCount > 0)
		{
			wp3 = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			wp2 = new Vector2(wp3.x, wp3.y);
			if (base.gameObject.GetComponent<Collider2D>().OverlapPoint(wp2))
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					DokunmaID = Input.GetTouch(0).fingerId;
					Basladi(Input.GetTouch(0).position);
				}
				if (Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					DokunmaID = Input.GetTouch(0).fingerId;
					Surtundu();
				}
			}
			else if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).fingerId == DokunmaID)
			{
				UzakSurtundu();
				DokunmaID = -1;
			}
			if (Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(0).fingerId == DokunmaID)
			{
				Bitti(Input.GetTouch(0).position);
				DokunmaID = -1;
			}
		}
		else
		{
			wp3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			wp2 = new Vector2(wp3.x, wp3.y);
			if (Input.GetMouseButtonDown(0) && base.gameObject.GetComponent<Collider2D>().OverlapPoint(wp2))
			{
				MouseDn = true;
				Basladi(Input.mousePosition);
			}
			if (MouseDn && Input.GetMouseButtonUp(0) && base.gameObject.GetComponent<Collider2D>().OverlapPoint(wp2))
			{
				MouseDn = false;
				Bitti(Input.mousePosition);
			}
			if (MouseDn && !base.gameObject.GetComponent<Collider2D>().OverlapPoint(wp2))
			{
				MouseDn = false;
				UzakSurtundu();
			}
		}
	}

	private void Buyut()
	{
		float num = Orjinalboy * 1.1f;
		Vector3 localScale = new Vector3(num, num, num);
		base.gameObject.transform.localScale = localScale;
	}

	private void Kucult()
	{
		Vector3 localScale = new Vector3(Orjinalboy, Orjinalboy, Orjinalboy);
		base.gameObject.transform.localScale = localScale;
	}

	public void Basladi(Vector3 konum)
	{
		ilkKonum3 = konum;
		Buyut();
	}

	public void Surtundu()
	{
		Buyut();
	}

	public void UzakSurtundu()
	{
		Kucult();
	}

	public void Bitti(Vector3 konum)
	{
		Kucult();
		if (!Kilitli)
		{
			Vector3 vector = Camera.main.ScreenToWorldPoint(ilkKonum3);
			Vector3 vector2 = Camera.main.ScreenToWorldPoint(konum);
			float num = Mathf.Abs(vector.x - vector2.x);
			if (num < 0.5f)
			{
				string s = base.name.Replace("level_", string.Empty);
				SceneManager.LoadScene("Level_" + int.Parse(s));
			}
		}
	}
}

using System.Collections.Generic;
using UnityEngine;

public class KirBlokTugla : MonoBehaviour
{
	private List<GameObject> Parcalar;

	private List<GameObject> Tozlar;

	private List<GameObject> Kiriklar;

	private int tozSay;

	private int parcaSay;

	private int KirikSay;

	protected GameObject yamuk;

	private bool Vuruldu;

	private int VurusSay;

	private bool HerseyBitti;

	public float KirilmaHizi = 4.5f;

	private void Start()
	{
		Parcalar = new List<GameObject>();
		Tozlar = new List<GameObject>();
		Kiriklar = new List<GameObject>();
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name.Contains("toz_"))
			{
				Tozlar.Add(gameObject);
				tozSay++;
			}
			if (gameObject.name.Contains("parca_"))
			{
				Parcalar.Add(gameObject);
				parcaSay++;
			}
			if (gameObject.name.Contains("kirik_"))
			{
				Kiriklar.Add(gameObject);
				KirikSay++;
			}
			if (gameObject.name.Contains("yamuk"))
			{
				yamuk = gameObject;
			}
		}
		for (int i = 0; i < tozSay; i++)
		{
			Tozlar[i].SetActive(false);
		}
		for (int j = 0; j < parcaSay; j++)
		{
			Parcalar[j].SetActive(false);
		}
		for (int k = 0; k < KirikSay; k++)
		{
			Kiriklar[k].SetActive(false);
		}
		if (yamuk != null)
		{
			yamuk.SetActive(false);
		}
	}

	protected void TozCikar()
	{
		GameObject gameObject = Object.Instantiate(Resources.Load("TozluP", typeof(GameObject))) as GameObject;
		gameObject.transform.position = base.transform.position;
	}

	private void ParcaDokB(Vector3 BombaYeri)
	{
		if (parcaSay != 0)
		{
			HerseyGizle();
			Vector2 vector = BombaYeri;
			for (int i = 0; i < parcaSay; i++)
			{
				Parcalar[i].SetActive(true);
				Parcalar[i].GetComponent<SpriteRenderer>().sortingOrder = 8;
				Rigidbody2D component = Parcalar[i].GetComponent<Rigidbody2D>();
				component.isKinematic = false;
				float num = Vector2.Angle(vector, Parcalar[i].transform.localPosition);
				component.AddForce(RotateVector2d(vector, num + 90f) * 10f);
			}
			TozCikar();
			Invoke("ParcaGizle", 1f);
			HerseyBitti = true;
		}
	}

	private void TozDokB(Vector3 BombaYeri)
	{
		if (tozSay == 0)
		{
			return;
		}
		Vector2 vector = BombaYeri;
		for (int i = 0; i < tozSay; i++)
		{
			Tozlar[i].SetActive(true);
			Tozlar[i].GetComponent<SpriteRenderer>().sortingOrder = 9;
			Rigidbody2D component = Tozlar[i].GetComponent<Rigidbody2D>();
			component.isKinematic = false;
			float num = Vector2.Angle(vector, Tozlar[i].transform.localPosition);
			if (BombaYeri.x > base.transform.position.x)
			{
				component.AddForce(RotateVector2d(vector, num + 90f) * 30f);
			}
			else
			{
				component.AddForce(-RotateVector2d(vector, num + 90f) * 30f);
			}
		}
		Invoke("TozlarGizle", 1f);
		TozCikar();
	}

	public void BombaPatladi(Vector3 BombaYeri)
	{
		VurusSay++;
		if (VurusSay == 1)
		{
			if (tozSay > 0)
			{
				TozDokB(BombaYeri);
				ParcaDokB(BombaYeri);
				return;
			}
			if (yamuk != null)
			{
				yamuk.GetComponent<SpriteRenderer>().sortingOrder = 7;
			}
			else if (KirikSay > 0)
			{
				KirikGoster();
			}
			else
			{
				ParcaDokB(BombaYeri);
				HerseyBitti = true;
			}
		}
		if (VurusSay == 2)
		{
			ParcaDokB(BombaYeri);
		}
		TozCikar();
	}

	private void OnCollisionEnter2D(Collision2D kim)
	{
		if (Vuruldu || HerseyBitti || (!(kim.gameObject.tag == "hareketli") && !(kim.gameObject.tag == "Player")) || (!(Mathf.Abs(kim.relativeVelocity.x) > KirilmaHizi) && !(Mathf.Abs(kim.relativeVelocity.y) > KirilmaHizi)))
		{
			return;
		}
		Vuruldu = true;
		Invoke("AgriGecti", 0.2f);
		VurusSay++;
		if (VurusSay == 1)
		{
			if (tozSay > 0)
			{
				TozDok(kim);
			}
			if (yamuk != null)
			{
				YamukGoster();
			}
			else if (KirikSay > 0)
			{
				KirikGoster();
			}
			else
			{
				ParcaDok(kim);
				HerseyBitti = true;
			}
		}
		if (VurusSay == 2)
		{
			ParcaDok(kim);
		}
	}

	private void TozDok(Collision2D kim)
	{
		//Discarded unreachable code: IL_015e
		if (tozSay == 0)
		{
			return;
		}
		ContactPoint2D[] contacts = kim.contacts;
		int num = 0;
		if (num < contacts.Length)
		{
			ContactPoint2D contactPoint2D = contacts[num];
			Vector2 point = contactPoint2D.point;
			for (int i = 0; i < tozSay; i++)
			{
				Tozlar[i].SetActive(true);
				Tozlar[i].GetComponent<SpriteRenderer>().sortingOrder = 6;
				Rigidbody2D component = Tozlar[i].GetComponent<Rigidbody2D>();
				component.isKinematic = false;
				float num2 = Vector2.Angle(point, Tozlar[i].transform.localPosition);
				if (kim.gameObject.transform.position.x > base.transform.position.x)
				{
					component.AddForce(RotateVector2d(point, num2 + 90f) * 10f);
				}
				else
				{
					component.AddForce(-RotateVector2d(point, num2 + 90f) * 10f);
				}
			}
			Vector2 velocity = kim.relativeVelocity.normalized * 2f;
			kim.rigidbody.velocity = velocity;
		}
		Invoke("TozlarGizle", 1f);
		TozCikar();
	}

	private void KirikGoster()
	{
		for (int i = 0; i < KirikSay; i++)
		{
			Kiriklar[i].SetActive(true);
			Kiriklar[i].GetComponent<SpriteRenderer>().sortingOrder = 7;
		}
		TozCikar();
	}

	protected virtual void YamukGoster()
	{
		yamuk.GetComponent<SpriteRenderer>().sortingOrder = 7;
		GetComponent<SpriteRenderer>().enabled = false;
		yamuk.SetActive(true);
		TozCikar();
	}

	private void HerseyGizle()
	{
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
		if (KirikSay > 0)
		{
			for (int i = 0; i < KirikSay; i++)
			{
				Kiriklar[i].SetActive(false);
			}
		}
		if (yamuk != null)
		{
			yamuk.SetActive(false);
		}
	}

	private void ParcaDok(Collision2D kim)
	{
		//Discarded unreachable code: IL_01ba
		CameraFollow component = Camera.main.GetComponent<CameraFollow>();
		if (base.name.Contains("tahta"))
		{
			component.TahtaSesi();
		}
		else if (base.name.Contains("tugla"))
		{
			component.TuglaSesi();
		}
		else if (base.name.Contains("cam"))
		{
			component.CamSesi();
		}
		else if (base.name.Contains("buz"))
		{
			component.CamSesi();
		}
		if (parcaSay == 0)
		{
			return;
		}
		HerseyGizle();
		ContactPoint2D[] contacts = kim.contacts;
		int num = 0;
		if (num < contacts.Length)
		{
			ContactPoint2D contactPoint2D = contacts[num];
			Vector2 point = contactPoint2D.point;
			for (int i = 0; i < parcaSay; i++)
			{
				Parcalar[i].SetActive(true);
				Parcalar[i].GetComponent<SpriteRenderer>().sortingOrder = 7;
				Rigidbody2D component2 = Parcalar[i].GetComponent<Rigidbody2D>();
				component2.isKinematic = false;
				float num2 = Vector2.Angle(point, Parcalar[i].transform.localPosition);
				if (base.name.Contains("buz"))
				{
					component2.AddForce(RotateVector2d(point, num2));
				}
				else
				{
					component2.AddForce(RotateVector2d(point, num2 + 90f) * 10f);
				}
			}
			Vector2 velocity = kim.relativeVelocity.normalized * 3f;
			kim.rigidbody.velocity = velocity;
		}
		Invoke("ParcaGizle", 1f);
		TozCikar();
		HerseyBitti = true;
		KirSeconIS component3 = GetComponent<KirSeconIS>();
		if ((bool)component3)
		{
			component3.Kirildik();
		}
	}

	public Vector2 RotateVector2d(Vector2 v2d, float derece)
	{
		return Quaternion.Euler(0f, 0f, derece) * v2d;
	}

	private void ParcaGizle()
	{
		for (int i = 0; i < parcaSay; i++)
		{
			Parcalar[i].SetActive(false);
		}
		base.gameObject.SetActive(false);
	}

	private void TozlarGizle()
	{
		for (int i = 0; i < tozSay; i++)
		{
			Tozlar[i].SetActive(false);
		}
	}

	private void AgriGecti()
	{
		Vuruldu = false;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class AsansorTakipli : MonoBehaviour
{
	public float Hiz = 10f;

	public bool HedefeBak;

	private int MevcutNokta;

	private bool RigVar;

	public List<Vector2> Noktalar = new List<Vector2>();

	private Transform yavru;

	private bool gotur;

	private void Start()
	{
		if (GetComponent<Rigidbody2D>() != null)
		{
			RigVar = true;
		}
		if (!RigVar)
		{
			Debug.Log(">>> RigidBody2D gerekiyor ! <<<");
			return;
		}
		base.transform.position = Noktalar[0];
		GazVer();
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "isaret")
			{
				yavru = gameObject.transform;
			}
		}
	}

	private int SonrakiNoktaBul()
	{
		if (MevcutNokta >= Noktalar.Count - 1)
		{
			return 0;
		}
		return MevcutNokta + 1;
	}

	private void GazVer()
	{
		int index = SonrakiNoktaBul();
		Vector2 vector = Noktalar[MevcutNokta];
		Vector2 vector2 = Noktalar[index];
		Vector2 vector3 = vector2 - vector;
		if (HedefeBak)
		{
			Cevir();
		}
		GetComponent<Rigidbody2D>().velocity = vector3.normalized * Hiz * 0.1f;
	}

	private void FixedUpdate()
	{
		float num = 1f;
		Vector3 a = new Vector3(-0.6585333f, -0.008960247f, 0f);
		Vector3 b = new Vector3(0.6807553f, -0.008960247f, 0f);
		yavru.localPosition = Vector3.Lerp(a, b, Mathf.PingPong(Time.time * num, 1f));
		if (RigVar && Vector2.Distance(base.transform.position, Noktalar[SonrakiNoktaBul()]) < 0.1f)
		{
			MevcutNokta++;
			if (MevcutNokta >= Noktalar.Count)
			{
				MevcutNokta = 0;
			}
			GazVer();
		}
	}

	public void DortNoktaVer()
	{
		if (Noktalar.Count < 1)
		{
			Vector3 position = base.transform.position;
			Noktalar.Add(new Vector2(position.x - 1f, position.y + 1f));
			Noktalar.Add(new Vector2(position.x + 1f, position.y + 1f));
			Noktalar.Add(new Vector2(position.x + 1f, position.y - 1f));
			Noktalar.Add(new Vector2(position.x - 1f, position.y - 1f));
		}
	}

	private void Cevir()
	{
		Vector3 vector = Noktalar[MevcutNokta];
		Vector3 vector2 = Noktalar[SonrakiNoktaBul()];
		Vector3 vector3 = vector2 - vector;
		float x = vector3.x;
		float y = vector3.y;
		float angle = Mathf.Atan2(y, x) * 180f / (float)Math.PI;
		base.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	public void NoktaEkle()
	{
		float num = 1f + (float)UnityEngine.Random.Range(1, 10) * 0.1f;
		Noktalar.Add(new Vector2(base.transform.position.x + num, base.transform.position.y + num));
	}

	public void sonNoktaSil()
	{
		Noktalar.RemoveAt(Noktalar.Count - 1);
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!gotur && kim.tag == "Player")
		{
			kim.gameObject.transform.parent = base.transform;
			kim.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			gotur = true;
		}
	}

	private void OnTriggerExit2D(Collider2D kim)
	{
		if (gotur && kim.tag == "Player")
		{
			kim.transform.parent = null;
			gotur = false;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UzayliKorumali : Pooler
{
	public float Hiz = 10f;

	public int AtmaGucu = 300;

	public bool Aktif;

	private int MevcutNokta;

	private GameObject SapkaGo;

	private bool Dead;

	private int SapkaGuc = 5;

	private Transform PlayrTrans;

	private float ActiveMesafesi = 4f;

	public List<Vector2> Noktalar = new List<Vector2>();

	private GameObject YedekoOg;

	private Uzayli YanSc;

	private GameObject PortalInX;

	public void HayatSon()
	{
		PortalInX.SetActive(true);
		Dead = true;
		Aktif = false;
	}

	private void Start()
	{
		YanSc = GetComponent<Uzayli>();
		base.transform.position = Noktalar[0];
		GazVer();
	}

	private void Awake()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name.Contains("DikenliBomb"))
				{
					PoolerGo = transform.gameObject;
				}
				if (transform.name.Contains("UzaylZirhi"))
				{
					SapkaGo = transform.gameObject;
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
		PoolerAwake();
		InvokeRepeating("Firlat", 3f, 3f);
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			PlayrTrans = gameObject.transform;
		}
		PortalInX = GameObject.Find("portalLittleIN");
		if ((bool)PortalInX)
		{
			PortalInX.SetActive(false);
		}
		YedekoOg = GameObject.Find("Uzayli1NinjaliX");
		YedekoOg.SetActive(false);
	}

	public void KorumaBak()
	{
		if (SapkaGuc >= 0)
		{
			SpriteRenderer component = SapkaGo.GetComponent<SpriteRenderer>();
			if (SapkaGuc == 5)
			{
				component.color = Color.yellow;
			}
			if (SapkaGuc == 4)
			{
				component.color = Color.blue;
			}
			if (SapkaGuc == 3)
			{
				component.color = Color.green;
			}
			if (SapkaGuc == 2)
			{
				component.color = Color.red;
			}
			if (SapkaGuc == 1)
			{
				SapkaGo.transform.parent = null;
				Rigidbody2D rigidbody2D = SapkaGo.AddComponent<Rigidbody2D>();
				rigidbody2D.AddTorque(10f);
				YedekoOg.SetActive(true);
				Vector3 position = base.transform.position;
				position.y -= 1f;
				YedekoOg.transform.position = position;
			}
			SapkaGuc--;
		}
	}

	private void Firlat()
	{
		if (Dead || YanSc.Hayat == 0 || !Aktif || Dead)
		{
			return;
		}
		GameObject gameObject = DepodanAl();
		if (!(gameObject == null))
		{
			gameObject.transform.position = base.transform.position;
			gameObject.SetActive(true);
			Vector2 vector = PlayrTrans.position - base.transform.position;
			gameObject.GetComponent<Rigidbody2D>().AddForce(vector.normalized * AtmaGucu);
			if (PlayrTrans.position.x < base.transform.position.x)
			{
				gameObject.GetComponent<Rigidbody2D>().AddTorque(3f);
			}
			else
			{
				gameObject.GetComponent<Rigidbody2D>().AddTorque(-3f);
			}
			gameObject.AddComponent<GizlenenBomba>();
		}
	}

	private void FixedUpdate()
	{
		if (Dead)
		{
			return;
		}
		if (!Aktif && YanSc.Hayat > 0)
		{
			float num = Vector3.Distance(PlayrTrans.position, base.transform.position);
			if (num < ActiveMesafesi)
			{
				Aktif = true;
			}
		}
		if (Vector2.Distance(base.transform.position, Noktalar[SonrakiNoktaBul()]) < 0.1f)
		{
			MevcutNokta++;
			if (MevcutNokta >= Noktalar.Count)
			{
				MevcutNokta = 0;
			}
			GazVer();
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
		GetComponent<Rigidbody2D>().velocity = vector3.normalized * Hiz * 0.1f;
	}
}

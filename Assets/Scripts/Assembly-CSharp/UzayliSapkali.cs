using System;
using System.Collections;
using UnityEngine;

public class UzayliSapkali : Pooler
{
	private float moveForce = 1f;

	private float jumpForce = 4f;

	private float maxSpeed = 4f;

	private Sprite[] sapkaS;

	private GameObject SapkaGo;

	private GameObject DikenGo;

	private int SapkaGuc = 3;

	private bool Aktif;

	private bool grounded;

	private Transform PlayrTrans;

	private float ActiveMesafesi = 4f;

	public int AtmaGucu = 100;

	public bool SagaGit;

	private bool Dead;

	public void HayatSon()
	{
		Dead = true;
	}

	public void BigBombaX()
	{
		SapkaGuc = 1;
		SapkaBak();
		Aktif = false;
		Dead = true;
	}

	private void Awake()
	{
		sapkaS = new Sprite[2];
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name.Contains("UzayliSapka"))
				{
					if (transform.name.Contains("Sapka1"))
					{
						SapkaGo = transform.gameObject;
					}
					if (transform.name.Contains("Sapka2"))
					{
						sapkaS[0] = transform.gameObject.GetComponent<SpriteRenderer>().sprite;
						transform.gameObject.SetActive(false);
					}
					if (transform.name.Contains("Sapka3"))
					{
						sapkaS[1] = transform.gameObject.GetComponent<SpriteRenderer>().sprite;
						transform.gameObject.SetActive(false);
					}
				}
				if (transform.name.Contains("DikenliBomb2"))
				{
					DikenGo = transform.gameObject;
					PoolerGo = DikenGo;
					DikenGo.SetActive(false);
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
	}

	private void Firlat()
	{
		if (Aktif)
		{
			GameObject gameObject = DepodanAl();
			if (!(gameObject == null))
			{
				gameObject.transform.position = base.transform.position;
				gameObject.SetActive(true);
				Vector2 vector = PlayrTrans.position - base.transform.position;
				gameObject.GetComponent<Rigidbody2D>().AddForce(vector.normalized * AtmaGucu);
				gameObject.AddComponent<GizlenenBomba>();
			}
		}
	}

	public void SapkaBak()
	{
		if (SapkaGuc >= 0)
		{
			SpriteRenderer component = SapkaGo.GetComponent<SpriteRenderer>();
			if (SapkaGuc == 3)
			{
				component.sprite = sapkaS[0];
			}
			if (SapkaGuc == 2)
			{
				component.sprite = sapkaS[1];
			}
			if (SapkaGuc == 1)
			{
				SapkaGo.transform.parent = null;
				Rigidbody2D rigidbody2D = SapkaGo.AddComponent<Rigidbody2D>();
				rigidbody2D.AddTorque(10f);
			}
			SapkaGuc--;
		}
	}

	private void FixedUpdate()
	{
		if (Dead)
		{
			return;
		}
		if (!Aktif)
		{
			float num = Vector3.Distance(PlayrTrans.position, base.transform.position);
			if (num < ActiveMesafesi)
			{
				Aktif = true;
			}
		}
		if (!Aktif)
		{
			return;
		}
		if (PlayrTrans.position.x < base.transform.position.x)
		{
			SagaGit = false;
		}
		else
		{
			SagaGit = true;
		}
		if (grounded)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
			grounded = false;
		}
		Vector2 zero = Vector2.zero;
		if (SagaGit)
		{
			if (grounded)
			{
				zero.x += moveForce;
			}
			else
			{
				zero.x += moveForce * 0.3f;
			}
			if (GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			{
				GetComponent<Rigidbody2D>().AddForce(zero);
			}
		}
		else
		{
			if (grounded)
			{
				zero.x -= moveForce;
			}
			else
			{
				zero.x -= moveForce * 0.3f;
			}
			if (GetComponent<Rigidbody2D>().velocity.x > 0f - maxSpeed)
			{
				GetComponent<Rigidbody2D>().AddForce(zero);
			}
		}
	}

	private void Update()
	{
		if (!Dead)
		{
			Vector2 point = base.transform.position;
			point.y -= 0.5f;
			grounded = Physics2D.OverlapPoint(point, 1 << LayerMask.NameToLayer("zemin"));
		}
	}
}

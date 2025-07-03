using System;
using System.Collections;
using UnityEngine;

public class UzayliZiplayan : Pooler
{
	public float moveForce = 1f;

	public float jumpForce = 4f;

	public float maxSpeed = 4f;

	public float BombaMesafesi = 3f;

	private bool Aktif;

	private bool grounded;

	private Transform PlayrTrans;

	private float ActiveMesafesi = 4f;

	private bool SagaGit;

	private bool Dead;

	private Uzayli YanSc;

	public void HayatSon()
	{
		Dead = true;
	}

	private void Awake()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			PlayrTrans = gameObject.transform;
		}
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name.Contains("DikenliBomb3"))
				{
					PoolerGo = transform.gameObject;
					PoolerGo.SetActive(false);
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
		InvokeRepeating("Firlat", 1f, 1f);
		YanSc = GetComponent<Uzayli>();
	}

	private void Firlat()
	{
		if (Dead || !Aktif || !(PlayrTrans.position.y < base.transform.position.y))
		{
			return;
		}
		float num = Vector3.Distance(PlayrTrans.position, base.transform.position);
		if (num < BombaMesafesi)
		{
			GameObject gameObject = DepodanAl();
			if (!(gameObject == null))
			{
				Vector3 position = base.transform.position;
				position.y -= 1f;
				gameObject.transform.position = position;
				gameObject.AddComponent<GizlenenBomba>().KayolmaSuresi = 7;
				gameObject.SetActive(true);
			}
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
			grounded = Physics2D.OverlapPoint(point, (1 << LayerMask.NameToLayer("zemin")) | (1 << LayerMask.NameToLayer("Oyuncu")));
		}
	}
}

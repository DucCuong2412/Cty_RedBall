using System;
using System.Collections;
using UnityEngine;

public class UzayliNinja : Pooler
{
	public int AtmaGucu = 100;

	public bool SagaGit;

	public int AtmaSure = 3;

	private float moveForce = 2f;

	private float jumpForce = 5f;

	private float maxSpeed = 4f;

	private bool Aktif;

	private bool grounded;

	private Transform PlayrTrans;

	private float ActiveMesafesi = 4f;

	public bool Dead;

	private Uzayli YanSc;

	public void HayatSon()
	{
		Dead = true;
		Aktif = false;
	}

	private void Awake()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name.Contains("UzayKilic"))
				{
					PoolerGo = transform.gameObject;
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
		InvokeRepeating("Firlat", AtmaSure, AtmaSure);
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			PlayrTrans = gameObject.transform;
		}
		YanSc = GetComponent<Uzayli>();
	}

	private void Firlat()
	{
		if (Dead || YanSc.Hayat == 0 || !Aktif)
		{
			return;
		}
		GameObject gameObject = DepodanAl();
		if (!(gameObject == null))
		{
			Vector3 position = base.transform.position;
			position.y += 1f;
			gameObject.transform.position = position;
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

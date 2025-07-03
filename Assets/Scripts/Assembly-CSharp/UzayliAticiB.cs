using System;
using System.Collections;
using UnityEngine;

public class UzayliAticiB : Pooler
{
	public int AtmaGucu = 300;

	public bool SagaGit;

	public float AtmaSure = 0.3f;

	private Transform PlayrTrans;

	public bool Aktif = true;

	public bool Dead;

	public void HayatSon()
	{
		Dead = true;
	}

	private void Awake()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name.Contains("bom_7"))
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
		int num = 0;
		for (int num2 = 2; num2 > 0; num2--)
		{
			IEnumerator enumerator2 = base.transform.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					Transform transform2 = (Transform)enumerator2.Current;
					if (transform2.name.Contains("bom_"))
					{
						PoolerGoDegis(num, transform2.gameObject);
						num++;
					}
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = enumerator2 as IDisposable) != null)
				{
					disposable2.Dispose();
				}
			}
		}
		IEnumerator enumerator3 = base.transform.GetEnumerator();
		try
		{
			while (enumerator3.MoveNext())
			{
				Transform transform3 = (Transform)enumerator3.Current;
				if (transform3.name.Contains("bom_"))
				{
					transform3.gameObject.SetActive(false);
				}
			}
		}
		finally
		{
			IDisposable disposable3;
			if ((disposable3 = enumerator3 as IDisposable) != null)
			{
				disposable3.Dispose();
			}
		}
		InvokeRepeating("Firlat", AtmaSure, AtmaSure);
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			PlayrTrans = gameObject.transform;
		}
	}

	private void Firlat()
	{
		if (!Dead && Aktif)
		{
			GameObject gameObject = DepodanAl();
			if (!(gameObject == null))
			{
				Vector3 position = base.transform.position;
				position.x -= 0.85f;
				position.y += 0.08f;
				gameObject.transform.position = position;
				gameObject.SetActive(true);
				Vector2 vector = PlayrTrans.position - base.transform.position;
				gameObject.GetComponent<Rigidbody2D>().AddForce(vector.normalized * AtmaGucu);
				gameObject.AddComponent<GizlenenBomba>().KayolmaSuresi = 15;
			}
		}
	}
}

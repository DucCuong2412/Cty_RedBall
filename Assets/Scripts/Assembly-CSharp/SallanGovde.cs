using System;
using System.Collections;
using UnityEngine;

public class SallanGovde : MonoBehaviour
{
	public float Hiz = 1f;

	private Vector3 Sol;

	private Vector3 Sag;

	private Transform MerkezV3;

	private float AktifHiz;

	private void Start()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name == "Merkez")
				{
					MerkezV3 = transform.gameObject.transform;
				}
				if (transform.name == "Sag")
				{
					Sag = transform.transform.position;
				}
				if (transform.name == "Sol")
				{
					Sol = transform.transform.position;
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
		AktifHiz = Hiz;
	}

	private void FixedUpdate()
	{
		if (MerkezV3.transform.position.x < Sol.x && MerkezV3.transform.position.y > Sol.y && AktifHiz == 0f - Hiz)
		{
			AktifHiz = Hiz;
		}
		else if (MerkezV3.transform.position.x > Sag.x && MerkezV3.transform.position.y > Sag.y && AktifHiz == Hiz)
		{
			AktifHiz = 0f - Hiz;
		}
		base.transform.Rotate(0f, 0f, AktifHiz);
	}
}

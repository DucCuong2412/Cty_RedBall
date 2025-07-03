using System;
using System.Collections;
using UnityEngine;

public class Uzayli : MonoBehaviour
{
	public int Hayat = 3;

	public int SapkaGuc;

	private GameObject kapak;

	private GameObject goz;

	private GameObject AgizLive;

	private GameObject AgizDead;

	private bool Dead;

	private bool Zararaldi;

	private Rigidbody2D ReD2;

	private void Start()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name.Contains("gozkapa"))
				{
					kapak = transform.gameObject;
					kapak.SetActive(false);
				}
				if (transform.name.Contains("uzayliGoz"))
				{
					goz = transform.gameObject;
				}
				if (transform.name.Contains("AgizLive"))
				{
					AgizLive = transform.gameObject;
				}
				if (transform.name.Contains("AgizDead"))
				{
					AgizDead = transform.gameObject;
					AgizDead.SetActive(false);
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
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			ReD2 = gameObject.GetComponent<Rigidbody2D>();
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!Dead && kim.gameObject.tag == "Player" && !Zararaldi)
		{
			float x = kim.gameObject.transform.position.x;
			int num = UnityEngine.Random.Range(150, 200);
			int num2 = UnityEngine.Random.Range(250, 300);
			Vector2 force = ((!(x > base.transform.position.x)) ? new Vector2(-num2, num) : new Vector2(num2, num));
			ReD2.AddForce(force);
			Invoke("Sifirla", 1f);
			GetComponent<SpriteRenderer>().color = Color.gray;
			if (Hayat < 1)
			{
				LifeEnd();
			}
			Oldur();
		}
	}

	private void Sifirla()
	{
		Zararaldi = false;
		GetComponent<SpriteRenderer>().color = Color.white;
	}

	public void Oldur()
	{
		if (Zararaldi || Dead)
		{
			return;
		}
		if (SapkaGuc > 0)
		{
			if (base.name.Contains("Korumali"))
			{
				UzayliKorumali component = GetComponent<UzayliKorumali>();
				component.KorumaBak();
			}
			else
			{
				UzayliSapkali component2 = GetComponent<UzayliSapkali>();
				component2.SapkaBak();
			}
			SapkaGuc--;
		}
		else
		{
			GetComponent<SpriteRenderer>().color = Color.gray;
			Hayat--;
			Zararaldi = true;
			Invoke("Sifirla", 1f);
		}
	}

	private void LifeEnd()
	{
		goz.SetActive(false);
		kapak.SetActive(true);
		if ((bool)AgizLive)
		{
			AgizLive.SetActive(false);
		}
		if ((bool)AgizDead)
		{
			AgizDead.SetActive(true);
		}
		Dead = true;
		Rigidbody2D component = GetComponent<Rigidbody2D>();
		if ((bool)component)
		{
			component.isKinematic = false;
			component.freezeRotation = false;
		}
		if (base.name.Contains("Lazerci"))
		{
			UzayliLazerci component2 = GetComponent<UzayliLazerci>();
			if ((bool)component2)
			{
				component2.HayatSon();
			}
		}
		if (base.name.Contains("uzayliSapkali"))
		{
			UzayliSapkali component3 = GetComponent<UzayliSapkali>();
			if ((bool)component3)
			{
				component3.HayatSon();
			}
		}
		if (base.name.Contains("Ninjali"))
		{
			UzayliNinja component4 = GetComponent<UzayliNinja>();
			if ((bool)component4)
			{
				component4.HayatSon();
			}
		}
		if (base.name.Contains("UzayliYercekimci"))
		{
			UzayliYercek component5 = GetComponent<UzayliYercek>();
			if ((bool)component5)
			{
				component5.HayatSon();
			}
		}
		if (base.name.Contains("uzayliBoss1"))
		{
			UzayliAticiB component6 = GetComponent<UzayliAticiB>();
			if ((bool)component6)
			{
				component6.HayatSon();
			}
		}
		if (base.name.Contains("TheBossKorumali"))
		{
			UzayliKorumali component7 = GetComponent<UzayliKorumali>();
			if ((bool)component7)
			{
				component7.HayatSon();
			}
		}
		if (base.name.Contains("UzayliZiplayan"))
		{
			UzayliZiplayan component8 = GetComponent<UzayliZiplayan>();
			if ((bool)component8)
			{
				component8.HayatSon();
			}
		}
		GetComponent<SpriteRenderer>().color = Color.white;
	}
}

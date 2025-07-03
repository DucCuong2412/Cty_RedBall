using System;
using System.Collections;
using UnityEngine;

public class Ufo : MonoBehaviour
{
	private GameObject[] isiklar;

	private GameObject kabak;

	private GameObject ReD2;

	private GameObject lazer;

	private GameObject anahtar;

	private int Kim;

	private SpriteRenderer lazerRenk;

	private bool Zararaldi;

	private bool anahtarver;

	private int Hayat = 6;

	private bool sesCaldi;

	public void Oldur()
	{
		if (!Zararaldi && Hayat >= 0)
		{
			Zararaldi = true;
			StartCoroutine(Sifirla(2f));
			GetComponent<SpriteRenderer>().color = Color.gray;
			Hayat--;
			isiklar[Hayat].GetComponent<SpriteRenderer>().color = Color.black;
			if (Hayat < 1)
			{
				StopAllCoroutines();
				GetComponent<Rigidbody2D>().isKinematic = false;
				lazer.SetActive(false);
				isiklar[0].GetComponent<SpriteRenderer>().color = Color.black;
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}

	private void Start()
	{
		isiklar = new GameObject[7];
		int num = 0;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.name == "ufolight")
				{
					isiklar[num] = transform.gameObject;
					num++;
				}
				if (transform.name == "ufolazer")
				{
					lazer = transform.gameObject;
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
		kabak = GameObject.Find("kabak");
		lazerRenk = lazer.GetComponent<SpriteRenderer>();
		ReD2 = GameObject.FindGameObjectWithTag("Player");
		anahtar = GameObject.Find("anahtar");
		anahtar.SetActive(false);
	}

	private void FixedUpdate()
	{
		if (!sesCaldi)
		{
			float num = Vector3.Distance(kabak.transform.position, base.transform.position);
			if (num < 7f)
			{
				CameraFollow component = Camera.main.GetComponent<CameraFollow>();
				if ((bool)component)
				{
					sesCaldi = true;
					component.UfoSesi();
				}
			}
		}
		if (base.transform.position.y < 1f && !anahtarver)
		{
			anahtarver = true;
			anahtar.SetActive(true);
			anahtar.transform.position = base.transform.position;
			Vector3 position = base.transform.position;
			position.y += 1f;
			anahtar.GetComponent<Anahtar>().HedefGoster(base.transform.position, position);
		}
		if (Hayat < 1)
		{
			return;
		}
		float a = Mathf.PingPong(Time.time, 0.9f);
		Color white = Color.white;
		white.a = a;
		SpriteRenderer component2 = isiklar[Kim].GetComponent<SpriteRenderer>();
		if (component2.color != Color.black)
		{
			component2.color = white;
		}
		Kim++;
		if (Kim > 6)
		{
			Kim = 0;
		}
		white.a = 0.2f;
		lazerRenk.color = white;
		RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, -Vector2.up, 10f, 1 << LayerMask.NameToLayer("haraketli"));
		if (raycastHit2D.collider != null)
		{
			Vector3 b = new Vector3(raycastHit2D.point.x, raycastHit2D.point.y, 0f);
			Vector3 position2 = base.transform.position;
			float num2 = Vector3.Distance(position2, b);
			position2.y -= num2 * 0.2f;
			if (raycastHit2D.collider.gameObject.name == "kabak")
			{
				b.y -= 0.5f;
				white.a = 0.7f;
				lazerRenk.color = white;
				if (raycastHit2D.collider.gameObject.transform.position.y < 0.5f)
				{
					kabak.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20f);
				}
			}
		}
		if (kabak.transform.position.x > base.transform.position.x)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.right;
		}
		else
		{
			GetComponent<Rigidbody2D>().velocity = -Vector2.right;
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player" && !Zararaldi)
		{
			float num = ((!((double)UnityEngine.Random.Range(0, 1) < 0.5)) ? 1 : (-1));
			int num2 = UnityEngine.Random.Range(150, 200);
			Vector2 force = new Vector2(num * 300f, num2);
			ReD2.GetComponent<Rigidbody2D>().AddForce(force);
			Zararaldi = true;
			StartCoroutine(Sifirla(2f));
			GetComponent<SpriteRenderer>().color = Color.gray;
			Hayat--;
			isiklar[Hayat].GetComponent<SpriteRenderer>().color = Color.black;
			if (Hayat < 1)
			{
				StopAllCoroutines();
				GetComponent<Rigidbody2D>().isKinematic = false;
				lazer.SetActive(false);
				isiklar[0].GetComponent<SpriteRenderer>().color = Color.black;
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}

	private IEnumerator Sifirla(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Zararaldi = false;
		GetComponent<SpriteRenderer>().color = Color.white;
	}
}

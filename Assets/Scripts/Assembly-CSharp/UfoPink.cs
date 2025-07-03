using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UfoPink : MonoBehaviour
{
	private GameObject[] isiklar;

	private GameObject kabak;

	private GameObject ReD2;

	private GameObject lazer;

	private GameObject Sapka;

	private int Kim;

	private SpriteRenderer lazerRenk;

	private bool Zararaldi;

	private int Hayat = 6;

	private bool finish;

	public void Oldur()
	{
		if (!finish && !Zararaldi && Hayat >= 1)
		{
			Zararaldi = true;
			StartCoroutine(Sifirla(2f));
			GetComponent<SpriteRenderer>().color = Color.gray;
			Hayat--;
			isiklar[Hayat].GetComponent<SpriteRenderer>().color = Color.black;
			if (Hayat < 1)
			{
				StopAllCoroutines();
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
				if (transform.name == "Sapka")
				{
					Sapka = transform.gameObject;
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
		Sapka.SetActive(false);
		kabak = GameObject.Find("PinkBall");
		lazerRenk = lazer.GetComponent<SpriteRenderer>();
		ReD2 = GameObject.FindGameObjectWithTag("Player");
	}

	private void FixedUpdate()
	{
		if (finish)
		{
			return;
		}
		if (base.transform.position.x > 34f || base.transform.position.x < -14f)
		{
			finish = true;
			string text = SceneManager.GetActiveScene().name;
			text = text.Replace("Level_", string.Empty);
			int num = int.Parse(text);
			int @int = PlayerPrefs.GetInt("sonLevelRB2");
			if (@int < num)
			{
				PlayerPrefs.SetInt("sonLevelRB2", num);
			}
			Camera.main.GetComponent<CameraFollow>().LevelBitti();
		}
		if (base.transform.position.x < 13f && lazer.transform.localScale.y < 360f)
		{
			Vector3 localScale = lazer.transform.localScale;
			localScale.y = 360f;
			lazer.transform.localScale = localScale;
		}
		if (base.transform.position.x > 13f && lazer.transform.localScale.y > 300f)
		{
			Vector3 localScale2 = lazer.transform.localScale;
			localScale2.y = 300f;
			lazer.transform.localScale = localScale2;
		}
		float num2 = Vector3.Distance(kabak.transform.position, base.transform.position);
		if (num2 < 1f)
		{
			kabak.transform.parent = base.transform;
			GetComponent<Rigidbody2D>().velocity = Vector2.right;
			return;
		}
		float a = Mathf.PingPong(Time.time, 0.9f);
		Color white = Color.white;
		white.a = a;
		SpriteRenderer component = isiklar[Kim].GetComponent<SpriteRenderer>();
		if (component.color != Color.black)
		{
			component.color = white;
		}
		Kim++;
		if (Kim > 6)
		{
			Kim = 0;
		}
		white.a = 0.2f;
		lazerRenk.color = white;
		RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, -Vector2.up, 10f, 1 << LayerMask.NameToLayer("haraketli"));
		if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.name == "PinkBall")
		{
			PembeKafa component2 = kabak.GetComponent<PembeKafa>();
			if ((bool)component2)
			{
				ReD2.GetComponent<Player>().UzgunSurat();
				kabak.GetComponent<Rigidbody2D>().isKinematic = true;
				UnityEngine.Object.Destroy(component2);
			}
			white.a = 0.7f;
			lazerRenk.color = white;
			kabak.GetComponent<Rigidbody2D>().velocity = Vector2.up * 0.1f;
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
		if (!finish && kim.gameObject.tag == "Player" && !Zararaldi)
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
				Sapka.SetActive(true);
				StopAllCoroutines();
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

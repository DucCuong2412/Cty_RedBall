using System;
using System.Collections;
using UnityEngine;

public class UfoGenel : MonoBehaviour
{
	public string KalkacakCisim = "kabak";

	public float CekmeGucu = 30f;

	private GameObject[] isiklar;

	private GameObject ReD2;

	private GameObject lazer;

	private GameObject kabak;

	private int Kim;

	private SpriteRenderer lazerRenk;

	private bool Zararaldi;

	private int Hayat = 6;

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
		kabak = GameObject.Find(KalkacakCisim);
		if (kabak == null)
		{
			Debug.Log("UFO ne kaldıracak ! adını ekle : " + KalkacakCisim);
		}
		lazerRenk = lazer.GetComponent<SpriteRenderer>();
		ReD2 = GameObject.FindGameObjectWithTag("Player");
	}

	private void FixedUpdate()
	{
		if (Hayat < 1)
		{
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
		Vector3 position = base.transform.position;
		position.y -= 0.2f;
		Vector3 vector = new Vector3(base.transform.position.x, kabak.transform.position.y - 0.3f, 0f);
		float num = 0.1f;
		white.a = 0.4f;
		lazerRenk.color = white;
		RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, -Vector2.up, 5f, 1 << LayerMask.NameToLayer("zemin"));
		if (raycastHit2D.collider != null)
		{
			vector = new Vector3(raycastHit2D.point.x, raycastHit2D.point.y + 0.5f, 0f);
		}
		float distance = Mathf.Abs(base.transform.position.y - kabak.transform.position.y + 1f);
		RaycastHit2D[] array = Physics2D.RaycastAll(base.transform.position, -Vector2.up, distance, 1 << LayerMask.NameToLayer("haraketli"));
		for (int i = 0; i < array.Length; i++)
		{
			raycastHit2D = array[i];
			if (raycastHit2D.collider == null)
			{
				return;
			}
			if (raycastHit2D.collider.gameObject.name == KalkacakCisim)
			{
				vector = new Vector3(base.transform.position.x, raycastHit2D.point.y, 0f);
				num = 1f;
				white.a = 0.7f;
				lazerRenk.color = white;
				if (raycastHit2D.collider.gameObject.transform.position.y < 0.5f)
				{
					kabak.GetComponent<Rigidbody2D>().AddForce(Vector2.up * CekmeGucu);
				}
			}
			else if ((bool)raycastHit2D.collider.gameObject.GetComponent<Rigidbody2D>())
			{
				float mass = raycastHit2D.collider.gameObject.GetComponent<Rigidbody2D>().mass;
				raycastHit2D.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * mass * 11f);
			}
		}
		Vector3 vector2 = vector - position;
		lazer.transform.position = position + vector2 / 2f;
		lazer.transform.up = vector2.normalized;
		lazer.transform.localScale = new Vector3(num, vector2.magnitude * 150f, num);
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
		if (Hayat >= 0 && kim.gameObject.tag == "Player" && !Zararaldi)
		{
			float num = ((!((double)UnityEngine.Random.Range(0, 1) < 0.5)) ? 1 : (-1));
			int num2 = UnityEngine.Random.Range(150, 200);
			Vector2 force = new Vector2(num * 300f, num2);
			ReD2.GetComponent<Rigidbody2D>().AddForce(force);
			Zararaldi = true;
			Invoke("Sifirla", 2f);
			GetComponent<SpriteRenderer>().color = Color.gray;
			Hayat--;
			MonoBehaviour.print(Hayat);
			if (Hayat > -1)
			{
				isiklar[Hayat].GetComponent<SpriteRenderer>().color = Color.black;
			}
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

	private void Sifirla()
	{
		Zararaldi = false;
		GetComponent<SpriteRenderer>().color = Color.white;
	}
}

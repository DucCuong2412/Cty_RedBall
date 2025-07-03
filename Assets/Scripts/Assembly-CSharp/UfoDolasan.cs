using System.Collections.Generic;
using UnityEngine;

public class UfoDolasan : MonoBehaviour
{
	public int ilkBeklemeSuresi;

	public bool Dusebilir;

	public float Hiz = 15f;

	private int MevcutNokta;

	private bool RigVar;

	private bool bittim;

	private bool Dikenli;

	private GameObject[] dikens = new GameObject[4];

	private SpriteRenderer usTsR;

	public List<Vector2> Noktalar = new List<Vector2>();

	private Player PlayerSc;

	private bool Patladi;

	private void Awake()
	{
		if (GetComponent<Rigidbody2D>() != null)
		{
			RigVar = true;
		}
		if (!RigVar)
		{
			Debug.Log(">>> RigidBody2D gerekiyor ! <<<");
			return;
		}
		if (Noktalar.Count > 0)
		{
			Vector3 position = Noktalar[0];
			position.z = 1.1f;
			base.transform.position = position;
		}
		else
		{
			Debug.LogWarning("miniUzay gemisine Nokta Ekle !!");
		}
		if (ilkBeklemeSuresi > 0)
		{
			Invoke("GazVer", ilkBeklemeSuresi);
		}
		else
		{
			GazVer();
		}
		int num = 0;
		GameObject gameObject = null;
		for (int num2 = base.transform.childCount - 1; num2 >= 0; num2--)
		{
			gameObject = base.transform.GetChild(num2).gameObject;
			if (gameObject.name.Contains("d"))
			{
				Dikenli = true;
				dikens[num] = gameObject;
				num++;
			}
			if (gameObject.name.Contains("ust"))
			{
				usTsR = gameObject.gameObject.GetComponent<SpriteRenderer>();
			}
		}
		PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	public void Oldur()
	{
		usTsR.color = Color.grey;
		bittim = true;
		SonBaslat();
	}

	private int SonrakiNoktaBul()
	{
		if (MevcutNokta >= Noktalar.Count - 1)
		{
			Vector3 position = Noktalar[0];
			position.z = 1.1f;
			base.transform.position = position;
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

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player" && !PlayerSc.KalkanAktif)
		{
			if ((bool)PlayerSc)
			{
				PlayerSc.GeberSurat();
			}
			Player.Geberdi = true;
		}
	}

	private void DikenGizle()
	{
		for (int i = 0; i < 4; i++)
		{
			dikens[i].SetActive(false);
		}
	}

	private void SonBaslat()
	{
		if (Patladi)
		{
			return;
		}
		if (Dikenli)
		{
			PolygonCollider2D component = GetComponent<PolygonCollider2D>();
			component.enabled = false;
			for (int i = 0; i < 4; i++)
			{
				Vector2 vector = dikens[i].transform.position - base.transform.position;
				dikens[i].AddComponent<Rigidbody2D>().velocity = vector * 3f;
				dikens[i].GetComponent<Rigidbody2D>().AddTorque(-50f);
			}
			Invoke("DikenGizle", 1.5f);
		}
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		if (Dusebilir)
		{
			Invoke("Dusur", 1.5f);
		}
		Patladi = true;
	}

	private void Dusur()
	{
		GetComponent<Rigidbody2D>().isKinematic = false;
	}

	private void Update()
	{
		if (!bittim)
		{
			Vector2 vector;
			Vector2 vector2 = (vector = base.transform.position);
			vector2.y = (vector.y = base.transform.position.y + 0.8219197f);
			vector2.x = base.transform.position.x - 0.2944912f;
			vector.x = base.transform.position.x + 0.4269774f;
			Debug.DrawLine(vector2, vector);
			if ((bool)Physics2D.Linecast(vector2, vector, 1 << LayerMask.NameToLayer("Oyuncu")))
			{
				bittim = true;
				Invoke("SonBaslat", 0.1f);
			}
		}
	}

	private void FixedUpdate()
	{
		if (!bittim && Noktalar.Count != 0 && Vector2.Distance(base.transform.position, Noktalar[SonrakiNoktaBul()]) < 0.1f)
		{
			MevcutNokta++;
			if (MevcutNokta >= Noktalar.Count)
			{
				MevcutNokta = 0;
			}
			GazVer();
		}
	}

	public void DortNoktaVer()
	{
		if (Noktalar.Count < 1)
		{
			Vector3 position = base.transform.position;
			Noktalar.Add(new Vector2(position.x + 3f, position.y + 1f));
			Noktalar.Add(new Vector2(position.x + 1.5f, position.y - 0.5f));
			Noktalar.Add(new Vector2(position.x - 1.5f, position.y - 0.5f));
			Noktalar.Add(new Vector2(position.x - 3f, position.y + 1f));
		}
	}

	public void NoktaEkle()
	{
		float num = 1f + (float)Random.Range(1, 10) * 0.1f;
		Noktalar.Add(new Vector2(base.transform.position.x + num, base.transform.position.y + num));
	}

	public void sonNoktaSil()
	{
		Noktalar.RemoveAt(Noktalar.Count - 1);
	}
}

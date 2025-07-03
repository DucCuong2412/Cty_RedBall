using UnityEngine;

public class Testere : MonoBehaviour
{
	private Vector2 sol;

	private Vector2 sag;

	public bool Yonileri;

	public bool Aktif = true;

	public float Hiz = 1f;

	private Vector2 direc;

	private bool pinkdead;

	private bool reddead;

	public void Sifirla()
	{
		reddead = false;
		pinkdead = false;
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player" && !reddead)
		{
			Player component = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			if (!component.KalkanAktif)
			{
				component.GeberSurat();
				Player.Geberdi = true;
				reddead = true;
			}
		}
		if (kim.gameObject.name == "PinkBall" && !pinkdead)
		{
			PembeKafa component2 = GameObject.Find("PinkBall").GetComponent<PembeKafa>();
			component2.DeadYap();
			pinkdead = true;
		}
	}

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.parent.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.parent.transform.GetChild(num).gameObject;
			if (gameObject.name == "Sag")
			{
				sag = gameObject.transform.position;
			}
			if (gameObject.name == "Sol")
			{
				sol = gameObject.transform.position;
			}
		}
		direc = sag - sol;
		direc = direc.normalized;
		if (Aktif)
		{
			if (Yonileri)
			{
				GetComponent<Rigidbody2D>().velocity = direc * Hiz;
			}
			else
			{
				GetComponent<Rigidbody2D>().velocity = direc * (0f - Hiz);
			}
		}
	}

	private void FixedUpdate()
	{
		if (!Aktif)
		{
			if (GetComponent<Rigidbody2D>().velocity.x != 0f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
			return;
		}
		if (base.transform.parent.transform.rotation.eulerAngles.z > 89f)
		{
			if (Yonileri)
			{
				if (GetComponent<Rigidbody2D>().velocity.y < 0f)
				{
					GetComponent<Rigidbody2D>().velocity = direc * Hiz;
				}
			}
			else if (GetComponent<Rigidbody2D>().velocity.y > 0f)
			{
				GetComponent<Rigidbody2D>().velocity = direc * (0f - Hiz);
			}
		}
		else if (Yonileri)
		{
			if (GetComponent<Rigidbody2D>().velocity.x < 0f)
			{
				GetComponent<Rigidbody2D>().velocity = direc * Hiz;
			}
		}
		else if (GetComponent<Rigidbody2D>().velocity.x > 0f)
		{
			GetComponent<Rigidbody2D>().velocity = direc * (0f - Hiz);
		}
		if (base.transform.parent.transform.rotation.eulerAngles.z > 89f)
		{
			if (base.transform.position.y < sol.y)
			{
				Yonileri = true;
			}
			if (base.transform.position.y > sag.y)
			{
				Yonileri = false;
			}
		}
		else
		{
			if (base.transform.position.x < sol.x)
			{
				Yonileri = true;
			}
			if (base.transform.position.x > sag.x)
			{
				Yonileri = false;
			}
		}
	}
}

using UnityEngine;

public class BocekKanatli : MonoBehaviour
{
	public bool YonSaga;

	public float Hiz = 1f;

	private Vector3 sagtaraf;

	private Vector3 soltaraf;

	private bool bittim;

	private bool SagaBakiyor;

	private GameObject kapagoz;

	private GameObject diken;

	private GameObject acgoz;

	private float sag;

	private float sol;

	private Player PlayerSc;

	private bool Patladi;

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!bittim && kim.gameObject.tag == "Player")
		{
			bittim = true;
			Invoke("SonBaslat", 0.1f);
		}
	}

	private void Flip()
	{
		SagaBakiyor = !SagaBakiyor;
		Vector3 localScale = base.transform.localScale;
		localScale.x *= -1f;
		base.transform.localScale = localScale;
	}

	private void Awake()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "goz")
			{
				acgoz = gameObject;
			}
			if (gameObject.name == "goz2")
			{
				kapagoz = gameObject;
			}
			if (gameObject.name == "Diken")
			{
				diken = gameObject;
			}
			if (gameObject.name == "Sol")
			{
				sol = gameObject.transform.position.x;
				gameObject.transform.parent = null;
			}
			if (gameObject.name == "Sag")
			{
				sag = gameObject.transform.position.x;
				gameObject.transform.parent = null;
			}
		}
		kapagoz.SetActive(false);
		PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	private void FixedUpdate()
	{
		if (bittim)
		{
			return;
		}
		if (YonSaga)
		{
			if (sag < base.transform.position.x)
			{
				YonSaga = false;
			}
		}
		else if (base.transform.position.x < sol)
		{
			YonSaga = true;
		}
		if (YonSaga)
		{
			if (!SagaBakiyor)
			{
				Flip();
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2(Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
		else
		{
			if (SagaBakiyor)
			{
				Flip();
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f - Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
	}

	public void Oldur()
	{
		bittim = true;
		SonBaslat();
	}

	private void SonBaslat()
	{
		if (!Patladi)
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
			diken.AddComponent<Rigidbody2D>().AddTorque(100f);
			diken.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 2f);
			kapagoz.SetActive(true);
			acgoz.SetActive(false);
			GetComponent<Animator>().SetBool("oldu", true);
			Invoke("DikenGizle", 1.5f);
			Patladi = true;
		}
	}

	private void DikenGizle()
	{
		diken.SetActive(false);
	}

	private void Update()
	{
		if (bittim)
		{
			return;
		}
		Vector2 vector;
		Vector2 vector2 = (vector = base.transform.position);
		if (YonSaga)
		{
			vector2.x = base.transform.position.x + 0.9646012f;
			vector2.y = base.transform.position.y - 0.02498746f;
			vector.x = base.transform.position.x + 0.4342281f;
			vector.y = base.transform.position.y + 0.006211042f;
		}
		else
		{
			vector2.x = base.transform.position.x - 0.9735434f;
			vector2.y = base.transform.position.y - 0.03498662f;
			vector.x = base.transform.position.x - 0.4266794f;
			vector.y = base.transform.position.y + 0.0006328821f;
		}
		if (!PlayerSc.KalkanAktif)
		{
			Debug.DrawLine(vector2, vector);
			if ((bool)Physics2D.Linecast(vector2, vector, 1 << LayerMask.NameToLayer("Oyuncu")))
			{
				PlayerSc.GeberSurat();
				Player.Geberdi = true;
			}
		}
	}
}

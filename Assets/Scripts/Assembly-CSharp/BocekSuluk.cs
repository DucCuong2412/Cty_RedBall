using UnityEngine;

public class BocekSuluk : MonoBehaviour
{
	public bool YonSaga;

	public float Hiz = 1f;

	private Vector3 sagtaraf;

	private Vector3 soltaraf;

	private bool bittim;

	private GameObject kapagoz;

	private GameObject diken;

	private Color32 GriMat = Color.gray;

	private bool SagaBakiyor;

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
		GriMat.r = (GriMat.g = (GriMat.b = 200));
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "goz")
			{
				kapagoz = gameObject;
			}
			if (gameObject.name == "diken")
			{
				diken = gameObject;
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
			diken.AddComponent<Rigidbody2D>().AddTorque(100f);
			diken.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 2f);
			GetComponent<SpriteRenderer>().color = GriMat;
			kapagoz.SetActive(true);
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
			vector2.y = base.transform.position.y - 0.0372249f;
			vector.y = base.transform.position.y - 0.1116747f;
			vector2.x = base.transform.position.x + 0.3696152f;
			vector.x = base.transform.position.x + 0.927723f;
		}
		else
		{
			vector2.y = base.transform.position.y - 0.02824903f;
			vector.y = base.transform.position.y - 0.1156018f;
			vector2.x = base.transform.position.x - 0.4188046f;
			vector.x = base.transform.position.x - 0.9135f;
		}
		if (!PlayerSc.KalkanAktif)
		{
			Debug.DrawLine(vector2, vector);
			if ((bool)Physics2D.Linecast(vector2, vector, 1 << LayerMask.NameToLayer("Oyuncu")))
			{
				Player.Geberdi = true;
				PlayerSc.GeberSurat();
			}
		}
		sagtaraf = (soltaraf = base.transform.position);
		sagtaraf.x += 0.8f;
		soltaraf.x -= 0.8f;
		if (YonSaga)
		{
			if ((bool)Physics2D.OverlapPoint(sagtaraf, 1 << LayerMask.NameToLayer("zemin")))
			{
				YonSaga = false;
			}
		}
		else if ((bool)Physics2D.OverlapPoint(soltaraf, 1 << LayerMask.NameToLayer("zemin")))
		{
			YonSaga = true;
		}
	}
}

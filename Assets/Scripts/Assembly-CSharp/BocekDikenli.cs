using UnityEngine;

public class BocekDikenli : MonoBehaviour
{
	public bool YonSaga = true;

	public float Hiz = 1f;

	private Vector3 sagtaraf;

	private Vector3 soltaraf;

	private bool bittim;

	private GameObject kapagoz1;

	private GameObject kapagoz2;

	private GameObject goz1;

	private GameObject goz2;

	private Color32 GriMat = Color.gray;

	private Player PlayerSc;

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player" && !PlayerSc.KalkanAktif)
		{
			PlayerSc.GeberSurat();
			Player.Geberdi = true;
		}
		if (kim.gameObject.name == "PinkBall")
		{
			PembeKafa component = GameObject.Find("PinkBall").GetComponent<PembeKafa>();
			component.DeadYap();
		}
	}

	public void Oldur()
	{
		if (!bittim)
		{
			SonBaslat();
			bittim = true;
		}
	}

	private void Awake()
	{
		GriMat.r = (GriMat.g = (GriMat.b = 200));
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "goz1")
			{
				goz1 = gameObject;
			}
			if (gameObject.name == "goz2")
			{
				goz2 = gameObject;
			}
			if (gameObject.name == "kapaligoz1")
			{
				kapagoz1 = gameObject;
				kapagoz1.GetComponent<SpriteRenderer>().color = GriMat;
			}
			if (gameObject.name == "kapaligoz2")
			{
				kapagoz2 = gameObject;
				kapagoz2.GetComponent<SpriteRenderer>().color = GriMat;
			}
		}
		kapagoz1.SetActive(false);
		kapagoz2.SetActive(false);
		PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	private void FixedUpdate()
	{
		if (!bittim)
		{
			if (YonSaga)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(Hiz, GetComponent<Rigidbody2D>().velocity.y);
			}
			else
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(0f - Hiz, GetComponent<Rigidbody2D>().velocity.y);
			}
		}
	}

	private void SonBaslat()
	{
		if (!bittim)
		{
			bittim = true;
			kapagoz1.SetActive(true);
			kapagoz2.SetActive(true);
			goz1.SetActive(false);
			goz2.SetActive(false);
			GetComponent<SpriteRenderer>().color = GriMat;
		}
	}

	private void Update()
	{
		if (bittim)
		{
			return;
		}
		Vector2 vector;
		Vector2 vector2 = (vector = base.transform.position);
		vector2.y = (vector.y = base.transform.position.y + 0.4f);
		vector2.x = base.transform.position.x - 0.45f;
		vector.x = base.transform.position.x + 0.45f;
		Debug.DrawLine(vector2, vector);
		if ((bool)Physics2D.Linecast(vector2, vector, 1 << LayerMask.NameToLayer("Oyuncu")))
		{
			Invoke("SonBaslat", 0.1f);
		}
		sagtaraf = (soltaraf = base.transform.position);
		sagtaraf.x += 0.75f;
		soltaraf.x -= 0.75f;
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

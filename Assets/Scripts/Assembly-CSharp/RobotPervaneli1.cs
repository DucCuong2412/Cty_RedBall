using UnityEngine;

public class RobotPervaneli1 : MonoBehaviour
{
	private float sol;

	private float sag;

	private float alt;

	private float ust;

	public bool YonSaga = true;

	public float Hiz = 1f;

	private bool bittim;

	private GameObject mil;

	private GameObject Disler;

	private GameObject goz1;

	private GameObject goz2;

	private GameObject gozk1;

	private GameObject gozk2;

	private float pos1;

	private float pos2;

	private Player PlayerSc;

	private bool Patladi;

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player" && !PlayerSc.KalkanAktif)
		{
			Player.Geberdi = true;
			PlayerSc.GeberSurat();
		}
	}

	public void Oldur()
	{
		bittim = true;
		SonBaslat();
	}

	private void Awake()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "mil")
			{
				mil = gameObject;
			}
			if (gameObject.name == "Sag")
			{
				sag = gameObject.transform.position.x;
				gameObject.transform.parent = null;
			}
			if (gameObject.name == "Sol")
			{
				sol = gameObject.transform.position.x;
				gameObject.transform.parent = null;
			}
			if (gameObject.name == "goz1")
			{
				goz1 = gameObject;
			}
			if (gameObject.name == "goz2")
			{
				goz2 = gameObject;
			}
			if (gameObject.name == "kapagoz1")
			{
				gozk1 = gameObject;
			}
			if (gameObject.name == "kapagoz2")
			{
				gozk2 = gameObject;
			}
			if (gameObject.name == "Disler")
			{
				Disler = gameObject;
			}
		}
		gozk1.SetActive(false);
		gozk2.SetActive(false);
		pos1 = base.transform.position.y + 0.5f;
		pos2 = base.transform.position.y - 0.5f;
		PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	private void SonBaslat()
	{
		if (!Patladi)
		{
			BoxCollider2D component = GetComponent<BoxCollider2D>();
			component.enabled = false;
			GetComponent<Rigidbody2D>().isKinematic = false;
			Disler.AddComponent<Rigidbody2D>().AddTorque(200f);
			GetComponent<Rigidbody2D>().AddTorque(-20f);
			gozk1.SetActive(true);
			gozk2.SetActive(true);
			goz1.SetActive(false);
			goz2.SetActive(false);
			Patladi = true;
		}
	}

	private void Update()
	{
		if (!bittim)
		{
			Vector2 vector;
			Vector2 vector2 = (vector = base.transform.position);
			vector2.y = (vector.y = base.transform.position.y + 0.3873848f);
			vector2.x = base.transform.position.x - 0.3310864f;
			vector.x = base.transform.position.x + 0.3310864f;
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
		if (bittim)
		{
			return;
		}
		mil.transform.Rotate(0f, 5f, 0f);
		Vector3 position = base.transform.position;
		position.y = Mathf.Lerp(pos1, pos2, (Mathf.Sin(0.5f * Time.time) + 1f) / 2f);
		base.transform.position = position;
		if (YonSaga)
		{
			if (GetComponent<Rigidbody2D>().velocity.x < 0f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.right * Hiz;
			}
		}
		else if (GetComponent<Rigidbody2D>().velocity.x > 0f)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.right * (0f - Hiz);
		}
		if (YonSaga)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
		else
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f - Hiz, GetComponent<Rigidbody2D>().velocity.y);
		}
		if (base.transform.position.x < sol)
		{
			YonSaga = true;
		}
		if (base.transform.position.x > sag)
		{
			YonSaga = false;
		}
	}
}

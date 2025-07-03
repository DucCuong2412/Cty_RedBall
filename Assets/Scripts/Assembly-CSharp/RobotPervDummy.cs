using UnityEngine;

public class RobotPervDummy : MonoBehaviour
{
	private bool bittim;

	private GameObject mil;

	private GameObject Disler;

	private float pos1;

	private float pos2;

	private Player PlayerSc;

	public bool HizliDon;

	private bool pinkdead;

	private bool Patladi;

	public void Sifirla()
	{
		pinkdead = false;
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player" && !PlayerSc.KalkanAktif)
		{
			PlayerSc.GeberSurat();
			Player.Geberdi = true;
		}
		if (kim.gameObject.name == "PinkBall" && !pinkdead)
		{
			PembeKafa component = GameObject.Find("PinkBall").GetComponent<PembeKafa>();
			component.DeadYap();
			pinkdead = true;
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
			if (gameObject.name == "Disler")
			{
				Disler = gameObject;
			}
		}
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
			Patladi = true;
		}
	}

	private void Update()
	{
		if (!bittim && !HizliDon)
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
		if (!bittim)
		{
			if (HizliDon)
			{
				mil.transform.Rotate(0f, 10f, 0f);
				return;
			}
			mil.transform.Rotate(0f, 5f, 0f);
			Vector3 position = base.transform.position;
			position.y = Mathf.Lerp(pos1, pos2, (Mathf.Sin(0.2f * Time.time) + 0.5f) / 2f);
			base.transform.position = position;
		}
	}
}

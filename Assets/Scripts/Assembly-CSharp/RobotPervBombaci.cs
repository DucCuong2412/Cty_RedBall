using UnityEngine;

public class RobotPervBombaci : MonoBehaviour
{
	public float Hiz = 1f;

	private bool BombaVar = true;

	private float sol;

	private float sag;

	private float alt;

	private float ust;

	private bool YonSaga = true;

	private bool bittim;

	private GameObject mil;

	private GameObject goz1;

	private GameObject goz2;

	private GameObject gozk1;

	private GameObject gozk2;

	private GameObject kiskSol;

	private GameObject kiskSag;

	private GameObject Bomba;

	private float ilkY;

	private Transform Redi;

	private Animator ani;

	private bool SonBasladi;

	private void Awake()
	{
		ani = GetComponent<Animator>();
		ilkY = base.transform.position.y;
		Redi = GameObject.FindGameObjectWithTag("Player").transform;
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
			if (gameObject.name == "kiskacSol")
			{
				kiskSol = gameObject;
			}
			if (gameObject.name == "KiskacSag")
			{
				kiskSag = gameObject;
			}
			if (gameObject.name == "Bombasi")
			{
				Bomba = gameObject;
			}
		}
		gozk1.SetActive(false);
		gozk2.SetActive(false);
	}

	public void Oldur()
	{
		Bomba.SetActive(false);
		bittim = true;
		SonBaslat();
	}

	private void SonBaslat()
	{
		if (!SonBasladi)
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
			kiskSag.AddComponent<Rigidbody2D>().AddTorque(200f);
			kiskSol.AddComponent<Rigidbody2D>().AddTorque(200f);
			GetComponent<Rigidbody2D>().AddTorque(-20f);
			gozk1.SetActive(true);
			gozk2.SetActive(true);
			goz1.SetActive(false);
			goz2.SetActive(false);
			Invoke("Oldur", 1f);
			SonBasladi = true;
		}
	}

	private void BombaGeldi()
	{
		BombaVar = true;
		ani.SetInteger("bombadurum", 0);
	}

	public void BombaGeliyor()
	{
		ani.SetInteger("bombadurum", 2);
		Invoke("BombaGeldi", 0.5f);
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

	private void BombaAt()
	{
		Bomba.transform.parent = null;
		Bomba.GetComponent<Rigidbody2D>().isKinematic = false;
	}

	private void FixedUpdate()
	{
		if (bittim)
		{
			return;
		}
		if (BombaVar)
		{
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			zero2.y = (zero.y = base.transform.position.y - 5f);
			zero2.x = base.transform.position.x - 1f;
			zero.x = base.transform.position.x + 1f;
			if (Redi.position.x < zero.x && Redi.position.x > zero2.x && Redi.position.y < base.transform.position.y && Redi.position.y > zero2.y)
			{
				Debug.DrawLine(zero, base.transform.position, Color.yellow);
				Debug.DrawLine(zero2, base.transform.position, Color.yellow);
				BombaVar = false;
				ani.SetInteger("bombadurum", 1);
				BombaAt();
			}
		}
		mil.transform.Rotate(0f, 5f, 0f);
		Vector3 position = base.transform.position;
		float num = Mathf.PingPong(Time.time * 0.5f, 0.5f);
		position.y = ilkY + num;
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

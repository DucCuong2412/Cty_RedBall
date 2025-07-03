using UnityEngine;

public class DikenLiYaprak : MonoBehaviour
{
	private bool dustu;

	private Transform RedTrans;

	private float rotSpeed = 2f;

	private bool reddead;

	private bool pinkdead;

	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			RedTrans = gameObject.transform;
		}
	}

	public void Sifirla()
	{
		reddead = false;
		pinkdead = false;
	}

	private void FixedUpdate()
	{
		if (!dustu)
		{
			if (RedTrans.position.x > base.transform.position.x - 0.1f && RedTrans.position.x < base.transform.position.x + 0.1f && RedTrans.position.y < base.transform.position.y)
			{
				GetComponent<Rigidbody2D>().isKinematic = false;
				dustu = true;
			}
			float z = Mathf.Sin(Time.time * rotSpeed) * 10f;
			base.transform.rotation = Quaternion.Euler(0f, 0f, z);
		}
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
		if (kim.gameObject.tag == "Piti")
		{
			kim.gameObject.GetComponent<Piti>().DikenBatti();
		}
		if (kim.gameObject.name == "PinkBall" && !pinkdead)
		{
			PembeKafa component2 = GameObject.Find("PinkBall").GetComponent<PembeKafa>();
			component2.DeadYap();
			pinkdead = true;
		}
	}
}

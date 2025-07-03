using UnityEngine;

public class Yanardag : MonoBehaviour
{
	private Transform LawPos;

	private GameObject Duman;

	private BoxCollider2D boxCol;

	public float YanmaSuresi = 1f;

	public float SonmeSuresi = 1f;

	private bool reddead;

	private ParticleSystem ps;

	private ParticleSystem.MainModule main;

	public void Sifirla()
	{
		reddead = false;
	}

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "law")
			{
				LawPos = gameObject.transform;
			}
			if (gameObject.name == "asitgaz")
			{
				Duman = gameObject;
			}
		}
		boxCol = GetComponent<BoxCollider2D>();
		if ((bool)Duman)
		{
			Invoke("Yak", SonmeSuresi);
		}
		ps = Duman.GetComponent<ParticleSystem>();
		main = ps.main;
		main.startSpeed = 0f;
	}

	private void Yak()
	{
		main.startSpeed = 2f;
		Invoke("Sondur", YanmaSuresi);
		Invoke("YakTrigger", 0.7f);
	}

	private void Sondur()
	{
		main.startSpeed = 0f;
		Invoke("Yak", SonmeSuresi);
		Invoke("SondurTrigger", 0.8f);
	}

	private void YakTrigger()
	{
		boxCol.size = new Vector2(0.4835296f, 1.659885f);
		boxCol.offset = new Vector2(0.06775451f, 1.45994f);
	}

	private void SondurTrigger()
	{
		boxCol.size = new Vector2(0.5751297f, 0.150301f);
		boxCol.offset = new Vector2(0.05859423f, 0.7051525f);
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
	}

	private void FixedUpdate()
	{
		float num = 1f;
		Vector3 b = new Vector3(0.0374074f, 0.6788235f, 0.1f);
		Vector3 a = new Vector3(0.0374074f, 0.7236767f, 0.1f);
		LawPos.localPosition = Vector3.Lerp(a, b, Mathf.PingPong(Time.time * num, 1f));
	}
}

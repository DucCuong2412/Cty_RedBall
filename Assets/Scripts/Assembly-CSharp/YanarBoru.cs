using UnityEngine;

public class YanarBoru : MonoBehaviour
{
	private Transform LawPos;

	private GameObject Duman;

	private BoxCollider2D boxCol;

	public float YanmaSuresi = 1f;

	public float SonmeSuresi = 1f;

	private ParticleSystem ps;

	private ParticleSystem.MainModule main;

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "asitgaz")
			{
				Duman = gameObject;
			}
		}
		boxCol = GetComponent<BoxCollider2D>();
		if ((bool)Duman)
		{
			Invoke("Yak", SonmeSuresi);
			Invoke("YakTrigger", 0.7f);
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
		Invoke("SondurTrigger", 0.3f);
	}

	private void YakTrigger()
	{
		boxCol.size = new Vector2(0.5426157f, 3.27151f);
		boxCol.offset = new Vector2(0.008692116f, -1.937854f);
	}

	private void SondurTrigger()
	{
		boxCol.size = new Vector2(0.5426157f, 0.1026125f);
		boxCol.offset = new Vector2(0.008692116f, -0.3534045f);
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player")
		{
			Player component = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			if (!component.KalkanAktif)
			{
				component.GeberSurat();
				Player.Geberdi = true;
			}
		}
	}
}

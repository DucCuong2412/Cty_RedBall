using UnityEngine;

public class Diken : MonoBehaviour
{
	private bool reddead;

	private bool pinkdead;

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

	private void OnTriggerStay(Collider kim)
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

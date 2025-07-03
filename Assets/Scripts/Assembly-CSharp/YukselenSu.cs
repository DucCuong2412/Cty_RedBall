using UnityEngine;

public class YukselenSu : MonoBehaviour
{
	public float itmeHizi = 0.5f;

	private Vector3 ilkYer;

	private bool reddead;

	public void Sifirla()
	{
		reddead = false;
	}

	private void Start()
	{
		ilkYer = base.transform.position;
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
		if (kim.gameObject.name.Contains("lastik"))
		{
			kim.gameObject.GetComponent<Rigidbody2D>().gravityScale = itmeHizi;
		}
		if (kim.gameObject.tag == "hareketli" && !kim.gameObject.name.Contains("lastik_") && base.transform.position.y < -12f)
		{
			ilkYer.y += 0.25f;
			base.transform.position = ilkYer;
			kim.gameObject.tag = "Untagged";
		}
	}

	private void OnTriggerExit2D(Collider2D kim)
	{
		if (kim.gameObject.name.Contains("lastik"))
		{
			kim.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
		}
	}
}

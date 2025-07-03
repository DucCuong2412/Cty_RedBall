using UnityEngine;

public class LazerYolu : MonoBehaviour
{
	private BoxCollider2D trigger;

	private SpriteRenderer SR;

	public float GorunmeZaman = 1f;

	public float GizlenmeZaman = 1f;

	public bool ilkDurumAktif = true;

	public float ilkZaman = 2f;

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

	private void Start()
	{
		trigger = GetComponent<BoxCollider2D>();
		SR = GetComponent<SpriteRenderer>();
		if (ilkDurumAktif)
		{
			Invoke("Gizle", ilkZaman);
		}
		else
		{
			Invoke("Goster", ilkZaman);
		}
	}

	private void Gizle()
	{
		SR.enabled = false;
		trigger.enabled = false;
		Invoke("Goster", GizlenmeZaman);
	}

	private void Goster()
	{
		SR.enabled = true;
		trigger.enabled = true;
		Invoke("Gizle", GorunmeZaman);
	}
}

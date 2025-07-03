using UnityEngine;

public class Candy : MonoBehaviour
{
	private SpriteRenderer SR;

	private bool tetik;

	private bool tetikBasladi;

	private bool Tetikbitti;

	public Color kendiRengi;

	private void Start()
	{
		SR = GetComponent<SpriteRenderer>();
		kendiRengi = SR.color;
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!tetik && kim.gameObject.name.Contains("candy"))
		{
			Color color = kim.gameObject.GetComponent<Candy>().kendiRengi;
			if (color == kendiRengi)
			{
				tetik = true;
				tetikBasladi = true;
				GetComponent<Rigidbody2D>().isKinematic = true;
			}
		}
	}

	private void FixedUpdate()
	{
		if (tetikBasladi && !Tetikbitti)
		{
			Color color = Color.Lerp(kendiRengi, Color.black, Mathf.SmoothStep(0f, 1f, Time.time));
			SR.color = color;
			if (color == Color.black)
			{
				Tetikbitti = true;
			}
		}
	}
}

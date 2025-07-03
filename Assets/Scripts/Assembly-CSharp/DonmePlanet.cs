using UnityEngine;

public class DonmePlanet : MonoBehaviour
{
	public float Hiz = 0.1f;

	private bool Aktif;

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player" && !Aktif)
		{
			BoxCollider2D component = GetComponent<BoxCollider2D>();
			if ((bool)component)
			{
				Aktif = true;
				Object.Destroy(component);
				base.transform.Rotate(0f, 0f, -45f);
			}
		}
	}

	private void FixedUpdate()
	{
		if (Aktif)
		{
			base.transform.Rotate(0f, 0f, Hiz);
		}
	}
}

using UnityEngine;

public class SuYukariIter : MonoBehaviour
{
	public float itmeHizi = 0.01f;

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player")
		{
			kim.gameObject.GetComponent<Rigidbody2D>().gravityScale = itmeHizi;
		}
	}

	private void OnTriggerExit2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player")
		{
			kim.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
		}
	}
}

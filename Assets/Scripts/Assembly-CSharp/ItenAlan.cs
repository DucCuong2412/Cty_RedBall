using UnityEngine;

public class ItenAlan : MonoBehaviour
{
	public float itmeHizi = 0.01f;

	public bool AsagiIter = true;

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (AsagiIter)
		{
			if (kim.gameObject.tag == "Player")
			{
				kim.gameObject.GetComponent<Rigidbody2D>().gravityScale = itmeHizi * 2f;
			}
		}
		else if (kim.gameObject.tag == "Player")
		{
			kim.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f - itmeHizi;
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

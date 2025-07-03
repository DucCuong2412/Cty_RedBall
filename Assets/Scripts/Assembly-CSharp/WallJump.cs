using UnityEngine;

public class WallJump : MonoBehaviour
{
	private Player playrSC;

	private float itmeHizi = 0.9f;

	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject)
		{
			playrSC = gameObject.GetComponent<Player>();
		}
	}

	private void OnTriggerStay2D(Collider2D kim)
	{
		if (kim.tag == "Player")
		{
			playrSC.groundedH = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.tag == "Player")
		{
			playrSC.grounded = true;
			kim.gameObject.GetComponent<Rigidbody2D>().gravityScale = itmeHizi;
		}
	}

	private void OnTriggerExit2D(Collider2D kim)
	{
		if (kim.tag == "Player")
		{
			kim.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
		}
	}
}

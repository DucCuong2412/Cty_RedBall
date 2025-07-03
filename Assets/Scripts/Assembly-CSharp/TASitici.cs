using UnityEngine;

public class TASitici : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D kim)
	{
		if (kim.gameObject.tag == "hareketli" || kim.gameObject.tag == "Player")
		{
			Rigidbody2D component = kim.gameObject.GetComponent<Rigidbody2D>();
			if ((bool)component)
			{
				component.AddForce(new Vector2(0f, 500f));
			}
		}
	}
}

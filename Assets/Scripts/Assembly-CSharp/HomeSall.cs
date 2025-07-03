using UnityEngine;

public class HomeSall : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!(kim.tag == "Player"))
		{
			return;
		}
		GameObject gameObject = GameObject.Find("metal2salla");
		if ((bool)gameObject)
		{
			HingeJoint2D component = gameObject.GetComponent<HingeJoint2D>();
			if ((bool)component)
			{
				component.enabled = false;
				base.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			}
		}
	}
}

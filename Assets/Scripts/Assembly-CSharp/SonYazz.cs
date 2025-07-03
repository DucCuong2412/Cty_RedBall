using UnityEngine;

public class SonYazz : MonoBehaviour
{
	private GameObject sonY;

	private bool pinkgeldi;

	private void Start()
	{
		sonY = GameObject.Find("SonYaz");
		sonY.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!pinkgeldi && kim.gameObject.name == "PinkBall")
		{
			sonY.SetActive(true);
			pinkgeldi = true;
			BoxCollider2D component = base.gameObject.GetComponent<BoxCollider2D>();
			if ((bool)component)
			{
				Object.Destroy(component);
			}
		}
	}
}

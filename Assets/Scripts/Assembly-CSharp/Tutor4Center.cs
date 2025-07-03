using UnityEngine;

public class Tutor4Center : MonoBehaviour
{
	private GameObject Ok1;

	private void Start()
	{
		Ok1 = GameObject.Find("okkey4");
		Ok1.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player")
		{
			Ok1.SetActive(true);
			CameraFollow component = Camera.main.GetComponent<CameraFollow>();
			if ((bool)component)
			{
				component.TutorOKSesi();
			}
			base.gameObject.SetActive(false);
		}
	}
}

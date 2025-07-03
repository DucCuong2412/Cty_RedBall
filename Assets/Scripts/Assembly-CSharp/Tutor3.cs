using UnityEngine;

public class Tutor3 : MonoBehaviour
{
	private bool bitti;

	private GameObject Ok1;

	private void Start()
	{
		Ok1 = GameObject.Find("okkey33");
		Ok1.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.name == "candy1" && !bitti)
		{
			base.gameObject.SetActive(false);
			Ok1.SetActive(true);
			bitti = true;
			CameraFollow component = Camera.main.GetComponent<CameraFollow>();
			if ((bool)component)
			{
				component.TutorOKSesi();
			}
		}
	}
}

using UnityEngine;

public class Tutor1 : MonoBehaviour
{
	private bool bitti;

	private GameObject Ok1;

	private void Start()
	{
		Ok1 = GameObject.Find("okkeyw1");
		Ok1.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player" && !bitti)
		{
			GameObject gameObject = GameObject.Find("wall1");
			gameObject.SetActive(false);
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

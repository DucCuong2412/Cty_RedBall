using UnityEngine;

public class Tutor5s : MonoBehaviour
{
	private bool bitti;

	private GameObject wall;

	private void Start()
	{
		wall = GameObject.Find("wall5s");
		if ((bool)wall)
		{
			wall.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!bitti && kim.gameObject.tag == "Player")
		{
			if ((bool)wall)
			{
				wall.SetActive(true);
			}
			base.gameObject.SetActive(false);
			bitti = true;
		}
	}
}

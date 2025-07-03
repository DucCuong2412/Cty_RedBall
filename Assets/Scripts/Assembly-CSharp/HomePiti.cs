using UnityEngine;

public class HomePiti : MonoBehaviour
{
	private GameObject homepo;

	private void Start()
	{
		homepo = GameObject.Find("Homepol");
		if ((bool)homepo)
		{
			homepo.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.name.Contains("Pitii"))
		{
			GameObject gameObject = GameObject.Find("blok_3ozel");
			if ((bool)gameObject)
			{
				gameObject.GetComponent<KayanBlkAsagi>().AsagiGit();
			}
			if ((bool)homepo)
			{
				homepo.SetActive(true);
			}
			base.gameObject.GetComponent<BoxCollider2D>().enabled = false;
		}
	}
}

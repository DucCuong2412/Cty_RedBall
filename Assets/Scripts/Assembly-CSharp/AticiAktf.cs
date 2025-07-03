using UnityEngine;

public class AticiAktf : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!(kim.tag == "Player"))
		{
			return;
		}
		GameObject gameObject = GameObject.Find("uzayliBoss1");
		if ((bool)gameObject)
		{
			if ((bool)gameObject)
			{
				gameObject.GetComponent<UzayliAticiB>().Aktif = true;
			}
			base.gameObject.SetActive(false);
		}
	}
}

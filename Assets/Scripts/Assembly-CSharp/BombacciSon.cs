using UnityEngine;

public class BombacciSon : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.name.Contains("BombacciBo") || kim.name.Contains("lastik_"))
		{
			kim.gameObject.SetActive(false);
		}
	}
}

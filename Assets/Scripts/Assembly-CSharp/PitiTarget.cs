using UnityEngine;

public class PitiTarget : MonoBehaviour
{
	private bool Tamam;

	private GameObject Hediye;

	private void Start()
	{
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			if (base.transform.GetChild(num).gameObject.name == "Hediye")
			{
				Hediye = base.transform.GetChild(num).gameObject;
				Hediye.SetActive(false);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!Tamam && kim.gameObject.name.Contains("Pitii"))
		{
			Hediye.SetActive(true);
			Hediye.transform.parent = kim.gameObject.transform;
			Tamam = true;
		}
	}
}

using UnityEngine;

public class TutorKontrol : MonoBehaviour
{
	private GameObject yildizs;

	private GameObject uzayli;

	private bool bitti;

	private void Start()
	{
		uzayli = GameObject.Find("Uzayli1Ninjali");
		if ((bool)uzayli)
		{
			uzayli.SetActive(false);
		}
		yildizs = GameObject.Find("ParaS");
		if ((bool)yildizs)
		{
			yildizs.SetActive(false);
		}
		int @int = PlayerPrefs.GetInt("TutorSay");
		if (@int > 2)
		{
			uzayli.SetActive(true);
		}
		else
		{
			yildizs.SetActive(true);
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player" && !bitti)
		{
			int @int = PlayerPrefs.GetInt("TutorSay");
			if (@int < 4)
			{
				@int++;
				PlayerPrefs.SetInt("TutorSay", @int);
			}
			base.gameObject.SetActive(false);
			bitti = true;
		}
	}
}

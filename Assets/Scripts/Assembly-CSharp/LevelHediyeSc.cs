using UnityEngine;

public class LevelHediyeSc : MonoBehaviour
{
	public int LevelNo = 1;

	public string LevelAd;

	public int Bomba;

	public int Kalkan;

	public bool Takip;

	public bool Jet;

	private void OnCollisionEnter2D(Collision2D kim)
	{
		if (!(kim.gameObject.name == "PinkBall") && !(kim.gameObject.tag == "Player"))
		{
			kim.gameObject.SetActive(false);
		}
	}
}

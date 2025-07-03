using UnityEngine;

public class KirSeconIS : MonoBehaviour
{
	private GameObject KirDuman1;

	public string DumanAd = "KirDuman";

	private void Start()
	{
		KirDuman1 = GameObject.Find(DumanAd);
		KirDuman1.SetActive(false);
	}

	public void Kirildik()
	{
		KirDuman1.SetActive(true);
		Object.Destroy(this);
	}
}

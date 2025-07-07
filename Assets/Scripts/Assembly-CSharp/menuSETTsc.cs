using UnityEngine;

public class menuSETTsc : MonoBehaviour
{
	private GameObject Sel1;

	private GameObject Sel2;

	private void Start()
	{
		Sel1 = GameObject.Find("okkey1");
		Sel2 = GameObject.Find("okkey2");
		//Sel1.SetActive(false);
		Sel2.SetActive(false);
		int @int = PlayerPrefs.GetInt("SanalJoystik");
		if (@int == 1)
		{
			Sel2.SetActive(true);
		}
		else
		{
			Sel1.SetActive(true);
		}
	}

	public void SecimYap(int Hangi)
	{
		int value = 0;
		if (Hangi == 1)
		{
			Sel1.SetActive(true);
			Sel2.SetActive(false);
		}
		else
		{
			value = 1;
			Sel1.SetActive(false);
			Sel2.SetActive(true);
		}
		PlayerPrefs.SetInt("SanalJoystik", value);
	}
}

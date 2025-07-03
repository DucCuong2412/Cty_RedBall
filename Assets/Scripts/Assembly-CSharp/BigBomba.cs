using UnityEngine;

public class BigBomba : MonoBehaviour
{
	private bool patladi;

	private GameObject paritkl;

	private void Start()
	{
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			GameObject gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "Particle_BDuman")
			{
				paritkl = gameObject;
				paritkl.GetComponent<Renderer>().enabled = false;
			}
		}
	}

	private void DumanIptal()
	{
		paritkl.GetComponent<Renderer>().enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.tag == "Player" && !patladi)
		{
			patladi = true;
			paritkl.GetComponent<Renderer>().enabled = true;
			Invoke("DumanIptal", 3f);
			GameObject gameObject = GameObject.Find("uzayliSapkali1");
			if ((bool)gameObject)
			{
				gameObject.GetComponent<UzayliSapkali>().BigBombaX();
			}
			GameObject gameObject2 = GameObject.Find("redrobot2");
			if ((bool)gameObject2)
			{
				gameObject2.GetComponent<RobotRed>().Oldur();
			}
		}
	}
}

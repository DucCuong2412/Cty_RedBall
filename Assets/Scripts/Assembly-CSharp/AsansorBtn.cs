using UnityEngine;

public class AsansorBtn : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.tag == "Player" || kim.tag == "hareketli")
		{
			if (base.name == "dumeSol")
			{
				base.transform.parent.GetComponent<AsansorButonlu>().SolaGitEmri();
			}
			if (base.name == "dumeSag")
			{
				base.transform.parent.GetComponent<AsansorButonlu>().SagaGitEmri();
			}
		}
	}
}

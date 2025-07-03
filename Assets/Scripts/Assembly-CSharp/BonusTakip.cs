using UnityEngine;

public class BonusTakip : BonusNe
{
	private GameObject Takipci;

	private void Start()
	{
		Takipci = Object.Instantiate(Resources.Load("takipli", typeof(GameObject))) as GameObject;
		Takipci.SetActive(false);
		Takipci.name = "takipli";
	}

	protected override void Ozelislem(GameObject go)
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if (!gameObject.GetComponent<Player>().TakipVar)
		{
			Takipci.SetActive(true);
			Takipci.transform.position = base.transform.position;
		}
	}
}

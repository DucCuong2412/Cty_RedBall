using UnityEngine;

public class BonusJet : BonusNe
{
	private Player PlayerSc;

	private void Start()
	{
		PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	protected override void Ozelislem(GameObject go)
	{
		if (go.tag == "Player" || go.name == "takipli")
		{
			PlayerSc.JetVer();
		}
	}
}

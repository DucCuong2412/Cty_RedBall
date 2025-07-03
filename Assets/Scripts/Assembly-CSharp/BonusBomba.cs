using UnityEngine;

public class BonusBomba : BonusNe
{
	public int bombaSay = 5;

	private Player PlayerSc;

	private void Start()
	{
		PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	protected override void Ozelislem(GameObject go)
	{
		PlayerSc.BombaVer(bombaSay);
	}
}

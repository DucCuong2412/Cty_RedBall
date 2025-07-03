using UnityEngine;

public class BonusPara : BonusNe
{
	private CameraFollow CamSc;

	private void Start()
	{
		Salinir = false;
		CamSc = Camera.main.GetComponent<CameraFollow>();
	}

	protected override void Ozelislem(GameObject go)
	{
		if (go.tag == "takipli")
		{
			go.GetComponent<Takipli>().ParaAlindi();
		}
		CamSc.ParaArttir();
	}
}

using UnityEngine;

public class UpdateTExt : MonoBehaviour
{
	private int Puan;

	private FontSprites DigerSc;

	private void Start()
	{
		DigerSc = GetComponent<FontSprites>();
		DigerSc.MetinDegis("LEVEL 123");
		InvokeRepeating("Artir", 0.1f, 0.1f);
	}

	private void Artir()
	{
		Puan++;
		DigerSc.MetinDegis("LEVEL:" + Puan);
	}
}

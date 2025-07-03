using UnityEngine;

public class update2 : MonoBehaviour
{
	private int Puan;

	private FontSprites DigerSc;

	private void Start()
	{
		DigerSc = GetComponent<FontSprites>();
		DigerSc.MetinDegis("3/200 *");
		InvokeRepeating("Artir", 0.1f, 0.1f);
	}

	private void Artir()
	{
		Puan++;
		DigerSc.MetinDegis(Puan + "/144 *");
	}
}

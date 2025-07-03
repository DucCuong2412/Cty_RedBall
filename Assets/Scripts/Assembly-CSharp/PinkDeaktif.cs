using UnityEngine;

public class PinkDeaktif : MonoBehaviour
{
	public bool ok;

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.gameObject.name == "PinkBall")
		{
			PembeKafa component = kim.gameObject.GetComponent<PembeKafa>();
			if ((bool)component)
			{
				component.DeaktifYap();
				ok = true;
			}
		}
	}
}

using UnityEngine;

public class PitiNormaleDon : MonoBehaviour
{
	private bool bitti;

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!bitti && kim.gameObject.name.Contains("Pitii"))
		{
			Piti component = kim.gameObject.GetComponent<Piti>();
			if ((bool)component)
			{
				component.NormaleDon();
				bitti = true;
			}
		}
	}
}

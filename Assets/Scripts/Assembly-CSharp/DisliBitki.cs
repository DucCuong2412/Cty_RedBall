using UnityEngine;

public class DisliBitki : MonoBehaviour
{
	private Transform ySol;

	private Transform ySag;

	private bool kapaniyor;

	private bool aciliyor;

	private void Start()
	{
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			GameObject gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "YiyiciSol")
			{
				ySol = gameObject.gameObject.transform;
			}
			if (gameObject.name == "YiyiciSag")
			{
				ySag = gameObject.gameObject.transform;
			}
		}
	}

	private void FixedUpdate()
	{
		if (kapaniyor && ySag.rotation.eulerAngles.z < 75f)
		{
			ySag.Rotate(0f, 0f, 1f);
			ySol.Rotate(0f, 0f, -1f);
		}
		if (aciliyor && ySag.rotation.eulerAngles.z > 1f)
		{
			ySag.Rotate(0f, 0f, -1f);
			ySol.Rotate(0f, 0f, 1f);
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!kapaniyor && kim.tag == "Player" && ySag.rotation.eulerAngles.z < 75f)
		{
			kapaniyor = true;
			aciliyor = false;
		}
	}

	private void OnTriggerExit2D(Collider2D kim)
	{
		if (kim.tag == "Player")
		{
			kapaniyor = false;
			aciliyor = true;
		}
	}
}

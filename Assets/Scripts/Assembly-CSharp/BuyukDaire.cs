using UnityEngine;

public class BuyukDaire : MonoBehaviour
{
	private Transform merkez;

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "merkezx")
			{
				merkez = gameObject.transform;
			}
		}
	}

	private void FixedUpdate()
	{
		merkez.rotation = Quaternion.Inverse(base.transform.rotation);
	}
}

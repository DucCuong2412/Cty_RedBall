using UnityEngine;

public class Kapi_4lu : MonoBehaviour
{
	private SliderJoint2D slider;

	private BoxCollider2D boxC;

	private bool islemTamam;

	private void Start()
	{
		slider = GetComponent<SliderJoint2D>();
		GameObject gameObject = base.transform.parent.gameObject;
		GameObject gameObject2 = null;
		for (int num = gameObject.transform.childCount - 1; num >= 0; num--)
		{
			gameObject2 = gameObject.transform.GetChild(num).gameObject;
			if (gameObject2.name == "LevelSonu")
			{
				boxC = gameObject2.GetComponent<BoxCollider2D>();
				boxC.enabled = false;
			}
		}
	}

	private void AzBekle()
	{
		boxC.enabled = true;
	}

	private void FixedUpdate()
	{
		if (!islemTamam && slider.jointTranslation < -1f)
		{
			islemTamam = true;
			Invoke("AzBekle", 2f);
		}
	}
}

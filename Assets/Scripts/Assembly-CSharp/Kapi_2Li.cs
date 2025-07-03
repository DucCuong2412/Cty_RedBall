using UnityEngine;

public class Kapi_2Li : MonoBehaviour
{
	private SliderJoint2D slider;

	private BoxCollider2D boxC;

	private bool islemTamam;

	private void Start()
	{
		slider = GetComponent<SliderJoint2D>();
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "LevelSonu")
			{
				boxC = gameObject.GetComponent<BoxCollider2D>();
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

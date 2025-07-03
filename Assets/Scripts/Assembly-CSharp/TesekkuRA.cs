using UnityEngine;

public class TesekkuRA : MonoBehaviour
{
	private void Start()
	{
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		GameObject gameObject3 = null;
		GameObject gameObject4 = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "langMX")
			{
				gameObject4 = gameObject.gameObject;
				gameObject4.SetActive(false);
			}
			if (gameObject.name == "langRU")
			{
				gameObject3 = gameObject.gameObject;
				gameObject3.SetActive(false);
			}
			if (gameObject.name == "langTR")
			{
				gameObject2 = gameObject.gameObject;
				gameObject2.SetActive(false);
			}
		}
		string text = Application.systemLanguage.ToString();
		if (text == "Turkish")
		{
			gameObject2.SetActive(true);
		}
		if (text == "Russian")
		{
			gameObject3.SetActive(true);
		}
		if (text == "Spanish")
		{
			gameObject4.SetActive(true);
		}
	}
}

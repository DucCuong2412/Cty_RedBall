using UnityEngine;

public class ButtonSelTyp : ButtonTek
{
	private menuSETTsc menuSC;

	private void Start()
	{
		GameObject gameObject = GameObject.Find("menuSettings");
		if (gameObject != null)
		{
			menuSC = gameObject.GetComponent<menuSETTsc>();
		}
	}

	protected override void Basladi()
	{
		if (base.name == "OptionSol")
		{
			menuSC.SecimYap(1);
		}
		if (base.name == "OptionSag")
		{
			menuSC.SecimYap(2);
		}
	}
}

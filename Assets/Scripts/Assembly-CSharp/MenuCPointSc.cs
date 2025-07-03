using UnityEngine;

public class MenuCPointSc : MonoBehaviour
{
	private GameObject Csol;

	private GameObject Csag;

	private GameObject Corta;

	private GameObject Cfont;

	private FontSprites FS;

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "CPsol")
			{
				Csol = gameObject;
			}
			if (gameObject.name == "CPsag")
			{
				Csag = gameObject;
			}
			if (gameObject.name == "COrta")
			{
				Corta = gameObject;
			}
			if (gameObject.name == "CFont")
			{
				Cfont = gameObject;
			}
		}
		Hizala();
	}

	private void Hizala()
	{
		Vector3 vector = Camera.main.ViewportToWorldPoint(Vector2.one);
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(Vector2.zero);
		Vector3 position = vector2;
		position.z = -0.2f;
		Csol.transform.position = position;
		Vector3 vector3 = new Vector3(vector.x, vector2.y, -0.2f);
		Csag.transform.position = vector3;
		Vector3 position2 = (vector2 + vector3) * 0.5f;
		position2.z = -0.2f;
		Corta.transform.position = position2;
		Vector3 vector4 = new Vector3(vector2.x, vector.y, -0.2f);
		Vector3 position3 = (vector4 + vector) * 0.5f;
		position3.y -= 0.5f;
		Cfont.transform.position = position3;
	}

	public void StarBaslat()
	{
		Csol.transform.GetChild(0).gameObject.SetActive(true);
		Csag.transform.GetChild(0).gameObject.SetActive(true);
		Corta.transform.GetChild(0).gameObject.SetActive(true);
		Hizala();
		AdsMenuden component = base.gameObject.GetComponent<AdsMenuden>();
		component.ajaxBAK();
	}
}

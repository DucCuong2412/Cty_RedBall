using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelKontrol : MonoBehaviour
{
	public int ToplamLevel = 99;

	private void Awake()
	{
		GameObject gameObject = GameObject.Find("level_0");
		float y = 1.7f;
		float y2 = -0.3f;
		float y3 = -2.3f;
		float num = -2.5f;
		Vector3 position = gameObject.transform.position;
		int num2 = 0;
		for (int i = 1; i < ToplamLevel; i++)
		{
			position.x = num;
			num2++;
			switch (num2)
			{
			case 1:
				position.y = y;
				break;
			case 2:
				position.y = y2;
				break;
			default:
				position.y = y3;
				break;
			}
			string text = i.ToString();
			GameObject gameObject2 = Object.Instantiate(gameObject, position, Quaternion.identity);
			gameObject2.name = "level_" + text;
			gameObject2.SetActive(true);
			LevelSc component = gameObject2.GetComponent<LevelSc>();
			string metn = i.ToString();
			if ((bool)component)
			{
				component.MetinYaz(metn);
			}
			if (num2 == 3)
			{
				num += 2.5f;
				num2 = 0;
			}
		}
		LevelSc component2 = gameObject.GetComponent<LevelSc>();
		if ((bool)component2)
		{
			component2.MetinYaz("0");
		}
		gameObject.transform.position = new Vector3(-8f, 0f, 0f);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("mainMenu");
		}
	}
}

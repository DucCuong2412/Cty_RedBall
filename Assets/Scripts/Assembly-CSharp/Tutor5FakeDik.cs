using UnityEngine;

public class Tutor5FakeDik : MonoBehaviour
{
	private GameObject PlayerGo;

	private GameObject Font2;

	private void Start()
	{
		PlayerGo = GameObject.FindGameObjectWithTag("Player");
		Font2 = GameObject.Find("UseSHfont");
		if ((bool)Font2)
		{
			Font2.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!(kim.gameObject.tag == "Player"))
		{
			return;
		}
		Player component = PlayerGo.GetComponent<Player>();
		if (!component.KalkanAktif)
		{
			Vector3 position = new Vector3(37f, 0.9f, 0f);
			PlayerGo.transform.position = position;
			if ((bool)Font2)
			{
				Font2.SetActive(true);
			}
			PlayerGo.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}
}

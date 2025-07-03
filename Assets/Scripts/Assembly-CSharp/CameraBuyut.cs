using UnityEngine;

public class CameraBuyut : MonoBehaviour
{
	public float hedefBoy = 6f;

	private bool Aldi;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (!Aldi && col.gameObject.tag == "Player")
		{
			float orthographicSize = Camera.main.orthographicSize;
			Camera.main.orthographicSize = hedefBoy;
			GameObject gameObject = GameObject.Find("Parmaklar");
			if ((bool)gameObject)
			{
				gameObject.GetComponent<ParmaklarSc>().kameraBuyuduu();
			}
			float num = hedefBoy / orthographicSize;
			GameObject gameObject2 = GameObject.Find("YildizSay");
			if ((bool)gameObject2)
			{
				FontSprites component = gameObject2.GetComponent<FontSprites>();
				component.Buyut = num * component.Buyut;
				component.KameraBuyudu();
			}
			GameObject gameObject3 = GameObject.Find("Levelismi");
			if ((bool)gameObject3)
			{
				FontSprites component2 = gameObject3.GetComponent<FontSprites>();
				component2.Buyut = num * component2.Buyut;
				component2.KameraBuyudu();
			}
			Camera.main.GetComponent<CameraFollow>().KameraBuyuMenu();
			BoxCollider2D component3 = GetComponent<BoxCollider2D>();
			component3.enabled = false;
			Aldi = true;
			base.gameObject.SetActive(false);
		}
	}
}

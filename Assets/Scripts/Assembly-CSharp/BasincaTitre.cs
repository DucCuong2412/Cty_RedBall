using UnityEngine;

public class BasincaTitre : MonoBehaviour
{
	public float beklemeSuresi = 3f;

	public float Carpan = 1f;

	public float Mesafe = 0.2f;

	private Vector3 ilkYer;

	private bool Titriyor;

	private float BasmaZaman;

	private Rigidbody2D playerRig;

	private bool Bitti;

	private GameObject dal;

	private void Start()
	{
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			GameObject gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name.Contains("dall"))
			{
				dal = gameObject.gameObject;
				dal.SetActive(false);
			}
		}
		ilkYer = base.transform.position;
		GameObject gameObject2 = GameObject.FindGameObjectWithTag("Player");
		if ((bool)gameObject2)
		{
			playerRig = gameObject2.GetComponent<Rigidbody2D>();
		}
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!Bitti && kim.tag == "Player")
		{
			Titriyor = true;
			BasmaZaman = Time.timeSinceLevelLoad;
		}
	}

	private void OnTriggerExit2D(Collider2D kim)
	{
		if (!Bitti && kim.tag == "Player")
		{
			Titriyor = false;
			BasmaZaman = 0f;
		}
	}

	private void Update()
	{
		if (!Bitti && Titriyor)
		{
			float num = Mathf.PingPong(Time.time, 0.05f);
			num += ilkYer.y;
			base.transform.position = new Vector3(ilkYer.x, num, ilkYer.z);
			if (BasmaZaman + beklemeSuresi < Time.timeSinceLevelLoad)
			{
				dal.SetActive(true);
				GetComponent<Rigidbody2D>().isKinematic = false;
				playerRig.AddForce(Vector2.up * 10f);
				Bitti = true;
			}
		}
	}
}

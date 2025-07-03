using UnityEngine;

public class AsansorBindi : MonoBehaviour
{
	public float Hiz = 1f;

	public float TepeNokta;

	public float ilkY;

	public bool YukariGit;

	public bool Asagigit;

	private int atesSay;

	private Vector3 ik1;

	private Vector3 ik2;

	private GameObject[] Atesler = new GameObject[3];

	private bool Ustunde;

	private void Start()
	{
		ilkY = base.transform.position.y;
		ik1.y = (ik2.y = ilkY);
		ik1.x = base.transform.position.x + 1f;
		ik2.x = base.transform.position.x - 1f;
		int num = 0;
		GameObject gameObject = null;
		for (int num2 = base.transform.childCount - 1; num2 >= 0; num2--)
		{
			gameObject = base.transform.GetChild(num2).gameObject;
			if (gameObject.name == "Ust")
			{
				TepeNokta = gameObject.transform.position.y;
				gameObject.transform.parent = null;
			}
			if (gameObject.name.Contains("roketAtes"))
			{
				Atesler[num] = gameObject.gameObject;
				num++;
			}
		}
		Goster(false);
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (kim.tag == "Player")
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.up * Hiz;
			YukariGit = true;
			Asagigit = false;
			Ustunde = true;
			Goster(true);
		}
	}

	private void OnTriggerExit2D(Collider2D kim)
	{
		if (kim.tag == "Player")
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.up * (0f - Hiz);
			Asagigit = true;
			YukariGit = false;
			Ustunde = false;
			Goster(false);
		}
	}

	private void Goster(bool goster)
	{
		if (goster)
		{
			Atesler[0].gameObject.SetActive(true);
			Atesler[1].gameObject.SetActive(true);
			Atesler[2].gameObject.SetActive(true);
		}
		else
		{
			Atesler[0].gameObject.SetActive(false);
			Atesler[1].gameObject.SetActive(false);
			Atesler[2].gameObject.SetActive(false);
		}
	}

	private void FixedUpdate()
	{
		LayerMask layerMask = (1 << LayerMask.NameToLayer("haraketli")) | (1 << LayerMask.NameToLayer("Pinky")) | (1 << LayerMask.NameToLayer("Oyuncu"));
		bool flag = Physics2D.Linecast(ik1, ik2, layerMask);
		if (flag && base.transform.position.y < ilkY + 0.5f && !Ustunde)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.up * Hiz;
			YukariGit = true;
			Asagigit = false;
			Goster(true);
		}
		if (!flag && base.transform.position.y > ilkY && !Ustunde)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.up * (0f - Hiz);
			Asagigit = true;
			YukariGit = false;
			Goster(false);
		}
		if (YukariGit)
		{
			float num = Mathf.PingPong(Time.time, 0.3f);
			num += 0.7f;
			Atesler[0].transform.localScale = new Vector3(num, num, 1f);
			Atesler[1].transform.localScale = new Vector3(num, num, 1f);
			Atesler[2].transform.localScale = new Vector3(num, num, 1f);
		}
		if (Asagigit && base.transform.position.y < ilkY)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
		if (YukariGit && base.transform.position.y > TepeNokta)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}
}

using UnityEngine;

public class SualtiAhtapot : MonoBehaviour
{
	private float ust;

	private float alt;

	private Animator ani;

	public float Hiz = 0.5f;

	public bool YonYukari;

	private void Start()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "Ust")
			{
				ust = gameObject.transform.position.y;
				gameObject.transform.parent = null;
			}
			if (gameObject.name == "Alt")
			{
				alt = gameObject.transform.position.y;
				gameObject.transform.parent = null;
			}
		}
		ani = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		Vector3 position = base.transform.position;
		position.x += Mathf.Sin(Time.time) * Time.deltaTime * 0.3f;
		base.transform.position = position;
		if (YonYukari)
		{
			if (ust < base.transform.position.y)
			{
				ani.SetBool("yukari", false);
				YonYukari = false;
			}
		}
		else if (base.transform.position.y < alt)
		{
			YonYukari = true;
			ani.SetBool("yukari", true);
		}
		if (YonYukari)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Hiz);
		}
		else
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0f - Hiz);
		}
	}
}

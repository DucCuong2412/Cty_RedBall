using UnityEngine;

public class UpointSc : MonoBehaviour
{
	private Color Seffaf;

	private SpriteRenderer SR;

	private void Start()
	{
		Invoke("YokEt", 3f);
		Seffaf = Color.white;
		Seffaf.a = 0.5f;
		SR = GetComponent<SpriteRenderer>();
	}

	private void YokEt()
	{
		SR.color = Color.white;
		base.gameObject.SetActive(false);
		Object.Destroy(this);
	}

	private void FixedUpdate()
	{
		Color color = Color.Lerp(Seffaf, Color.white, Mathf.PingPong(Time.time * 4f, 1f));
		SR.color = color;
	}
}

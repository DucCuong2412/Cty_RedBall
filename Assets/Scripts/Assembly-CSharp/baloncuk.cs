using UnityEngine;

public class baloncuk : MonoBehaviour
{
	public Vector2 ilkyeri;

	public float maxY;

	public float Hizi = 0.03f;

	private void Start()
	{
		ilkyeri = base.transform.position;
	}

	private void FixedUpdate()
	{
		Vector2 zero = Vector2.zero;
		zero.y = base.transform.position.y + Hizi;
		zero.x = ilkyeri.x + Mathf.Sin(Time.time * 1f) * 0.1f;
		base.transform.position = zero;
		Vector3 position = GameObject.FindGameObjectWithTag("Player").transform.position;
		float num = Vector3.Distance(zero, position);
		if (num < 0.5f)
		{
			Object.Destroy(base.gameObject);
		}
		if (base.transform.position.y > maxY)
		{
			Object.Destroy(base.gameObject);
		}
	}
}

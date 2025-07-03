using UnityEngine;

public class TestC : MonoBehaviour
{
	private void Start()
	{
		Vector3 vector = Camera.main.ViewportToWorldPoint(Vector2.one);
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(Vector2.zero);
		Vector3 position = (vector + vector2) * 0.5f;
		base.transform.position = position;
	}
}

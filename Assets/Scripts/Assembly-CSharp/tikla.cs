using UnityEngine;

public class tikla : MonoBehaviour
{
	private void Update()
	{
		bool flag = false;
		Vector3 vector;
		if (Input.touchCount > 0)
		{
			vector = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				flag = true;
			}
		}
		else
		{
			vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Input.GetMouseButtonDown(0))
			{
				flag = true;
			}
		}
		if (flag && GetComponent<Collider2D>().OverlapPoint(vector))
		{
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 500f));
		}
		Vector3 vector2 = new Vector3(Input.GetAxis("Horizontal") * 10f, 0f, Input.GetAxis("Vertical"));
		GetComponent<Rigidbody2D>().AddForce(vector2 * 10f);
	}
}

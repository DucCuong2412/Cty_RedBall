using UnityEngine;

public class TestScale : MonoBehaviour
{
	private float Speed = 2f;

	private float moveTime;

	private Vector3 Scal = Vector3.one;

	private int islem = 1;

	private void FixedUpdate()
	{
		moveTime += Time.deltaTime * Speed;
		switch (islem)
		{
		case 1:
			Scal.x = Mathf.Lerp(1f, 0.8f, moveTime);
			Scal.y = Mathf.Lerp(1f, 1.2f, moveTime);
			if (Scal.x == 0.8f)
			{
				islem = 2;
				moveTime = 0f;
			}
			break;
		case 2:
			Scal.x = Mathf.Lerp(0.8f, 1f, moveTime);
			Scal.y = Mathf.Lerp(1.2f, 1f, moveTime);
			if (Scal.x == 1f)
			{
				islem = 1;
				moveTime = 0f;
			}
			break;
		}
		base.transform.localScale = Scal;
	}
}

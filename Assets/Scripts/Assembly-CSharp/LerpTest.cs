using UnityEngine;

public class LerpTest : MonoBehaviour
{
	private float Speed = 1f;

	private float moveTime;

	private Vector3 startLocation;

	private Vector3 enemyLocation;

	private void Start()
	{
		moveTime = 0f;
		startLocation = base.transform.position;
		enemyLocation = Vector3.one;
	}

	private void FixedUpdate()
	{
		moveTime += Time.deltaTime * Speed;
		base.transform.position = Vector3.Lerp(startLocation, enemyLocation, moveTime);
		if (base.transform.position == Vector3.one)
		{
			startLocation = base.transform.position;
			moveTime = 0f;
			enemyLocation = new Vector3(-2f, 3f, 0f);
		}
		if (base.transform.position.y == 3f)
		{
			moveTime = 0f;
			startLocation = new Vector3(-2f, 3f, 0f);
			enemyLocation = new Vector3(-2f, 2f, 0f);
		}
	}
}

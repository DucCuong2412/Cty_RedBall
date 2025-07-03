using UnityEngine;

public class LambaYanar : MonoBehaviour
{
	public Color Renk = Color.yellow;

	private SpriteRenderer yavruSR;

	private void Start()
	{
		GameObject gameObject = base.transform.GetChild(0).gameObject;
		if ((bool)gameObject)
		{
			yavruSR = gameObject.GetComponent<SpriteRenderer>();
		}
	}

	private void FixedUpdate()
	{
		float a = Mathf.PingPong(Time.time, 1f);
		Color renk = Renk;
		renk.a = a;
		yavruSR.color = renk;
	}
}

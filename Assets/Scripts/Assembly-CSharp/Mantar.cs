using UnityEngine;

public class Mantar : MonoBehaviour
{
	private void Start()
	{
		if (base.transform.parent != null)
		{
			base.transform.GetChild(0).transform.parent = base.transform.parent;
		}
		else
		{
			base.transform.GetChild(0).transform.parent = null;
		}
	}

	private void OnCollisionEnter2D(Collision2D kim)
	{
		if (kim.gameObject.tag == "Player")
		{
			Vector3 one = Vector3.one;
			one.y = 1.1f;
		}
	}

	private void Gerial()
	{
		base.transform.localScale = Vector3.one;
	}
}

using UnityEngine;

public class ufonot : MonoBehaviour
{
	private GameObject not;

	private void Start()
	{
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			if (base.transform.GetChild(num).gameObject.name == "not")
			{
				not = base.transform.GetChild(num).gameObject;
			}
		}
		not.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			not.SetActive(true);
			Object.Destroy(this);
		}
	}
}

using UnityEngine;

public class YuruyenMerdiven : MonoBehaviour
{
	private GameObject Merdi;

	private Vector3 Sol;

	private Vector3 Sag;

	private GameObject[] merdiler;

	private Vector3 direc;

	public float Hiz = 1f;

	private void Start()
	{
		merdiler = new GameObject[16];
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "yuruyen")
			{
				Merdi = gameObject;
				Merdi.transform.parent = null;
			}
			if (gameObject.name == "sol")
			{
				Sol = gameObject.transform.position;
			}
			if (gameObject.name == "sag")
			{
				Sag = gameObject.transform.position;
			}
		}
		direc = Sag - Sol;
		direc = direc.normalized;
		merdiler[0] = Merdi;
		Vector3 sol = Sol;
		for (int i = 1; i < 8; i++)
		{
			GameObject gameObject2 = Object.Instantiate(Merdi);
			gameObject2.name = "merdi_" + i;
			sol += direc.normalized * 1f;
			Vector3 position = sol;
			gameObject2.transform.position = position;
			merdiler[i] = gameObject2;
		}
		sol = Sol;
		for (int j = 8; j < 16; j++)
		{
			GameObject gameObject3 = Object.Instantiate(Merdi);
			gameObject3.name = "merdi_" + j;
			Vector3 position = sol;
			position.x -= 0.1f;
			gameObject3.transform.position = position;
			gameObject3.transform.Rotate(0f, 0f, 180f);
			merdiler[j] = gameObject3;
			sol += direc.normalized * 1f;
		}
		Merdi.GetComponent<Rigidbody2D>().velocity = Vector2.right * Hiz;
		int num2 = 1;
		for (int k = 0; k < 16; k++)
		{
			if (k > 7)
			{
				num2 = -1;
			}
			merdiler[k].GetComponent<Rigidbody2D>().velocity = direc * Hiz * num2;
		}
	}

	private void FixedUpdate()
	{
		for (int i = 0; i < 16; i++)
		{
			GameObject gameObject = merdiler[i];
			if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0f)
			{
				if (gameObject.transform.position.x > Sag.x)
				{
					gameObject.transform.position = Sag;
					if (gameObject.transform.rotation.eulerAngles.z != 180f)
					{
						gameObject.transform.Rotate(0f, 0f, -5f);
					}
					if (gameObject.transform.rotation.eulerAngles.z > 180f && gameObject.transform.rotation.eulerAngles.z < 181f)
					{
						gameObject.GetComponent<Rigidbody2D>().velocity = direc * (0f - Hiz);
					}
				}
			}
			else if (gameObject.transform.position.x < Sol.x)
			{
				gameObject.transform.position = Sol;
				if (gameObject.transform.rotation.eulerAngles.z != 0f)
				{
					gameObject.transform.Rotate(0f, 0f, -5f);
				}
				if (gameObject.transform.rotation.eulerAngles.z > 0f && gameObject.transform.rotation.eulerAngles.z < 1f)
				{
					gameObject.GetComponent<Rigidbody2D>().velocity = direc * Hiz;
				}
			}
		}
	}
}

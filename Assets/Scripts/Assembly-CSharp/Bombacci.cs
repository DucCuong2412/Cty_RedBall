using UnityEngine;

public class Bombacci : Pooler
{
	public float AtmaSuresi = 1f;

	public float HIZ = 1f;

	public bool RandomDagit;

	public float YoketmeMesafesi = 7f;

	private Vector3 ileriPos;

	private void Awake()
	{
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name == "ates")
			{
				gameObject.name = "BombacciBo";
				PoolerGo = gameObject;
				gameObject.transform.parent = null;
				ileriPos = gameObject.transform.position;
			}
		}
		PoolerAwake();
		InvokeRepeating("Firlat", AtmaSuresi, AtmaSuresi);
	}

	private void FixedUpdate()
	{
		for (int i = 0; i < DepoDizisi.Count; i++)
		{
			float num = Vector3.Distance(DepoDizisi[i].transform.position, base.transform.position);
			if (num > YoketmeMesafesi)
			{
				DepoDizisi[i].gameObject.SetActive(false);
			}
		}
	}

	private void Firlat()
	{
		GameObject gameObject = DepodanAl();
		if (!(gameObject == null))
		{
			gameObject.transform.position = base.transform.position;
			gameObject.SetActive(true);
			Vector2 vector = default(Vector2);
			vector.x = ileriPos.x - base.transform.position.x;
			vector.y = ileriPos.y - base.transform.position.y;
			if (RandomDagit)
			{
				vector.x += Random.Range(-0.1f, 0.1f);
			}
			gameObject.GetComponent<Rigidbody2D>().velocity = vector * HIZ;
		}
	}
}

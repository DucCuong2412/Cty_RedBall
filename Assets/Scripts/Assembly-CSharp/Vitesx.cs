using UnityEngine;

public class Vitesx : MonoBehaviour
{
	private HingeJoint2D hinge;

	private bool kine;

	private GameObject parenT;

	private void Start()
	{
		hinge = GetComponent<HingeJoint2D>();
		parenT = base.transform.parent.gameObject;
		GameObject gameObject = base.transform.parent.gameObject;
		if ((bool)gameObject.transform.parent)
		{
			base.transform.parent = gameObject.transform.parent;
		}
		else
		{
			base.transform.parent = null;
		}
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (!kine)
		{
			GetComponent<Rigidbody2D>().isKinematic = false;
			kine = true;
		}
	}

	private void FixedUpdate()
	{
		if (hinge.jointAngle > 40f)
		{
			parenT.GetComponent<VitesKolu>().Sagda();
		}
		if (hinge.jointAngle < -40f)
		{
			parenT.GetComponent<VitesKolu>().Solda();
		}
	}
}

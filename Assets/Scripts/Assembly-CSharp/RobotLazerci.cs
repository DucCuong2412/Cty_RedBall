using UnityEngine;

public class RobotLazerci : MonoBehaviour
{
	private Transform Redpos;

	private Animator ani;

	private bool bittim;

	private bool SagaBakiyor;

	private bool Acik;

	private bool animde;

	public bool YonSaga;

	private void Start()
	{
		ani = GetComponent<Animator>();
		Redpos = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void Flip()
	{
		SagaBakiyor = !SagaBakiyor;
		Vector3 localScale = base.transform.localScale;
		localScale.x *= -1f;
		base.transform.localScale = localScale;
	}

	private void Kapandi()
	{
		Acik = false;
		animde = false;
	}

	private void Acildi()
	{
		Acik = true;
		animde = false;
	}

	private void AtesBitti()
	{
		animde = false;
	}

	public void Update()
	{
		if (bittim)
		{
			return;
		}
		Vector2 end;
		Vector2 start = (end = base.transform.position);
		start.y = (end.y = base.transform.position.y + 0.8709388f);
		start.x = base.transform.position.x - 0.2542901f;
		end.x = base.transform.position.x + 0.2033188f;
		if ((bool)Physics2D.Linecast(start, end, 1 << LayerMask.NameToLayer("Oyuncu")))
		{
			if (Acik)
			{
				ani.SetTrigger("geberacik");
			}
			else
			{
				ani.SetTrigger("geberkapali");
			}
			bittim = true;
			Invoke("AniDisable", 3f);
		}
	}

	private void AniDisable()
	{
		ani.enabled = false;
		GameObject gameObject = null;
		for (int num = base.transform.childCount - 1; num >= 0; num--)
		{
			gameObject = base.transform.GetChild(num).gameObject;
			if (gameObject.name.Contains("patlak"))
			{
				gameObject.SetActive(false);
			}
		}
	}

	public void Oldur()
	{
		if (Acik)
		{
			ani.SetTrigger("geberacik");
		}
		else
		{
			ani.SetTrigger("geberkapali");
		}
		bittim = true;
		Invoke("AniDisable", 3f);
		bittim = true;
	}

	private void FixedUpdate()
	{
		if (animde || bittim)
		{
			return;
		}
		if (YonSaga)
		{
			if (Redpos.position.x < base.transform.position.x)
			{
				YonSaga = false;
			}
		}
		else if (base.transform.position.x < Redpos.position.x)
		{
			YonSaga = true;
		}
		if (YonSaga)
		{
			if (!SagaBakiyor)
			{
				Flip();
			}
		}
		else if (SagaBakiyor)
		{
			Flip();
		}
		float num = Vector3.Distance(Redpos.position, base.transform.position);
		if (num > 3f)
		{
			if (Acik)
			{
				ani.SetTrigger("kapat");
			}
			else
			{
				ani.SetTrigger("kapalizaten");
			}
		}
		else if (Acik)
		{
			ani.SetTrigger("ateset");
			animde = true;
		}
		else
		{
			ani.SetTrigger("ac");
			animde = true;
		}
	}
}

using UnityEngine;

public class ButtonTek : MonoBehaviour
{
	protected int DokunmaID = -1;

	protected Vector3 parnak;

	protected virtual void Basladi()
	{
	}

	protected virtual void Surtundu()
	{
	}

	protected virtual void UzakSurtundu()
	{
	}

	protected virtual void DokunmaBitti()
	{
	}

	private void Update()
	{
		if (!(base.gameObject.GetComponent<Collider2D>() != null))
		{
			return;
		}
		if (Input.touchCount < 1)
		{
			parnak = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Input.GetMouseButtonDown(0) && GetComponent<Collider2D>().OverlapPoint(new Vector2(parnak.x, parnak.y)))
			{
				DokunmaID = 1;
				Basladi();
			}
			if (Input.GetMouseButtonUp(0))
			{
				DokunmaID = -1;
				DokunmaBitti();
			}
			if (DokunmaID == 1)
			{
				if (GetComponent<Collider2D>().OverlapPoint(new Vector2(parnak.x, parnak.y)))
				{
					Surtundu();
				}
				else
				{
					UzakSurtundu();
				}
			}
			return;
		}
		for (int i = 0; i < Input.touchCount; i++)
		{
			parnak = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
			Vector2 point = new Vector2(parnak.x, parnak.y);
			if (base.gameObject.GetComponent<Collider2D>().OverlapPoint(point))
			{
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
					DokunmaID = Input.GetTouch(i).fingerId;
					Basladi();
				}
				if (Input.GetTouch(i).phase == TouchPhase.Moved)
				{
					DokunmaID = Input.GetTouch(i).fingerId;
					Surtundu();
				}
			}
			else if (Input.GetTouch(i).phase == TouchPhase.Moved && Input.GetTouch(i).fingerId != DokunmaID)
			{
				UzakSurtundu();
				DokunmaID = -1;
			}
			if (Input.GetTouch(i).phase == TouchPhase.Ended && Input.GetTouch(i).fingerId == DokunmaID)
			{
				DokunmaBitti();
				DokunmaID = -1;
			}
		}
	}
}

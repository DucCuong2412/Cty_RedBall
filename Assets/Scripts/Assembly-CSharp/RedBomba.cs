using System.Collections;
using UnityEngine;

public class RedBomba : MonoBehaviour
{
	private Animator anim;

	private float DaireCapi = 0.75f;

	public float HIZ = 3f;

	private float PatlamaZaman = 1.4f;

	private CameraFollow CamSc;

	private void Start()
	{
		anim = GetComponent<Animator>();
		Beklemede();
		CamSc = Camera.main.GetComponent<CameraFollow>();
	}

	private void Tazele()
	{
		anim.enabled = true;
		Invoke("PatlamaBaslat", PatlamaZaman);
		base.gameObject.GetComponent<SpriteRenderer>().enabled = true;
	}

	private IEnumerator AnimYukle()
	{
		yield return new WaitForEndOfFrame();
		anim = GetComponent<Animator>();
		anim.SetBool("patlama", false);
		anim.SetBool("yeniden", true);
		Tazele();
	}

	public void Beklemede()
	{
		if (anim == null)
		{
			AnimYukle();
		}
		else
		{
			Tazele();
		}
	}

	private void GizleGo()
	{
		base.gameObject.SetActive(false);
	}

	private void PatlamaBaslat()
	{
		CamSc.BombaPatla();
		anim.SetBool("patlama", true);
		anim.SetBool("yeniden", false);
		LayerMask layerMask = (1 << LayerMask.NameToLayer("haraketli")) | (1 << LayerMask.NameToLayer("Oyuncu"));
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, DaireCapi, layerMask);
		Collider2D[] array2 = array;
		Vector2 vector = default(Vector2);
		foreach (Collider2D collider2D in array2)
		{
			Rigidbody2D component = collider2D.gameObject.GetComponent<Rigidbody2D>();
			if (component != null)
			{
				vector.x = collider2D.gameObject.transform.position.x - base.transform.position.x;
				vector.y = collider2D.gameObject.transform.position.y - base.transform.position.y + 0.3f;
				if (collider2D.gameObject.tag == "Player")
				{
					component.velocity = Vector2.up * HIZ * 2f;
				}
				else
				{
					component.velocity = vector.normalized * HIZ;
				}
			}
		}
		Collider2D[] array3 = Physics2D.OverlapCircleAll(base.transform.position, DaireCapi, 1 << LayerMask.NameToLayer("Kirilan"));
		Collider2D[] array4 = array3;
		foreach (Collider2D collider2D2 in array4)
		{
			KirBlokTugla component2 = collider2D2.GetComponent<KirBlokTugla>();
			if (component2 != null)
			{
				component2.BombaPatladi(base.transform.position);
			}
		}
		LayerMask layerMask2 = (1 << LayerMask.NameToLayer("Bocek")) | (1 << LayerMask.NameToLayer("Robot"));
		Collider2D[] array5 = Physics2D.OverlapCircleAll(base.transform.position, DaireCapi, layerMask2);
		Collider2D[] array6 = array5;
		foreach (Collider2D collider2D3 in array6)
		{
			DusmanDead component3 = collider2D3.gameObject.GetComponent<DusmanDead>();
			if (component3 != null)
			{
				component3.Oldur();
			}
		}
		Invoke("GizleGo", 0.2f);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(base.transform.position, DaireCapi);
	}
}

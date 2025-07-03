using UnityEngine;

public class Tutor5 : MonoBehaviour
{
	private bool bitti;

	private GameObject Ok1;

	private Player PlayerSc;

	private void Start()
	{
		Ok1 = GameObject.Find("okkey5");
		Ok1.SetActive(false);
		PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	private void OnTriggerEnter2D(Collider2D kim)
	{
		if (!bitti && kim.gameObject.tag == "Player" && !bitti && PlayerSc.KalkanAktif)
		{
			Ok1.SetActive(true);
			bitti = true;
			CameraFollow component = Camera.main.GetComponent<CameraFollow>();
			if ((bool)component)
			{
				component.TutorOKSesi();
			}
			base.gameObject.SetActive(false);
		}
	}
}

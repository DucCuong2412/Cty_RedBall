using UnityEngine;

public class KirBlokSupriz : KirBlokTugla
{
	public GameObject FirlayacakGo;

	private Vector3 ilkpos;

	protected override void YamukGoster()
	{
		base.YamukGoster();
		FirlayacakGo.SetActive(true);
	}

	private void Awake()
	{
		FirlayacakGo.SetActive(false);
	}
}

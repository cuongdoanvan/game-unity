using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	public Transform weaponHold;// liên kết súng với người chơi
	public Gun[] allGuns;
	Gun equippedGun;
	void Start()
	{

	}
	// tuong ung 3 kieu ban
	
	public void EquipGun(Gun gunToEquip) // khi tạo ra 1 kiểu súng mới xóa súng cũ
	{
		if (equippedGun != null)
		{
			Destroy(equippedGun.gameObject);
		}
		equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Gun;// vị trí khẩu súng 
		equippedGun.transform.parent = weaponHold;
	}

	public void OnTriggerHold()
	{
		if (equippedGun != null)
		{
			equippedGun.OnTriggerHold();
		}
	}

	public void OnTriggerRelease()
	{
		if (equippedGun != null)
		{
			equippedGun.OnTriggerRelease();
		}
	}
	//so thu tu sung
   public void EquipGun(int weaponIndex)
    {
		EquipGun(allGuns[weaponIndex]);
    }

    // chieu cao vong tron tam ban
    public float GunHeight
	{
		get
		{
			return weaponHold.position.y;
		}
	}
}

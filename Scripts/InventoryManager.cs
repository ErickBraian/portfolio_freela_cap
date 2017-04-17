using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour {

	public GameObject[] weapons;
	bool[] weaponAvailable;
	public Image weaponImage;

	int currentWeapon;

	// Use this for initialization
	void Start () {
		weaponAvailable = new bool[weapons.Length];
		for (int i = 0; i < weapons.Length; i++){
			weaponAvailable [i] = false;
			}
		currentWeapon = 0;
		//weaponAvailable [currentWeapon] = true;
		for (int i = 0; i < weapons.Length; i++){
			weaponAvailable [i] = true;
		}

		deactivateWeapons();

		setWeaponActive (currentWeapon);
	}
	
	// Update is called once per frame
	void Update () {
		//toggle weapon
		if (Input.GetButtonDown("Submit")) {
			int i;
			for (i = currentWeapon+1; i<weapons.Length; i++) {
				if (weaponAvailable[i] == true) {
					currentWeapon = i;
					setWeaponActive (currentWeapon);
					return;
				}
			}
			for (i = 0; i < currentWeapon; i++) {
				if (weaponAvailable[i] == true) {
					currentWeapon = i;
					setWeaponActive (currentWeapon);
					return;
				}
			}
		}
	}

	public void setWeaponActive(int whichWeapon){
		if (!weaponAvailable[whichWeapon]) {
			return;
		}
		deactivateWeapons ();
		weapons [whichWeapon].SetActive (true);
		weapons [whichWeapon].GetComponentInChildren<FireBullet> ().initializeWeapon ();
	}

	public void deactivateWeapons(){
		for (int i = 0; i < weapons.Length; i++) {
			weapons [i].SetActive (false);
		}
	}

	public void activateWeapons(int whichWeapon){
		weaponAvailable [whichWeapon] = true;
	}
}

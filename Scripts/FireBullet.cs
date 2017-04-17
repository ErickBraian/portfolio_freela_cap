using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireBullet : MonoBehaviour {

	public float timeBetweenBullets = 0.15f;	//tempo entre disparos
	public GameObject projectile;				//bala que eh instanciada 
	public GameObject BulletSpawn;				//posicao para spawn das balas

	//Bullet info
	//public Image playerAmmo;
	public Slider playerAmmoSlider;
	public Text ammoText;
	//public int minRounds;
	//public int maxRounds;
	public int startingRounds;					//quantidade inicial de balas na arma
	float remainingRounds;						//quantida atual de balas na arma

	float nextBullet;							//tempo peritido para o proximo disparo
	//float calculateAmmo;
	//float outputAmmo;

	public float maxAmmoValue = 999;			//quantidade maxima de balas

	//audio info
	AudioSource bulletSpawnAS;
	public AudioClip shootSound;
	public AudioClip reloadSound;

	PlayerController myPlayer;

	//graphic info
	public Sprite weaponSprite;
	public Image weaponImage;

	// Use this for initialization
	void Awake () {
		nextBullet = 0f;
		//playerAmmo.fillAmount = maxRounds;
		//playerAmmo.fillAmount = remainingRounds;
		//startingRounds = 100;
		remainingRounds = startingRounds;
		playerAmmoSlider.value = startingRounds;
		bulletSpawnAS = GetComponent<AudioSource>();
		myPlayer = transform.root.GetComponent<PlayerController> ();
	}
		
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Fire1") > 0 && nextBullet<Time.time && remainingRounds>0)
		{
			//Sets when you can shoot the next bullet
			nextBullet = Time.time + timeBetweenBullets;
			Vector3 rot;
			//If you're facing right, you're gonna shoot right and vice-versa
			if (myPlayer.GetFacing () == -1f)
			{
				rot = new Vector3 (0, -90, 0);
			} else {
				rot = new Vector3 (0, 90, 0);
			}

			//Instantiate the bullet (Quaternion is the rotation of the object)
			Instantiate(projectile, transform.position, Quaternion.Euler(rot));

			playASound (shootSound);

			remainingRounds -= 1f;
			playerAmmoSlider.value = remainingRounds;
		}

		ammoText.text = remainingRounds.ToString ();
	}

	public void reload(int amount){
		remainingRounds += amount;
		if (remainingRounds > maxAmmoValue)
		{
			remainingRounds = maxAmmoValue;
		}
		playerAmmoSlider.value = remainingRounds;
		playASound (reloadSound);
	}

//	public void SetAmmo(){
//		calculateAmmo = remainingRounds / maxAmmoValue;
//		outputAmmo = calculateAmmo * (maxRounds - minRounds) + minRounds;
//
//		playerAmmo.fillAmount = Mathf.Clamp (outputAmmo, minRounds, maxRounds);
//	}

	void playASound(AudioClip playTheSound){
		bulletSpawnAS.clip = playTheSound;
		bulletSpawnAS.Play ();
	}

	public void initializeWeapon() {
		bulletSpawnAS.clip = reloadSound;
		bulletSpawnAS.Play ();
		nextBullet = 1f;
		playerAmmoSlider.value = remainingRounds;
		weaponImage.sprite = weaponSprite;
	}
}
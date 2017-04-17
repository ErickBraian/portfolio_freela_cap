using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[Header("Spawner Properties")]
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] int maxEnemies = 10;
	[SerializeField] float spawnRate = 5f;

	EnemyHealth[] enemies; //(polling) em array.
//	List<EnemyHealth> enemies; //(polling) em lista.

	WaitForSeconds spawnDelay; //otimiza o waitForSeconds

	void Awake()
	{
		//Cria um array para armazenar o POLL de objetos
		enemies = new EnemyHealth[maxEnemies];  	//Quantidade dos inimigos dentro do poolling em array.
//		enemies = new List<EnemyHealth>(); 			//Quantidade dos inimigos dentro do poolling em Lista.

		//Percorre o array, criando objetos e preenchendo-o com a referência para este

		int tamanhoDoArray = enemies.Length;
		for (int i = 0; i < tamanhoDoArray; i++)
		{
			GameObject obj = Instantiate (enemyPrefab) as GameObject; //Cast
			EnemyHealth enemy = obj.GetComponent<EnemyHealth> ();
			obj.transform.SetParent (transform);
			obj.SetActive (false);
			enemies [i] = enemy;
		}
		spawnDelay = new WaitForSeconds (spawnRate);
	}

	IEnumerator Start()
	{
		while (true)
		{
			yield return spawnDelay;
			SpawnEnemy ();
		}
	}

	void SpawnEnemy()
	{
		for (int i = 0; i < maxEnemies; i++)
		{
			if (!enemies[i].gameObject.activeSelf)
			{
				enemies [i].transform.position = transform.position;
				enemies [i].transform.rotation = transform.rotation;
				enemies [i].gameObject.SetActive (true);
				return;
			}
		}
	}
}
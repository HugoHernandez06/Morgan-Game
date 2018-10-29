using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour {

    public GameObject enemyPrebab;
    public float generatorTime = 1.8f;
    

    // Use this for initialization
    void Start() {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateEnemy()
    {
        Instantiate(enemyPrebab, transform.position, Quaternion.identity);
        

    }

    public void StartGenerator()
    {
        InvokeRepeating("CreateEnemy", 0f, generatorTime);
    }

    public void CancelGenerator(bool clean = false)
    {
        CancelInvoke("CreateEnemy");
        if (clean)
        {
            object[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach( GameObject enemy in allEnemies)
            {
                Destroy(enemy);
            }
        }
    }

}

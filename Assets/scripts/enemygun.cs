using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemygun : gun

{
    gun gun;
    
    public Transform enemytransform;
    public float dist_enemy_player;
   
    public EnemyAI enemyAI;


   

    // Start is called before the first frame update
    void Start()
    {
        
        tip = transform.Find("tip").gameObject;
        source = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        gun = player.GetComponentInChildren<gun>(); 
        enemytransform = gameObject.GetComponentInParent<Transform>();
        enemyAI = gameObject.GetComponentInParent<EnemyAI>();
        bloodimpact = gun.bloodimpact;
        hitimpact = gun.hitimpact;







    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        dist_enemy_player = Vector3.Distance(player.transform.position, enemytransform.position);
        if (dist_enemy_player < enemyAI.sensingRange)
        {
            Quaternion quaternion = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * 5f);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            
            
        }

        

    }
}

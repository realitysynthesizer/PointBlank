using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour


{
    public Vector3 aimdirection;
    public GameObject player;
    Health playerhealth;
    public GameObject levelcompleter;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        player = GameObject.FindWithTag("Player");
        playerhealth = player.GetComponent<Health>();
        levelcompleter = GameObject.Find("levelcompleter");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerhealth.GetCurrentHealth() <= 0)
        {
            StartCoroutine(GameOver());
        
        }
        
        
    }

    IEnumerator GameOver()
    {

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("gameover");
    }
}

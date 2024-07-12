using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

public class gun : MonoBehaviour
{
    
    public GameObject tip;
    public GameObject player;
    public RectTransform crosshairrect;
    Camera cam;
    Quaternion lastrotation;
    
    public Vector3 hitpoint;
    public GameObject bullet;
    public GameObject muzzleflash;
    public AudioSource source;
    public float firedelay = 0.1f;
    public float canfire;
    public TemporalOdyssey inputasset;
    public gamemanager gamemanager;
    public GameObject bloodimpact;



    // Start is called before the first frame update
    void Start()
    {
        tip = transform.Find("tip").gameObject;
        player = GameObject.Find("player");
        cam = Camera.main;
        source = GetComponent<AudioSource>();
        canfire = Time.time;
        
        inputasset = new TemporalOdyssey();
        inputasset.Player.Enable();
        gamemanager = GameObject.Find("gamemanager").GetComponent<gamemanager>();
        




    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane tipplane = new Plane(Vector3.up, tip.transform.position);
        tipplane.Raycast(ray, out float distance);
        Vector3 hitpoint = ray.GetPoint(distance);   
        gamemanager.aimdirection = (hitpoint - tip.transform.position);
        Quaternion temprotation = Quaternion.LookRotation(gamemanager.aimdirection);
        Vector3 euler = temprotation.eulerAngles;
        euler.y += 40; //rotation offset for animation correction
        temprotation = Quaternion.Euler(euler);
       
        if (gamemanager.aimdirection.magnitude < 1)
        {
            player.transform.rotation = lastrotation;
        }
        else
        {
            player.transform.rotation=Quaternion.Lerp(player.transform.rotation, temprotation, Time.deltaTime* 8f);
      
            lastrotation = player.transform.rotation;
        }
        
     
        crosshairrect.position = cam.WorldToScreenPoint(hitpoint);
        

        if (inputasset.Player.Fire.inProgress)
        {
            if (Time.time > canfire)
            {
                shoot();
                canfire = Time.time + firedelay;
            }
        }

    }

    public void shoot()
    {
        Instantiate(bullet, tip.transform.position, tip.transform.rotation);
        source.Play();
        Instantiate(muzzleflash, tip.transform);
        if (Physics.Raycast(tip.transform.position, tip.transform.forward, out RaycastHit hit, 100f))
        {
            
            Health health = hit.collider.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(5);
                Instantiate(bloodimpact, hit.point, Quaternion.LookRotation(hit.normal));

            }
            
        }

    }

    




}

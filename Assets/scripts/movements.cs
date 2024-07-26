
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class movements : MonoBehaviour
{
    public float movementsensitivity;
    public float speeddecrement;
    public TemporalOdyssey inputasset;
    public Camera cam;
    public Animator animator;
    public GameObject bullet;
    public PlayerShooting playershooting;
    public float speedincrement;
    
    public Vector3 centre;
    public Vector3 screenspacecentre;
    public Vector3 distance;
    public Vector3 dist_cam_player;
    public Vector3 mousepos;
    public gamemanager gamemanager;
 
    private void Awake()
    {
        
        inputasset = new TemporalOdyssey();
        inputasset.Player.Enable();
       
        screenspacecentre = new Vector3(Screen.width / 2, Screen.height / 2, 0);

    }
    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("gamemanager").GetComponent<gamemanager>();
        cam = Camera.main;
        animator = GetComponent<Animator>();
        speedincrement = 1.5f;
        playershooting = GetComponent<PlayerShooting>();

        inputasset.Player.sprint.started += ctx =>

        {
            movementsensitivity *= speedincrement;
            animator.SetFloat("movementspeedmultiplier", speedincrement);
        };



        inputasset.Player.sprint.canceled += ctx =>
        {
            movementsensitivity /= speedincrement;
            animator.SetFloat("movementspeedmultiplier", 1);
        };


    }

    private void Update()
    {
        
        Vector2 move = inputasset.Player.Move.ReadValue<Vector2>();
        if (move.magnitude != 0)
        {
            Vector2 face = new Vector2(gamemanager.aimdirection.normalized.x, gamemanager.aimdirection.normalized.z);

            float angle = Vector2.SignedAngle(move, face);

            if (angle > -45 && angle < 45)
            {
                animator.SetInteger("state", 1);
            }
            else if (angle > 45 && angle < 135)
            {
                animator.SetInteger("state", 3);
            }
            else if (angle > 135 || angle < -135)
            {
                animator.SetInteger("state", 4);
            }
            else if (angle > -135 && angle < -45)
            {
                animator.SetInteger("state", 2);
            }


            transform.position += new Vector3(move.x * Time.deltaTime * movementsensitivity, 0, move.y * Time.deltaTime * movementsensitivity);

        }
        else
        {
            animator.SetInteger("state", 0);
        }

        if (playershooting.currentAmmo > 0)
        {
            

            if (inputasset.Player.Fire.triggered)
            {
                animator.SetInteger("fire", 1);
                
            }


            if (inputasset.Player.Fire.ReadValue<float>()==0)
            {
                if (animator.GetInteger("fire") == 1)
                {
                    animator.SetInteger("fire", 0);

                }
            }
        
        }
        else
        {
            if (inputasset.Player.Fire.triggered)
            {
                playershooting.emptyshotsound.Play();

            }
        }

        
        

    }

    private void OnDisable()
    {
        inputasset.Player.Disable();

    }




}

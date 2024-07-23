
using UnityEngine;
using UnityEngine.UIElements;


public class movements : MonoBehaviour
{
    public float movementsensitivity;
    public float speeddecrement;
    public TemporalOdyssey inputasset;
    public Camera cam;
    public Animator animator;
    public GameObject bullet;
    
    
    
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



        inputasset.Player.Fire.started += ctx =>
        {
            animator.SetInteger("fire", 1);

        };



        inputasset.Player.Fire.canceled += ctx =>
        {
            animator.SetInteger("fire", 0);
        };

        inputasset.Player.sprint.started += ctx =>

        {
            movementsensitivity *= 1.5f;
            animator.speed = 1.5f;
        };

        inputasset.Player.sprint.canceled += ctx =>
        {
            movementsensitivity /= 1.5f;
            animator.speed = 1;
        };

        

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        

        




    }

    






}

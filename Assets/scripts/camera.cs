using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;      
    public float lookAheadDistance = 2f;
    public gamemanager manager;
    public Vector3 dist_cam_player;
    public Vector3 cam_initialpos;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        manager = GameObject.Find("gamemanager").GetComponent<gamemanager>();
        cam_initialpos = transform.position;
        dist_cam_player = cam_initialpos - player.transform.position;





    }


    private void Update()
    {
        if (player == null)
        {
            return;
        }

        cam_initialpos = new Vector3(player.transform.position.x + dist_cam_player.x, cam_initialpos.y, player.transform.position.z + dist_cam_player.z);




        Vector3 desiredPosition = cam_initialpos + manager.aimdirection.normalized * lookAheadDistance;
            Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, Time.deltaTime * 5f);
            transform.position = smoothedPosition;
           




        

        

    }
}

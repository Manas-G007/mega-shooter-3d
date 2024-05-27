using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 lastPos;

    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    public Vector3 LastPos { get => lastPos; set => lastPos = value; }

    public Path path;

    [Header("Sight Values")]
    public float sightDistance=5f;
    public float fieldView = 85f;
    public float eyeHeight;

    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;
    // debug state
    [SerializeField]
    private string currState;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        eyeHeight = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            // checking distance
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                // angle height
                var angleHeight = Vector3.up * eyeHeight;
                // finding angle
                Vector3 targetDir =  player.transform.position - transform.position - angleHeight;
                float angleToPlayer = Vector3.Angle(targetDir, transform.forward);

                // checking FOV
                if (angleToPlayer>=-fieldView && angleToPlayer <= fieldView)
                {
                    Ray ray = new(transform.position + angleHeight,targetDir);
                    RaycastHit hitInfo = new();
                    Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                    if (Physics.Raycast(ray,out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player) return true;
                    }
                }
            };
        }
        return false;
    }
}

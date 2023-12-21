using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlanetCoreController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject circle;

    [SerializeField] int id;
    [SerializeField] bool inRadius;
    Vector3 scale;
    Rigidbody rb_player;
    Rigidbody rb;
    public float len;
    //private float K = 0.5f; //максимум 15 

    private void Start()
    {
        rb_player = player.GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        circle.SetActive(true);
        inRadius = false;
        scale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    private void Update()
    {
        circle.transform.localScale = Vector3.Lerp(circle.transform.localScale, scale, Time.deltaTime);
    }

    private void FixedUpdate()
    {

        if (player != null && inRadius && (transform.position - rb_player.position).magnitude <= 15)
        {
            Vector3 vect = transform.position - player.transform.position;

            rb_player.AddForce(-GameController.springForse * (len - vect.magnitude) *
                               vect.normalized);

            float drag = GameController.springForse / 5f;
            float velocity = Vector3.Dot(rb_player.velocity,
                -vect.normalized) * drag;
            float coeff = Mathf.Clamp01(1 - Time.fixedDeltaTime * drag);

            rb_player.AddForce(vect.normalized * (velocity / coeff) *
                                    rb_player.mass);

            rb_player.AddForce(rb.mass * rb_player.mass / vect.magnitude * vect.normalized);

        }
    }


    private void OnEnable()
    {
        GameController.onConnectPlanet += ConnectedPlanet;
        GameController.onMouseClickDown += MouseClickDown;
    }

    private void OnDisable()
    {
        GameController.onConnectPlanet -= ConnectedPlanet;
        GameController.onMouseClickDown -= MouseClickDown;
    }

    private void ConnectedPlanet(int id)
    {
        if (this.id == id)
        {
            inRadius = true;
            scale = new Vector3(3.4f, 3.4f, 3.4f);
            len = (player.transform.position - transform.position).magnitude;
        }
        if(this.id + 1 == id)
        {
            scale = Vector3.zero;
            delete();
        }
    }

    private void MouseClickDown()
    {
        inRadius = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameController.onGameOver?.Invoke("Столкновение с планетой");
    }

    public void delete()
    {
        GameObject gameObject = Instantiate(parent, new Vector3((float)((new System.Random().NextDouble() * 24) - 12), 0, transform.position.z + 60), Quaternion.identity);      
        gameObject.GetComponentInChildren<PlanetCoreController>().Id = id + 2;
        gameObject.GetComponentInChildren<PlanetOrbitController>().Id = id + 2;
        gameObject.GetComponentsInChildren<SphereCollider>()[1].enabled = true;
        Destroy(parent, 5f);
    }

    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }
}

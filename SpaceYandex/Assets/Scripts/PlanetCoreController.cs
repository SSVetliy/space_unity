using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlanetCoreController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject circle;

    [SerializeField] PlanerMaterials planerMaterials;

    [SerializeField] int id;
    [SerializeField] bool inRadius;

    bool isGameOver = false;

    Vector3 scale;
    Rigidbody rb_player;
    Rigidbody rb;
    public float len;
    static float springForse;

    private void Start()
    {
        rb_player = player.GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        circle.SetActive(true);
        circle.transform.localScale = Vector3.zero;
        inRadius = false;
        scale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    private void Update()
    {
        circle.transform.localScale = Vector3.Lerp(circle.transform.localScale, scale, 2 * Time.deltaTime);
    }

    private void FixedUpdate()
    {

        if (player != null && inRadius && (transform.position - rb_player.position).magnitude <= 15)
        {
            Vector3 vect = transform.position - player.transform.position;

            float drag = springForse / 5f;
            float velocity = Vector3.Dot(rb_player.velocity,
                -vect.normalized) * drag;
            float coeff = Mathf.Clamp01(1 - Time.fixedDeltaTime * drag);

            rb_player.AddForce((rb_player.mass * ((velocity / coeff) + (rb.mass / vect.magnitude))
                               + (-springForse * (len - vect.magnitude))) * vect.normalized);
        }
    }


    private void OnEnable()
    {
        GameController.onConnectPlanet += ConnectedPlanet;
        GameController.onMouseClickDown += MouseClickDown;
        GameController.onGameOver += GameOver;
        GameController.onPlayGame += PlayGame;
    }

    private void OnDisable()
    {
        GameController.onConnectPlanet -= ConnectedPlanet;
        GameController.onMouseClickDown -= MouseClickDown;
        GameController.onGameOver -= GameOver;
        GameController.onPlayGame -= PlayGame;
    }

    void PlayGame()
    {
       springForse = SaveLoadData.DATA.springForce;
    }

    private void ConnectedPlanet(int id)
    {
        if (this.id == id)
        {
            inRadius = true;
            player.GetComponent<PlayerController>().CurPlanetId = id;
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

    void GameOver(string str)
    {
        isGameOver = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isGameOver)
            GameController.onGameOver?.Invoke("Столкновение с планетой");
    }

    public void delete()
    {
        GameObject gameObject = Instantiate(parent, new Vector3((float)((new System.Random().NextDouble() * 24) - 12), 0, transform.position.z + 60), Quaternion.identity);      
        gameObject.GetComponentInChildren<PlanetCoreController>().Id = id + 2;
        gameObject.GetComponentInChildren<PlanetOrbitController>().Id = id + 2;
        gameObject.GetComponentsInChildren<SphereCollider>()[1].enabled = true;

        Material[] mat = new Material[3];
        for(int i = 0; i < 3; i++)
            mat[i] = planerMaterials.GetArrMaterial[new System.Random().Next(0, planerMaterials.GetArrMaterial.Length)];
        
        gameObject.GetComponentsInChildren<Renderer>()[0].materials = mat;

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

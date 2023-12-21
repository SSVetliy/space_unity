using System.Collections;
using UnityEngine;

public class PlanetOrbitController : MonoBehaviour
{
    [SerializeField] int id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")){
            GameController.onConnectPlanet?.Invoke(id);
            GetComponent<SphereCollider>().enabled = false;
        }
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

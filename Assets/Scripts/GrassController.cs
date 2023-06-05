using UnityEngine;

public class GrassController : MonoBehaviour
{
    GameObject playerGameObject;
    Material grassMaterial;
    Vector3 playerPosition;

    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        grassMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        playerPosition = playerGameObject.transform.position;
        grassMaterial.SetVector("_TrackedPosition", playerPosition);
    }
}
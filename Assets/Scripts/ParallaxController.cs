using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    Vector3 playerStartPos;
    float distance; // distance between the player start position and current position
    GameObject[] Bckgnd;
    Material[] mat;
    float[] backSpeed;
    float farthestBack;
    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    void Start()
    {
        playerStartPos = player.position;
        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        Bckgnd = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            Bckgnd[i] = transform.GetChild(i).gameObject;
            mat[i] = Bckgnd[i].GetComponent<Renderer>().material;
        }

        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++) // find the farthest background
        {
            if ((Bckgnd[i].transform.position.z - player.position.z) > farthestBack)
            {
                farthestBack = Bckgnd[i].transform.position.z - player.position.z;
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (Bckgnd[i].transform.position.z - player.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance = player.position.x - playerStartPos.x;

        for (int i = 0; i < Bckgnd.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            Vector2 offset = mat[i].mainTextureOffset;
            offset.x = distance * speed;
            mat[i].mainTextureOffset = offset;
        }
    }
}

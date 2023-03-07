using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] GameObject[] trashItems;
    private float spawnTime;
    private float spawnRotation;
    private int spawnItem;

    private int currentAmount = 0;
    [SerializeField] private int maxSpawnAmount = 7;

    private float timer = 0f;
    private BoxCollider coll;

    [SerializeField] private float boatSpeed = 1;

    private Rigidbody boatRb;
    private float depthB4Submerged = 1f;
    private float displacementAmount = 3f;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<BoxCollider>() != null)
        {
            coll = GetComponent<BoxCollider>();
        }
        boatRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (transform.position.y < 0f)
        {
            float displacementMult = Mathf.Clamp01(-transform.position.y / depthB4Submerged) * displacementAmount;
            boatRb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMult, 0f), ForceMode.Acceleration);

        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
        
            if (currentAmount < maxSpawnAmount)
            {
                spawnItem = Random.Range(0, trashItems.Length);
                spawnTime = Random.Range(1f, 3f);
                spawnRotation = Random.Range(0f, 360f);
                timer += Time.deltaTime;
                
            }
            else
            {
                
                timer -= Time.deltaTime;
                timer = 0;
            }
            if (timer >= spawnTime && trashItems.Length != 0 )
            {
                timer = 0f;
                
                Instantiate(trashItems[spawnItem], transform.position + coll.center,
                Quaternion.Euler(spawnRotation, spawnRotation, spawnRotation));
                
                currentAmount++;

                return;
            }
        
    }
    void Move()
    {
        // change trans right to forward once pivot gets fixed
        transform.position += -transform.right * boatSpeed * Time.deltaTime;
    }
}

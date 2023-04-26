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
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<BoxCollider>() != null)
        {
            coll = GetComponent<BoxCollider>();
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
        transform.position += -transform.right * boatSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * (2 * Time.deltaTime));
    }
}

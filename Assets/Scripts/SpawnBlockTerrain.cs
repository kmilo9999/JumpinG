using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnBlockTerrain : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject[] terrainColumnPrefabs;
    private float currenCounterTime = 0;
    private int numBlocks = 0;
    private float currentPosition = 0f;
    private Vector3 worldLowerRight;
    private Vector3 worldLowerLeft;
    private bool[] columsOnScreen;
   
    void Start()
    {
        
        worldLowerRight = Camera.main.ViewportToWorldPoint(Vector3.zero);
        worldLowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(1.0f,0.0f,0.0f));
        var distance = worldLowerLeft.x - worldLowerRight.x;
        numBlocks = (int) (distance / 0.5);
        columsOnScreen = Enumerable.Repeat(false, numBlocks).ToArray();
        currentPosition = worldLowerRight.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        currenCounterTime += Time.deltaTime;
        if (currenCounterTime > 3)
        {
            createBlock();
            currenCounterTime = 0;
        }
    }

    private void createBlock()
    {
        var randomPosition = 0;
        do
        {
            randomPosition = Random.Range(0, numBlocks);
        }
        while (columsOnScreen[randomPosition]);
        
        // mapping array index to world coordinates
        var worldPosition = 0.0f;
        if (randomPosition % 2 == 0)    
        {
            worldPosition = randomPosition / 2;
        }
        else
        {
            worldPosition = ((randomPosition - 1) / 2) + 0.5f;
        }
        
        var blockPosition = new Vector3(worldLowerRight.x + worldPosition + 0.25f, worldLowerRight.y + 0.25f, 0); 
        spawnColumn(Random.Range(0, terrainColumnPrefabs.Length), blockPosition);
        columsOnScreen[randomPosition] = true;
        
    }

    private void spawnColumn(int columnType, Vector3 worldPosition)
    {
        GameObject.Instantiate(terrainColumnPrefabs[columnType], worldPosition, new Quaternion());
    }
}

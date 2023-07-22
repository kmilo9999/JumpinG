using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnBlockTerrain : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject terrainColumnPrefab;
    private float currenCounterTime = 0;
    private Renderer renderComponent;
    private int numBlocks = 0;
    private float currentPosition = 0f;
    private Vector3 worldLowerRight;
    private Vector3 worldLowerLeft;
    private bool[] columsOnScreen;
    private int currentBlocks = 0;
    void Start()
    {
        //renderComponent = terrainColumnPrefab.GetComponent<Renderer>();
        worldLowerRight = Camera.main.ViewportToWorldPoint(Vector3.zero);
        worldLowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(1.0f,0.0f,0.0f));
        var distance = worldLowerLeft.x - worldLowerRight.x;
        numBlocks = (int) (distance / 0.5);
        columsOnScreen = Enumerable.Repeat(false, numBlocks).ToArray();
        currentPosition = worldLowerRight.x;
        //Debug.Log(numBlocks);
    }

    // Update is called once per frame
    void Update()
    {
        currenCounterTime += Time.deltaTime;
        if (currenCounterTime > 2)
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
            //randomPosition = 0;
        }
        while (columsOnScreen[randomPosition]);

        //var randomPosition = Random.Range(worldLowerRight.x, worldLowerLeft.x);
        // rescale array position 0-numBlocks to viewport position 0-1
        
        var worldPosition = 0.0f;
        if (randomPosition % 2 == 0)
        {
            worldPosition = randomPosition / 2;
        }
        else
        {
            worldPosition = ((randomPosition - 1) / 2) + 0.5f;
        }
        
        Debug.Log(randomPosition+" "+worldPosition);
        //var blockPosition = new Vector3(currentPosition+0.25f, worldLowerRight.y + 0.25f, 0);
        var blockPosition = new Vector3(worldLowerRight.x + worldPosition + 0.25f, worldLowerRight.y + 0.25f, 0);
        //currentPosition += 0.5f;
        GameObject.Instantiate(terrainColumnPrefab, blockPosition, new Quaternion());
        columsOnScreen[randomPosition] = true;
        //currentBlocks++;
        //Debug.Log(currentBlocks);



    }
}

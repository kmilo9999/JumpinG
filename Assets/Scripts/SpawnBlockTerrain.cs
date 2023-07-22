using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int[] columsOnScreen;
    private int currentBlocks = 0;
    void Start()
    {
        //renderComponent = terrainColumnPrefab.GetComponent<Renderer>();
        worldLowerRight = Camera.main.ViewportToWorldPoint(Vector3.zero);
        worldLowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(1.0f,0.0f,0.0f));
        var distance = worldLowerLeft.x - worldLowerRight.x;
        numBlocks = (int) (distance / 0.5);
        columsOnScreen = new int[numBlocks];
        currentPosition = worldLowerRight.x;
        Debug.Log(numBlocks);
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
        var randomPosition = Random.Range(worldLowerRight.x, worldLowerLeft.x);
       
        var blockPosition = new Vector3(currentPosition+0.25f, worldLowerRight.y + 0.25f, 0);
        currentPosition += 0.5f;
        GameObject.Instantiate(terrainColumnPrefab, blockPosition, new Quaternion());
        currentBlocks++;
        Debug.Log(currentBlocks);



    }
}

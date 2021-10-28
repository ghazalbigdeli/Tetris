using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public GameObject[] blocks;
    public GameObject[] ghostBlocks;

    // Start is called before the first frame update
    void Start()
    {
        newBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newBlock()
    {
        int x = Random.Range(0, 6);
        Instantiate(blocks[x], transform.position, Quaternion.identity);
        Instantiate(ghostBlocks[x], transform.position - new Vector3(0,8,0), Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGhostBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (ValidMoves() == false)
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (ValidMoves() == false)
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // rotate
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            if (ValidMoves() == false)
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.enabled = false;
            Destroy(this.gameObject);
            //FinalizeBlock();
        }
        else if (Time.time - previousTime > (Input.GetKeyDown(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;

            if (ValidMoves() == false)
            {
                //FinalizeBlock();
                Destroy(this.gameObject);
            }
        }
    }


    bool ValidMoves()
    {
        
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedY < 0 || roundedX >= width || roundedY >= height || TetrisBlock.grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }
}

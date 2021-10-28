using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGhostBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public static int height = 20;
    public static int width = 10;

    void Start()
    {
        while (ValidMoves())
        {
            transform.position += new Vector3(0, -1, 0);
        }
        transform.position += new Vector3(0, 1, 0);
    }


    // Update is called once per frame
    void Update()
    { 

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            transform.position += new Vector3(0, 20, 0);
            if (ValidMoves() == false)
            {
                transform.position += new Vector3(1, 0, 0);
                transform.position += new Vector3(0, -20, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            transform.position += new Vector3(0, 20, 0);
            if (ValidMoves() == false)
            {
                transform.position += new Vector3(-1, 0, 0);
                transform.position += new Vector3(0, -20, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // rotate
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            transform.position += new Vector3(0, 20, 0);
            if (ValidMoves() == false)
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                transform.position += new Vector3(0, -20, 0);
            }
        }

        while (ValidMoves())
        {
            transform.position += new Vector3(0, -1, 0);
        }
        transform.position += new Vector3(0, 1, 0);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.enabled = false;
            Destroy(this.gameObject);
            //FinalizeBlock();
        }
    }

    bool ValidMoves()
    {
        
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedY < height)
            {
                if (roundedX < 0 || roundedY < 0 || roundedX >= width || TetrisBlock.grid[roundedX, roundedY] != null)
                {
                    return false;
                }
            } else
            {
                if (roundedX < 0 || roundedY < 0 || roundedX >= width)
                {
                    return false;
                }
            }

            
        }

        return true;
    }
}

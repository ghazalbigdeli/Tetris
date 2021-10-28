using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    public static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
     void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        { 
            transform.position += new Vector3(-1, 0, 0);
            if (ValidMove() == false)
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (ValidMove() == false)
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // rotate
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            if (ValidMove() == false)
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
        } 


        if (Input.GetKeyDown(KeyCode.Space))
        {
            while (ValidMove())
            {
                transform.position += new Vector3(0, -1, 0);
            }
            transform.position += new Vector3(0, 1, 0);
            FinalizeBlock();
        }
        else if (Time.time - previousTime > (Input.GetKeyDown(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;

            if(ValidMove() == false)
            {
                transform.position += new Vector3(0, 1, 0);
                FinalizeBlock();
            }
        }
    }

    void FinalizeBlock()
    {
        this.enabled = false;
        FindObjectOfType<SpawnBlock>().newBlock();
        // add to grid
        AddToGrid();
        CheckForLines();
        CheckForGameOver();
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedY < 0 || roundedX >= width || roundedY >= height || grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }

    bool CheckForGameOver()
    {
        foreach (Transform children in transform)
        {
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedY >= height)
            {
                Debug.Log("GAME OVER");
                return true;
            }
        }
 
        return false;
    }

    void CheckForLines()
    {
        for (int j = height - 1; j >= 0; j--)
        {
            if (HasLine(j))
            {
                DeleteLine(j);
                RowDown2(j);
            }
        }
    }

    bool HasLine(int j)
    {
        for (int i = 0; i < width; i++)
        {
            if (grid[i, j] == null)
            {
                return false;
            }
        }

        return true;
    }

    void DeleteLine(int j)
    {
        for (int i = 0; i < width; i++)
        {
            Destroy(grid[i, j].gameObject);
            grid[i, j] = null;
        }
    }

    // move all rows down after clearing a line
    void RowDown(int j)
    {
        for (int y = j; y < height; y++)
        {
            for (int i = 0; i < width; i++)
            {
               if (grid[i, y] != null)
               {
                    grid[i, y - 1] = grid[i, y];
                    grid[i, y] = null;
                    grid[i, y - 1].transform.position -= new Vector3(0, 1, 0);
                    Debug.Log("moving row");
               }
            }
        }
    }

    public void RowDown2(int j)
    {
        for (int y = j; y < height - 1; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y + 1] != null)
                {
                    grid[x, y] = grid[x, y + 1];
                    grid[x, y + 1] = null;
                    grid[x, y].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
}

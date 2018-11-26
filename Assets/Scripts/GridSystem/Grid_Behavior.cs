using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Behavior : MonoBehaviour {
[SerializeField]
    private int rows;
    [SerializeField]
    private int cols;
    [SerializeField]
    private Vector2 gridSize;
    [SerializeField]
    private Vector2 gridOffset;

    //about cells
    [SerializeField]
    private Sprite cellSprite;
    private Vector2 cellSize;
    private Vector2 cellScale;
    public List<Vector3> gridPositions = new List<Vector3>();
    public List <Vector3> playerPositions = new List<Vector3>();
    private GameObject [] players = GameObject.FindGameObjectsWithTag("Player");

    void Awake () {
        InitCells(); //Initialize all cells
	}
  /*  void update(){
        playerPositions.Clear();
        for (int i = 0 ; i < players.Length; i++){
            playerPositions.Add(players[i].transform.position);
        }
    }
*/
    void InitCells() {
        GameObject cellObject = new GameObject();


        
        cellObject.AddComponent<SpriteRenderer>().sprite = cellSprite;

        
        cellSize = cellSprite.bounds.size;

        
        Vector2 newCellSize = new Vector2(gridSize.x / (float)cols, gridSize.y / (float)rows);

        
        cellScale.x = newCellSize.x / cellSize.x;
        cellScale.y = newCellSize.y / cellSize.y;

        cellSize = newCellSize; 

        cellObject.transform.localScale = new Vector2(cellScale.x, cellScale.y);

        
        gridOffset.x = -(gridSize.x / 2) + cellSize.x / 2;
        gridOffset.y = -(gridSize.y / 2) + cellSize.y / 2;

        //fill the grid with cells by using Instantiate
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                //add the cell size so that no two cells will have the same x and y position
                Vector2 pos = new Vector2(col * cellSize.x + gridOffset.x + transform.position.x, row * cellSize.y + gridOffset.y + transform.position.y);

                //instantiate the game object, at position pos, with rotation set to identity
                GameObject cO = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
                gridPositions.Add(new Vector3(cO.transform.position.x, cO.transform.position.y, -3.0f));

                //set the parent of the cell to GRID so you can move the cells together with the grid;
                cO.transform.parent = transform;
                
            }
        }

        //destroy the object used to instantiate the cells
        Destroy(cellObject);
    }

    //so you can see the width and height of the grid on editor
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }
}

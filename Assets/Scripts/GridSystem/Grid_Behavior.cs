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

    
    [SerializeField]
    private Sprite cellSprite;
    private Vector2 cellSize;
    private Vector2 cellScale;

	public Tile[,] gridPositions;

	void Awake() {
		gridPositions = new Tile[3, 3];
		InitCells(); //Initialize all cells
	}
	
	public bool initAddCharacter(Character c) {
		c.setField(this);
		foreach(Tile t in gridPositions) {
			if(t.transform.position.x == c.transform.position.x && t.transform.position.y == c.transform.position.y) {
				t.placeCharacter(c);
				return true;
			}
		}
		return false;
	}

	public Tile getTileAt(Vector3 pos) {
		foreach(Tile t in gridPositions)
			if(t.transform.position.x == pos.x && t.transform.position.y == pos.y)
				return t;
		return null;
	}
	
	/*void update(){
        playerPositions.Clear();
        GameObject [] players =  new GameObject[4]; 
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0 ; i < players.Length; i++){
            playerPositions.Add(players[i].transform.position);
        }
    }
    public List<Vector3> getPlayerPositions(){
        return playerPositions;
    }
    public List<Vector3> getGridPositions(){
        return gridPositions;
    }*/

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
		
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                
                Vector2 pos = new Vector2(col * cellSize.x + gridOffset.x + transform.position.x, row * cellSize.y + gridOffset.y + transform.position.y);
				
				//instantiate the game object, at position pos, with rotation set to identity
                GameObject c0 = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
				Tile t = c0.AddComponent<Tile>();
				t.setField(this);
				gridPositions[row, col] = t;// new Vector3(c0.transform.position.x, c0.transform.position.y, -3.0f));

                //set the parent of the cell to GRID so you can move the cells together with the grid;
                c0.transform.parent = transform;
                
            }
        }

        
        Destroy(cellObject);
    }

    
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }
}

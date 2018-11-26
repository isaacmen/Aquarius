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
	public Tile[,] gridPositions;

	private List<Character> characters;
	//   private GameObject [] players = GameObject.FindGameObjectsWithTag("Player");

	void Awake() {
		characters = new List<Character>();
		gridPositions = new Tile[3, 3];
		InitCells(); //Initialize all cells
	}

	public bool addCharacter(Character c) {
		if(characters.Contains(c)) {
			return false;
		} else {
			characters.Add(c);
			/*for(int i = 0; i < 3; i++)
				if(positions[WIDTH / 2, i].placeCharacter(c))
					break;*/

			return true;
		}
	}

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
                GameObject c0 = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
				Tile t = c0.AddComponent<Tile>();
				gridPositions[row, col] = t;// new Vector3(c0.transform.position.x, c0.transform.position.y, -3.0f));

                //set the parent of the cell to GRID so you can move the cells together with the grid;
                c0.transform.parent = transform;
                
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

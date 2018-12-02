using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
	[SerializeField]
    private int rows;
    [SerializeField]
    private int cols;
    [SerializeField]
    private Vector2 gridSize;
    [SerializeField]
    private Vector2 gridOffset;

	[Header("Side")]
	public bool isRight;
    
    [Header("Sprites")]
    public Sprite tileRegularSprite;
	public Sprite tileClickableSprite;
	public Sprite tileSecondarySprite;

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

	public Tile[,] getTiles() {
		return gridPositions;
	}

	public Tile getTileAtYX(int y, int x) {
		return (y < 0 || y > 2 || x < 0 || x > 2) ? null : gridPositions[y, x];
	}

	public bool isCharacterAtYX(int y, int x) {
		return getTileAtYX(y, x) != null && getTileAtYX(y, x).getCharacter() != null;
	}

	public Tile getTileAtYX(int[] yx) {
		return getTileAtYX(yx[0], yx[1]);
	}

	public Tile getTileAt(Vector3 pos) {
		foreach(Tile t in gridPositions)
			if(t.transform.position.x == pos.x && t.transform.position.y == pos.y)
				return t;
		return null;
	}

	public int[] getTileArrayCoordsYX(Tile t) {
		for(int y = 0; y < gridPositions.GetLength(0); y++) {
			for(int x = 0; x < gridPositions.GetLength(1); x++) {
				if(gridPositions[y, x] == t)
					return new int[] { y, x };
			}
		}
		return null;
	}

	public static Tile getTileForCharacter(Character c) {
		foreach(Tile t in GameLoop.getInstance().getAllyField().gridPositions)
			if(t.getCharacter() == c)
				return t;
		foreach(Tile t in GameLoop.getInstance().getEnemyField().gridPositions)
			if(t.getCharacter() == c)
				return t;
		return null;
	}

	public static List<Tile> tilesInCharacterRange(Character c, int low, int high, bool sameField) {
		Tile charTile = getTileForCharacter(c);
		List<Tile> inRange = new List<Tile>();

		if(sameField) {
			foreach(Tile t in charTile.getField().gridPositions)
				if(low <= Field.rangeBetween(charTile, t) && Field.rangeBetween(charTile, t) <= high)
					inRange.Add(t);
		} else {
			foreach(Tile t in (charTile.getField() == GameLoop.getInstance().getAllyField() ? GameLoop.getInstance().getEnemyField() : GameLoop.getInstance().getAllyField()).gridPositions)
				if(low <= Field.rangeBetween(charTile, t) && Field.rangeBetween(charTile, t) <= high)
					inRange.Add(t);
		}
		return inRange;
	}

	public static int rangeBetween(Tile t1, Tile t2) {
		if(t1.getField() == t2.getField()) {
			int[] t1YX = t1.getTileArrayCoordsYX();
			int[] t2YX = t2.getTileArrayCoordsYX();

			if(t1YX == null || t2YX == null)
				return -1;

			return Mathf.Abs(t1YX[0] - t2YX[0]) + Mathf.Abs(t1YX[1] - t2YX[1]);
		} else {
			Tile right = (t1.transform.position.x - t2.transform.position.x > 0) ? t1 : t2 ;
			int[] leftYX = (right == t1) ? t2.getTileArrayCoordsYX() : t1.getTileArrayCoordsYX();
			int[] rightYX = right.getTileArrayCoordsYX();

			if(leftYX == null || rightYX == null)
				return -1;

			return Mathf.Abs(rightYX[0]-leftYX[0]) + 3 + rightYX[1]-leftYX[1];
		}
	}


    void InitCells() {
        GameObject cellObject = new GameObject();
        
        cellObject.AddComponent<SpriteRenderer>().sprite = tileRegularSprite;

        
        cellSize = tileRegularSprite.bounds.size;

        
        Vector2 newCellSize = new Vector2(gridSize.x / (float)cols, gridSize.y / (float)rows);

        
        cellScale.x = newCellSize.x / cellSize.x;
        cellScale.y = newCellSize.y / cellSize.y;

        cellSize = newCellSize; 

        cellObject.transform.localScale = new Vector2(cellScale.x, cellScale.y);

        
        gridOffset.x = -(gridSize.x / 2) + cellSize.x / 2;
        gridOffset.y = -(gridSize.y / 2) + cellSize.y / 2;
		
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                
                Vector2 pos = new Vector2(col * cellSize.x + gridOffset.x + transform.position.x, (2-row) * cellSize.y + gridOffset.y + transform.position.y);
				
				//instantiate the game object, at position pos, with rotation set to identity
                GameObject c0 = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
				c0.name = "Tile(" + row + ", " + col + ")"; 

				Tile t = c0.AddComponent<Tile>();
				t.setField(this);
				gridPositions[2-row, col] = t;

				c0.AddComponent<BoxCollider>();

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

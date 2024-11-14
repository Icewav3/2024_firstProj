using UnityEngine;

namespace Prog1_LectureCode.Week_7_Multidimensional_Arrays.CellularAutomata1D_v0
{
	public class LifeCreator : MonoBehaviour
	{
		[SerializeField] private int rows;
		[SerializeField] private int cols;
		[SerializeField] private float gridSize;
		[SerializeField] private bool debug;
		[SerializeField] private float delay;
		[SerializeField] private SpriteRenderer sprite;
		private GameObject[,] _grid;
		private GameOfLife _board;
		private float _nextUpdateTime;

		private void Start()
		{
			_board = new GameOfLife(rows, cols);
			_grid = new GameObject[rows, cols];

			if (debug)
			{
				print(_board.DebugDisplayGrid());
			}

			_nextUpdateTime = Time.time + delay;
		}

		private void Update()
		{
			//todo also need to create logic to update the value of the grid at current mouse position when click is detected
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				int x = Mathf.FloorToInt(mousePos.x / gridSize);
				int y = Mathf.FloorToInt(mousePos.y / gridSize);

				if (x >= 0 && x < cols && y >= 0 && y < rows)
				{
					_board.ToggleCell(x, y);

					if (debug)
					{
						print($"Toggled cell at ({x}, {y})");
					}
				}
			}

			if (Time.time >= _nextUpdateTime)
			{
				UpdateGrid();
				_nextUpdateTime = Time.time + delay;
			}
		}

		private void UpdateGrid()
		{
			for (int x = 0; x < cols; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					var tar = _grid[x, y];
					if (tar != null)
					{
						Destroy(tar.gameObject);
					}
					Vector3 position = new Vector3(x * gridSize, y * gridSize, 0);
					SpriteRenderer newSprite = Instantiate(sprite, position, Quaternion.identity);
					newSprite.color = _board.Cells[x, y] == 1 ? Color.white : Color.black;
					tar = newSprite.gameObject;
				}
			}
		}
	}
}
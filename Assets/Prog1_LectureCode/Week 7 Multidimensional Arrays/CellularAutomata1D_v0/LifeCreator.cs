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
		private SpriteRenderer[,] _displayGrid;
		private GameOfLife _board;
		private float _nextUpdateTime;

		private void Start()
		{
			_board = new GameOfLife(rows, cols);

			// Initialize the display grid with SpriteRenderer instances
			_displayGrid = new SpriteRenderer[rows, cols];
			for (int x = 0; x < cols; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					Vector3 position = new Vector3(x * gridSize, y * gridSize, 0);
					_displayGrid[x, y] = Instantiate(sprite, position, Quaternion.identity);
				}
			}
			_board.ToggleCell(3,3);
			_board.ToggleCell(4,3);
			_board.ToggleCell(5,3);

			if (debug)
			{
				print(_board.DebugDisplayGrid());
			}

			_nextUpdateTime = Time.time + delay;
		}

		private void Update()
		{
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

			if (!(Time.time >= _nextUpdateTime))
			{
				return;
			}

			_board.UpdateLife();
			UpdateGrid();
			_nextUpdateTime = Time.time + delay;
			if (debug)
			{
				print(_board.DebugDisplayGrid());
			}
		}

		private void UpdateGrid()
		{
			for (int x = 0; x < cols; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					_displayGrid[x, y].color = _board.Cells[x, y] == 1 ? Color.white : Color.black;
				}
			}
		}
	}
}
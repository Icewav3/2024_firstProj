using JetBrains.Annotations;
using UnityEngine;

namespace Prog1_LectureCode.Week_7_Multidimensional_Arrays.CellularAutomata1D_v0
{
	public class LifeCreator : MonoBehaviour
	{
		[Header("GridSetup")]
		[SerializeField] private int rows;
		[SerializeField] private int cols;
		[SerializeField] private float gridSize;
		[SerializeField] private bool debug;
		[SerializeField] private float baseDelay;
		[SerializeField] private SpriteRenderer sprite;

		#region Private

		private float _delay; 
		private float _timeScale;
		private bool _isPaused;
		private SpriteRenderer[,] _displayGrid;
		private GameOfLife _board;
		private float _nextUpdateTime;

		#endregion

		private void Start()
		{
			_delay = baseDelay;
			_board = new GameOfLife(cols, rows);
			_displayGrid = new SpriteRenderer[cols, rows];
			ResetVisualGrid();

			if (debug)
			{
				print(_board.DebugDisplayGrid());
			}

			_nextUpdateTime = Time.time + _delay;
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
			
			_nextUpdateTime = Time.time + _delay;

			UpdateVisual();
			
			if (_isPaused)
			{
				return;
			}
			
			UpdateLife();
			
			if (debug)
			{
				print(_board.DebugDisplayGrid());
			}
		}
		
		private void UpdateLife()
		{
			_board.UpdateLife();
		}

		private void UpdateVisual()
		{
			for (int x = 0; x < _displayGrid.GetLength(0); x++)
			{
				for (int y = 0; y < _displayGrid.GetLength(1); y++)
				{
					_displayGrid[x, y].color = _board.Cells[x, y] == 1 ? Color.white : Color.black;
				}
			}
		}

		private void ResetVisualGrid()
		{
			for (int x = 0; x < _displayGrid.GetLength(0); x++)
			{
				for (int y = 0; y < _displayGrid.GetLength(1); y++)
				{
					Vector3 position = new Vector3(x * gridSize, y * gridSize, 0);
					_displayGrid[x, y] = Instantiate(sprite, position, Quaternion.identity);
				}
			}
		}

		#region UI

		public void RandomReset()
		{
			_board.RandomizeGrid();
			UpdateVisual();
		}
		public void StarPreset()
		{
			_board.ResetGrid();
			//hardcode star
			_board.ToggleCell(3,3);
			_board.ToggleCell(4,3);
			_board.ToggleCell(5,3);
		}
		
		public void Reset()
		{
			_board.ResetGrid();
		}
		public void Pause()
		{
			_isPaused = !_isPaused;
		}
		public void UpdateTimeScale(float value)
		{
			if (debug)
			{
				print(value);
			}
			_delay = value;
		}
		
		#endregion
	}
}
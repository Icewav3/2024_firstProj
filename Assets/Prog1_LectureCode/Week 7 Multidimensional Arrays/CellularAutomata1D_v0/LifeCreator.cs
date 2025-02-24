﻿using JetBrains.Annotations;
using UnityEngine;

namespace Prog1_LectureCode.Week_7_Multidimensional_Arrays.CellularAutomata1D_v0
{
	public class LifeCreator : MonoBehaviour
	{
		[Header("Config")]
		[Tooltip("RandomRatio is the ratio of ON cells to OFF cells.\nExample: 2 would mean it's half as likely to be ON as it is OFF")]
		[SerializeField] private int randomRatio = 2;
		[Header("GridSetup")]
		[SerializeField] private int rows;
		[SerializeField] private int cols;
		[SerializeField] private float gridSize;
		[SerializeField] private bool debug;
		[SerializeField] private float baseDelay;
		[SerializeField] private SpriteRenderer sprite;

		#region Private variables

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
			if (Input.GetMouseButton(0))
			{
				Vector2Int v = GetMousePosition();
				
				if (v.x >= 0 && v.x < cols && v.y >= 0 && v.y < rows)
				{
					_board.ToggleCell(v.x, v.y);

					if (debug)
					{
						print($"Toggled cell ({v.x}, {v.y}) ON");
					}
				}
			}

			if (Input.GetMouseButton(1))
			{
				Vector2Int v = GetMousePosition();
				
				if (v.x >= 0 && v.x < cols && v.y >= 0 && v.y < rows)
				{
					_board.ToggleCell(v.x, v.y, false);

					if (debug)
					{
						print($"Toggled cell ({v.x}, {v.y}) OFF");
					}
				}
			}
			
			if (!(Time.time >= _nextUpdateTime))
			{
				return;
			}
			
			_nextUpdateTime = Time.time + _delay;

			UpdateVisual();
			
			if (debug)
			{
				print(_board.DebugDisplayGrid());
			}
			
			if (_isPaused)
			{
				return;
			}
			
			UpdateLife();
			
		}

		private Vector2Int GetMousePosition()
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			int x = Mathf.FloorToInt(mousePos.x / gridSize);
			int y = Mathf.FloorToInt(mousePos.y / gridSize);
			return new Vector2Int(x, y);
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

		#region UI Buttons

		public void RandomReset()
		{
			_board.RandomizeGrid(randomRatio);
			UpdateVisual();
		}
		public void StarPreset()
		{
			_board.ResetGrid();
			//hardcode flyer
			_board.ToggleCell(22, 12);
			_board.ToggleCell(22, 10);
			_board.ToggleCell(26, 13);
			_board.ToggleCell(23, 13);
			_board.ToggleCell(24, 13);
			_board.ToggleCell(25, 13);
			_board.ToggleCell(26, 13);
			_board.ToggleCell(26, 12);
			_board.ToggleCell(26, 11);
			_board.ToggleCell(25, 10);
			_board.ToggleCell(26, 13);
		}
		
		public void Reset()
		{
			_board.ResetGrid();
		}

		public void Step()
		{
			UpdateLife();
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
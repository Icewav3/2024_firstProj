using System;
using System.Text;
using UnityEngine;

namespace Prog1_LectureCode.Week_7_Multidimensional_Arrays.CellularAutomata1D_v0
{
	public class GameOfLife
	{
		/// <summary>
		/// using int to make addition easier to apply rules
		/// </summary>
		public int[,] Cells { get; private set; }

		public GameOfLife(int rows, int columns)
		{
			Cells = new int[rows, columns];
			ResetGrid();
		}

		/// <summary>
		/// Initializes grid with 0 in all places
		/// </summary>
		public void ResetGrid()
		{
			for (int i = 0; i < Cells.GetLength(0); i++)
			{
				for (int j = 0; j < Cells.GetLength(1); j++)
				{
					Cells[i, j] = 0;
				}
			}
		}

		/// <summary>
		/// Updates the life state of each cell in the grid
		/// based on the number of live neighbors.
		/// </summary>
		public void UpdateLife()
		{
			int rows = Cells.GetLength(0);
			int columns = Cells.GetLength(1);
			var tempArray = new int[rows, columns];

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					int liveNeighbors = 0;

					for (int x = -1; x <= 1; x++)
					{
						for (int y = -1; y <= 1; y++)
						{
							if (x == 0 && y == 0)
								continue; // skip the cell itself
							int neighborRow = i + x;
							int neighborCol = j + y;

							if (neighborRow >= 0 && neighborRow < rows && neighborCol >= 0 && neighborCol < columns)
							{
								liveNeighbors += Cells[neighborRow, neighborCol];
							}
						}
					}

					// Apply the rules of Conway's Game of Life
					if (Cells[i, j] == 1)
					{
						// Cell is currently alive
						if (liveNeighbors < 2 || liveNeighbors > 3)
						{
							tempArray[i, j] = 0; // Cell dies
						}
						else
						{
							tempArray[i, j] = 1; // Cell lives
						}
					}
					else
					{
						// Cell is currently dead
						if (liveNeighbors == 3)
						{
							tempArray[i, j] = 1; // Cell becomes alive
						}
						else
						{
							tempArray[i, j] = 0; // Cell remains dead
						}
					}
				}
			}
			Cells = tempArray; // Update the grid
		}

		public string DebugDisplayGrid()
		{
			StringBuilder gridString = new StringBuilder();
			for (int x = 0; x < Cells.GetLength(0); x++)
			{
				for (int y = 0; y < Cells.GetLength(1); y++)
				{
					string cell = Cells[x, y] == 1 ? "O" : "-";
					gridString.Append(cell + " ");
				}
				gridString.Append(Environment.NewLine);
			}
			return gridString.ToString();
		}

		public void RandomizeGrid()
		{
			var random = new System.Random();
			for (int i = 0; i < Cells.GetLength(0); i++)
			{
				for (int j = 0; j < Cells.GetLength(1); j++)
				{
					Cells[i, j] = random.Next(2); // Randomly sets the cell to either 0 or 1
				}
			}
		}
		
		public void ToggleCell(int x, int y)
		{
			if (Cells[x, y] == 1)
			{
				Cells[x, y] = 0;
			}
			else
			{
				Cells[x, y] = 1;
			}
		}
	}
}
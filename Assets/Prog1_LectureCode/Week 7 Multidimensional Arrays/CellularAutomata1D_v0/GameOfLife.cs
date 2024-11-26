using System;
using System.Text;
using UnityEngine;
using Random = System.Random;

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
			int[,] tempArray = new int[rows, columns];

			for (int cellRow = 0; cellRow < rows; cellRow++)
			{
				for (int cellColumn = 0; cellColumn < columns; cellColumn++)
				{
					int liveNeighbors = 0;

					for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
					{
						for (int colOffset = -1; colOffset <= 1; colOffset++)
						{
							if (rowOffset == 0 && colOffset == 0)
								continue; // skip the cell itself
							int neighborRow = cellRow + rowOffset;
							int neighborCol = cellColumn + colOffset;

							if (neighborRow >= 0 && neighborRow < rows && neighborCol >= 0 && neighborCol < columns)
							{
								liveNeighbors += Cells[neighborRow, neighborCol];
							}
						}
					}

					// Apply the rules of Conway's Game of Life
					if (Cells[cellRow, cellColumn] == 1)
					{
						// Cell is currently alive
						if (liveNeighbors < 2 || liveNeighbors > 3)
						{
							tempArray[cellRow, cellColumn] = 0; // Cell dies
						}
						else
						{
							tempArray[cellRow, cellColumn] = 1; // Cell lives
						}
					}
					else
					{
						// Cell is currently dead
						if (liveNeighbors == 3)
						{
							tempArray[cellRow, cellColumn] = 1; // Cell becomes alive
						}
						else
						{
							tempArray[cellRow, cellColumn] = 0; // Cell remains dead
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

		public void RandomizeGrid(int ratio)
		{
			Random random = new(); // had to use system random cuz non monobehavoir
			for (int i = 0; i < Cells.GetLength(0); i++)
			{
				for (int j = 0; j < Cells.GetLength(1); j++)
				{
					//if value is equal to 1 cell on, if not cell off
					Cells[i, j] = random.Next(ratio) == 1 ? 1 : 0; 
				}
			}
		} 
		/// <summary>
		/// Toggles the state of a cell at the specified coordinates.
		/// </summary>
		/// <param name="x">The x-coordinate of the cell.</param>
		/// <param name="y">The y-coordinate of the cell.</param>
		/// <param name="value">If true, sets the cell to alive; if false, sets the cell to dead. Default is true.</param>
		public void ToggleCell(int x, int y, bool value = true)
		{
			Cells[x, y] = value ? 1 : 0;
		}
	}
}
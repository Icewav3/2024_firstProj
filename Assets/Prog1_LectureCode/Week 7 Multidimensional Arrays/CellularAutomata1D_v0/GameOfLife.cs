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
			InitializeGrid();
		}

		/// <summary>
		/// Initializes grid with 0 in all places
		/// </summary>
		private void InitializeGrid()
		{
			for (int i = 0; i < Cells.GetLength(0); i++)
			{
				for (int j = 0; j < Cells.GetLength(1); j++)
				{
					Cells[i, j] = 0;
				}
			}
		}

		private void UpdateLife()
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

					tempArray[i, j] = liveNeighbors;
				}
			}

			Cells = tempArray;
			//todo create temp int array for the result then move it into _cells once we have calculated all new tiles
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

		public void ToggleCell(int x, int y)
		{
			int tar = Cells[x, y];
			tar = tar != 0 ? 0 : 1;
		}
	}
}
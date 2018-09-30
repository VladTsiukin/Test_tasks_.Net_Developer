using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class Universe
    {
        #region ctor
        /// <summary>
        /// To create the new universe
        /// </summary>
        /// <param name="numRows">Number of rows must be at least 10 and not more than 40</param>
        /// <param name="numColumns">Number of columns must be at least 10 and not more than 40</param>
        /// <param name="population">The population must be at least 3 and not more than 10</param>
        public Universe(int numRows, int numColumns, int population)
        {
            if (population < 3 || population > 10)
            {
                throw new ArgumentOutOfRangeException("The population must be at least 3 and not more than 10");
            }
            else if (numRows < 10 || numRows > 40)
            {
                throw new ArgumentOutOfRangeException("Number of rows must be at least 10 and not more than 40");
            }
            else if (numColumns < 10 || numColumns > 40)
            {
                throw new ArgumentOutOfRangeException("Number of columns must be at least 10 and not more than 40");
            }

            this._grid = new Cell[numRows, numColumns];
            this._numRows = numRows;
            this._numColumns = numColumns;
            SetBackground();
            InitUniverse(population);
        }
        #endregion

        #region fields
        private Cell[,] _grid;
        private int _numRows;
        private int _numColumns;
        #endregion

        #region methods
        private void SetBackground()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// The first generation initialization
        /// </summary>
        /// <param name="population"></param>
        private void InitUniverse(int population = 5)
        {
            Random random = new Random();

            for (int i = 0; i < _numRows; i++)
            {
                for (int j = 0; j < _numColumns; j++)
                {
                    if (random.Next(population) == 1)
                    {
                        this._grid[i, j] = new Cell(State.Alive);
                    }
                    else
                    {
                        this._grid[i, j] = new Cell();
                    }
                }
            }
        }

        /// <summary>
        /// The next generation initialization
        /// </summary>
        /// <param name="bufGrid"></param>
        /// <returns></returns>
        private Cell[,] InitNewGeneration(Cell[,] bufGrid)
        {
            if (bufGrid == null)
            {
                throw new ArgumentNullException("The grid buffer can not be null");
            }

            for (int i = 0; i < _numRows; i++)
            {
                for (int j = 0; j < _numColumns; j++)
                {
                    bufGrid[i, j] = new Cell();
                }
            }
            return bufGrid;
        }

        /// <summary>
        /// The next generation evolve
        /// </summary>
        private void Evolve()
        {
            Cell[,] bufGrid = new Cell[this._numRows, this._numColumns];
            bufGrid = InitNewGeneration(bufGrid);

            for (int row = 0; row < this._numRows; row++)
            {
                for (int col = 0; col < this._numColumns; col++)
                {
                    // count the alives neighbours
                    var numberOfAlive = GetAlivesNeighbours(row, col);
                    // apply the rules game
                    ApplyRules(ref bufGrid, numberOfAlive, row, col);
                }
            }
            this._grid = bufGrid;
        }

        /// <summary>
        /// To count the number of alive cells
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private int GetAlivesNeighbours(int row, int col)
        {
            if (row < 0 || col < 0)
            {
                throw new ArgumentNullException("Rows and columns can not be < 0");
            }
            var numberOfAlive = 0;

            foreach (var cell in GetIndicesIfExist(row, col))
            {
                if (this._grid[cell.Key, cell.Value].State == State.Alive) numberOfAlive++;
            }
            return numberOfAlive;
        }

        /// <summary>
        /// To check the indices by Moore neighborhood and return if its exist
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private IEnumerable<KeyValuePair<int, int>> GetIndicesIfExist(int row, int col)
        {
            if (row > 0)
            {
                if (col > 0)
                {
                    yield return new KeyValuePair<int, int>(row - 1, col - 1); // north-west
                }
                if (col < this._numColumns - 1)
                {
                    yield return new KeyValuePair<int, int>(row - 1, col + 1); // north-east
                }
                yield return new KeyValuePair<int, int>(row - 1, col); // north
            }
            if (col > 0)
            {
                yield return new KeyValuePair<int, int>(row, col - 1); // west
            }
            if (col < this._numColumns - 1)
            {
                yield return new KeyValuePair<int, int>(row, col + 1); // east
            }
            if (row < this._numRows - 1)
            {
                if (col > 0)
                {
                    yield return new KeyValuePair<int, int>(row + 1, col - 1); // south-west
                }
                if (col < this._numColumns - 1)
                {
                    yield return new KeyValuePair<int, int>(row + 1, col + 1); // south-east
                }
                yield return new KeyValuePair<int, int>(row + 1, col); // south
            }
        }

        /// <summary>
        /// Apply the rules to the new generation
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="numberOfAlive"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void ApplyRules(ref Cell[,] bufGrid, int numberOfAlive, int row, int col)
        {
            if (row < 0 || col < 0)
            {
                throw new ArgumentNullException("Rows and columns can not be < 0");
            }
            var gen = this._grid[row, col].Generation;

            if (this._grid[row, col].State == State.Alive)
            {
                if (numberOfAlive < 2 || numberOfAlive > 3)
                {
                    bufGrid[row, col].Generation = gen;
                    bufGrid[row, col].UpdateState();
                }
                else if (numberOfAlive == 2 || numberOfAlive == 3)
                {
                    bufGrid[row, col].Generation = gen;
                    bufGrid[row, col].UpdateState(State.Alive);
                }
            }
            else
            {
                if (numberOfAlive == 3)
                {
                    bufGrid[row, col].Generation = gen;
                    bufGrid[row, col].UpdateState(State.Alive);
                }
            }
        }

        /// <summary>
        /// Render the universe
        /// </summary>
        private void Render()
        {
            Console.Clear();
            for (int row = 0; row < this._numRows; row++, Console.WriteLine())
            {
                for (int col = 0; col < this._numColumns; col++)
                {
                    if (this._grid[row, col].State == State.Alive)
                    {
                        if (this._grid[row, col].Generation == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else if (this._grid[row, col].Generation == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else if (this._grid[row, col].Generation >= 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        Console.Write(" * ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }
            }
        }

        /// <summary>
        /// Start the game of life
        /// </summary>
        public void StartLife()
        {
            while (true)
            {
                this.Evolve();
                this.Render();
            }
        }
        #endregion
    }
}
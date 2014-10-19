    public int FindShortestPath()
        {
            // Queue for traversing the cells in the maze
            Queue<Cell> visitedCells = new Queue<Cell>();
            VisitCell(visitedCells, this.startCell.Row,
            this.startCell.Column, 0);
            // Perform Breath-First-Search (BFS)
            while (visitedCells.Count > 0)
            {
                Cell currentCell = visitedCells.Dequeue();
                int row = currentCell.Row;
                int column = currentCell.Column;
                int distance = currentCell.Distance;
                if ((row == 0) || (row == size - 1)
                || (column == 0) || (column == size - 1))
                {
                    // We are at the maze border
                    return distance + 1;
                }
                VisitCell(visitedCells, row, column + 1, distance + 1);
                VisitCell(visitedCells, row, column - 1, distance + 1);
                VisitCell(visitedCells, row + 1, column, distance + 1);
                VisitCell(visitedCells, row - 1, column, distance + 1);
            }
            // We didn't reach any cell at the maze border -> no path
            return -1;
        }
     
        private void VisitCell(Queue<Cell> visitedCells,
        int row, int column, int distance)
        {
            if (this.maze[row, column] != 'x')
            {
                // The cell is free --> visit it
                maze[row, column] = 'x';
                Cell cell = new Cell(row, column, distance);
                visitedCells.Enqueue(cell);
            }
        }


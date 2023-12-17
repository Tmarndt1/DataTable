using System;
using System.Collections.Generic;

namespace DataTable
{

    /// <summary>
    /// A generic table data structure with api to add new cells.
    /// </summary>
    /// <typeparam name="TCell">The generic cell type.</typeparam>
    public class Table<TCell>
        where TCell : Cell
    {
        private readonly SortedList<int, Column<TCell>> _columns = new SortedList<int, Column<TCell>>();

        private readonly SortedList<int, Row<TCell>> _rows = new SortedList<int, Row<TCell>>();

        /// <summary>
        /// A sorted collection of table columns.
        /// </summary>
        public IEnumerable<Column<TCell>> Columns => _columns.Values;

        /// <summary>
        /// A sorted collection of table rows.
        /// </summary>
        public IEnumerable<Row<TCell>> Rows => _rows.Values;

        /// <summary>
        /// The amount of cells in the table.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Indexer that returns a cell based on the column index and row index.
        /// </summary>
        /// <param name="columnIndex">The column index.</param>
        /// <param name="rowIndex">The row index.</param>
        /// <returns>The cell in that matching column and row index.</returns>
        public TCell this[int columnIndex, int rowIndex]
        {
            get => _columns[columnIndex][rowIndex];
        }
        
        public void Add(TCell cell)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell));

            if (_columns.TryGetValue(cell.ColumnIndex, out var column))
            {
                column.Add(cell);
            }
            else
            {
                _columns.Add(cell.ColumnIndex, new Column<TCell>(cell));
            }

            if (_rows.TryGetValue(cell.RowIndex, out var row))
            {
                row.Add(cell);
            }
            else
            {
                _rows.Add(cell.RowIndex, new Row<TCell>(cell));
            }

            Count++;
        }

        public bool TryAdd(TCell cell)
        {
            if (_columns.TryGetValue(cell.ColumnIndex, out var column) && column.SortedCells.ContainsKey(cell.RowIndex)) return false;

            if (_rows.TryGetValue(cell.RowIndex, out var row) && row.SortedCells.ContainsKey(cell.ColumnIndex)) return false;
           
            Add(cell);

            return true;
        }

        public void Remove(TCell cell)
        {
            _columns.Remove(cell.RowIndex);

            _rows.Remove(cell.ColumnIndex);

            Count--;
        }

        public void Clear()
        {
            _columns.Clear(); 

            _rows.Clear();

            Count = 0;
        }
    }
}
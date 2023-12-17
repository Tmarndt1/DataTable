using System;
using System.Collections.Generic;

namespace DataTable
{
    public interface ICell
    {
        public int ColumnIndex { get; init; }

        public int RowIndex { get; init; }
    }

    /// <summary>
    /// A generic table data structure with api to add new cells.
    /// </summary>
    /// <typeparam name="TCell">The generic cell type.</typeparam>
    public class Table<TCell>
        where TCell : ICell
    {
        private readonly SortedList<int, Column<TCell>> _columns = new SortedList<int, Column<TCell>>();

        private readonly SortedList<int, Row<TCell>> _rows = new SortedList<int, Row<TCell>>();

        public IEnumerable<Column<TCell>> Columns => _columns.Values;

        public IEnumerable<Row<TCell>> Rows => _rows.Values;

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
        }

        public bool TryAdd(TCell cell)
        {
            if (_columns.TryGetValue(cell.ColumnIndex, out var column) && column.Cells.ContainsKey(cell.RowIndex)) return false;

            if (_rows.TryGetValue(cell.RowIndex, out var row) && row.Cells.ContainsKey(cell.ColumnIndex)) return false;
           
            Add(cell);

            return true;
        }

        public void Clear()
        {
            _columns.Clear(); 
            _rows.Clear();
        }
    }
}
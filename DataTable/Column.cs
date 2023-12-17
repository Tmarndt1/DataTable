using System.Collections.Generic;

namespace DataTable
{
    public abstract class Column 
    {
        public int Index { get; init; }
    }

    public class Column<TCell> : Column
        where TCell : Cell
    {
        internal SortedList<int, TCell> SortedCells { get; private set; } = new SortedList<int, TCell>();

        public IEnumerable<TCell> Cells => SortedCells.Values;

        public TCell this[int index]
        {
            get => SortedCells[index];
        }

        public Column() { }

        internal Column(TCell cell)
        {
            Index = cell.ColumnIndex;

            SortedCells.Add(cell.RowIndex, cell);

            cell.Column = this;
        }

        public void Add(TCell cell)
        {
            SortedCells.Add(cell.RowIndex, cell);

            cell.Column = this;
        }

        public bool TryAdd(TCell cell)
        {
            bool added = SortedCells.TryAdd(cell.RowIndex, cell);

            if (added) cell.Column = this;

            return added;
        }
    }
}

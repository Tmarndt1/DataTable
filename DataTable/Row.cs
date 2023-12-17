using System.Collections.Generic;

namespace DataTable
{
    public abstract class Row
    {
        public int Index { get; init; }
    }

    public class Row<TCell> : Row
        where TCell : Cell
    {
        internal SortedList<int, TCell> SortedCells { get; private set; } = new SortedList<int, TCell>();

        public IEnumerable<TCell> Cells => SortedCells.Values;

        public TCell this[int index]
        {
            get => SortedCells[index];
        }

        public Row() { }

        internal Row(TCell cell)
        {
            Index = cell.RowIndex;

            SortedCells.Add(cell.ColumnIndex, cell);

            cell.Row = this;
        }

        public void Add(TCell cell)
        {
            SortedCells.Add(cell.ColumnIndex, cell);

            cell.Row = this;
        }

        public bool TryAdd(TCell cell)
        {
            bool added = SortedCells.TryAdd(cell.ColumnIndex, cell);

            if (added) cell.Row = this;

            return added;
        }
    }
}

using System.Collections.Generic;

namespace DataTable
{
    public class Row<TCell>
        where TCell : ICell
    {
        public int Index { get; init; }

        public SortedList<int, TCell> Cells { get; private set; } = new SortedList<int, TCell>();

        public TCell this[int index]
        {
            get => Cells[index];
        }

        public Row() { }

        internal Row(TCell cell)
        {
            Cells.Add(cell.ColumnIndex, cell);
        }

        public void Add(TCell cell)
        {
            Cells.Add(cell.ColumnIndex, cell);
        }

        public bool TryAdd(TCell cell)
        {
            return Cells.TryAdd(cell.ColumnIndex, cell);
        }
    }
}

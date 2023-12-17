using System.Collections.Generic;

namespace DataTable
{
    public class Column<TCell>
        where TCell : ICell
    {
        public int Index { get; init; }

        public SortedList<int, TCell> Cells { get; private set; } = new SortedList<int, TCell>();

        public TCell this[int index]
        {
            get => Cells[index];
        }

        public Column() { }

        internal Column(TCell cell)
        {
            Cells.Add(cell.RowIndex, cell);
        }

        public void Add(TCell cell)
        {
            Cells.Add(cell.RowIndex, cell);
        }

        public bool TryAdd(TCell cell)
        {
            return Cells.TryAdd(cell.RowIndex, cell);
        }
    }
}


namespace DataTable
{
    public abstract class Cell
    {
        public required int ColumnIndex { get; set; }

        public required int RowIndex { get; set; }

        internal Column Column { get; set; }

        internal Row Row { get; set; }
    }
}

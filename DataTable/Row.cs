using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace DataTable
{
    public abstract class Row
    {
        [JsonPropertyName("index")]
        [XmlElement("index")]
        public int Index { get; init; }
    }

    public class Row<TCell> : Row
        where TCell : Cell
    {
        [JsonIgnore]
        [XmlIgnore]
        internal SortedList<int, TCell> SortedCells { get; private set; } = new SortedList<int, TCell>();

        [JsonPropertyName("cells")]
        [XmlElement("cells")]
        public IEnumerable<TCell> Cells
        {
            get
            {
                return SortedCells.Values;
            }
            set
            {
                SortedCells.Clear();

                foreach (var item in value)
                {
                    SortedCells.Add(item.ColumnIndex, item);
                }
            }
        }

        [JsonIgnore]
        public int Count => SortedCells.Count;

        public TCell this[int index] => SortedCells[index];

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

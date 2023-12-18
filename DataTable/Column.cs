using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace DataTable
{
    public abstract class Column 
    {
        public Guid Uuid { get; set; } = Guid.NewGuid();

        [JsonPropertyName("index")]
        [XmlElement("index")]
        public int Index { get; init; }
    }

    public class Column<TCell> : Column
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
                    SortedCells.Add(item.RowIndex, item);
                }
            }
        }

        [JsonIgnore]
        public int Count => SortedCells.Count;

        public TCell this[int index] => SortedCells[index];

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

using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTable
{
    /// <summary>
    /// A generic table data structure with api to add new cells and enumerate through the columns and rows.
    /// </summary>
    /// <typeparam name="TCell">The generic cell type.</typeparam>
    public class DataTable<TCell>
        where TCell : ICell
    {
        private readonly List<CellCollection<TCell>> _columns = new List<CellCollection<TCell>>();

        private readonly List<CellCollection<TCell>> _rows = new List<CellCollection<TCell>>();

        /// <summary>
        /// The ordered columns in the data table.
        /// </summary>
        public IEnumerable<CellCollection<TCell>> Columns => _columns;

        /// <summary>
        /// The ordered rows in the data table.
        /// </summary>
        public IEnumerable<CellCollection<TCell>> Rows => _rows;
        
        public void Add(TCell cell)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell));

            AddToCollection(_columns, cell, x => x.ColIndex);
            AddToCollection(_rows, cell, x => x.RowIndex);
        }

        public TCell FirstOrDefault(Func<TCell, bool> predicate)
        {
            for (int i = 0; i < _columns.Count; i++)
            {
                for (int j = 0; j < _columns[i].Count; j++)
                {
                    TCell cell = _columns[i][j];

                    if (predicate.Invoke(cell))
                    {
                        return cell;
                    }
                }
            }

            return default;
        }

        public void AddOrUpdate(TCell cell)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell));

            CellCollection<TCell> column = _columns.FirstOrDefault(x => x.Index == cell.ColIndex);

            if (column == null)
            {
                column = new CellCollection<TCell>((a, b) => a.ColIndex < b.ColIndex ? -1 : 1)
                {
                    Index = cell.ColIndex
                };

                column.Add(cell);

                _columns.Add(column);

                _columns.Sort((a, b) => a.Index < b.Index ? -1 : 1);
            }

            CellCollection<TCell> row = _rows.FirstOrDefault(x => x.Index == cell.ColIndex);

            if (row == null)
            {
                row = new CellCollection<TCell>((a, b) => a.RowIndex < b.RowIndex ? -1 : 1)
                {
                    Index = cell.RowIndex
                };

                row.Add(cell);

                _rows.Add(row);

                _rows.Sort((a, b) => a.Index < b.Index ? -1 : 1);
            }
        }

        public void Clear()
        {
            _columns.Clear(); 
            _rows.Clear();
        }

        private void AddToCollection(List<CellCollection<TCell>> list, TCell cell, Func<TCell, int> indexSelector)
        {
            int index = indexSelector.Invoke(cell);

            CellCollection<TCell> cells = list.FirstOrDefault(c => c.Index == index);

            if (cells == null)
            {
                cells = new CellCollection<TCell>((a, b) => indexSelector(a) - indexSelector(b))
                {
                    Index = index
                };

                cells.Add(cell);

                list.Add(cells);

                list.Sort((a, b) => a.Index - b.Index);
            }
            else
            {
                cells.Add(cell);
            }
        }
    }
}
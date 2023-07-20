using System;
using System.Collections;
using System.Collections.Generic;

namespace DataTable
{
    /// <summary>
    /// Interface that defines the basic properties required for the DataTable class.
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// The cell's column index
        /// </summary>
        public int ColIndex { get; }

        /// <summary>
        /// The cell's row index
        /// </summary>
        public int RowIndex { get; }
    }

    /// <summary>
    /// A generic ordered data structure that represents the collection of cells in the DataTable.
    /// </summary>
    /// <typeparam name="TCell">The generic cell type.</typeparam>
    public class CellCollection<TCell> : IEnumerable<TCell>
        where TCell : ICell
    {
        public int Index { get; set; } = -1;

        private readonly List<TCell> _cells = new List<TCell>();

        public IList<TCell> Cells => _cells;

        public int Count => _cells.Count;

        private readonly Comparison<TCell> _comparer;

        internal CellCollection(Comparison<TCell> comparer)
        {
            _comparer = comparer;
        }

        internal void Add(TCell cell)
        {
            _cells.Add(cell);

            _cells.Sort(_comparer);
        }

        public int FindIndex(Predicate<TCell> match) => _cells.FindIndex(match);

        public IEnumerator<TCell> GetEnumerator()
        {
            return _cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

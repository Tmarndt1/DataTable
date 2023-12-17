namespace DataTable.Test
{
    public class Cell : ICell
    {
        public required int ColumnIndex { get; init; }    
        public required int RowIndex { get; init; }
        public required string Value { get; init; }
    }

    public class UnitTest1
    {
        [Fact]
        public void RowTest()
        {
            Table<Cell> table = new Table<Cell>();

            table.Add(new Cell()
            {
                ColumnIndex = 1,
                RowIndex = 2,
                Value = "TEST2"
            });

            table.Add(new Cell()
            {
                ColumnIndex = 1,
                RowIndex = 1,
                Value = "TEST1"
            });

            Assert.Equal("TEST1", table[1, 1].Value);
            Assert.Equal("TEST2", table[1, 2].Value);
        }

        [Fact]
        public void ColumnTest()
        {
            Table<Cell> table = new Table<Cell>();

            table.Add(new Cell()
            {
                ColumnIndex = 2,
                RowIndex = 1,
                Value = "TEST2"
            });

            table.Add(new Cell()
            {
                ColumnIndex = 1,
                RowIndex = 1,
                Value = "TEST1"
            });

            Assert.Equal("TEST1", table[1, 1].Value);
            Assert.Equal("TEST2", table[2, 1].Value);
        }
    }
}
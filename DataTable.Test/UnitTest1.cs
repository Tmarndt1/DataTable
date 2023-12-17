namespace DataTable.Test
{
    public class Cell2 : Cell
    {
        public required string Value { get; init; }
    }

    public class UnitTest1
    {
        [Fact]
        public void RowTest()
        {
            Table<Cell2> table = new Table<Cell2>();

            table.Add(new Cell2()
            {
                ColumnIndex = 1,
                RowIndex = 2,
                Value = "TEST2"
            });

            table.Add(new Cell2()
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
            Table<Cell2> table = new Table<Cell2>();

            table.Add(new Cell2()
            {
                ColumnIndex = 2,
                RowIndex = 1,
                Value = "TEST2"
            });

            table.Add(new Cell2()
            {
                ColumnIndex = 1,
                RowIndex = 1,
                Value = "TEST1"
            });

            Assert.Equal("TEST1", table[1, 1].Value);
            Assert.Equal("TEST2", table[2, 1].Value);
        }

        [Fact]
        public void MultipleTest()
        {
            Table<Cell2> table = new Table<Cell2>();

            table.Add(new Cell2()
            {
                ColumnIndex = 2,
                RowIndex = 1,
                Value = "1"
            });

            table.Add(new Cell2()
            {
                ColumnIndex = 1,
                RowIndex = 1,
                Value = "2"
            });

            table.Add(new Cell2()
            {
                ColumnIndex = 2,
                RowIndex = 2,
                Value = "3"
            });

            table.Add(new Cell2()
            {
                ColumnIndex = 1,
                RowIndex = 2,
                Value = "TEST1"
            });


            Assert.Equal(4, table.Count);
        }

        [Fact]
        public void RemoveTest()
        {
            Table<Cell2> table = new Table<Cell2>();

            Cell2 cell = new Cell2()
            {
                ColumnIndex = 2,
                RowIndex = 1,
                Value = "1"
            };

            table.Add(cell);

            table.Add(new Cell2()
            {
                ColumnIndex = 1,
                RowIndex = 1,
                Value = "2"
            });

            table.Add(new Cell2()
            {
                ColumnIndex = 2,
                RowIndex = 2,
                Value = "3"
            });

            Assert.Equal(3, table.Count);

            table.Remove(cell);

            Assert.Equal(2, table.Count);
        }
    }
}
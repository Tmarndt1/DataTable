using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;

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
            // Arrange
            Table<Cell2> table = new Table<Cell2>();

            // Act
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

            // Assert
            Assert.Equal("TEST1", table[1, 1].Value);
            Assert.Equal("TEST2", table[1, 2].Value);
        }

        [Fact]
        public void ColumnTest()
        {
            // Arrange
            Table<Cell2> table = new Table<Cell2>();

            // Act
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
            // Arrange
            Table<Cell2> table = new Table<Cell2>();

            // Act
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
                Value = "4"
            });

            // Assert
            Assert.Equal(4, table.Count);
        }

        [Fact]
        public void RemoveTest()
        {
            // Arrange
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

            // Act
            table.Remove(cell);

            // Assert
            Assert.Equal(2, table.Count);
        }

        [Fact]
        public void DeserializeTest()
        {
            // Arrange
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
                Value = "4"
            });

            string json = JsonSerializer.Serialize(table);

            // Act
            Table<Cell2>? deserialed = JsonSerializer.Deserialize<Table<Cell2>>(json);

            // Assert
            Assert.Equal(4, deserialed?.Count);
        }
    }
}
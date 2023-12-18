using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace DataTable
{
    public abstract class Cell
    {
        [JsonPropertyName("columnIndex")]
        public required int ColumnIndex { get; set; }

        [JsonPropertyName("rowIndex")]
        public required int RowIndex { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        internal Column Column { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        internal Row Row { get; set; }
    }
}

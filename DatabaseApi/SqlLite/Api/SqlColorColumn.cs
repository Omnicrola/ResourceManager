using System;
using System.Windows.Media;

namespace DatabaseApi.SqlLite.Api
{
    public class SqlColorColumn : ISqlColumn
    {
        public SqlColorColumn(string columnName)
        {
            this.Name = columnName;
        }

        public string BuildCreateQuery()
        {
            string nullable = Nullable ? "" : "NOT NULL";
            return $"{Name} varchar(7) {nullable}";
        }

        public string Name { get; }

        public bool IsPrimaryKey => false;
        public bool Nullable => false;
        public string EncapsulateValue(object value)
        {
            string hexValue = "000000";
            if (value is Color)
            {
                Color color = (Color)value;
                hexValue = color.R.ToString("X2") +
                           color.G.ToString("X2") +
                           color.B.ToString("X2");
            }
            return $"'#{hexValue}'";
        }

        public object ParseValue(object valueFromSql)
        {
            if (valueFromSql is string)
            {
                string hexString = (string)valueFromSql;
                if (hexString.Length != 7)
                {
                    return null;
                }
                byte r = Convert.ToByte(hexString.Substring(1, 2), 16);
                byte g = Convert.ToByte(hexString.Substring(3, 2), 16);
                byte b = Convert.ToByte(hexString.Substring(5, 2), 16);
                return Color.FromRgb(r, g, b);
            }
            return null;
        }
    }
}
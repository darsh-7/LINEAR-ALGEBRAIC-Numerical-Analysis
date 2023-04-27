using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINEAR_ALGEBRAIC_Numerical_Analysis
{
    public static class Methods
    {
        public static DataTable dataTable;
        public static DataRow myDataRow;

        public static void setupTable(string m)
        {
            dataTable = new DataTable();

            DataColumn dtColumn;

            // Create i column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Int32);
            dtColumn.ColumnName = "step";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = true;

            dataTable.Columns.Add(dtColumn);

            // Create X1 column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "value";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            dataTable.Columns.Add(dtColumn);


        }
    }

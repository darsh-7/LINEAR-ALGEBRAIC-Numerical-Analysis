using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LINEAR_ALGEBRAIC_Numerical_Analysis
{
    public partial class StepBox : UserControl
    {
        public StepBox(int m)
        {
            InitializeComponent();
            method = m;
            setupTable();
        }
        public int method ;
        public  DataTable dataTable;
        public  DataRow myDataRow;


        public string text1
        {
            get => label1.Text;
            set => label1.Text = value;
        }
        public string text2
        {
            get => label2.Text;
            set => label2.Text = value;
        }
        public double[,] A ={{ 0, 0, 0 ,0},
                             { 0, 0, 0 ,0},
                             { 0, 0, 0 ,0}
                                        };
        //public double[] B
        //{
        //    get => B;
        //    set
        //    {
        //        B = value;
        //        CreateNewMatrix();
        //    }
        //}


        public void CreateNewMatrix()
        {
            for (int j = 0; j < 3; j++)
            {
                myDataRow = dataTable.NewRow();
                myDataRow["X1"] = A[j, 0];
                myDataRow["X2"] = A[j, 1];
                myDataRow["X3"] = A[j, 2];
                if (method == 0 )
                {
                    myDataRow["B"] = A[j,3];
                }

                dataTable.Rows.Add(myDataRow);
            }
            dataGridView1.DataSource = dataTable;

        }
        public void setupTable()
        {
            dataTable = new DataTable();

            DataColumn dtColumn;


            // Create X1 column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "X1";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            dataTable.Columns.Add(dtColumn);

            // Create X1 column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "X2";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            dataTable.Columns.Add(dtColumn);

            // Create X1 column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "X3";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            dataTable.Columns.Add(dtColumn);

            if (method == 0)
            {
                // Create X1 column
                dtColumn = new DataColumn();
                dtColumn.DataType = typeof(double);
                dtColumn.ColumnName = "B";
                dtColumn.ReadOnly = false;
                dtColumn.Unique = false;

                dataTable.Columns.Add(dtColumn);
            }

        }

    }
}

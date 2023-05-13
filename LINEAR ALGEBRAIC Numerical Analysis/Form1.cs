using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LINEAR_ALGEBRAIC_Numerical_Analysis
{
    public partial class Form1 : Form
    {
        public static DataTable dataTable;
        public static DataRow myDataRow;
        public static int method = 0;
        public Form1()
        {
            InitializeComponent();
            setupTable();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            try
            {
                double[,] Matrix = { { double.Parse(x11.Text), double.Parse(x12.Text), double.Parse(x13.Text) },
                                      { double.Parse(x21.Text), double.Parse(x22.Text), double.Parse(x23.Text) },
                                      { double.Parse(x31.Text), double.Parse(x32.Text), double.Parse(x33.Text) } };

                double[] tTest = { double.Parse(a.Text), double.Parse(b.Text), double.Parse(c.Text) };
            }
            catch (Exception)
            {
                MessageBox.Show("pls enter vald data");
                return;
            }

            double[,] tMatrix = { { double.Parse(x11.Text), double.Parse(x12.Text), double.Parse(x13.Text) },
                                      { double.Parse(x21.Text), double.Parse(x22.Text), double.Parse(x23.Text) },
                                      { double.Parse(x31.Text), double.Parse(x32.Text), double.Parse(x33.Text) } };

            double[] Test = { double.Parse(a.Text), double.Parse(b.Text), double.Parse(c.Text) };

            //double[,] tMatrix = {   { 2,1,-1},
            //                        { 5,2,2},
            //                        { 3,1,1}
            //};

            //double[] Test = { 1, -4, 5 };

            double[] reuselt = { 0, 0, 0 };
            switch (method)
            {
                case 0:
                    reuselt = GaussianElimination(tMatrix, Test);
                    break;
                case 1:
                    reuselt = LUDecomposition(tMatrix, Test);
                    break;
                case 2:
                    reuselt = Cramer(tMatrix, Test);
                    break;

                default:
                    MessageBox.Show("method unknowwn");
                    break;
            }




            X1.Text = reuselt[0].ToString();
            X2.Text = reuselt[1].ToString();
            X3.Text = reuselt[2].ToString();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            method = 0;
            button2.BackColor = Color.IndianRed;
            button3.BackColor = Color.LightYellow;
            button4.BackColor = Color.LightYellow;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            method = 1;
            button2.BackColor = Color.LightYellow;
            button3.BackColor = Color.IndianRed;
            button4.BackColor = Color.LightYellow;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            method = 2;
            button2.BackColor = Color.LightYellow;
            button3.BackColor = Color.LightYellow;
            button4.BackColor = Color.IndianRed;
        }


        public void addStep(double[,] A, int method = 0, string text1 = "", string text2 = "")
        {
            StepBox step = new StepBox(method);
            step.text1 = text1;
            step.text2 = text2;
            step.A = A;
            step.CreateNewMatrix();
            //if(StepBox.method==1||StepBox.method==2)
            //    step.B= B;
            flowLayoutPanel1.Controls.Add(step);
        }

        public double[,] swapRows(double[,] array, int row1, int row2)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                double temp = array[row1, i];
                array[row1, i] = array[row2, i];
                array[row2, i] = temp;
            }
            return array;
        }

        public double[] GaussianElimination(double[,] A, double[] B)
        {
            //int method = 0;
            double x1, x2, x3;

            double[,] matrix = {{ A[0, 0], A[0,1], A[0,2] ,B[0]},
                                { A[1, 0], A[1,1], A[1,2] ,B[1]},
                                { A[2, 0], A[2,1], A[2,2] ,B[2]}
            };
            //Partial 
            if (Partial.Checked && Math.Abs(matrix[1, 0]) > Math.Abs(matrix[0, 0])&& Math.Abs(matrix[1, 0]) > Math.Abs(matrix[2, 0]) )
            {
                matrix = swapRows(matrix, 1, 0);
                addStep(matrix, method, $"swap 1->2");
            }
            else if (Partial.Checked && Math.Abs(matrix[2, 0]) > Math.Abs(matrix[0, 0]) && Math.Abs(matrix[2, 0]) > Math.Abs(matrix[1, 0]))
            {
                matrix = swapRows(matrix, 2, 0);
                addStep(matrix, method, $"swap 1->3");
            }
            if (Partial.Checked && Math.Abs(matrix[2, 0]) > Math.Abs(matrix[1, 0]))
            {
                matrix = swapRows(matrix, 2, 1);
                addStep(matrix, method, $"swap 2->3");
            }
            //
            double m21 = matrix[1, 0] / matrix[0, 0];
            double m31 = matrix[2, 0] / matrix[0, 0];
            addStep(matrix, method, $"m21 = {m21}\nm31 = {m31}");
            for (int i = 0; i <= 3; i++)
            {
                matrix[1, i] = matrix[1, i] - (m21 * matrix[0, i]);
            }
            for (int i = 0; i <= 3; i++)
            {
                matrix[2, i] = matrix[2, i] - (m31 * matrix[0, i]);
            }
            //Partial 
            addStep(matrix, method);
            if (Partial.Checked && Math.Abs(matrix[2, 1]) > Math.Abs(matrix[1, 1]))
            {
                matrix = swapRows(matrix, 2, 1);
                addStep(matrix, method, $"swap 2->3");
            }
            //
            double m32 = matrix[2, 1] / matrix[1, 1];
            addStep(matrix, method, $"m32 = {m32}");
            for (int i = 0; i <= 3; i++)
            {
                matrix[2, i] = matrix[2, i] - (m32 * matrix[1, i]);
            }

            x3 = matrix[2, 3] / matrix[2, 2];
            x2 = (matrix[1, 3] - (matrix[1, 2] * x3)) / matrix[1, 1];
            x1 = ((matrix[0, 3] - (matrix[0, 2] * x3 + matrix[0, 1] * x2)) / matrix[0, 0]);

            addStep(matrix, method, $"x1={x1}\nx2={x2}\nx3={x3}");

            double[] rr = { x1, x2, x3 };

            return rr;
        }
        static void swapElements(double[] array, int index1, int index2)
        {
            double temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
        public double[] LUDecomposition(double[,] A, double[] B)
        {
            double x1, x2, x3, c1, c2, c3;

            double[,] matrix = {{ A[0, 0], A[0,1], A[0,2] ,B[0]},
                                { A[1, 0], A[1,1], A[1,2] ,B[1]},
                                { A[2, 0], A[2,1], A[2,2] ,B[2]}
            };
            //Partial 
            if (Partial.Checked && Math.Abs(matrix[1, 0]) > Math.Abs(matrix[0, 0]) && Math.Abs(matrix[1, 0]) > Math.Abs(matrix[2, 0]))
            {
                matrix = swapRows(matrix, 1, 0);
                swapElements(B, 1, 0);
                addStep(matrix, method, $"swap 1->2");
            }
            else if (Partial.Checked && Math.Abs(matrix[2, 0]) > Math.Abs(matrix[0, 0]) && Math.Abs(matrix[2, 0]) > Math.Abs(matrix[1, 0]))
            {
                matrix = swapRows(matrix, 2, 0);
                swapElements(B, 2, 0);
                addStep(matrix, method, $"swap 1->3");
            }
            if (Partial.Checked && Math.Abs(matrix[2, 0]) > Math.Abs(matrix[1, 0]))
            {
                matrix = swapRows(matrix, 2, 1);
                swapElements(B, 2, 1);
                addStep(matrix, method, $"swap 2->3");
            }

            double m21 = matrix[1, 0] / matrix[0, 0];
            double m31 = matrix[2, 0] / matrix[0, 0];
            addStep(matrix, method, $"m21 = {m21}\nm31 = {m31}");
            for (int i = 0; i <= 3; i++)
            {
                matrix[1, i] = matrix[1, i] - (m21 * matrix[0, i]);
            }
            for (int i = 0; i <= 3; i++)
            {
                matrix[2, i] = matrix[2, i] - (m31 * matrix[0, i]);
            }
            addStep(matrix, method);
            if (Partial.Checked && Math.Abs(matrix[2, 1]) > Math.Abs(matrix[1, 1]))
            {
                matrix = swapRows(matrix, 2, 1);
                swapElements(B, 2, 1);
                addStep(matrix, method, $"swap 2->3");
            }

            double m32 = matrix[2, 1] / matrix[1, 1];
            addStep(matrix, method, $"m32 = {m32}");
            for (int i = 0; i <= 3; i++)
            {
                matrix[2, i] = matrix[2, i] - (m32 * matrix[1, i]);
            }

            //show U
            addStep(matrix, method, "", "=U");
            double[,] L = {{ 1  ,   0, 0 },
                           { m21,   1, 0 },
                           { m31, m32, 1 }
            };
            //show L
            addStep(L, method, "", "=L");
            c1 = B[0];
            c2 = B[1] - (c1 * m21);
            c3 = B[2] - ((c1 * m31) + (c2 * m32));

            addStep(L, method, "LC=B", $"c1={c1}\nc2={c2}\nc3={c3}");

            x3 = matrix[2, 3] / matrix[2, 2];
            x2 = (c2 - (matrix[1, 2] * x3)) / matrix[1, 1];
            x1 = ((c1 - (matrix[0, 2] * x3 + matrix[0, 1] * x2)) / matrix[0, 0]);
            addStep(matrix, method, "LC=B", $"x1={x1}\nx2={x2}\nx3={x3}");

            double[] rr = { x1, x2, x3 };

            return rr;
        }
        public double[] Cramer(double[,] AA, double[] B)
        {
            double x1, x2, x3;
            double[,] matrix = {{ AA[0, 0], AA[0,1], AA[0,2] ,B[0]},
                                { AA[1, 0], AA[1,1], AA[1,2] ,B[1]},
                                { AA[2, 0], AA[2,1], AA[2,2] ,B[2]}
            };

            double A = Det3x3Matrix(matrix);
            addStep(matrix, method, "=A", $"={A}");

            double A1 = Det3x3Matrix(SwapColumns(matrix, 0, 3));
            addStep(matrix, method, "=A1", $"={A1}");

            double A2 = -1 * Det3x3Matrix(SwapColumns(matrix, 1, 3));
            addStep(matrix, method, "=A2", $"={A2}");

            double A3 = Det3x3Matrix(SwapColumns(matrix, 2, 3));
            addStep(matrix, method, "=A3", $"={A3}");

            x1 = A1 / A;
            x2 = A2 / A;
            x3 = A3 / A;

            double[] rr = { x1, x2, x3 };

            return rr;
        }
        public double[,] SwapColumns(double[,] matrix, int col1, int col2)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double temp = matrix[i, col1];
                matrix[i, col1] = matrix[i, col2];
                matrix[i, col2] = temp;
            }
            return matrix;
        }
        public double Det3x3Matrix(double[,] matrix)
        {
            double det = matrix[0, 0] * matrix[1, 1] * matrix[2, 2] + matrix[0, 1] * matrix[1, 2] * matrix[2, 0]
                       + matrix[0, 2] * matrix[1, 0] * matrix[2, 1] - matrix[0, 2] * matrix[1, 1] * matrix[2, 0]
                       - matrix[0, 1] * matrix[1, 0] * matrix[2, 2] - matrix[0, 0] * matrix[1, 2] * matrix[2, 1];


            return det;
        }
        public void setupTable()
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

            // Create i column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            dataTable.Columns.Add(dtColumn);

            // Create X1 column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DataTable);
            dtColumn.ColumnName = "value";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;

            dataTable.Columns.Add(dtColumn);


        }

    }
}

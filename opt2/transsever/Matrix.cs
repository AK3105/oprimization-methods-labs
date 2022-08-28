using System;
using System.Collections.Generic;
using System.Text;

namespace Opt
{
	class Matrix
	{
		private int cols;
		private int rows;
		public int Cols { get { return cols; } }
		public int Rows { get { return rows; } }

		private double[,] data;
		private double[,] Tdata;

		private int rank;

		private Vector coloumnVector;
		private Vector rowVector;
		public Matrix(int row, int coloumn)
		{

			rows = row;
			cols = coloumn;
			data = new double[row, coloumn];
			if (row == coloumn)
				rank = row;
		}



		public Matrix(int rowSize, int coloumnSize, double[,] data)
		{
			this.rows = rowSize;
			this.cols = coloumnSize;
			this.data = new double[rowSize, coloumnSize];
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					this.data[i, j] = data[i, j];
				}

			}
			if (rowSize == coloumnSize)
				rank = rowSize;

		}

		public Matrix(Matrix matrix)
		{
			this.rows = matrix.rows;
			this.cols = matrix.cols;
			this.data = new double[rows, cols];
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					data[i, j] = matrix[i, j];
				}

			}
		}
		public double this[int row, int coloumn]
		{
			get
			{
				if (row < 0 && coloumn < 0 || row >= rows && coloumn >= 0) return default(double);
				return data[row, coloumn];
			}
			set
			{
				if (row >= 0 && coloumn >= 0 && row < rows && coloumn < rows)
					data[row, coloumn] = value;
			}
		}
		public int GetRow()
		{
			return rows;
		}
		public int GetColoumn()
		{
			return cols;
		}

		public int GetRank()
		{

			if (cols == rows)
				return cols;
			else
				Console.WriteLine("не квадратная!");
			return 0;


		}
		public static Matrix operator *(double c, Matrix m)
		{
			Matrix rez = new Matrix(m.rows, m.cols);

			for (int i = 0; i < m.rows; i++)
				for (int j = 0; j < m.cols; j++)
					rez.data[i, j] = m.data[i, j] * c;
			return rez;

		}
		public static Matrix operator *(Matrix m, double c)
		{
			Matrix rez = new Matrix(m.rows, m.cols);

			for (int i = 0; i < m.rows; i++)
				for (int j = 0; j < m.cols; j++)
					rez.data[i, j] = m.data[i, j] * c;
			return rez;

		}

		public static Matrix operator +(Matrix u, Matrix v)
		{
			if (u.rows != v.rows || u.cols != v.cols) return null;
			Matrix rez = new Matrix(v.rows, v.cols);
			for (int i = 0; i < v.rows; i++)
				for (int j = 0; j < v.cols; j++)
					rez.data[i, j] = u.data[i, j] + v.data[i, j];
			return rez;

		}
		public static Matrix operator -(Matrix u, Matrix v)
		{
			if (u.rows != v.rows || u.cols != v.cols) return null;
			Matrix rez = new Matrix(v.rows, v.cols);
			for (int i = 0; i < v.rows; i++)
				for (int j = 0; j < v.cols; j++)
					rez.data[i, j] = u.data[i, j] - v.data[i, j];
			return rez;

		}

		public static Matrix operator *(Matrix u, Matrix v)
		{
			if (u.cols != v.rows) return null;
			Matrix rez = new Matrix(v.rows, v.cols);
			for (int i = 0; i < u.rows; i++)
				for (int j = 0; j < v.cols; j++)
					for (int k = 0; k < v.rows; k++)
						rez[i, j] += u[i, k] * v[k, j];

			return rez;
		}


		public void SetElement(double value, int row, int coloumn)
		{
			data[row, coloumn] = value;
		}
		public double GetValue(int row, int coloumn)
		{
			return data[row, coloumn];
		}

		public Vector GetRow(int RowIndex)
		{
			rowVector = new Vector(rows);
			for (int i = 0; i < rows; i++)
			{
				rowVector[i] = data[RowIndex, i];

			}
			return rowVector;
		}
		public Vector GetColoumn(int ColoumnIndex)
		{
			coloumnVector = new Vector(rows);
			for (int i = 0; i < rows; i++)
			{
				coloumnVector[i] = data[i, ColoumnIndex];

			}
			return coloumnVector;
		}

		public static Matrix Transparent(Matrix matrix)
		{
			int Rows = matrix.Rows;
			Matrix Tdata = matrix;
			for (int row = 0; row < Rows; row++)
			{
				for (int col = 0; col < row; col++)
				{
					double sub = Tdata[row, col];
					Tdata[row, col] = Tdata[col, row];
					Tdata[col, row] = sub;

				}
			}


			return Tdata;
		}


		public static double DetRec(Matrix matrix)
		{
			if (matrix.rank == 4)
			{
				return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
			}
			double sign = 1, result = 0;
			for (int i = 0; i < matrix.rank; i++)
			{
				Matrix minor = GetMinor(matrix, i);
				result += sign * matrix[0, i] * DetRec(minor);
				sign = -sign;
			}
			return result;
		}

		private static Matrix GetMinor(Matrix matrix, int n)
		{
			Matrix result = new Matrix(matrix.rank - 1, matrix.rank - 1);
			for (int i = 1; i < matrix.rank; i++)
			{
				for (int j = 0, col = 0; j < matrix.rank; j++)
				{
					if (j == n)
						continue;
					result[i - 1, col] = matrix[i, j];
					col++;
				}
			}
			return result;
		}




		public static Matrix SetColoumn(Vector vector, Matrix matrix, int pos)
		{
			int size = matrix.GetRank();

			Matrix rez = matrix;
			for (int i = 0; i < size; i++)
			{
				rez[i, pos] = vector[i];
			}

			return rez;
		}
		public Matrix Copy()
		{
			Matrix rez = new Matrix(cols, rows, data);
			return rez;
		}
		public static Matrix SetRow(Vector vector, Matrix matrix, int pos)
		{
			int size = matrix.GetRank();
			Matrix rez = new Matrix(size, size);
			rez = matrix;
			for (int i = 0; i < size; i++)
			{
				rez[pos, i] = vector[i];
			}

			return rez;
		}

		public override string ToString()
		{
			string s = "";
			for (int i = 0; i < rows; i++)
			{
				s += "\n";
				for (int j = 0; j < rows; j++)
				{
					s += " | " + data[i, j].ToString();
				}

			}

			return s;
		}
		public void Show()
		{

			for (int row = 0; row < 3; row++)
			{
				for (int coloumn = 0; coloumn < 3; coloumn++)
				{
					Console.Write(" " + data[row, coloumn]);

				}
				Console.WriteLine();
			}
		}
	}
}

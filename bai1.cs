using System;
class MatrixProgram
{
    static int[,] InputMatrix()
    {
        Console.Write("nhap so dong:");
        int rows = int.Parse(Console.ReadLine());
        Console.Write("nhap so cot:");
        int cols = int.Parse(Console.ReadLine());
        int[,] matrix = new int[rows, cols];

        Console.WriteLine("nhap cac phan tu cua ma tran:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"phan tu [{i},{j}]: ");
                matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }
        return matrix;
    }

    static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        Console.WriteLine("ma tran vua nhap la:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
    static int[,] AddMatrix(int[,] A, int[,] B)
    {
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = A[i, j] + B[i, j];
            }
        }
        return result;
    }
    static int[,] MultiplyMatrix(int[,] A, int[,] B)
    {
        int rowsA = A.GetLength(0);
        int colsA = A.GetLength(1);
        int rowsB = B.GetLength(0);
        int colsB = B.GetLength(1);
        if (colsA != rowsB)
        {
            Console.WriteLine("Không thể nhân! Số cột của A phải bằng số dòng của B.");
            return null;
        }
        int[,] result = new int[rowsA, colsB];
        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsB; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < colsA; k++)
                {
                    result[i, j] += A[i, k] * B[k, j];
                }
            }
        }
        return result;
    }
    static int[,] TransposeMatrix(int[,] A)
    {
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);
        int[,] result = new int[cols, rows];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[j, i] = A[i, j];
            }
        }
        return result;
    }
    static void FindMaxMin(int[,] A)
    {
        int max = A[0, 0];
        int min = A[0, 0];
        foreach (int value in A)
        {
            if (value > max) max = value;
            if (value < min) min = value;
        }
        Console.WriteLine($"Gia tri lon nhat: {max}");
        Console.WriteLine($"Gia tri nho nhat: {min}");
    }

    static int Determinant(int[,] A)
    {
        int n = A.GetLength(0);
        if (n != A.GetLength(1))
        {
            throw new ArgumentException("Ma trận phải là ma trận vuông.");
        }
        if (n == 1)
        {
            return A[0, 0];
        }
        if (n == 2)
        {
            return A[0, 0] * A[1, 1] - A[0, 1] * A[1, 0];
        }
        int det = 0;
        for (int p = 0; p < n; p++)
        {
            int[,] subMatrix = new int[n - 1, n - 1];
            for (int i = 1; i < n; i++)
            {
                int colIndex = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j == p) continue;
                    subMatrix[i - 1, colIndex] = A[i, j];
                    colIndex++;
                }
            }
            det += A[0, p] * Determinant(subMatrix) * (p % 2 == 0 ? 1 : -1);
        }
        return det;
    }
    static int[,] Minor(int[,] A, int row, int col)
    {
        int n = A.GetLength(0);
        int[,] result = new int[n - 1, n - 1];
        int r = 0, c = 0;
        for (int i = 0; i < n; i++)
        {
            if (i == row) continue;
            c = 0;
            for (int j = 0; j < n; j++)
            {
                if (j == col) continue;
                result[r, c] = A[i, j];
                c++;
            }
            r++;
        }
        return result;
    }
    static bool IsSymmetric(int[,] A)
    {
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);
        if (rows != cols) return false;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (A[i, j] != A[j, i]) return false;
            }
        }
        return true;
    }

    static void Main()
    {
        int[,] A = null;
        int[,] B = null;

        while (true)
        {
            Console.WriteLine("\n       MENU        ");
            Console.WriteLine("1. Nhap va hien thi ma tran  A");
            Console.WriteLine("2. Nhap ma tran  B");
            Console.WriteLine("3. Cong A + B");
            Console.WriteLine("4. Nhan A × B");
            Console.WriteLine("5. Chuyen vi ma tran A");
            Console.WriteLine("6. tim min max cua ma tran  A");
            Console.WriteLine("7. Tinh dinh thuc cua  A");
            Console.WriteLine("8. Kiem tra A co doi xung khong");
            Console.WriteLine("0. Thoat");
            Console.Write("Chon chuc nang : ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    A = InputMatrix();
                    Console.WriteLine("Ma trận A:");
                    PrintMatrix(A);
                    break;
                case 2:
                    B = InputMatrix();
                    Console.WriteLine("Ma trận B:");
                    PrintMatrix(B);
                    break;
                case 3:
                    if (A != null && B != null &&
                        A.GetLength(0) == B.GetLength(0) && A.GetLength(1) == B.GetLength(1))
                    {
                        Console.WriteLine("Kết quả A + B:");
                        PrintMatrix(AddMatrix(A, B));
                    }
                    else Console.WriteLine("Hai ma trận không cùng kích thước!");
                    break;
                case 4:
                    if (A != null && B != null)
                    {
                        var mul = MultiplyMatrix(A, B);
                        if (mul != null)
                        {
                            Console.WriteLine("Kết quả A × B:");
                            PrintMatrix(mul);
                        }
                    }
                    else Console.WriteLine("Cần nhập A và B trước!");
                    break;
                case 5:
                    if (A != null)
                    {
                        Console.WriteLine("Chuyển vị của A:");
                        PrintMatrix(TransposeMatrix(A));
                    }
                    else Console.WriteLine("Chưa có ma trận A!");
                    break;
                case 6:
                    if (A != null) FindMaxMin(A);
                    else Console.WriteLine("Chưa có ma trận A!");
                    break;
                case 7:
                    if (A != null && A.GetLength(0) == A.GetLength(1))
                        Console.WriteLine("Định thức của A = " + Determinant(A));
                    else Console.WriteLine("A phải là ma trận vuông!");
                    break;
                case 8:
                    if (A != null)
                        Console.WriteLine(IsSymmetric(A) ? "A là ma trận đối xứng" : "A không đối xứng");
                    else Console.WriteLine("Chưa có ma trận A!");
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    break;
            }
        }
    }

}
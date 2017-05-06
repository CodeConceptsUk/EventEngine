using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    public static class StringUtils
    {
        public static string ToStringTable<T>(
            this IEnumerable<T> values,
            string[] columnHeaders,
            params Func<T, object>[] valueSelectors)
        {
            return ToStringTable(values.ToArray(), columnHeaders, valueSelectors);
        }

        public static string ToStringTable<T>(
            this T[] values,
            string[] columnHeaders,
            params Func<T, object>[] valueSelectors)
        {
            if (columnHeaders.Length != valueSelectors.Length)
                throw new Exception("Unable to render table");

            var arrValues = new string[values.Length + 1, valueSelectors.Length];

            // Fill headers
            for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                arrValues[0, colIndex] = columnHeaders[colIndex];
            }

            // Fill table rows
            for (var rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (var colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    arrValues[rowIndex, colIndex] = valueSelectors[colIndex]
                        .Invoke(values[rowIndex - 1]).ToString();
                }
            }

            return ToStringTable(arrValues);
        }

        public static string ToStringTable(this string[,] arrValues)
        {
            int[] maxColumnsWidth = GetMaxColumnsWidth(arrValues);
            var headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

            var sb = new StringBuilder();

            PrintHeader(0, sb, headerSpliter);
            for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    // Print cell
                    string cell = arrValues[rowIndex, colIndex];
                    cell = cell.PadRight(maxColumnsWidth[colIndex]);
                    sb.Append(" | ");
                    sb.Append(cell);
                }

                // Print end of line
                sb.Append(" | ");
                sb.AppendLine();

                // Print splitter
                PrintSplit(rowIndex, sb, headerSpliter);
            }
            PrintHeader(0, sb, headerSpliter);

            return sb.ToString();
        }

        private static void PrintHeader(int rowIndex, StringBuilder sb, string headerSpliter)
        {
            if (rowIndex == 0)
            {
                sb.AppendFormat(" +{0}+ ", headerSpliter);
                sb.AppendLine();
            }
        }

        private static void PrintSplit(int rowIndex, StringBuilder sb, string headerSpliter)
        {
            if (rowIndex == 0)
            {
                sb.AppendFormat($" |{headerSpliter}| ");
                sb.AppendLine();
            }
        }

        private static int[] GetMaxColumnsWidth(string[,] arrValues)
        {
            var maxColumnsWidth = new int[arrValues.GetLength(1)];
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
                {
                    int newLength = arrValues[rowIndex, colIndex].Length;
                    int oldLength = maxColumnsWidth[colIndex];

                    if (newLength > oldLength)
                    {
                        maxColumnsWidth[colIndex] = newLength;
                    }
                }
            }

            return maxColumnsWidth;
        }
    }
}
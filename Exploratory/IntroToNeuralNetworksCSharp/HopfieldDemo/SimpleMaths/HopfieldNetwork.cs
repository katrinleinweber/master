﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMaths
{
    public class HopfieldNetwork
    {
        private Matrix _WeightMatrix;

        public HopfieldNetwork(int nodeCount)
        {
            double[][] matrix = _GetEmptyMatrix(nodeCount);
            _WeightMatrix = new Matrix(matrix);
        }

        private double[][] _GetEmptyMatrix(int nodeCount)
        {
            var matrix = new double[nodeCount][];

            for (var row = 0; row < nodeCount; row++)
            {
                matrix[row] = new double[nodeCount];

                for(var col=0; col<nodeCount;col++)
                {
                    matrix[row][col] = -1.0;
                }
            }
            return matrix;
        }

        public void Train(HopfieldPattern pattern)
        {
            _WeightMatrix = _WeightMatrix.Multiply(new Matrix(pattern.GetWeightMatrix()));
        }

        /// <summary>
        /// Takes in a Hopfield pattern and returns the pattern it most closely matches.
        /// </summary>
        /// <param name="hopfieldPattern"></param>
        /// <returns></returns>
        public HopfieldPattern Recognize(HopfieldPattern hopfieldPattern)
        {
            var inputPattern = hopfieldPattern;
            var recognizedPattern = HopfieldPattern.CreateEmptyPattern(hopfieldPattern.Length);

            // Get value from each column and place it in an array.
            for(var colIndex=0; colIndex < inputPattern.Length; colIndex++)
            {
                // We need to sum the totals for each column
                var columnResult = 0.0;

                for(var rowIndex=0; rowIndex < inputPattern.Length; rowIndex++)
                {
                    if (inputPattern[rowIndex] > 0)
                        columnResult += _WeightMatrix.GetValue(rowIndex, colIndex);
                }

                // Then place each column's scalar result in the result vector.
                recognizedPattern[colIndex] = columnResult;
            }

            // We have the result vector so return it as a pattern.
            return recognizedPattern;
        }
    }
}

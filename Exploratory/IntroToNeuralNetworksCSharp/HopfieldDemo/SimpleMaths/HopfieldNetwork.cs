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
            _WeightMatrix = Matrix.CreateEmptySquareMatrix(nodeCount);
        }

        public void Train(HopfieldPattern pattern)
        {
            _WeightMatrix = _WeightMatrix.Add(new Matrix(pattern.GetWeightMatrix()));
        }

        /// <summary>
        /// Takes in a Hopfield pattern and returns the pattern it most closely matches.
        /// </summary>
        /// <param name="inputPattern"></param>
        /// <returns></returns>
        public HopfieldPattern Recognize(HopfieldPattern inputPattern)
        {
            var recognizedPattern = HopfieldPattern.CreateEmptyPattern(inputPattern.Length);

            // Get value from each column and place it in an array.
            for(var colIndex=0; colIndex < inputPattern.Length; colIndex++)
            {
                // We need to sum the totals for each column
                var columnResult = 0.0;

                for(var rowIndex=0; rowIndex < inputPattern.Length; rowIndex++)
                {
                    columnResult += _WeightMatrix.GetValue(rowIndex, colIndex) * inputPattern[rowIndex];
                }

                // Then place each column's scalar result in the result vector.
                recognizedPattern[colIndex] = columnResult;
            }

            // switch from bipolar form
            for (var i = 0; i < recognizedPattern.Length; i++)
            {
                recognizedPattern[i] = recognizedPattern[i] < 0 ? 0 : 1;
            }

            // We have the result vector so return it as a pattern.
            return recognizedPattern;
        }
    }
}

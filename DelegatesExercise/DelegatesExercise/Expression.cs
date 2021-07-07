using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesExercise
{
    public enum Operation { Sum, Substract, Multiply }

    public class Expression
    {
        private int first;
        private int second;

        private Dictionary<Operation, Func<int>> operations = new();

        public Expression(int first, int second)
        {
            this.first = first;
            this.second = second;

            this.operations.Add(Operation.Sum, Sum);
            this.operations.Add(Operation.Substract, Substract);
            this.operations.Add(Operation.Multiply, Multiply);
        }

        private int Sum()
        {
            return first + second;
        }

        private int Substract()
        {
            return first - second;
        }

        private int Multiply()
        {
            return first * second;
        }

        public int ApplyOperator(Operation operation)
        {
            if (this.operations.ContainsKey(operation))
            {
                return this.operations[operation]();
            }

            return -1;
        }
    }
}

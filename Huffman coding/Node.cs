using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_coding
{
    public class Node
    {
        public char Symbol;
        public int Number;
        public int PointOfWeight;
        public Node Left;
        public Node Right;
        public bool IsRead;

        public Node(char _symbol, int num)
        {
            Symbol = _symbol;
            Number = num;
            Left = null;
            Right = null;
            PointOfWeight = 0;
            IsRead = false;
        }

        public Node(char _symbol, int num, int pointW)
        {
            Symbol = _symbol;
            Number = num;
            Left = null;
            Right = null;
            PointOfWeight = pointW;
            IsRead = false;
        }
    }
}

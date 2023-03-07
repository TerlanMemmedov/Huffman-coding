using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman_coding
{
    public class HuffmanTree
    {
        public Node Root;
        //public Node Left;
        //public Node Right;
        public List<Node> Nodes;

        public HuffmanTree(Dictionary<char, int> lettersInSentence)
        {
            Root = null;

            Nodes = CreateAndAddNodesToList(lettersInSentence);
        }

        public static List<Node> CreateAndAddNodesToList(Dictionary<char, int> letters)
        {
            List<Node> _Nodes = new List<Node>();

            foreach (var key in letters.Keys)
            {
                Node newNode = new Node(key, letters.GetValueOrDefault(key));

                _Nodes.Add(newNode);
            }

            return _Nodes;
        }

        public static void SortByNumber(List<Node> newNodes)
        {
            //normal selection sort
            for (int i = 0; i < newNodes.Count - 1; i++)
            {
                int index = i;
                for (int y = i + 1; y < newNodes.Count; y++)
                {
                    if (newNodes[index].Number > newNodes[y].Number)
                    {
                        index = y;
                    }
                }

                Node tempNode = newNodes[index];
                newNodes[index] = newNodes[i];
                newNodes[i] = tempNode;
            }

            //sort for pointweight highers is going to the end
            //bad for performance
            //bir for da elave edecem full yoxlasin bir nece defe
            for (int k = 0; k < newNodes.Count; k++)
            {
                for (int i = 1; i < newNodes.Count; i++)
                {
                    int index = i;
                    if (newNodes[i - 1].Number == newNodes[i].Number)
                    {
                        if (newNodes[i - 1].PointOfWeight > newNodes[i].PointOfWeight)
                        {
                            Node tempNode = newNodes[index];
                            newNodes[index] = newNodes[i - 1];
                            newNodes[i - 1] = tempNode;
                        }
                    }
                }
            }

            //no return for now :). I did it with ref
        }

        public void CreateTheTreeInFormat(List<Node> newNodes)
        {
            while (newNodes.Count > 1)
            {
                SortByNumber(newNodes);

                //meqsed leaf-leri saga cekmek
                //bunu yazsaq asagidakini else-de yazmaliyiq
                //if (newNodes[0].PointOfWeight > newNodes[1].PointOfWeight)
                //{
                //    Node first = newNodes[1];
                //    Node second = newNodes[0];
                //}

                Node first = newNodes[0];
                Node second = newNodes[1];

                Node newNode =
                    new Node('/', first.Number + second.Number, second.PointOfWeight + 1);

                newNode.Left = first;
                newNode.Right = second;

                Nodes.Add(newNode);

                newNodes.Remove(first);
                newNodes.Remove(second);
                newNodes.Add(newNode);
            }

            this.Root = newNodes[0];
        }

        public Dictionary<char, string> ReadAndCreateDictionary()
        {
            Dictionary<char, string> newDictioanryForEncoding = new Dictionary<char, string>();
            Node parent = this.Root;
            Node current = this.Root;

            string token = "";
            while (true)
            {
                if (current.Left == null && current.Right == null)
                {
                    //maybe check if it read is true;
                    //code and write to dictionary
                    //isRead to true
                    newDictioanryForEncoding.Add(current.Symbol, token);
                    current.IsRead = true;

                    //check if the code is only 1
                    ////if it is break;
                    //else return one node back
                    if (!token.Contains('0') && token != "")
                    {
                        //break
                        break;
                    }

                    current = parent;
                    token = token.Substring(0, token.Length - 1);

                    if (current == Root)
                    {
                        parent = current;
                    }
                    else
                    {
                        //finding parent
                        foreach (var NodeItem in Nodes)
                        {
                            if (NodeItem.Left == current || NodeItem.Right == current)
                            {
                                parent = NodeItem;
                            }
                        }
                    }
                }
                else if (current.Left != null && current.Left.IsRead != true)
                {
                    parent = current;
                    current = current.Left;
                    token += "0";
                }
                else if (current.Right != null && current.Right.IsRead != true)
                {
                    parent = current;
                    current = current.Right;
                    token += "1";
                }
                else if (current.Left.IsRead == true && current.Right.IsRead == true) //can write just else maybe ?????
                {
                    //if all sides are read so it is also read
                    current.IsRead = true;

                    //return to parent 
                    current = parent;
                    token = token.Substring(0, token.Length - 1);

                    //finding parent
                    if (current == Root)
                    {
                        parent = current;
                    }
                    else
                    {
                        //finding parent
                        foreach (var NodeItem in Nodes)
                        {
                            if (NodeItem.Left == current || NodeItem.Right == current)
                            {
                                parent = NodeItem;
                            }
                        }
                    }
                }
            }


            return newDictioanryForEncoding;
            //return Dictionary<char, string>
        }


        public List<string> Encode(string sentence, Dictionary<char, string> codingF)
        {
            List<string> encodedList = new List<string>();

            foreach (var letter in sentence)
            {
                encodedList.Add(codingF[letter]);
            }

            return encodedList;
        }
        public string Decode(List<string> encodedList, Dictionary<char, string> codingF)
        {
            string decoded = "";

            foreach (var item in encodedList)
            {
                foreach (var keyInCodingF in codingF.Keys)
                {
                    if (item == codingF[keyInCodingF].ToString())
                    {
                        decoded += keyInCodingF;
                    }
                }
            }

            return decoded;
        }
    }
}

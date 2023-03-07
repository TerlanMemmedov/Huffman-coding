// See https://aka.ms/new-console-template for more information
using Huffman_coding;

public class Program
{
    public static Dictionary<char, int> CountTheLetters(string sentence)
    {
        Dictionary<char, int> letters = new Dictionary<char, int>();

        foreach (var letter in sentence)
        {
            if (!letters.Keys.Contains(letter))
            {
                //int k = letters.GetValueOrDefault(letter);
                letters.Add(letter, 1);
            }
            else
            {
                letters[letter]++;
                //++
            }
        }

        return letters;
    }


    public static void Main(string[] args)
    {
        Console.WriteLine("Write something: ");
        string sentence = Console.ReadLine();
        sentence = sentence.ToLower();

        Dictionary<char, int> lettersInSentenceWithNum = CountTheLetters(sentence);

        HuffmanTree newTree = new HuffmanTree(lettersInSentenceWithNum);

        List<Node> newNodes = new List<Node>();
        foreach (Node nodeInTree in newTree.Nodes)
        {
            newNodes.Add(nodeInTree);
        }

        newTree.CreateTheTreeInFormat(newNodes);

        Dictionary<char, string> codingF = new Dictionary<char, string>();

        codingF = newTree.ReadAndCreateDictionary();

        string encodedSentence = "";
        List<string> encodedList = newTree.Encode(sentence, codingF);

        foreach (var item in encodedList)
        {
            encodedSentence += item;
        }

        Console.WriteLine(encodedSentence);

        string decodedSentence = newTree.Decode(encodedList, codingF);

        Console.WriteLine(decodedSentence);

        Console.Read();
    }
}

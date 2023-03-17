namespace DesignAddSearchWordsDataStructure
{
    internal class Program
    {
        #region Solution Wtih Array
        public class WordDictionary1
        {
            private readonly WordDictionary1[] children;
            private bool isEndWord;

            public WordDictionary1()
            {
                children = new WordDictionary1[26];
                isEndWord = false;
            }

            public void AddWord(string word)
            {
                WordDictionary1 curr = this;
                foreach (char letter in word)
                {
                    if (curr.children[letter - 'a'] == null)
                    {
                        curr.children[letter - 'a'] = new WordDictionary1();
                    }
                    curr = curr.children[letter - 'a'];
                }
                curr.isEndWord = true;
            }

            public bool Search(string word)
            {
                WordDictionary1 curr = this;
                for (int i = 0; i < word.Length; ++i)
                {
                    if (word[i] == '.')
                    {
                        foreach (var child in curr.children)
                        {
                            if (child != null && child.Search(word[(i + 1)..]))
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                    else
                    {
                        if (curr.children[word[i] - 'a'] == null)
                        {
                            return false;
                        }
                        curr = curr.children[word[i] - 'a'];
                    }
                }
                return curr != null && curr.isEndWord;
            }
        }
        #endregion

        #region Solution With Dictionary Time Limit Explanation
        public class WordDictionary
        {

            private class TrieNode
            {
                private readonly Dictionary<char, TrieNode> children;

                public TrieNode()
                {
                    children = new Dictionary<char, TrieNode>();
                }

                public Dictionary<char, TrieNode> Children => children;
            }

            private readonly TrieNode trie;
            public WordDictionary()
            {
                trie = new TrieNode();
            }

            public void AddWord(string word)
            {
                TrieNode root = trie;
                foreach (char letter in word)
                {
                    if (!root.Children.ContainsKey(letter))
                    {
                        root.Children.Add(letter, new TrieNode());
                    }
                    root = root.Children[letter];
                }
                root.Children.TryAdd('*', null);
            }

            private bool Search(string word, int index, TrieNode trie)
            {
                if (trie is null)
                    return false;
                TrieNode root = trie;
                for (int i = index; i < word.Length; ++i)
                {
                    if (word[i] == '.')
                    {
                        foreach (var child in root.Children.Values)
                        {
                            if (Search(word, i + 1, child))
                                return true;
                        }
                        return false;
                    }
                    if (!root.Children.ContainsKey(word[i]))
                    {
                        root.Children.Add(word[i], new TrieNode());
                    }
                    root = root.Children[word[i]];
                }
                return root != null && root.Children.ContainsKey('*');
            }

            public bool Search(string word)
            {
                return Search(word, 0, trie);
            }
        }
        #endregion
        static void Main(string[] args)
        {
            WordDictionary wordDictionary = new();
            wordDictionary.AddWord("bad");
            wordDictionary.AddWord("dad");
            wordDictionary.AddWord("mad");
            Console.WriteLine(wordDictionary.Search("pad")); // return False
            Console.WriteLine(wordDictionary.Search("bad")); // return True
            Console.WriteLine(wordDictionary.Search(".ad")); // return True
            Console.WriteLine(wordDictionary.Search("b..")); // return True
        }
    }
}
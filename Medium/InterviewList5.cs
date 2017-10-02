using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Medium
{
    public partial class InterviewList
    {
		//179. Largest Number
        //這題一開始就有想要用自訂的字串排序來解
        //但是邊解卻發現，特例多到見鬼了… 短的不一定對，長的不見得差，我以各領頭數字先分群
        //但每群中的排序卻需要類遞迴的解法，而且基準又是靠這個群的領先字元
        // 3，34，310，331 => 34，3，331，310 那個 3 的位置很難捉…
        //後來看了人家的解法，實在太聰明了… 就很簡單，兩個都拿來組組看，看那一組結果好就選它…xd
		public string LargestNumber(int[] nums)
		{
            if (nums.Count() == 0)
            {
                return "0";
            }
            
            List<string> data = new List<string>();
            foreach(var i in nums)
            {
                data.Add(i.ToString());
            }
            data.Sort(strcmp);

            string result = "";

            //資料只給了你一海面的0
            if (data.First()[0] == '0')
            {
                return "0";
            }

            foreach(var s in data)
            {
                result += s;
            }

            return result;
		}

        private int strcmp(string x, string y)
        {
            string s1 = x + y;
            string s2 = y + x;
            return s2.CompareTo(s1);
        }

		//200. Number of Islands
        //這題和上次那個找圍棋的超像，所以沒那麼笨還用遞迴了，它肯定給你超大地圖的
		public int NumIslands(char[,] grid)
		{
            int result = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if(canInfectIsland(grid, new Point(i,j)))
                    {
                        result++;
                    }
                }
            }

            return result;
		}

        private bool canInfectIsland(char[,] grid, Point p)
        {
            if (grid[p.X, p.Y] == '1')
            {
                Queue<Point> next = new Queue<Point>();
                grid[p.X, p.Y] = '@';
                next.Enqueue(p);

                while(next.Count()> 0)
                {
                    Point temp = next.Dequeue();

                    if(temp.X > 0 && grid[temp.X - 1, temp.Y] == '1')
                    {
                        grid[temp.X - 1, temp.Y] = '@';
                        next.Enqueue(new Point(temp.X - 1, temp.Y));
                    }
                    if (temp.X < grid.GetLength(0)-1 && grid[temp.X + 1, temp.Y] == '1')
					{
						grid[temp.X + 1, temp.Y] = '@';
                        next.Enqueue(new Point(temp.X + 1, temp.Y));
					}
                    if (temp.Y > 0 && grid[temp.X, temp.Y - 1] == '1')
                    {
                        grid[temp.X, temp.Y - 1] = '@';
                        next.Enqueue(new Point(temp.X, temp.Y - 1));
                    }

                    if (temp.Y < grid.GetLength(1) - 1 && grid[temp.X, temp.Y + 1] == '1')
                    {
                        grid[temp.X, temp.Y + 1] = '@';
                        next.Enqueue(new Point(temp.X, temp.Y + 1));
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

		//207. Course Schedule
        //這題也是像上面的找小島，只是得先把輸入資料轉個陣列，比較好寫檢查
        //比較難的是什麼時間點檢查循環，我一開始一直用 p.X 去檢查，但其實不對
        //每次進來就表示 p.X 這門課被需求了沒錯，要記下來
        //但是為了 p.X 得去描述 p.Y，但 p.Y 不能是已需求的課程，不然就循環了
		public bool CanFinish(int numCourses, int[,] prerequisites)
		{
            int[,] map = new int[numCourses, numCourses];

            for (int i = 0; i < prerequisites.GetLength(0); i++)
            {
                map[prerequisites[i, 0], prerequisites[i, 1]] = 1;
            }

            for (int i = 0; i < numCourses; i++)
            {
                for (int j = 0; j < numCourses; j++)
                {
                    if(map[i,j] == 1)
                    {
                        if (!canFinishChecking(map, new Point(i,j)))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
		}

        private bool canFinishChecking(int[,] map, Point p)
        {
            HashSet<int> classLog = new HashSet<int>();
            Queue<Point> next = new Queue<Point>();
            next.Enqueue(p);

            while (next.Count() > 0)
            {
                p = next.Dequeue();

                map[p.X, p.Y] = 0;
                classLog.Add(p.X);

                if(classLog.Contains(p.Y))
                {
                    return false;
                }

                for (int i = 0; i < map.GetLength(1); i++)
                {
                    if(map[p.Y, i] == 1)
                    {
                        next.Enqueue(new Point(p.Y, i));
                    }
                }
            }

            return true;
        }

		//208. Implement Trie (Prefix Tree) 
        //特里樹，專門處理 prefix 同的快速比較
		//https://leetcode.com/problems/implement-trie-prefix-tree/solution/
        //這題我是寫完一次過，可是我完全不知道怎麼測啊!!!! 那個輸入我怎麼改，不是 runtime error 就是 null false
        //從來沒看過 true ....Orz
		public class Trie
		{
            private TrieNode root = new TrieNode();

            public Trie()
			{

			}

			public void Insert(string word)
			{
                if (word.Length > 0)
                {
                    TrieNode curr = root;
                    foreach (char c in word)
                    {
                        curr = curr.Put(c);
                    }
                    curr.IsEnd = true;
                }
			}

            private bool startsWith(string prefix, out TrieNode last)
            {
				TrieNode curr = root;

				foreach (var c in prefix)
				{
					curr = curr.Get(c);
					if (curr == null)
					{
                        last = null;
						return false;
					}
				}

                last = curr;
				return true;
            }

			public bool Search(string word)
			{
                if(word.Length > 0)
                {
                    TrieNode temp;
                    bool res = startsWith(word,out temp);
                    return res && temp.IsEnd;
                }
                else
                {
                    return true;
                }
			}

			public bool StartsWith(string prefix)
			{
                if (prefix.Length > 0)
                {
					TrieNode temp;
					bool res = startsWith(prefix, out temp);
                    return res;
                }
                else
                {
                    return true;
                }
			}
		}

        public class TrieNode
        {
            private const int bound = 26;

            private TrieNode[] nextNode = new TrieNode[bound];

            public bool IsEnd { get; set; }

            public TrieNode Put (char c)
            {
                if (nextNode[c - 'a'] == null)
                {
                    nextNode[c - 'a'] = new TrieNode();
                }

                return nextNode[c - 'a'];
            }

            public TrieNode Get(char c)
            {
				if (nextNode[c - 'a'] != null)
				{
					return nextNode[c - 'a'];
				}
                else
                {
                    return null;
                }
            }

        }

        //210. Course Schedule II
        //這題是寫到目前為止，讓我最傷心的一題
        public int[] FindOrder(int numCourses, int[,] prerequisites)
        {
			//先建成DAG
			//有關 graph 的資料結構，學弟有大作可以參考
			//http://www.csie.ntnu.edu.tw/~u91029/Graph.html#1
            //這裡就是建那個 adjacency lists
            //degree 又有分 in-degree and out-degree，這裡是用 in-degree
            //目的是用來判定，這個節點的所有上遊節點，是否都被處理過了

			List<int>[] graph = new List<int>[numCourses];
            int[] indegree = new int[numCourses];
            for (int i = 0; i < prerequisites.GetLength(0) ; i++)
            {
                int needer = prerequisites[i, 0];
                int beneed = prerequisites[i, 1];

                if (graph[beneed] == null)
                {
                    graph[beneed] = new List<int>();
                }
                graph[beneed].Add(needer);

                //需求別人，數量加1
                indegree[needer]++;
            }

			//這裡就類似 BFS ，每次都只處理 需求量為 0 的元素，因為它沒有被需求，可以輸出
			//如果資料有循環，則循環區的所有 indegree 都是會大於0，因為每個元素都被需要
			//你會找不到一個開口去把它們的 indegree 減一，造成新的點不會進 zeroNex
			//只要沒辦法正常的跑完 numCourses 數量，就是不對
			//這種寫法，可以避開無窮迴圈的問題
			//我有實作另一篇討論的 BFS 作法，結果就卡在無窮迴圈不知如何處理，它的 visited 永遠空不了，所以它的輸出會重覆出現，所以它要加 if 過濾
			//https://discuss.leetcode.com/topic/13873/two-ac-solution-in-java-using-bfs-and-dfs-with-explanation

			List<int> result = new List<int>();
            Queue<int> zeroNext = new Queue<int>();
            for (int i = 0; i < indegree.Count(); i++)
            {
                //從所有的 0 開始 
                if (indegree[i] == 0)
                {
                    zeroNext.Enqueue(i);
                }
            }

            //正常的輸出，答案一定剛好等於 numCourses 的數量，多一少一都有問題
            for (int i = 0; i < numCourses; i++)
            {		

				if (zeroNext.Count() == 0)
                {
                    return new int[] { }; 
                }

                //每次取出就輸出
                int curr = zeroNext.Dequeue();
                result.Add(curr);

                if (graph[curr] != null)
                {
                    foreach (var child in graph[curr])
                    {
                        indegree[child]--;

						//只要 0，表示它不再被別人需要，輸出它
						if (indegree[child] == 0)
						{
                            zeroNext.Enqueue(child);
						}
                    }
                }				
            }       

            return result.ToArray();
        }

		//215. Kth Largest Element in an Array
		//這題其實只要不自己搞排序，就一點都不難
        //我順手用了自己作的 priority queue 來解，而且接受可重覆，但我假設移除最後一個元素的成本是常數
        //照文件來說，它應該是 O(n)
        //最強大解是利用自已寫的類 quick sort 來改善，可以作到 O(n)，但我還沒有自己實作 quck 的經驗，這裡我先跳過了
        public int FindKthLargest(int[] nums, int k)
		{
            if(nums.Count() == 0)
            {
                return -1;
            }

            // NLog(N) solution...
            //Array.Sort(nums);
            //return nums[nums.Length - k];

            //NLog(K)
            SortedList<int,int> kQueue = new SortedList<int,int>(Comparer<int>.Create((x, y) => { return x > y ? - 1 : 1; }));
            for (int i = 0; i < nums.Length; i++)
            {
                kQueue.Add(nums[i], nums[i]);
                if (kQueue.Count() > k)
                {
                    kQueue.RemoveAt(k);
                }
            }
            return kQueue.Last().Key;
		}

		//227. Basic Calculator II
		//題目本身並不難，先乘除後加減可以在 stack 中打混掉
		//真正難發現的是，加減的計算，在 stack 中的順序是不對的，要一路正序才正常
		//stack 是為了解決該先算的要先算，但一路正常算就不該是 stack 的事了
		//https://discuss.leetcode.com/topic/16935/share-my-java-solution
		//這個解法和我的思路很像，但他更精簡，乘除也是直接作，加減就是轉正負值
		//最後它只是把一堆值加起來，不像我有順序的問題，這樣看起來很精簡
		//https://discuss.leetcode.com/topic/16807/17-lines-c-easy-20-ms
		//這一個我覺得是類似的，但他的碼真的少到我不太理解，那個c++ 的 in >> n 這樣可以直接切出一個數字??
		public int Calculate(string s)
		{
            Stack<string> sk = new Stack<string>();

            s = s.Replace(" ", "");

            string currDigit = "";

			//把算元推到 stack 裡，有乘除會先算完，把算過的拿掉，再推進去
			Action<string> pushOperand = (opn2)=> 
            {
                if (sk.Count() == 0)
                {
                    sk.Push(opn2);
                    return;
                }

                string op = sk.Peek();

                if (op == "*" || op == "/")
                {
                    op = sk.Pop();
                    string opn1 = sk.Pop();

                    //caculate
                    int val = op == "*" ? int.Parse(opn1) * int.Parse(opn2) : int.Parse(opn1) / int.Parse(opn2);
                    sk.Push(val.ToString());
                }
                else
                {
                    sk.Push(opn2);
                }
            };

            //函式主體，把整個乘除先轉換掉，變成連續加減
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= '0' && s[i] <= '9')
                {
                    currDigit += s[i];

                    if (i == s.Length - 1)
                    {
                        //last operand												
                        pushOperand(currDigit);
                        currDigit = "";
                    }
                }
                else 
                {
                    pushOperand(currDigit);
                    currDigit = "";

                    sk.Push(s[i].ToString());
                }
            }

            //加減要一路正序的作過來才會對!! 這裡是最容易忘的
            List<string> ascData = sk.Reverse().ToList();
            int result = int.Parse(ascData[0]);
            for (int i = 1; i < ascData.Count() - 1; i+=2)
            {
                string op = ascData[i];
                string opn2 = ascData[i + 1];

                result = (op == "+") ? result + int.Parse(opn2) : result - int.Parse(opn2);
            }

            return result;
		}

		//230. Kth Smallest Element in a BST
		//這題很容題，就看會不會寫 DFS 加上 BST 在 inorder 下是由小到大，只要知道這個組合簡單
        public int KthSmallest(TreeNode root, int k)
		{
            currTh = 0;
            currValue = 0;
            recKth(root, k);
            return currValue;
		}

        int currTh = 0;
        int currValue = 0;
        private void recKth(TreeNode root, int k)
        {
            if (root == null)
            {
                return;
            }
            else
            {
                recKth(root.left, k);

                if (currTh < k)
                {
                    currValue = root.val;
                    currTh++;
                }
                else
                {
                    return;
                }

                recKth(root.right, k);
            }
        }


        //236. Lowest Common Ancestor of a Binary Tree
        //這題線上沒得測，用 DFS 遞迴寫應該不難，送上去後發現測大型的 case 又 Runtime error
        //所以猜測又是遞迴到死人… 想改用 BFS 但應該沒辦法解
        //結果看了別人的答案，還是用 DFS 遞迴解，只是更精簡
        //這裡要注意它為何就是把找到外傳就可以，因為，在 DFS 中如果你能找到，表示你已經到了最底退回來
        //所以你就是最低的那個答案，其它呼叫都只是回傳這個結果，我原本的寫法是回找到了幾個，答案也是對的
        //但就需要有個 lca 去記得第一次找到的對像，但我不清楚，為何這樣的遞迴在它測超長單邊樹時會死
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
			if (root == null || root == p || root == q)
			{
				return root;
			}
			else
			{
				TreeNode left = LowestCommonAncestor(root.left, p, q);
				TreeNode right = LowestCommonAncestor(root.right, p, q);

				if (left != null && right != null)
				{
					return root;
				}
				else if (left != null)
				{
					return left;
				}
				else if (right != null)
				{
					return right;
				}
				else
				{
					return null;
				}
			}
        }

        //private int recLCA(TreeNode root, TreeNode p, TreeNode q, ref TreeNode lca)
        //{
        //    if (root == null || lca != null)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        int me = root == p || root == q ? 1 : 0;
        //        int left = recLCA(root.left, p, q, ref lca);
        //        int right = recLCA(root.right, p, q, ref lca);

        //        if (me + left + right == 2 && me != 2 && left != 2 && right != 2)
        //        {
        //            lca = root;
        //        }

        //        return me + left + right;
        //    }
        //}
		

		public static void Test()
		{
            InterviewList obj = new InterviewList();

			var tree = new TreeNode(4) { left = new TreeNode(2) { left = new TreeNode(1), right = new TreeNode(3)}, right = new TreeNode(6) { left = new TreeNode(5), right = new TreeNode(7) } };
			//var tree = new TreeNode(4) { left = new TreeNode(2) { left = new TreeNode(1) }, right = new TreeNode(6) { right = new TreeNode(7) } };
			//1 4 4 2 2 4
			Console.WriteLine(obj.LowestCommonAncestor(tree, tree.left.left, tree.left.left).val);
			Console.WriteLine(obj.LowestCommonAncestor(tree, tree.left, tree).val);
			Console.WriteLine(obj.LowestCommonAncestor(tree, tree.right, tree).val);
			Console.WriteLine(obj.LowestCommonAncestor(tree, tree.left.left, tree.left).val);
			Console.WriteLine(obj.LowestCommonAncestor(tree, tree.left.left, tree.left.right).val);
			Console.WriteLine(obj.LowestCommonAncestor(tree, tree.left.left, tree.right.right).val);

			var tree2 = new TreeNode(4) { left = new TreeNode(2) { left = new TreeNode(1) { left = new TreeNode(3) { left = new TreeNode(7) { left = new TreeNode(9) } } } } };
			//7
            Console.WriteLine(obj.LowestCommonAncestor(tree2, tree2.left.left.left.left, tree2.left.left.left.left.left).val);

            //Console.WriteLine(obj.KthSmallest(tree, 6));
            //Console.WriteLine(obj.KthSmallest(tree, 7));

            //Console.WriteLine(obj.Calculate(" 3 + 51 * 21 "));
            //Console.WriteLine(obj.Calculate(" 30 / 2 / 2 + 5 * 2 * 2 "));
            //Console.WriteLine(obj.Calculate(" 0 "));
            //Console.WriteLine(obj.Calculate(" 3 "));
            //Console.WriteLine(obj.Calculate(" 3 + 5 "));
            //Console.WriteLine(obj.Calculate(" 3 - 5 * 2"));
            //Console.WriteLine(obj.Calculate(" 3 * 5 -20 "));
            //Console.WriteLine(obj.Calculate(" 1 - 1 +1 "));

            //var temp = obj.FindOrder(4, new int[,]{
            //    {1,0},
            //    {2,1},
            //    {0,2},
            //    {3,2},
            //});

			//var temp = obj.MaxProduct(new int[] { -2, 2, 2, -2, -1 });

			//ListNode h1 = new ListNode(1) { next = new ListNode(1){ next = new ListNode(2)} } ;
			//ListNode h2 = new ListNode(5){ next = new ListNode(4)  };

			//var temp = (8.0 / 55).ToString();
			//var temp = obj.FractionToDecimal(-1, 5);

			//obj.Solve(new char[,]
			//{
			//{'X','X','X','X'},
			//{'X','O','O','X'},
			//{'O','X','O','X'},
			//    {'X','O','O','X'},
			//});

			//var temp = obj.NumIslands(new char[,] {
			//	{'1','1','1','0'},
			//	{'0','0','1','0'},
			//	{'0','0','1','0'},
			//	{'0','1','1','0'},
			//});

			//Console.Write(temp);

			//----------------------------------------------------------

			//var temp = obj.IsValidSudoku(new char[,]
			//{
			//    {'.','2','3','4','5','6','7','8','9'},
			//    {'8','.','.','.','.','.','.','.','.'},
			//    {'7','.','.','.','.','.','.','.','.'},
			//    {'6','.','.','.','.','.','.','.','.'},
			//    {'5','.','.','.','.','.','.','.','.'},
			//    {'4','.','.','.','.','.','.','.','.'},
			//    {'3','.','.','.','.','.','.','.','.'},
			//    {'2','.','.','.','.','.','.','.','.'},
			//    {'.','.','.','.','.','.','.','.','.'}
			//});       

			//ListNode h1 = new ListNode(5) { next = new ListNode(4) { next = new ListNode(5) } };
			//ListNode h2 = new ListNode(5) { next = new ListNode(6) { next = new ListNode(4) } };
			//var temp = obj.AddTwoNumbers(h1, h2);   
			//var tree = new TreeNode(1) { left = new TreeNode(2) { left = new TreeNode(4), right = new TreeNode(5)}, right = new TreeNode(3) { left = new TreeNode(6), right = new TreeNode(7) } };
			//List<int> temp = new List<int>();
			//obj.BST_DFS(tree, temp);

		}

		private void BST_DFS(TreeNode root, List<int> bTree)
		{
			if (root == null)
			{
				return;
			}
			else
			{
				//PreOrder
				bTree.Add(root.val);
				BST_DFS(root.left, bTree);
				BST_DFS(root.right, bTree);
			}
		}
    }
}
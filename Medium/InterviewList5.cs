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

        /*

        //210. Course Schedule II
        public int[] FindOrder(int numCourses, int[,] prerequisites)
        {
            //先建成以目標課為索引，需求課組串列
            List<int>[] graph = new List<int>[numCourses];
            for (int i = 0; i < prerequisites.GetLength(0) ; i++)
            {
                int x = prerequisites[i, 0];
                int y = prerequisites[i, 1];

                if (graph[x] == null)
                {
                    graph[x] = new List<int>();
                }
                graph[x].Add(y);
            }

            //幫每堂建比重
            int[] degree = new int[numCourses];
            for (int i = 0; i < graph.Count(); i++)
            {
                if (graph[i] != null)
                {
                    degree[i] = graph[i].Count();
                }
            }

            //利用 degree 進行排隊，愈重愈後面，輕的先作，這裡還要接受重覆的 degree 作key
            SortedList<int, int> next = new SortedList<int, int>(Comparer<int>.Create((x, y) => 
            {
				if (x > y)
				{
					return 1;
				}
				else
				{
					return -1;
				}
            }));
            
            for (int i = 0; i < degree.Count(); i++)
            {
                next.Add(degree[i], i);
            }

            //沒有 degree 為 0 的起點，表示必有循環，就算有為0的，也不代表沒循環
            if (next.Where(kv => kv.Value == 0).Count() == 0)
            {
                return new int[] { };
            }

            //這裡就類似 BFS ，每次都把下個點拿出來，但要照 degree 排隊
            HashSet<int> duplicate = new HashSet<int>();
            List<int> result = new List<int>();


            //沒有循環下，就只會輸出 numcourses 個，不多也不少
            while (result.Count < numCourses) 
            {
                var curr = next.First();
                next.RemoveAt(0);

                if (!duplicate.Contains(curr.Value))
                {
					//有輸出處理過的點，就不再處理了
					result.Add(curr.Value);
                    duplicate.Add(curr.Value);
                }

				if (graph[curr.Value] != null)
				{
					foreach (var child in graph[curr.Value])
					{
						next.Add(degree[child], child);
					}
				}
            }

            //如果全輸出完，就不該有剩，有剩就表示有重覆了
            //if(next.Count > 0)
            //{
            //    return new int[] { };
            //}

            //能跑完，表示條件都輸出了，最有依賴的會在最後面，所以要反轉答案
            result.Reverse();
            result = result.Distinct().ToList();

            //if(result.Count() != )
            return result.ToArray();
        }
        */



		

		public static void Test()
		{
            //List<string> temps = new List<string>() {"34","310","9","5","3","95","51","0" };
            //temps.Sort();

            InterviewList obj = new InterviewList();
            var temp = obj.FindOrder(3, new int[,]{
                //{1,0},
                {2,0},
                {0,2},
                {1,2},
            });

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

			Console.Write(temp);

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
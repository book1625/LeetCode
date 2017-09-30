using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Medium
{
    public partial class InterviewList
    {
        //56. Merge Intervals
        public IList<Interval> Merge(IList<Interval> intervals)
        {
            List<Interval> temp = new List<Interval>(intervals);
            temp.Sort((x, y) => { return x.start.CompareTo(y.start); });

            List<Interval> result = new List<Interval>();

            Interval curr = null;

            foreach(var intl in temp)
            {
                if(curr == null)
                {
                    curr = intl;
                }
                else if(curr.end >= intl.start)
                {
                    //merge, make sure increasing
                    if (intl.end > curr.end)
                    {
                        curr.end = intl.end;
                    }
                }
                else
                {
                    //keep
                    result.Add(curr);
                    curr = intl;
                }
            }

            if(curr != null)
            {
                result.Add(curr);
            }

            return result;
		}

		//62. Unique Paths
        //這題也不用幻想用數學階層作，因為最多到 17階就爆int，23階就爆long，它要到100階，沒有函式庫就免談…
        //這題的 dp 其實很容易，是一開始就想後面的版本所以難推，先想 m*n 的版本就比較好推
        //它還可以再簡化不用開到 m*n 的空間，但我覺得不超過 100 * 100，減空間沒有什麼影響
        //它可以只記得當前這一排與前一個加好的數字就好
        public int UniquePaths(int m, int n)
        {
            int[,] map = new int[m,n];
            for (int i = 0; i < m; i++)
                map[i,0] = 1;
            for (int j = 0; j < n; j++)
                map[0,j] = 1;

            for (int i = 1; i < m; i++)
                for (int j = 1; j < n; j++)
                {
                    map[i, j] = map[i, j - 1] + map[i - 1, j];   
                }

            return map[m - 1, n - 1];
        }

		/* 這個解法只是為了驗證自已在寫遞迴有進步了，效率肯定是過不了
		public int UniquePaths(int m, int n)
		{
            totalPath = 0;
            recPath(m - 1, n - 1);
            return totalPath;
		}

        private int totalPath = 0;
        private void recPath(int m, int n)
        {
            if(m == 0 && n == 0)
            {
                totalPath++;
                return;
            }

            if(m > 0)
            {
                recPath(m-1, n);
            }

            if(n > 0)
            {
                recPath(m, n-1);
            }
        }
        */

		//73. Set Matrix Zeroes
        //這題不想太燒腦，我只開兩個 hashset 算客氣
        //其實可以用 第一row 和第一col 去記誰該設成0
		public void SetZeroes(int[,] matrix)
		{
            HashSet<int> zeroRow = new HashSet<int>();
            HashSet<int> zeroCol = new HashSet<int>();
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if(matrix[i,j] == 0)
                    {
                        zeroRow.Add(i);
                        zeroCol.Add(j);
                    }
                }
            }

            foreach(var r in zeroRow)
            {
                for (int i = 0; i < n; i++)
                {
                    matrix[r, i] = 0;
                }
            }

            foreach(var c in zeroCol)
            {
                for (int i = 0; i < m; i++)
                {
                    matrix[i, c] = 0;
                }
            }
		}

		//75. Sort Colors
        //這題我一開始就知道用快慢指標來作移動交換，但是我想要移動一輪就作完，所以我設計了兩個指標
        //但是我就會面臨到換來的東西是要再處理的，這會造成我的 curr 跑的比另兩隻指通指標都慢，因為它得在原地
        //如果只處理一個，就沒有這種問題，兩隻一定一快一慢
        //後來才想到，那就作兩次 O(N) 不就好了…又沒有加複雜度
		public void SortColors(int[] nums)
		{
            Action<int, int> swap = (x, y) =>
            {
                int temp = nums[x];
                nums[x] = nums[y];
                nums[y] = temp;
            };

            int left = 0;
            int curr = 0;
            while (curr < nums.Length)
            {
                if (nums[curr] == 0)
                {
                    swap(curr, left);
                    left++;
                }
                curr++;
            }

            int right = nums.Length -1;
            curr = nums.Length - 1;
            while(curr >= 0)
            {
                if(nums[curr] == 2)
                {
                    swap(curr, right);
                    right--;
                }
                curr--;
            }
		}

		//78. Subsets
        //秒想用遞迴生，開始有感覺了…
        //其實我覺得，用字串組比較方便，最後再轉回來…，不然那個集合一旦忘了複制就死人了…
		public IList<IList<int>> Subsets(int[] nums)
		{
            List<IList<int>> result = new List<IList<int>>();
            recSubSet(new List<int>(nums), 0, new List<int>(), result);
            return result;
		}

        private void recSubSet(List<int> src, int ind, IList<int> currStr, List<IList<int>> result)
        {
            if(ind < src.Count())
            {
                int temp = src[ind];

                currStr = new List<int>(currStr);
                recSubSet(src, ind+1, currStr, result);

                currStr = new List<int>(currStr);
                currStr.Add(temp);
                recSubSet(src, ind+1, currStr, result);
            }
            else
            {
                result.Add(currStr);
            }
        }

		//79. Word Search
        //這題又再次開了眼界… 當然，一開始對這種指數型的展開，都會意識到要遞迴
        //所以我也寫了遞迴，但是，卻死在題目的要求上，它要求用過的字不能再用
        //這時我就呆了，因為如果亂改 board 的值，那其它的遞迴答案也都會被影響
        //這時我才學到一招大絕，如果我在發動遞迴前與後，都把亂改的值再進行改回，則在 DFS 的過程中
        //亂改的值會一直先往深處保留下去，如果往回退時，就會一路又改回來到本次遞迴發動時的狀態
        //然後進入另一個遞迴，同上面的動作，一直到所有往下的遞迴結束，我就把自己也修正回來，回到我上層的遞迴
        //這樣就完全解決了遞迴需要各自有資料的需求，完全用同一個空間就可以作到
        //另外，我在一開始，就找出所有的字母的起點，還建字典查，實在很無聊，因為它根本不是效率的動點
        //只要全掃一次，首字對上就發動遞迴，全掃一次的成本也差不那去，真正貴的是遞迴，還把時間浪費在寫那個建字典…阿呆
		public bool Exist(char[,] board, string word)
		{
            if(word.Length == 0|| board.Length == 0)
            {
                return false;
            }

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    char c = board[i, j];                   
                    char first = word[0];
                    if (c == first)
                    {
                        if(recExist(board, i,j,word))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }       

        private bool recExist(char[,] board, int x, int y, string target)
        {
            int xBound = board.GetLength(0) - 1;
            int yBound = board.GetLength(1) - 1;
            if(target.Length == 0)
            {
                return true;
            }
            else
            {
                if (x >= 0 && x <= xBound && y >= 0 && y <= yBound)
                {
					char tar = target[0];
					target = target.Substring(1);

                    if (board[x, y] != tar)
                    {
                        return false;
                    }
                    else
                    {
                        board[x, y] = (char)((int)tar << 8);

                        bool isGet = recExist(board, x - 1, y, target)
                            || recExist(board, x + 1, y, target)
                            || recExist(board, x, y - 1, target)
                            || recExist(board, x, y + 1, target);
                        
                        board[x, y] = (char)((int)board[x,y] >> 8);
                        return isGet;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        //91. Decode Ways
        public int NumDecodings(string s)
        {
			if (s.Length == 0 || s[0] == '0')
			{
				return 0;
			}

            //dp 表在記，到目前這個數字，可以得到多少組合
            //數字有幾種情況…，寫在碼裡作說明
            int[] dp = new int[s.Length];
            dp[0] = 1;

            for (int i = 1; i < dp.Length; i++)
            {
                if (i + 1 < dp.Length && s[i+1] == '0')
                {
                    //這個數字被後面的 0 限定了，它目前無法產生任何組合的運算
                }
                else
                {
                    //進來這，表示數字是自由的，那它又有兩種可能

                    //它就是個可以獨立解碼的數，所以不含 0
                    //那它的值就是前一個碼所能得到的組合數，加上它並沒有新增組合
                    if(s[i] >= '1' && s[i] <= '9')
                    {
                        //non zero digit
                        dp[i] += dp[i - 1];
                    }

                    //它如果可以和前面的數字作組合，則我們可以得到額外一條路線的所有組合
                    //想像在兩個節點前，共有 n 組合，然後後面來的兩個數字，可以合併，也可以分開
                    //如果我選擇分開，那就有前一個數字的組合那麼多組
                    //如果我選擇組合，那就有 n 組，這兩種加起來才是我當前的所有可能…
                    //這裡為了避最開頭就可以組合造成的超索引，所以加入判斷，不然初值會一直不對
                    if (s[i - 1] == '1' && s[i] >= '0' && s[i] <= '9')
                    {
                        dp[i] += i-2 >= 0 ? dp[i - 2] : 1;
                    }
                    else if (s[i - 1] == '2' && s[i] >= '0' && s[i] <= '6')
                    {
                        dp[i] += i-2 >= 0 ? dp[i - 2] : 1;
                    }
                }
            }
            return dp[s.Length - 1];
        }

		/* 可惜了，這題有 dp 解，所以遞迴雖對卻不夠看了…
        public int NumDecodings(string s)
		{
			if (s.Length == 0 || s[0] == '0')
			{
				return 0;
			}

			recNumDecode(s, 0);
			return total;
		}

		private int total = 0;
		private void recNumDecode(string s, int ind)
		{
			if (ind == s.Length)
			{
				//is over...
				total++;
			}
            else if (s[ind] == '0')
            {
                //this is wrong path, we cannot deal 0
                //這個條件很容易忽略，我是因為最後剩一個0 才發現
                //測 202110 就知道了
                //有了這個檢查，反而下面原本都在查0的就不管它了，反正遞進去也是又 return 出來
                return;
            }
			else if (ind == s.Length - 1)
			{
				//is final
				recNumDecode(s, ind + 1);
			}
			else
			{
				//at least two digits
				if (s[ind] == '1')
				{
                    recNumDecode(s, ind + 1);				
					recNumDecode(s, ind + 2);
				}
				else if (s[ind] == '2' && s[ind + 1] <= '6')
				{
                    recNumDecode(s, ind + 1);				
					recNumDecode(s, ind + 2);
				}
				else
				{
					recNumDecode(s, ind + 1);
				}
			}
		}
        */


		//94. Binary Tree Inorder Traversal
        //這是學校題了，只是雄雄看到 inorder traversal 時呆了
        //除了 DFS BFS 外，DFS 還 preorder inorder postorder 三種 lay 法
		public IList<int> InorderTraversal(TreeNode root)
		{
			List<int> result = new List<int>();
			recInorder(root, result);
			return result;
		}

        private void recInorder(TreeNode root, List<int> result)
		{
			if (root == null)
			{
                return;
			}
			else
			{
                recInorder(root.left, result);
                result.Add(root.val);
                recInorder(root.right, result);
			}
		}

		//98. Validate Binary Search Tree
        //這題花了一個多小時，真的是笨了，上面都學到三種lay法了，不會拿來lay一次就知了
        //別人有寫一個把最大最小值傳入遞回的，很厲害，在每次進子樹時動態的決定要新的邊界，但總是保留一邊
		public bool IsValidBST(TreeNode root)
        {
            List<int> bTree = new List<int>();
            recIsValidBST(root, bTree);
            for (int i = 0; i< bTree.Count()-1; i++)
            {
                if(bTree[i] >= bTree [i+1])
                {
                    return false;
                }
            }

            return true;
		}

        private void recIsValidBST(TreeNode root, List<int> bTree)
        {
            if (root == null)
            {
                return;
            }
            else
            {
                recIsValidBST(root.left, bTree);
                bTree.Add(root.val);
                recIsValidBST(root.right, bTree);
            }
        }

        //102. Binary Tree Level Order Traversal
        //很好，這題沒卡到，標準 BFS 先前有學過了，用 queue 的概念作
		public IList<IList<int>> LevelOrder(TreeNode root)
		{
            List<IList<int>> result = new List<IList<int>>();
            List<TreeNode> que = new List<TreeNode>();
            que.Add(root);

            while(que.Count() > 0)
            {
                List<TreeNode> ls = new List<TreeNode>();
                List<int> tempResult = new List<int>();
                foreach(var q in que)
                {
                    if (q != null)
                    {
                        tempResult.Add(q.val);
                        ls.Add(q.left);
                        ls.Add(q.right);
                    }
                }

                que = ls;
                if (tempResult.Count() > 0)
                {
                    result.Add(tempResult);
                }
            }

            return result;
		}

		//103. Binary Tree Zigzag Level Order Traversal
        //這題只是上題加點變化，個人覺得無聊…不知道有啥用途
		public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
		{
			List<IList<int>> result = new List<IList<int>>();
			List<TreeNode> que = new List<TreeNode>();
			que.Add(root);

            int loop = 1;
			while (que.Count() > 0)
			{
				List<TreeNode> ls = new List<TreeNode>();
				List<int> tempResult = new List<int>();
				foreach (var q in que)
				{
					if (q != null)
					{
						tempResult.Add(q.val);
						ls.Add(q.left);
						ls.Add(q.right);
					}
				}

				que = ls;
				if (tempResult.Count() > 0)
				{
                    if (loop % 2 == 0)
                    {
                        tempResult.Reverse();
                    }
                    result.Add(tempResult);
				}
                loop++;
			}

			return result;
		}		

        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
			/* 先記一下測試，不太好找出來 XD
            [1,2,4,5,3,6,7]
            [4,2,5,1,6,3,7]
            */

			//沒辦法算的資料
			if (preorder.Length == 0 || inorder.Length == 0)
            {
                return null;
            }

			//real root value is at the first of preorder
			TreeNode root = new TreeNode(preorder[0]);

            int mid = -1;
            for (int i = 0; i < inorder.Length; i++)
            {
                if(inorder[i] == root.val)
                {
                    mid = i;
                }
            }

            //這裡 mid 一定要有值才合理

            //利用 mid 切開 陣列

            int[] leftInord = new int[mid];
            Array.Copy(inorder, 0, leftInord,0, leftInord.Length);

            int[] rightInord = new int[inorder.Length - 1 - mid];
            Array.Copy(inorder, mid+1, rightInord, 0, rightInord.Length);

            int[] leftPreord = new int[leftInord.Length];
            Array.Copy(preorder, 1, leftPreord, 0, leftPreord.Length);

            int[] rightPreord = new int[rightInord.Length];
            Array.Copy(preorder, leftPreord.Length + 1, rightPreord,0, rightPreord.Length);

            root.left = BuildTree(leftPreord, leftInord);
            root.right = BuildTree(rightPreord, rightInord);

            return root;
        }


        //127. Word Ladder
        //這個寫法是照人家的解寫出來的，但我其實有些不認同
        //用湊字去猜，字長會成為新的參數，雖然我實測20個字元，就是要猜 25*20 個字
        //雖然級數沒有用 n 來的高，但也一定存在反例
        //這種站在小集合掃大集合的想法，有時候也很難反應過來，我一開始就是想說 25**20 大的見鬼怎麼可能全拿出來比
        //所以一直沒反應到，一次只會改一個位置，只有25種變化
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            HashSet<string> wordList2 = new HashSet<string>(wordList);
			List<string> next = new List<string>();
			HashSet<string> visited = new HashSet<string>();
            next.Add(beginWord);
            int loop = 1;
            while (next.Count() > 0)
            {
                List<string> tempNext = new List<string>();

                foreach (string curr in next)
                {
                    if(curr == endWord)
                    {
                        return loop;
                    }

                    //create all possible word
                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        for (int i = 0; i < curr.Length; i++)
                        {
                            if(curr[i] != c)
                            {
                                char[] temp = curr.ToArray();
                                temp[i] = c;
                                string str = new string(temp);

                                if(!visited.Contains(str) && wordList2.Contains(str))
                                {
                                    tempNext.Add(str);
                                    visited.Add(str);
                                }
                            }
                        }
                    }
                }

                next = tempNext;
                loop++;
            }

            return 0;
        }

		/* 
         * 這個寫法，我認為精神有作到了，只是輸在字果多了就開始慢，因為每次都要把沒用過的字再作一次字對
         * 
		public int LadderLengthOld(string beginWord, string endWord, IList<string> wordList)
		{
			//1 全等 0 只差一碼 -1 差很大
			Func<string, string, int> compare = (x, y) =>
			{
				if (x == y)
					return 1;

				int fuzzy = 1;
				for (int i = 0; i < x.Length; i++)
				{
					if (x[i] != y[i])
					{
						fuzzy--;
					}

					if (fuzzy < 0)
					{
						return -1;
					}
				}

				return 0;
			};

			List<string> next = new List<string>();
			HashSet<int> visited = new HashSet<int>();
			next.Add(beginWord);
			int loop = 1;
			while (next.Count() > 0)
			{
				List<string> tempNext = new List<string>();

				foreach (string curr in next)
				{
					if (curr == endWord)
					{
						return loop;
					}

					for (int i = 0; i < wordList.Count(); i++)
					{
						if (!visited.Contains(i))
						{
							int cmp = compare(curr, wordList[i]);
							if (cmp == 0)
							{
								tempNext.Add(wordList[i]);
								visited.Add(i);
							}
						}
					}
				}

				next = tempNext;
				loop++;
			}

			return 0;
		}
        */

		//130. Surrounded Regions
		/*
          原始版本的遞迴，對所有的0發動遞回檢查，成本太高也太暴力
          後來看人家的想法，發現，有問題的就是一路從邊界感染進來的
          所以中途改寫了一個螺旋式從外感染到內的算法，寫完才發現完全不行，因為有一種狀態是由一個破口感染到，然後開始沿這目前這一層傳開
          但只要它的方向和我們處理的順序不一致，就會變成錯誤 

           xxxxxxoxx    由上而下處理就會對，但由下而上處理就只剩一個有判斷到
           xxxxxxoxx
           xxxxxxoxx
           xxxooooxx
           xxxxxxxxx

          後來，又改以遞回把外圈的資料掃一次，這個版本理論上答案對了，但是它測了一個很大的資料，應該造成我遞迴到爆掉，和第一解死的差不多
          為了避免大量呼遞迴，其實可以用一個 queue 來代取代它，把所有有問題的點打入 queue，然後拿出來就檢查四方是否感染新的點，有就再打入 queue
          直到 queue 沒有東西，事情就結束，這樣作的計算量其實沒有遞迴少，但好在不用一直呼函式造成 stack 爆炸
          
        */
		public void Solve(char[,] board)
		{
			int xBound = board.GetLength(0);
			int yBound = board.GetLength(1);
            Queue<Point> next = new Queue<Point>();

			for (int i = 0; i < xBound; i++)
			{
                next.Enqueue(new Point(i,0));
                next.Enqueue(new Point(i,yBound-1));
			}

			for (int i = 0; i < yBound; i++)
			{
                next.Enqueue(new Point(0,i));
                next.Enqueue(new Point(xBound - 1, i));
			}

            while(next.Count() > 0)
            {
                Point p = next.Dequeue();

                if (board[p.X, p.Y] == 'O')
                {
                    board[p.X, p.Y] = '@';               

                    if (p.X > 0 && board[p.X - 1, p.Y] == 'O')
                    {
                        next.Enqueue(new Point(p.X - 1, p.Y));
                    }

                    if (p.X < xBound - 1 && board[p.X + 1, p.Y] == 'O')
                    {
                        next.Enqueue(new Point(p.X + 1, p.Y));
                    }

                    if (p.Y > 0 && board[p.X, p.Y - 1] == 'O')
                    {
                        next.Enqueue(new Point(p.X, p.Y - 1));
                    }

                    if (p.Y < yBound - 1 && board[p.X, p.Y + 1] == 'O')
                    {
                        next.Enqueue((new Point(p.X, p.Y + 1)));
                    }
                }
            }


            //reupdate the symbol
			for (int i = 0; i < xBound; i++)
			{
				for (int j = 0; j < yBound; j++)
				{
					if (board[i, j] == 'O')
					{
						board[i, j] = 'X';
					}
					else if (board[i, j] == '@')
					{
						board[i, j] = 'O';
					}
				}
			}
		}

        private class Point 
        {
            public int X;
            public int Y;
            public Point(int x, int y) { X = x; Y = y; }
        }
		
		/*
		public void Solve(char[,] board)
        {
            int xBound = board.GetLength(0);
            int yBound = board.GetLength(1);

            for (int i = 0; i < xBound; i++)
            {   
                recSolve(board, i,0);
                recSolve(board, i,yBound-1);
            }

            for (int i = 0; i < yBound; i++)
            {
                recSolve(board, 0, i);
                recSolve(board, xBound-1, i);
            }

            for (int i = 0; i < xBound; i++)
            {
                for (int j = 0; j < yBound; j++)
                {
                    if(board[i,j] == 'O')
                    {
                        board[i, j] = 'X';
                    }
                    else if(board[i,j] == '@')
                    {
                        board[i, j] = 'O';
                    }
                }
            }
        }

        private void recSolve(char[,] board, int x, int y)
        {
            //out of boundary
            if( x < 0 || x >= board.GetLength(0) || y < 0 || y >= board.GetLength(1))
            {
                return;
            }

            if(board[x,y] == 'O')
            {
                board[x, y] = '@';
                recSolve(board, x - 1, y);
                recSolve(board, x + 1, y);
                recSolve(board, x, y - 1);
                recSolve(board, x, y + 1);
            }
        }
        */

		
    }
}
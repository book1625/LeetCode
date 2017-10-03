using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Medium
{
    public partial class InterviewList
    {
        //238. Product of Array Except Self
        public int[] ProductExceptSelf(int[] nums)
        {
            if (nums.Length < 1)
            {
                return nums;
            }

            int[] forwardPd = new int[nums.Length];
            int[] reversePd = new int[nums.Length];
            int currFP = 1;
            int currRP = 1;

            for (int i = 0; i < nums.Length; i++)
            {
                forwardPd[i] = currFP;
                currFP *= nums[i];
            }

            for (int i = nums.Length - 1; i >= 0; i--)
            {
                reversePd[i] = currRP;
                currRP *= nums[i];
            }

            int[] result = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                result[i] = forwardPd[i] * reversePd[i];
            }
            return result;
		}

        //240. Search a 2D Matrix II
        //這題一開始用 queue 去作往右下的展開，以為沒遞迴應會過
        //但沒想到由於有排序，所以有這麼聰明的想法，可以作到 O(m+n)
        //比起我的 O(m*n) 強多了…
        public bool SearchMatrix(int[,] matrix, int target)
        {
            if (matrix.Length == 0 || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
            {
                return false;
            }

            int row = 0;
            int col = matrix.GetLength(1) - 1;

            while (col >= 0 && row < matrix.GetLength(0))
            {
                //下面的兩句話能成立是因為我們一路從右下角走下來
                if (matrix[row, col] == target)
                {
                    return true;
                }
                else if (matrix[row, col] > target)
                {
                    //如果我的值大過目標，那我所在的這一個column 就沒用了
                    //因為下面的 column 都比我還大，上面的都查過了
                    col--;
                }
                else
                {
                    //如果我的值小過目標，那我所在的這一個 row 就全沒用了
                    //因為右邊的值都比我還小，自然也比目標下，而左邊都查過了
                    row++;
                }
            }
            return false;
        }

		/*
		public bool SearchMatrix(int[,] matrix, int target)
		{
            //如果我的值比目標小，它有可能在我的右或下…

            if (matrix.Length == 0)
            {
                return false;
            }

            int xBound = matrix.GetLength(0) - 1;
            int yBound = matrix.GetLength(1) - 1;

            Queue<Point> next = new Queue<Point>();
            next.Enqueue(new Point(0,0));
            while (next.Count() > 0)
            {
                Point curr = next.Dequeue();

                if (matrix[curr.X, curr.Y] == target)
                {
                    return true;
                }
                else if (matrix[curr.X, curr.Y] < target)
                {
                    if (curr.X < xBound)
                    {
                        next.Enqueue(new Point(curr.X + 1, curr.Y));
                    }
                    if (curr.Y < yBound)
                    {
                        next.Enqueue(new Point(curr.X, curr.Y + 1));
                    }
                }// rest is useless point
            }

            return false;
		}
		*/

		//279. Perfect Squares
		//一開始沒想法，就想說每次都選一個最近的，貪婪法，結果卻沒有先想 12 就錯了… XD 都寫了.. 
		//這個 dp 法是看解答作的，精簡而且有神，它還有多種其它解，我現在沒時間一個個試
		//https://discuss.leetcode.com/topic/24255/summary-of-4-different-solutions-bfs-dp-static-dp-and-mathematics
		public int NumSquares(int n)
		{
            if (n == 0)
            {
                return 0;
            }

            int[] dp = new int[n+1];
            dp[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                int minNum = int.MaxValue;
                for (int sq = 1; sq * sq <= i; sq++)
                {
                    //每個數字的最佳組合是一路算出來的
                    //在算最新數字 i 時，考慮把所有的平方數拿來湊一次，不足的值必小於我，所以它的最佳也算過，我就是選它再多配上一個平方數
                    //在所有的過程中，選個最小的組合，就是我的最佳化!!
                    minNum = Math.Min(minNum, dp[i - sq * sq] + 1);
                }
                dp[i] = minNum;
            }

            return dp[n];
		}

		//287. Find the Duplicate Number
		//https://discuss.leetcode.com/topic/25580/two-solutions-with-explanation-o-nlog-n-and-o-n-time-o-1-space-without-changing-the-input-array
		//一開始我想要用  Xor 去解，但是一直找不到方法，連續 XOR 是用來找某個不見的值，或某個不重覆出現的值
        //看完解答，先實作 nLogn 的 binary search 法 
        //然後去看那個雙指標的解法 O(n) space O(1) ，我覺得有幾件事有困難... 
        //首先，把陣列想像成 linked list 這件事就不太實際，除非是很愛用陣列寫 list 的人
        //再來，用一快慢指標，造成在循環區相遇，這個我能理解沒問題，但是，再重走一次，每人都走一步，就會剛好在開口相遇… Orz，這應該哪裡想的到…
        //他們一個走一步，一個走兩步，在循環區的某點遇，然後它們離入口的距離會剛好等於從頭走到路口的距離，這件事要證明，叫我空想我想不出來
        public int FindDuplicate(int[] nums)
		{
            int beg = 1;
            int end = nums.Length - 1;

            int result = -1;
            while (beg <= end)
            {
                int mid = (beg + end) / 2;

                int count = 0;
                foreach (var n in nums)
                {
                    if (n <= mid)
                    {
                        count++;
                    }
                }

                //所有比我小的值，1..mid 應該有 mid 個，超過表示，這一段值，有重覆
                if (count > mid)
                {
                    end = mid - 1;
                    result = mid;
                }
                else
                {
                    beg = mid + 1;
                }
            }

            return result;
		}

		//289. Game of Life
        //這題實在有點無聊，那個 inplace 只要用編碼混過去讓程式一樣知道它原來的狀態就好了
        //最後只是再把編碼刷成死活而已…
		public void GameOfLife(int[,] board)
		{
            int xBound = board.GetLength(0);
            int yBound = board.GetLength(1);

            int toDie = 2;
            int toLive = -1;

            for (int x = 0; x < xBound; x++)
            {
                for (int y = 0; y < yBound; y++)
                {
                    int liveCount;
                    int dieCount;
                    GetSurrond(board, x, y, out liveCount, out dieCount);
                    if (board[x, y] == 0)
                    {
                        if (liveCount == 3)
                        {
                            board[x, y] = toLive;
                        }
                    }
                    else //board[x,y] == 1
                    {
                        if (liveCount < 2 || liveCount > 3)
                        {
                            board[x, y] = toDie;
                        }
                        
                    }
                }
            }

            for (int x = 0; x < xBound; x++)
            {
                for (int y = 0; y < yBound; y++)
                {
                    if (board[x, y] == toDie)
                    {
                        board[x, y] = 0;
                    }

                    if (board[x, y] == toLive)
                    {
                        board[x, y] = 1;
                    }
                }
            }
		}

        private void GetSurrond(int[,] board, int x, int y, out int live, out int die)
        {
            //為了 inner function 可以共用變數，那個有 out 的變數不能放到 inner functtion 使用
            int liveCount = 0;
            int dieCount = 0;

            Action<int, int> check = (tx, ty) =>
            {
                //如果點合法，就追加死活的數量，不合法就算了
                if (tx >= 0 && tx < board.GetLength(0) && ty >= 0 && ty < board.GetLength(1))
                {
                    if (board[tx, ty] > 0)
                    {
                        liveCount++;
                    }
                    else
                    {
                        dieCount++;
                    }
                }
            };

            //這裡用八個點去刷函式，各自檢查合法性，是我能想到最乾淨的寫法了，不用寫一堆 if
            //八個點就不寫迴圈了，很多餘…
            check(x - 1, y);
            check(x + 1, y);

            check(x - 1, y - 1);
            check(x, y - 1);
            check(x + 1, y - 1);

			check(x - 1, y + 1);
			check(x, y + 1);
			check(x + 1, y + 1);

            live = liveCount;
            die = dieCount;
        }

        //300. Longest Increasing Subsequence
        //https://www.youtube.com/watch?v=CE2b_-XfVDk
        //參照網路上的影片後，我大概知道我為何會死了，因為我只關心前一個人，但它每次都作全面關心
        //然後，它初始化每個人的值都是 1 ，也就是再差也可以單選一個人啊，然後站在現有的位罝，考慮前面所有我能接上的可能
        //如果我接上以後可以得到一個更大的長度，我就接上，當然，我接上時，會避開與我相同的值去接，我只去接比我小的值
        //這可以避免另一個問題，就是選過了不能再出現，你想，我都去接比我小的值的，所以這份統計裡，我是不會接到與我相同，或是有接過與我相同值的其它值(因為它更大)
        //沒有人規定 DP 只能 O(n)，這就是很典型的 O(n*n) 的 DP
        public int LengthOfLIS(int[] nums)
        {
            if (nums.Length == 0)
            {
                return 0;
            }

            int[] dp = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                dp[i] = 1;
            }

            for (int curr = 1; curr < nums.Length; curr++)
            {
                for (int check = 0; check < curr; check++)
                {
                    if (nums[check] < nums[curr])
                    {
                        dp[curr] = Math.Max(dp[check] + 1, dp[curr]);
                    }
                }
            }

            return dp.Max();
        }

		//322. Coin Change
        //這題在 codility 上有作過很像的，它這裡硬幣是傳入的，所以寫時要依 coin 作迴圈，沒辦法開死陣列
        //而且它有考你換不到，所以你不只每個數進來要考慮所有 coin ，還要考慮換不到時如何記下判定
		public int CoinChange(int[] coins, int amount)
		{
            int[] dp = new int[amount + 1];
            dp[0] = 0;
            for (int i = 1; i < dp.Length; i++)
            {
                int minChange = int.MaxValue;
                foreach (var co in coins)
                {
                    if (i >= co && dp[i-co] >= 0)
                    {
                        minChange = Math.Min(minChange, dp[i - co] + 1);
                    }
                }

                if (minChange != int.MaxValue)
                {
                    dp[i] = minChange;
                }
                else
                {
                    dp[i] = -1;
                }
            }

            return dp[amount];
		}

		/* 
		public int LengthOfLIS(int[] nums)
		{
            //這個 DP 解，可以通過一半以上的測試，而且 O(n)
            //但是 死在 [4,10,4,3,8,9] 這組測試，第二個4出現時，選不選就和前面那個4有關
            //但我無法在 dp 時檢查它，因為我手上的兩個數字分別是所有元素選或不選的最佳結果
            //但我並不知道這個結果裡，有沒有 4…
            //而且我總覺得這個解就算沒有重覆元素，也應該是不對的，因為每次如果選擇 meIn 的條件就只是看我比前一個大
            //這樣如果前面的組合選了某個比我大的數字，只是剛好我的前一個比我小，但我也不該被加入，依照這個想法我果然找到反例
            //[1,2,3,9,10,5,6] 應該只有 5 不該是我算出來的 6 個

            if (nums.Length == 0)
            {
                return 0;
            }

            HashSet<int> visited = new HashSet<int>();
            int[] meIn = new int[nums.Length];
            int[] meOut = new int[nums.Length];
            meIn[0] = 1;
            meOut[0] = 0;

            for (int i = 1; i < nums.Length; i++)
            {
                meOut[i] = Math.Max(meIn[i - 1], meOut[i - 1]);

                if (nums[i] >= nums[i - 1] && ! visited.Contains(nums[i]))
                {
                    meIn[i] = meOut[i] + 1;
                    visited.Add(nums[i]);
                }
                else
                {
                    meIn[i] = 0;
                }
            }

            return Math.Max(meIn.Max(), meOut.Max());
		}		
		*/

		public static void Test()
		{
			InterviewList obj = new InterviewList();


            Console.WriteLine(obj.CoinChange(new int[] { 2 }, 3));


            //Console.Write(temp);
			//----------------------------------------------------------

			var tree = new TreeNode(4) { left = new TreeNode(2) { left = new TreeNode(1), right = new TreeNode(3) }, right = new TreeNode(6) 
                { left = new TreeNode(5), right = new TreeNode(7) } };

			var tree2 = new TreeNode(4) { left = new TreeNode(2) { left = new TreeNode(1) { left = new TreeNode(3) { left = new TreeNode(7) 
                { left = new TreeNode(9) } } } } };


			//var temp = obj.NumIslands(new char[,] {
			//  {'1','1','1','0'},
			//  {'0','0','1','0'},
			//  {'0','0','1','0'},
			//  {'0','1','1','0'},
			//});

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

			//var temp = obj.SearchMatrix(new int[,]
			//{

			//	{5,   6,  9, 14, 17, 17, 19},
			//	{8,  10, 14, 15, 21, 24, 28},
			//	{8,  10, 16, 21, 21, 26, 33},
			//	{13, 17, 17, 23, 26, 27, 33},
			//	{16, 22, 23, 27, 31, 31, 34},
			//	{16, 26, 28, 30, 32, 32, 37},
			//	{19, 31, 35, 35, 39, 44, 44},
			//	{20, 31, 39, 44, 48, 51, 52},
			//	{23, 36, 40, 47, 51, 51, 53},

			//}, 45);
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
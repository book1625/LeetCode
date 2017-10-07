using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.InterviewList
{
    public class DynaminProgramming
    {
		//70. Climbing Stairs
		public int ClimbStairs(int n)
		{
			long[] fib = new long[n + 1];

			fib[0] = 1;
			fib[1] = 1;
			for (int i = 2; i < fib.Length; i++)
			{
				fib[i] = fib[i - 1] + fib[i - 2];
			}

			return (int)fib[n];
		}

		//121. Best Time to Buy and Sell Stock
		public int MaxProfit(int[] prices)
		{
			int maxProfit = 0;
			int currlow = int.MaxValue;
			for (int i = 0; i < prices.Length; i++)
			{
				if (prices[i] - currlow > 0)
				{
					maxProfit = Math.Max(maxProfit, prices[i] - currlow);
				}

				currlow = Math.Min(currlow, prices[i]);
			}

			return maxProfit;
		}

		public int MaxProfit2(int[] prices)
		{
			int maxProfit = 0;
			int currlow = int.MaxValue;
			int total = 0;
			for (int i = 0; i < prices.Length; i++)
			{
				//大過買價，而且明天更貴，撐
				if (prices[i] - currlow > 0 && i < prices.Length - 1 && prices[i + 1] > prices[i])
				{
					maxProfit = Math.Max(maxProfit, prices[i] - currlow);
					currlow = Math.Min(currlow, prices[i]);
				}
				//今天價格變差，或沒有明天，所以不持有了
				else
				{
					total += Math.Max(maxProfit, prices[i] - currlow);
					maxProfit = 0;
					currlow = prices[i];
				}
			}

			return total;
		}

		//198. House Robber
		//這題 dp 沒想完真的可惜了，我都走到最後一動了
		//建立一個二維的狀態 dp，分別代表「有我」，「無我」
		//「有我」的值，必從前一個「無我」來，而「無我」值，則前面的有無沒關系，所以選個大的
		//我錯在 「無我必從前一個有我而來…」
		public int Rob(int[] nums)
		{
			if (nums.Length == 0)
			{
				return 0;
			}

			int[,] dp = new int[2, nums.Length];
			dp[0, 0] = nums[0];
			int len = nums.Length;
			for (int i = 1; i < len; i++)
			{
				dp[0, i] = dp[1, i - 1] + nums[i];
				dp[1, i] = Math.Max(dp[0, i - 1], dp[1, i - 1]);
			}

			return Math.Max(dp[0, len - 1], dp[1, len - 1]);
		}

		//62. Unique Paths
		//這題也不用幻想用數學階層作，因為最多到 17階就爆int，23階就爆long，它要到100階，沒有函式庫就免談…
		//這題的 dp 其實很容易，是一開始就想後面的版本所以難推，先想 m*n 的版本就比較好推
		//它還可以再簡化不用開到 m*n 的空間，但我覺得不超過 100 * 100，減空間沒有什麼影響
		//它可以只記得當前這一排與前一個加好的數字就好
		public int UniquePaths(int m, int n)
		{
			int[,] map = new int[m, n];
			for (int i = 0; i < m; i++)
				map[i, 0] = 1;
			for (int j = 0; j < n; j++)
				map[0, j] = 1;

			for (int i = 1; i < m; i++)
				for (int j = 1; j < n; j++)
				{
					map[i, j] = map[i, j - 1] + map[i - 1, j];
				}

			return map[m - 1, n - 1];
		}

		////這個解法只是為了驗證自已在寫遞迴有進步了，效率肯定是過不了
		//public int UniquePaths(int m, int n)
		//{
		//    totalPath = 0;
		//    recPath(m - 1, n - 1);
		//    return totalPath;
		//}

		//private int totalPath = 0;
		//private void recPath(int m, int n)
		//{
		//    if(m == 0 && n == 0)
		//    {
		//        totalPath++;
		//        return;
		//    }

		//    if(m > 0)
		//    {
		//        recPath(m-1, n);
		//    }

		//    if(n > 0)
		//    {
		//        recPath(m, n-1);
		//    }
		//}

		//152. Maximum Product Subarray
		public int MaxProduct(int[] nums)
		{
			if (nums.Length == 0)
				return 0;

			long[] dpMax = new long[nums.Length];
			long[] dpMin = new long[nums.Length];
			dpMax[0] = nums[0];
			dpMin[0] = nums[0];

			long max = nums[0];

			for (int i = 1; i < nums.Length; i++)
			{
				//從 我，連乘最大可能，連乘最小可能 選出新的 最大，最小，重點是，這三個值都含有我的成份，這樣 dp 才有辦法合理
				dpMax[i] = Math.Max(nums[i], Math.Max(nums[i] * dpMin[i - 1], nums[i] * dpMax[i - 1]));
				dpMin[i] = Math.Min(nums[i], Math.Min(nums[i] * dpMin[i - 1], nums[i] * dpMax[i - 1]));

				//歷史最大值
				max = Math.Max(max, dpMax[i]);
			}

			return (int)max;
		}

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

			int[] dp = new int[n + 1];
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
					if (i >= co && dp[i - co] >= 0)
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
	}
}

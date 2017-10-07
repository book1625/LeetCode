using System;
using System.Collections.Generic;
using System.Linq;


namespace LeetCode.InterviewList
{
    public class Jump
    {
		//55. Jump Game
		public bool CanJump(int[] nums)
		{
			//這叫「盡量往左跳，多跳幾次沒關系」
			//盡可能到能跳到的最左邊，如果這樣還找到不點可以跳過來，那就沒辦法
			//會不會有一個點在最左邊，但它到不了起點，可是往右一個點卻可以?? 
			//如果最左邊以後的點都到不了，那表示這些到不了的點，它裡面的 max 不夠大到可以走到最左邊
			//這樣那有辦法走到更右的一格，所以上面的假設不存在
			int left = nums.Length - 1;
			for (int i = left - 1; i >= 0; i--)
			{
				if (nums[i] + i >= left)
				{
					left = i;
				}
			}

			return left == 0;
		}

		public bool CanJump2(int[] nums)
		{
			/*
             * 這個演算法是在 codility 上學會的，而且還可以計算最少的jump數
             * 然而… 它的速度不被 leetcode 接受，看了解答大概知道
             * 它只管問可不可以跳到，所以，它的時間要能過，要用 greedy 來拼… Orz
            */
			if (nums.Length == 0)
			{
				return false;
			}

			HashSet<int> nextPlace = new HashSet<int>();
			nextPlace.Add(0);

			while (nextPlace.Count() > 0)
			{
				List<int> allPlace = nextPlace.ToList();
				nextPlace.Clear();
				foreach (int p in allPlace)
				{
					int step = nums[p];

					//由於步數可以是0，所以i得從0開始檢查，也就是可以不走
					//但… 不走就會造成無限廻圈，永遠都有下個 plce (死都不走的留在原地)
					//所以只好把單放一個0，作為特例來處理
					if (p == nums.Length - 1)
					{
						return true;
					}

					for (int i = 1; i <= step; i++)
					{
						if (p + i < nums.Length - 1)
						{
							nextPlace.Add(p + i);
						}
						else
						{
							return true;
						}
					}
				}

			}

			return false;
		}
    }
}

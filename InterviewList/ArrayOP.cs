using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.InterviewList
{
    public class ArrayOP
    {
		//1. Two Sum
		public int[] TwoSum(int[] nums, int target)
		{
			int i = 0, j = 0;
			for (i = 0; i < nums.Length - 1; i++)
			{
				for (j = i + 1; j < nums.Length; j++)
				{
					if (nums[i] + nums[j] == target)
					{
						return new int[] { i, j };
					}
				}
			}

			return new int[] { i, j };
		}

		//26. Remove Duplicates from Sorted Array
		public int RemoveDuplicates(int[] nums)
		{
			int curr = nums[0];
			int count = 0;
			for (int i = 1; i < nums.Length; i++)
			{
				if (nums[i] != curr)
				{
					curr = nums[i];
					count++;
				}
			}

			return count;
		}

		//53. Maximum Subarray
		public int MaxSubArray(int[] nums)
		{
			int max = nums[0];
			int[] counter = new int[nums.Length];
			counter[0] = nums[0];

			for (int i = 1; i < nums.Length; i++)
			{
				counter[i] = Math.Max(nums[i], counter[i - 1] + nums[i]);
				max = Math.Max(max, counter[i]);
			}

			return max;
		}

		//66. Plus One
		public int[] PlusOne(int[] digits)
		{
			int[] temp = new int[digits.Length + 1];
			Array.Copy(digits, 0, temp, 1, digits.Length);

			bool isUp = true;
			for (int i = temp.Length - 1; i >= 0; i--)
			{
				if (isUp)
				{
					if (temp[i] != 9)
					{
						temp[i] += 1;
						isUp = false;
					}
					else
					{
						temp[i] = 0;
						isUp = true;
					}
				}
			}

			if (temp[0] == 0)
			{
				int[] result = new int[temp.Length - 1];
				Array.Copy(temp, 1, result, 0, result.Length);
				return result;
			}
			else
			{
				return temp;
			}
		}

		//88. Merge Sorted Array
		public void Merge(int[] nums1, int m, int[] nums2, int n)
		{
			//笨，寫的爛…
			//if (m > 0)
			//{
			//    int ind = m - 1;
			//    for (int i = nums1.Length - 1; i >= nums1.Length - 1 - n; i--, ind--)
			//    {
			//        nums1[i] = nums1[ind];
			//    }
			//}

			Array.Copy(nums1, 0, nums1, nums1.Length - m, m);

			int toNum1 = nums1.Length - m;
			int toNum2 = 0;
			int curr = 0;

			while (toNum1 < nums1.Length || toNum2 < n)
			{
				if (toNum1 < nums1.Length && toNum2 < n)
				{
					if (nums1[toNum1] < nums2[toNum2])
					{
						nums1[curr] = nums1[toNum1];
						curr++;
						toNum1++;
					}
					else
					{
						nums1[curr] = nums2[toNum2];
						curr++;
						toNum2++;
					}
				}
				else if (toNum1 < nums1.Length)
				{
					nums1[curr] = nums1[toNum1];
					curr++;
					toNum1++;
				}
				else
				{
					nums1[curr] = nums2[toNum2];
					curr++;
					toNum2++;
				}
			}
		}

		//118. Pascal's Triangle
		//https://stackoverflow.com/questions/8142389/returning-ilistilistt
		//這是的回傳型別比較機車，需要花時間看上面的說明，…
		public IList<IList<int>> Generate(int numRows)
		{
			List<IList<int>> result = new List<IList<int>>();

			if (numRows == 0)
			{
				return result;

			}

			result.Add(new List<int>() { 1 });

			for (int loop = 0; loop < numRows - 1; loop++)
			{
				List<int> temp = new List<int>();
				IList<int> preLayer = result[loop];

				temp.Add(preLayer[0]);

				for (int i = 0; i < preLayer.Count(); i++)
				{
					if (i + 1 < preLayer.Count())
					{
						temp.Add(preLayer[i] + preLayer[i + 1]);
					}
				}

				temp.Add(preLayer[preLayer.Count() - 1]);

				result.Add(temp);
			}

			return result;
		}

		//136. Single Number
		public int SingleNumber(int[] nums)
		{
			/* 這只是二流解法，線性不省空間
            HashSet<int> hs = new HashSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if(hs.Contains(nums[i]))
                {
                    hs.Remove(nums[i]);
                }
                else
                {
                    hs.Add(nums[i]);
                }
            }

            return hs.Count() > 0 ? hs.First() : 0;
            */

			//這才是一流解 
			// a xor a = 0 ,  a xor 0 = a
			// a xor b xor a = a xor a xor b = 0 xor b = b
			int xorValue = 0;
			foreach (var num in nums)
			{
				xorValue = xorValue ^ num;
			}
			return xorValue;
		}

		//155. Min Stack
		//自幹一個stack，也沒有很難，那個打?是題目沒說會怎樣，我測線上會它程式會當
		//所以我就不再多處理了
		public class MinStack
		{
			int[] data = new int[1024];

			int top = -1;


			/** initialize your data structure here. */
			public MinStack()
			{

			}

			public void Push(int x)
			{
				if (top == data.Length - 1)
				{
					int[] temp = new int[data.Length * 2];
					Array.Copy(data, temp, data.Length);
					data = temp;
				}

				top++;
				data[top] = x;
			}

			public void Pop()
			{
				if (top >= 0)
				{
					top--;
				}
			}

			public int Top()
			{
				if (top >= 0)
				{
					return data[top];
				}
				else
				{
					return -1; //???????
				}
			}

			public int GetMin()
			{
				if (top < 0)
				{
					return -1; //?????
				}

				int min = int.MaxValue;
				for (int i = 0; i <= top; i++)
				{
					min = Math.Min(min, data[i]);
				}
				return min;
			}
		}

		//169. Majority Element
		public int MajorityElement(int[] nums)
		{
			Stack<int> sk = new Stack<int>();

			for (int i = 0; i < nums.Length; i++)
			{
				if (sk.Count() > 0)
				{
					if (sk.Peek() != nums[i])
					{
						sk.Pop();
					}
					else
					{
						sk.Push(nums[i]);
					}
				}
				else
				{
					sk.Push(nums[i]);
				}
			}

			HashSet<int> cand = new HashSet<int>();
			while (sk.Count() > 0)
			{
				cand.Add(sk.Pop());
			}

			foreach (var ca in cand)
			{
				if (nums.Where(x => x == ca).Count() > nums.Length / 2)
				{
					return ca;
				}
			}

			//impossible
			return -1;
		}

		//189. Rotate Array
		//解答裡有個 reverse 法很聰明可以學一下
		//只是要自己寫 reverse
		//三步驟，全部 reverse ，前半reverse, 後半 reverse
		//O(n) & O(1) good~
		public void Rotate(int[] nums, int k)
		{
			k = k % nums.Length;

			int[] temp = new int[nums.Length];
			Array.Copy(nums, nums.Length - k, temp, 0, k);
			Array.Copy(nums, 0, temp, k, nums.Length - k);
			Array.Copy(temp, nums, temp.Length);
		}

		//217. Contains Duplicate
		public bool ContainsDuplicate(int[] nums)
		{
			HashSet<int> hs = new HashSet<int>();
			foreach (var num in nums)
			{
				if (hs.Contains(num))
				{
					return true;
				}
				else
				{
					hs.Add(num);
				}
			}

			return false;
		}

		//268. Missing Number
		//這題就是現學現賣了，利用 xor 來找不重覆的值
		public int MissingNumber(int[] nums)
		{
			if (nums.Length == 0)
			{
				return 0;
			}

			int sum = nums[0];
			for (int i = 1; i < nums.Length; i++)
			{
				sum ^= nums[i];
			}

			for (int i = 0; i <= nums.Length; i++)
			{
				sum ^= i;
			}

			return sum;
		}

		//283. Move Zeroes
		public void MoveZeroes(int[] nums)
		{
			/*
             * 這個解法有點長


             if(nums.Length < 2)
             {
                 return;
             }

             int curr = 0;
             int notZero = 0;
             Action moveNotZero = () =>
             {
                 if(notZero < curr)
                 {
                     notZero = curr + 1;
                 }

                 while (notZero < nums.Length && nums[notZero] == 0)
                 {
                     notZero++;
                 }
             };


             moveNotZero();

             while(notZero < nums.Length && curr < nums.Length )
             {
                 if(nums[curr] == 0)
                 {
                     nums[curr] = nums[notZero];
                     nums[notZero] = 0;
                 }
                 curr++;
                 moveNotZero();
             }
             */


			for (int curr = 0, lastZero = 0; curr < nums.Length; curr++)
			{
				if (nums[curr] != 0)
				{
					//不能這樣寫的原因是，如果第一個位置不是0
					//lastzero 在第一動是無效的，不真的指在 0 上面
					//這時你其實會因為這樣把第一個蓋掉，如果第一個值是 0
					//則答案就會對… 
					//nums[lastZero] = nums[curr];
					//lastZero++;
					//nums[curr] = 0;

					int temp = nums[lastZero];
					nums[lastZero] = nums[curr];
					nums[curr] = temp;
					lastZero++;
				}
			}
		}

		//350. Intersection of Two Arrays II
		public int[] Intersect(int[] nums1, int[] nums2)
		{
			//這個寫法會過濾掉重覆的元素，所以不合題目要求
			//return nums1.Intersect(nums2).ToArray();

			//由於它要求要知道出現幾次，所以 hashset 也不適用

			Dictionary<int, int> numCount = new Dictionary<int, int>();

			foreach (var nm in nums1)
			{
				if (numCount.ContainsKey(nm))
				{
					numCount[nm]++;
				}
				else
				{
					numCount[nm] = 1;
				}
			}

			List<int> result = new List<int>();
			foreach (var nm in nums2)
			{
				if (numCount.ContainsKey(nm))
				{
					if (numCount[nm] > 0)
					{
						numCount[nm]--;
						result.Add(nm);
					}
				}
			}

			return result.ToArray();
		}

		//11. Container With Most Water
		public int MaxArea(int[] height)
		{
			/* brute-force 很明顯會對但超時
            int maxV = int.MinValue;
            for (int i = 0; i < height.Length - 1 ; i++)
                for (int j = i + 1; j < height.Length; j++)
                    maxV = Math.Max(maxV, (j - i) * Math.Min(height[i], height[j]));
            return maxV;
            */

			//實作 two point
			//它的想法是，面積是被小的那隻限制住的，而那隻去對，最多就只能對到最遠的兩邊，往內縮只會更小
			//然後短的這隻的所有可能就被看透了，以後的人也不再考慮它，所以移動位置到下一隻
			//同裡，新的兩隻裡，短的能到的最遠就是高的那裡，再過去由於先前考慮過了，就不考慮了
			//反復這個過程，得到一個最大的面積

			int left = 0;
			int right = height.Length - 1;
			int max = int.MinValue;
			while (left < right)
			{
				max = Math.Max(max, (right - left) * Math.Min(height[left], height[right]));

				if (height[left] > height[right])
				{
					right--;
				}
				else
				{
					left++;
				}
			}
			return max;
		}

		//15. 3Sum
		//這個演算法成本為 O(n**2)
		//首先，我一開始打算沿用我原本的 hash 來過濾重覆答案
		//所以我學它一樣使用固定 i , 夾擊剩下的所有元素，結果時間一直不會過
		//後來只好學它，在所有重覆元素區進行略過的動作，但我認為這一動並不容易想到!!
		//可是，就算如此，時間依然沒有過，很明顯我處理重覆的效率足以拖跨這一題
		//最後只好還是拿掉我的 hash 物件，直接加就會過…
		//這個系列的題目，有取兩和0(用夾擊)，取兩和絕對值最小(用絕對值排序)
		//取三和0(一定點配夾擊，取 x<=n 個和最大(用累計 dp 解)，取兩組 x,y<n 和最大(用雙向dp解)
		public IList<IList<int>> ThreeSum(int[] nums)
        {
            List<IList<int>> result = new List<IList<int>>();

            Array.Sort(nums);

            for (int i = 0; i < nums.Length - 2; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                {
                    continue;
                }

                int left = i + 1;
                int right = nums.Length - 1;

                while (left < right)
                {
                    int sum = nums[i] + nums[left] + nums[right];
                    if (sum == 0)
                    {
                        result.Add(new List<int>() { nums[i], nums[left], nums[right] });
                        while (left < right && nums[left] == nums[left + 1])
                        {
                            left++;
                        }
                        while (left < right && nums[right] == nums[right - 1])
                        {
                            right--;
                        }
                        left++;
                        right--;
                    }
                    else if (sum > 0)
                    {
                        right--;
                    }
                    else
                    {
                        left++;
                    }
                }
            }

            return result;

          
            ////這個算法，比 O(n**2)大一些，因為是基於 n**2 上再去作推動一輪K
            //HashSet<ThreeNum> result = new HashSet<ThreeNum>();
            //Array.Sort(nums);
            //for (int i = 0; i < nums.Length - 2; i++)
            //{
            //    int k = nums.Length - 1;
            //    for (int j = i + 1; j < nums.Length - 1; j++)
            //    {
            //        while (k > j)
            //        {
            //            int temp = nums[i] + nums[j] + nums[k];

            //            //check sum
            //            if (temp > 0)
            //            {
            //                k--;
            //            }
            //            else if(temp == 0)
            //            {
            //                //add result
            //                result.Add(new ThreeNum(nums[i], nums[j], nums[k]));
            //                k--;
            //                break;
            //            }
            //            else
            //            {
            //                break;
            //            }
            //        }
            //    }
            //}

            //List<IList<int>> tempResult = new List<IList<int>>();
            //foreach(var t in result)
            //{
            //    tempResult.Add(t.ToList());   
            //}
            //return tempResult;
           
        }

        //private class ThreeNum
        //{
        //    public int[] data;

        //    public ThreeNum(int n1, int n2, int n3)
        //    {
        //        data = new int[3] { n1, n2, n3 };
        //        Array.Sort(data);
        //    }

        //    //物件的比較，從 hashcode 先比，相同再比 Equals
        //    //所以這兩個都要覆寫，才有效

        //    public override int GetHashCode()
        //    {
        //        return 0;
        //    }

        //    public override bool Equals(object obj)
        //    {
        //        ThreeNum temp = (ThreeNum)obj;
        //        return temp.data[0] == this.data[0]
        //                && temp.data[1] == this.data[1]
        //                && temp.data[2] == this.data[2];
        //    }

        //    public List<int> ToList()
        //    {
        //        return new List<int>() { data[0], data[1], data[2] };
        //    }
        //}


		//33. Search in Rotated Sorted Array
		public int Search(int[] nums, int target)
		{
			if (nums.Length == 0)
				return -1;
			if (nums.Length == 1)
			{
				if (nums[0] == target)
					return 0;
				else
					return -1;
			}

			//找反轉點，一路找下去，有中直接回，沒中找反轉
			int rotInd = 0;
			for (int i = 0; i < nums.Length - 1; i++)
			{
				if (nums[i] == target)
				{
					return i;
				}

				if (nums[i] > nums[i + 1])
				{
					rotInd = i;
					break;
				}
			}

			//如果有，必在反轉區，前半上面已經找過，發動後半 binary search
			Func<int, int, int, int> bs = (beg, end, t) =>
			{
				while (beg <= end)
				{
					int mid = (beg + end) / 2;
					if (mid < 0 || mid >= nums.Length)
					{
						return -1;
					}

					if (nums[mid] == target)
					{
						return mid;
					}
					else if (nums[mid] > target)
					{
						end = mid - 1;
					}
					else
					{
						beg = mid + 1;
					}
				}
				return -1;
			};

			return bs(rotInd + 1, nums.Length - 1, target);
		}


		//34. Search for a Range
		public int[] SearchRange(int[] nums, int target)
		{
			int beg = 0;
			int end = nums.Length - 1;
			int tar = -1;
			while (beg <= end)
			{
				int mid = (beg + end) / 2;
				if (nums[mid] == target)
				{
					tar = mid;
					break;
				}
				else if (nums[mid] < target)
				{
					beg = mid + 1;
				}
				else
				{
					end = mid - 1;
				}
			}

			if (tar == -1)
			{
				return new int[] { -1, -1 };
			}

			beg = tar;
			end = tar;

			while (beg - 1 >= 0 && nums[beg - 1] == target)
			{
				beg--;
			}

			while (end + 1 < nums.Length && nums[end + 1] == target)
			{
				end++;
			}

			return new int[] { beg, end };
		}

		//36. Valid Sudoku
		public bool IsValidSudoku(char[,] board)
		{
			for (int i = 0; i < 9; i++)
			{
				if (!IsValidRow(board, i, true))
				{
					return false;
				}

				if (!IsValidRow(board, i, false))
				{
					return false;
				}
			}

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (!IsValidSub(board, i * 3, j * 3))
					{
						return false;
					}
				}

			}

			return true;
		}

		private bool IsValidRow(char[,] board, int ind, bool isRow)
		{
			HashSet<char> hs = new HashSet<char>();
			for (int i = 0; i < 9; i++)
			{
				char temp = isRow ? board[ind, i] : board[i, ind];
				if (temp != '.')
				{
					if (hs.Contains(temp))
					{
						return false;
					}
					else
					{
						hs.Add(temp);
					}
				}
			}
			return true;
		}

		private bool IsValidSub(char[,] board, int x, int y)
		{
			HashSet<char> hs = new HashSet<char>();
			for (int i = x; i < x + 3; i++)
				for (int j = y; j < y + 3; j++)
				{
					char temp = board[i, j];
					if (temp != '.')
					{
						if (hs.Contains(temp))
						{
							return false;
						}
						else
						{
							hs.Add(temp);
						}
					}
				}

			return true;
		}

		//46. Permutations
		public IList<IList<int>> Permute(int[] nums)
		{
			if (nums.Length == 0)
			{
				return new List<IList<int>>() { new List<int>() };
			}

			List<IList<int>> result = new List<IList<int>>();

			var src = new List<int>(nums);
			var curr = new List<int>();
			curr.Add(src[0]);
			permuteRec(src, 1, curr, result);
			return result;
		}

		private void permuteRec(List<int> src, int srcindex, List<int> curr, List<IList<int>> result)
		{
			if (srcindex > src.Count() - 1)
			{
				result.Add(curr);
			}
			else
			{
				Func<List<int>, List<int>> cloneList = (ls) =>
				{
					return new List<int>(ls);
				};

				int currValue = src[srcindex];
				List<int> cl;

				for (int i = 0; i < curr.Count(); i++)
				{
					cl = cloneList(curr);
					cl.Insert(i, currValue);
					permuteRec(src, srcindex + 1, cl, result);
				}

				cl = cloneList(curr);
				cl.Add(currValue);
				permuteRec(src, srcindex + 1, cl, result);
			}
		}

		//54. Spiral Matrix
		//也沒什麼神奇解，就是苦工的把 spiral 自幹一次，還要小心特例
		//左右邊界由外面算給函式用，函式會比較好理解，外面控起來也比較明白
		public IList<int> SpiralOrder(int[,] matrix)
		{
			if (matrix.Length == 0)
			{
				return new List<int>();
			}

			List<int> result = new List<int>();

			int begRow = 0;
			int endRow = matrix.GetLength(0) - 1;
			int begCol = 0;
			int endCol = matrix.GetLength(1) - 1;

			while (begRow <= endRow && begCol <= endCol)
			{
				sprial(matrix, begRow, endRow, begCol, endCol, result);

				begRow++;
				endRow--;
				begCol++;
				endCol--;
			}
			return result;
		}

		private void sprial(int[,] matrix, int begRow, int endRow, int begCol, int endCol, List<int> result)
		{
			for (int c = begCol; c <= endCol; c++)
			{
				result.Add(matrix[begRow, c]);
			}

			for (int r = begRow + 1; r <= endRow; r++)
			{
				result.Add(matrix[r, endCol]);
			}

			//下面兩個檢查非常重要，因為在收到快結束時，會造成重覆上面兩個的行為(在等號成立時)
			//而且它們各自有著自己的重覆條件，用想的不容易，要用測的才會發現
			if (begRow < endRow)
			{
				for (int c = endCol - 1; c >= begCol; c--)
				{
					result.Add(matrix[endRow, c]);
				}
			}

			if (begCol < endCol)
			{
				for (int r = endRow - 1; r >= begRow + 1; r--)
				{
					result.Add(matrix[r, begRow]);
				}
			}
		}

		//56. Merge Intervals
		public IList<Interval> Merge(IList<Interval> intervals)
		{
			List<Interval> temp = new List<Interval>(intervals);
			temp.Sort((x, y) => { return x.start.CompareTo(y.start); });

			List<Interval> result = new List<Interval>();

			Interval curr = null;

			foreach (var intl in temp)
			{
				if (curr == null)
				{
					curr = intl;
				}
				else if (curr.end >= intl.start)
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

			if (curr != null)
			{
				result.Add(curr);
			}

			return result;
		}

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
					if (matrix[i, j] == 0)
					{
						zeroRow.Add(i);
						zeroCol.Add(j);
					}
				}
			}

			foreach (var r in zeroRow)
			{
				for (int i = 0; i < n; i++)
				{
					matrix[r, i] = 0;
				}
			}

			foreach (var c in zeroCol)
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

			int right = nums.Length - 1;
			curr = nums.Length - 1;
			while (curr >= 0)
			{
				if (nums[curr] == 2)
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
			if (ind < src.Count())
			{
				int temp = src[ind];

				currStr = new List<int>(currStr);
				recSubSet(src, ind + 1, currStr, result);

				currStr = new List<int>(currStr);
				currStr.Add(temp);
				recSubSet(src, ind + 1, currStr, result);
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
			if (word.Length == 0 || board.Length == 0)
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
						if (recExist(board, i, j, word))
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
			if (target.Length == 0)
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

						board[x, y] = (char)((int)board[x, y] >> 8);
						return isGet;
					}
				}
				else
				{
					return false;
				}
			}
		}

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
				next.Enqueue(new Point(i, 0));
				next.Enqueue(new Point(i, yBound - 1));
			}

			for (int i = 0; i < yBound; i++)
			{
				next.Enqueue(new Point(0, i));
				next.Enqueue(new Point(xBound - 1, i));
			}

			while (next.Count() > 0)
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

		//可惜，爆 TLE ，沒辦法被接收
		//public void Solve(char[,] board)
		//{
		//    int xBound = board.GetLength(0);
		//    int yBound = board.GetLength(1);

		//    for (int i = 0; i < xBound; i++)
		//    {   
		//        recSolve(board, i,0);
		//        recSolve(board, i,yBound-1);
		//    }

		//    for (int i = 0; i < yBound; i++)
		//    {
		//        recSolve(board, 0, i);
		//        recSolve(board, xBound-1, i);
		//    }

		//    for (int i = 0; i < xBound; i++)
		//    {
		//        for (int j = 0; j < yBound; j++)
		//        {
		//            if(board[i,j] == 'O')
		//            {
		//                board[i, j] = 'X';
		//            }
		//            else if(board[i,j] == '@')
		//            {
		//                board[i, j] = 'O';
		//            }
		//        }
		//    }
		//}

		//private void recSolve(char[,] board, int x, int y)
		//{
		//    //out of boundary
		//    if( x < 0 || x >= board.GetLength(0) || y < 0 || y >= board.GetLength(1))
		//    {
		//        return;
		//    }

		//    if(board[x,y] == 'O')
		//    {
		//        board[x, y] = '@';
		//        recSolve(board, x - 1, y);
		//        recSolve(board, x + 1, y);
		//        recSolve(board, x, y - 1);
		//        recSolve(board, x, y + 1);
		//    }
		//}

		//134. Gas Station
		//這題放中級是有點怪，不過它有些狀況一開始確實沒掌握到
		//主要有兩情況，tank 一旦負值就不能再跑了，不能全跑完再看有沒有負
		//一旦 tank 負了就是負了，就不能再跑第二段計算
		//這是  O(n**2) 的解法，看了別人寫的才發現有好幾種 O(n) 解，但都不太容易了解
		//我找了一個我覺得有看懂的來試寫一次，他的想法是，找到一個中間點可以開到底
		//而它剩下的油足夠開完其它的前半段，所以它一路累計前半的值，前試著在當前的點往下開
		//每次失敗它就放棄中間的所有點，直接從失敗點的下一站開始，並留下前半的累計
		//它為何可以直接放棄一段失敗過程中的所有點?? A 是這一段的起點 B 是失敗點 B-1 是失敗前一點
		//在到達 B-1 時，油都還是0以上，是進入了 B 成本太高才失敗的，所以 A ~ B-1 之間
		//不論你從任何點出發前往 B 你也湊不到一個足夠 B的成本，別忘了 A ~ B-1 一路都是保有油的狀態，
		//少掉 A 甚至 少掉 A+1，都只會讓油持平更少，不可能變多，因為這表示拿掉的是負的站點，但它是負的你一開始就過不來了
		//別忘了 A ~ B-1 每一個站點的油累計，不是變多，就是變0，不會是負的，所以你拿掉的區，不是正值就是0，不會是負的
		//…不過我覺得這覺這很難想到就是了，
		public int CanCompleteCircuit(int[] gas, int[] cost)
		{
			int leftSum = 0;
			int rightSum = 0;
			int start = 0;
			for (int i = 0; i < gas.Length; i++)
			{
				rightSum += gas[i] - cost[i];
				if (rightSum < 0)
				{
					leftSum += rightSum;
					rightSum = 0;
					start = i + 1;
				}
			}

			leftSum += rightSum;
			return leftSum >= 0 ? start : -1;
		}

		////原始寫法，會過關的，只是有些弱 
		//public int CanCompleteCircuit(int[] gas, int[] cost)
		//{
		//	int s1, s2, e1, e2;

		//	for (int i = 0; i < gas.Length; i++)
		//	{
		//		if (i == 0)
		//		{
		//			s1 = 0;
		//			e1 = gas.Length - 1;
		//			s2 = -1;
		//			e2 = -1;
		//		}
		//		else
		//		{
		//			s1 = i;
		//			e1 = gas.Length - 1;
		//			s2 = 0;
		//			e2 = s1 - 1;
		//		}

		//		int tank = 0;
		//		for (int j = s1; j <= e1; j++)
		//		{
		//			tank += gas[j];
		//			tank -= cost[j];
		//			if (tank < 0)
		//				break;
		//		}

		//		if (i != 0 && tank >= 0)
		//		{
		//			for (int j = s2; j <= e2; j++)
		//			{
		//				tank += gas[j];
		//				tank -= cost[j];
		//				if (tank < 0)
		//					break;
		//			}
		//		}

		//		if (tank >= 0)
		//		{
		//			return i;
		//		}
		//	}

		//	return -1;
		//}

		//162. Find Peak Element
		//這題初看很容易，只是找個峰值，codility 也有
		//但它的假設其實不一樣，它的頭尾可以是峰值
		//而且，它假設你可以和 int.minValue 比，但!! 相等它也判定是峰值
		//而題目又強調它不會給你連續的相同值，所以程式寫等號才會對…我是覺得有點無聊
		//如果我沒有另開空間來寫，則我的判定條件就一堆的醜
		//這題真正可怕的是，它有個不強迫條件，是你要用 O(logn)作完，這就可怕了
		//我沒真的寫，但我看了一下，它基本上就是用二分法，由於數字不會連續一樣
		//所以它就先隨便踩個中間，然後如果剛好踩中就結束，如果踩在下坡就往左半逼，反正就往右半逼，反正也不平有高原區
		//只要能逼中一個就行，對於只有二個元素含以下的，就算特例解
		public int FindPeakElement(int[] nums)
		{
			if (nums.Count() == 0)
			{
				return -1;
			}

			List<int> data = new List<int>();
			data.Add(int.MinValue);
			data.AddRange(nums);
			data.Add(int.MinValue);

			for (int i = 1; i < data.Count() - 1; i++)
			{
				if (data[i] >= data[i - 1] && data[i] >= data[i + 1])
				{
					return i - 1;
				}
			}
			return -1;
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
					if (canInfectIsland(grid, new Point(i, j)))
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

				while (next.Count() > 0)
				{
					Point temp = next.Dequeue();

					if (temp.X > 0 && grid[temp.X - 1, temp.Y] == '1')
					{
						grid[temp.X - 1, temp.Y] = '@';
						next.Enqueue(new Point(temp.X - 1, temp.Y));
					}
					if (temp.X < grid.GetLength(0) - 1 && grid[temp.X + 1, temp.Y] == '1')
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
					if (map[i, j] == 1)
					{
						if (!canFinishChecking(map, new Point(i, j)))
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

				if (classLog.Contains(p.Y))
				{
					return false;
				}

				for (int i = 0; i < map.GetLength(1); i++)
				{
					if (map[p.Y, i] == 1)
					{
						next.Enqueue(new Point(p.Y, i));
					}
				}
			}

			return true;
		}

		//215. Kth Largest Element in an Array
		//這題其實只要不自己搞排序，就一點都不難
		//我順手用了自己作的 priority queue 來解，而且接受可重覆，但我假設移除最後一個元素的成本是常數
		//照文件來說，它應該是 O(n)
		//最強大解是利用自已寫的類 quick sort 來改善，可以作到 O(n)，但我還沒有自己實作 quck 的經驗，這裡我先跳過了
		public int FindKthLargest(int[] nums, int k)
		{
			if (nums.Count() == 0)
			{
				return -1;
			}

			// NLog(N) solution...
			//Array.Sort(nums);
			//return nums[nums.Length - k];

			//NLog(K)
			SortedList<int, int> kQueue = new SortedList<int, int>(Comparer<int>.Create((x, y) => { return x > y ? -1 : 1; }));
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

		//324. Wiggle Sort II
		//我決定先學一下正常人能想的出來的版本 
		//https://leetcode.com/problems/wiggle-sort-ii/discuss/
		public void WiggleSort(int[] nums)
		{
			//先排序，然後把排好的前半放在答案的偶數位，後半放在奇數位，這樣奇數位的所有值必大於偶數位的所有值
			//這個解法是 O(nLogn)  space O(n) 不是最佳解
			//測到 [1,1,2,1,2,2,1] 錯誤，發現在長度為奇偶數時的索引算錯了
			//測到 [4,5,5,6] 錯誤，才發現大家為什麼都要倒過來放
			//這題的程式沒有難，但是想法不好想到，然後特例也難想到
			//倒過來放，可以避開重覆，但由於都在測不重覆，所以正放的答案也都對，
			//而重覆的中位值，又只有奇數個才有，所以就容易忽略
			//至於這題的  O(n) O(1) 解，那個想法光看真的不太懂，先被我跳過了，但那個分三部份的手法不會很難就先記得了

			Array.Sort(nums);
			int mid = nums.Length % 2 == 0 ? nums.Length / 2 - 1 : nums.Length / 2;
			int[] result = new int[nums.Length];

			for (int i = 0; i < nums.Length; i += 2)
			{
				result[i] = nums[mid - i / 2];
			}

			for (int i = 1; i < nums.Length; i += 2)
			{
				result[i] = nums[nums.Length - 1 - i / 2];
			}
			Array.Copy(result, nums, result.Length);
		}

		/* 這是自以為是的版本，先排序再由中間左右左右的外推開
         * 這種寫法在沒有重覆元素時可以，但如果中間元素剛好有重覆，就會造成一開頭就是錯 1,1,2,2,3,3 -> 2,2,1,3,1,3
         * 
        public void WiggleSort(int[] nums)
        {
            Array.Sort(nums);

            int left = nums.Length % 2 == 0 ? nums.Length / 2 - 1 : nums.Length / 2;
            int right = left + 1;

            int[] temp = new int[nums.Length];
            bool isToRight = false;
            int curr = 0;

            while (left >= 0 || right < nums.Length)
            {
                if (isToRight)
                {
                    if (right < nums.Length)
                    {
                        temp[curr] = nums[right];
                        curr++;
                        right++;
                        isToRight = false;
                    }
                }
                else 
                {
                    if (left >= 0)
                    {
                        temp[curr] = nums[left];
                        curr++;
                        left--;
                        isToRight = true;
                    }
                }
            }
            Array.Copy(temp, nums, temp.Length);
        }
        */

		//334. Increasing Triplet Subsequence
		//這個解法，猛一看好像是錯的，但其實它是對的，而且很神…
		public bool IncreasingTriplet(int[] nums)
		{
			int min1 = int.MaxValue;  //曾經看到的最小值
			int min2 = int.MaxValue;  //曾經看過的第二小值

			for (int i = 0; i < nums.Length; i++)
			{
				if (nums[i] < min1)
				{
					min1 = nums[i];
				}
				else if (nums[i] < min2)
				{
					//這裡是整個算法的核心，它代表多個意思
					//首先，這個值能被刷，表示一定存在一個更小的值在它的前面了要不然刷不到
					//再來，如果有一個值沒有比最小還小，但小於這個值，那它可以幫我們放寬找第三個的條件
					//上面新的更小如果更新，這裡的第二小也不會作廢，因為能有值就已經有效
					//但上面的最小如果變的更小，則很多值就會進來比第二小，所以上面也得一值刷…
					//這樣可以盡可得到一組更小的 最小和二小，讓找三個更容易成功 
					min2 = nums[i];
				}
				else
				{
					//值都有被刷過才有可能大的過它，而兩備都大過，答案就成立了
					return true;
				}
			}
			return false;
		}

		/* O(n) O(n) 解，爆 TLE
        public bool IncreasingTriplet(int[] nums)
        {
            if (nums.Length == 0)
            {
                return false;
            }

            int[] minToR = new int[nums.Length];
            int[] maxToL = new int[nums.Length];

            minToR[0] = nums[0];
            maxToL[nums.Length - 1] = nums[nums.Length - 1];

            for (int i = 1; i < nums.Length; i++)
            {
                minToR[i] = Math.Min(minToR[i - 1], nums[i]);
                maxToL[nums.Length - 1 - i] = Math.Max(maxToL[nums.Length - i], nums[nums.Length - 1 - i]);                                     
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > minToR[i] & nums[i] < maxToL[i])
                {
                    return true;
                }
            }
            return false;
        }
        */

		//347. Top K Frequent Elements
		//這題只要會用字典和自訂排序就一定寫的完
		//另一種方法是，先排序，然後來始統計，每看一個數字知道它出現幾次，就去 k 排行上比看它可以放那個位置，不是沒用就是擠下別人
		public IList<int> TopKFrequent(int[] nums, int k)
		{
			if (nums.Length == 0)
			{
				return null;
			}

			Dictionary<int, int> statistic = new Dictionary<int, int>();

			foreach (var n in nums)
			{
				if (!statistic.ContainsKey(n))
				{
					statistic.Add(n, 0);
				}

				statistic[n]++;
			}

			var temp = statistic.OrderBy(kv => kv.Value, Comparer<int>.Create((x, y) => y.CompareTo(x))).Select(kv => kv.Key).ToList();

			return temp.GetRange(0, k);
		}

		//378. Kth Smallest Element in a Sorted Matrix
		//這個解，也是看人家的，不過我認為很值得學
		//首先，他使用 Binary Search 去逼值，反正極值邊緣是可以直接得到
		//再來就是 binary 的條件，他去算小於等於它的有幾個，用這個條件來決定猜的值是太大還是太小
		//如果每次算數量都用從頭到尾那就不如不要了，但它利用了上次找矩陣的手法來掃，這個而且一成立就直接加總
		//它的複雜度只有O(n)，再配上外面的 binary，得到 log(max-min) * n
		//這比對著 n**2 個元素作排序要強大的多了 !!!
		public int KthSmallest(int[,] matrix, int k)
		{
			if (matrix.Length == 0)
			{
				return 0;
			}

			int dim = matrix.GetLength(0);
			long beg = matrix[0, 0];
			long end = matrix[dim - 1, dim - 1];
			int result = int.MaxValue;
			while (beg <= end)
			{
				long mid = (beg + end) / 2;
				if (getLessEqual(matrix, mid) >= k)
				{
					result = (int)mid;
					end = mid - 1;
				}
				else
				{
					beg = mid + 1;
				}
			}
			return result;
		}

		private int getLessEqual(int[,] matrix, long target)
		{
			//這和前面有一題搜尋二維矩陣的手法一樣，從右下走來，只是這次改成了算有幾個
			int x = 0;
			int y = matrix.GetLength(0) - 1;
			int count = 0;
			while (x < matrix.GetLength(0) && y >= 0)
			{
				if (matrix[x, y] <= target)
				{
					count += y + 1;
					x++;
				}
				else if (matrix[x, y] > target)
				{
					y--;
				}
			}

			return count;
		}

		/*
         * 這個方法死了，因為直接看下面的例子，找第5大時，應該要找到11，但照我的算法我是只看第一layer，所以排除11* 
         * {1,5,9},
           {10,11,13},
           {12,13,15}
         * 
        public int KthSmallest(int[,] matrix, int k)
        {         
            int layer = matrix.GetLength(0);

            while (k - (2 * layer - 1) > 0)
            {
                k -= 2 * layer - 1;
                layer--;
            }

            int x = matrix.GetLength(0) - layer;
            int y = matrix.GetLength(0) - layer;

            List<int> targets = new List<int>();
            targets.Add(matrix[x, y]);
            for (int i = 1; i < matrix.GetLength(0) - x; i++)
            {
                targets.Add(matrix[x + i, y]);
                targets.Add(matrix[x, y + i]);
            }
            targets.Sort();
            return targets.ElementAt(k-1);
        }
        */

		//454. 4Sum II
		//這題我一開始題目沒看清楚，一直用 n*n 陣列在想，所以算法也一直出不來
		//中間也有想到是不是先兩條合併，但合完的結果就沒有減少
		//後來看了別人的解法才發現，固定只有四陣列，只是長度長了點
		public int FourSumCount(int[] A, int[] B, int[] C, int[] D)
		{
			Dictionary<int, int> counter = new Dictionary<int, int>();
			int len = A.Length;
			for (int i = 0; i < len; i++)
			{
				for (int j = 0; j < len; j++)
				{
					int tar = A[i] + B[j];
					if (counter.ContainsKey(tar))
					{
						counter[tar]++;
					}
					else
					{
						counter[tar] = 1;
					}
				}
			}

			int tuples = 0;
			for (int i = 0; i < len; i++)
			{
				for (int j = 0; j < len; j++)
				{
					int tar = -(C[i] + D[j]);
					int tempCount;
					if (counter.TryGetValue(tar, out tempCount))
					{
						tuples += tempCount;
					}
				}
			}
			return tuples;
		}
	}
}

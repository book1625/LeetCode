using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Medium
{
    public partial class InterviewList
    {
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

            for (int i = 0; i < nums.Length; i+=2)
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

		//328. Odd Even Linked List
		//不難，畫一下圖就有了，只是一次就跳兩個指標，所以條件比多一層
		public ListNode OddEvenList(ListNode head)
		{
			if (head == null)
			{
				return null;
			}

            ListNode odd = head;
            ListNode even = null;
            ListNode evenHead = null;
            ListNode last = odd;

            while (odd != null)
            {
                last = odd;

                if (odd.next != null)
                {
                    if (even == null)
                    {
                        even = odd.next;
                        evenHead = even;
                    }
                    else
                    {
                        even.next = odd.next;
                        even = even.next;
                    }

                    odd.next = even.next;
                    even.next = null;
                    odd = odd.next;
                }
                else
                {
                    odd = odd.next;
                }
            }

            last.next = evenHead;

            return head;
        }

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

		/* O(n) O(n) 解，對也不夠好
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


		//341. Flatten Nested List Iterator
        //這題一開始會看到暈暈的，因為它就是一層包一層的無限可能，然後名字也很鬼
        //它給了你 NestedInteger 的定義，然後叫你實作它的 Iterator，但名字很像就看的暈
        //我一開始就笨笨的在記讀到第幾個，然後就發現沒辦法，無限層是怎麼記，我只能實作最外圍的 Iterater
        //又不能叫那個 Integer 有方法可以讓我遞迴叫 next...
        //後來才想到，我為何不一開始就全面遞迴一次，把所有可以直接轉的值換一次不就好了，再來就只是一直拿
        //然後它的輸出又是 DFS ，所以只要搞個集合記下來依序輸出就好，這題來說 Queue 剛好
		public interface NestedInteger
        {    
            bool IsInteger();
            int GetInteger();
            IList<NestedInteger> GetList();
        } 

		public class NestedIterator
		{
			public NestedIterator(IList<NestedInteger> nestedList)
			{
                recProcList(nestedList);
			}

            private Queue<NestedInteger> allInteger = new Queue<NestedInteger>();

            //使用 DFS 分解所有 NestedInteger,留下可以直接變成 int 的
            private void recProcList(IList<NestedInteger> nestedList)
            {
                foreach (var nl in nestedList)
                {
                    if (nl == null)
                    { 
                        //do nothing....
                    }
                    else if (nl.IsInteger())
                    {
                        allInteger.Enqueue(nl);
                    }
                    else
                    {
                        recProcList(nl.GetList());
                    }
                }
            }

			public bool HasNext()
			{
                return allInteger.Count() > 0;
			}

			public int Next()
			{
                return allInteger.Dequeue().GetInteger();
			}
		}

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

		//380. Insert Delete GetRandom O(1)
        //這也不是我的功勞，是 hashtable 的強大
        //我只是接口，另外，ElementAt 是linq提供的，我不確定它是 O(1)
        //如果集合對像有實作 IList，就會靠 index 作到 O(1)，但我看 HashSet 是沒有
        //所以多數的解答都會靠多一個 List 來解決
		public class RandomizedSet
		{
            private HashSet<int> data;
            Random rand;
			
			public RandomizedSet()
			{
                data = new HashSet<int>();
                rand = new Random(Environment.TickCount);
			}

			public bool Insert(int val)
			{
                if (data.Contains(val))
                {
                    return false;
                }
                else
                {
                    data.Add(val);
                    return true;
                }
			}
            			
			public bool Remove(int val)
			{
                if (data.Contains(val))
                {
                    data.Remove(val);
                    return true;
                }
                return false;
			}
            			
			public int GetRandom()
			{
                int ind = rand.Next() % data.Count();
                return data.ElementAt(ind);
			}
		}
		
        //384. Shuffle an Array
		public class Solution
		{
            private int[] original;
            private int[] currNums;
            //這個 random 寫在這裡很重要我現在才理解
            //如果我們用同一個 tick 去建 random ，那它拿出來的數列是一樣的
            //所以如果每次都重建，則要保證 tick 不同，但是，我沒辦法保證我不會被狂連呼!!
            //所以這個 random 只能建一次，這樣至少保證這個物件自己是 random 有效
            //但如果有人狂建這種物件，那… 很可能有一堆人是用同一個 tick，那就死定了…
            Random rand = new Random(Environment.TickCount);

			public Solution(int[] nums)
			{
                original = nums;
                currNums = new int[nums.Length];
                Array.Copy(nums, currNums, nums.Length);
			}

			public int[] Reset()
			{
                Array.Copy(original, currNums, original.Length);
                return currNums;
			}

			public int[] Shuffle()
			{
                for (int i = 0; i < currNums.Length; i++)
                {
                    //random a position to swap eachother
                    int ind = rand.Next() % currNums.Length;
                    int temp = currNums[i];
                    currNums[i] = currNums[ind];
                    currNums[ind] = temp;
                }
                return currNums;
			}
		}

		//395. Longest Substring with At Least K Repeating Characters
		//這個寫法是看別人的，它的主要精神是，只要子字串裡的重覆字元統計都至少有k個
		//那這個子字串就有效了，如果子字串的長度根本不到k，那也不需要管它
		//真的比較難思考的是，把子字串用不到k次的字元開，去找它的所有子字串能建出的最長
		//我一開始的寫法就是對不到k次字元所開的所有子字串都送進去，也就是那個 gp 我是 foreach g 來的
		//但這樣時間爆炸，根本跑不完…
		//後來我看了一下他們的說明，發現他們其實只拿出現最少的那個字元來切開，這點我一開始不太理解
		//後來想了一下，如果我的字串是 abcd 我會分別用 a b c d 去切 
		//而 a 切後要 bcd 
		//而 b 切後要 a cd
		//而 c 切後要 ab d
        //而 d 切後要 abc
        //然後他們後面又都還有…  很明顯的重覆的行為一堆，其實只要切最爛的那個，第二爛的也會在最爛的切割中最格為最爛的又拿去切，所以~ 沒必要處理
		public int LongestSubstring(string s, int k)
        {
            if (s.Length < k)
            {
                return 0;
            }

            var g = s.GroupBy(c => c);
            if (g.Where(gp => gp.Count() < k).Count() == 0)
            {
                return s.Length;
            }
            else
            {
                int maxlen = 0;
                var gp = g.Where(gg => gg.Count() < k).OrderBy(gg => gg.Count()).First();

                foreach (var substr in s.Split(gp.Key))
                {
                    maxlen = Math.Max(maxlen, LongestSubstring(substr, k));
                }

                return maxlen;
            }
        }

        /* 這個寫法確定答案對，但是在測一個超大的 case 時 TLE 了…
         * 我目前的寫法是 O(n*26) + O(n**2*26)
		public int LongestSubstring(string s, int k)
		{
            int[,] map = new int[26, s.Length+1];
            for (int si = 0; si < s.Length; si++)
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    map[i, si + 1] = map[i, si];
                }
                map[s[si] - 'a', si + 1]++;
            }

            int result = 0;
            for (int i = map.GetLength(1) - 1; i > 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    int goodSubLen = i - j;
                    for (int c = 0; c < map.GetLength(0); c++)
                    {
                        if (map[c, i] - map[c, j] > 0 && map[c, i] - map[c, j] < k)
                        {
                            goodSubLen = 0;
                            break;
                        }
                    }
                    result = Math.Max(result, goodSubLen);
                }
            }
            return result;
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
                    int tar = -( C[i] + D[j] ) ;
                    int tempCount;
                    if (counter.TryGetValue(tar, out tempCount))
                    {
                        tuples += tempCount;
                    }
                }
            }
            return tuples;
		}

		public static void Test()
        {
            InterviewList obj = new InterviewList();
            Console.WriteLine(obj.LongestSubstring("abcdedghijklmnopqrstuvwxyz", 2));
            Console.WriteLine(obj.LongestSubstring("ababbc", 2));
            Console.WriteLine(obj.LongestSubstring("abc", 2));
            Console.WriteLine(obj.LongestSubstring("dababbccd", 2));
            Console.WriteLine(obj.LongestSubstring("", 2));
            Console.WriteLine(obj.LongestSubstring("a", 2));
            Console.WriteLine(obj.LongestSubstring("aa", 2));
            //obj.FourSumCount(
            //    new int[] { -1, -1 },
            //    new int[] { -1, 1 },
            //    new int[] { -1, 1 },
            //    new int[] { 1, -1 }
            //);

            //        int[,] array = new int[,]
            //        {
            //{1,5,9},
            //{10,11,13},
            //{12,13,15}
            //};

            //Console.WriteLine(obj.KthSmallest(array, 1));
            //Console.WriteLine(obj.KthSmallest(array, 2));
            //Console.WriteLine(obj.KthSmallest(array, 3));
            //Console.WriteLine(obj.KthSmallest(array, 4));
            //Console.WriteLine(obj.KthSmallest(array, 5));
            //Console.WriteLine(obj.KthSmallest(array, 6));
            //Console.WriteLine(obj.KthSmallest(array, 7));
            //Console.WriteLine(obj.KthSmallest(array, 8));
            //Console.WriteLine(obj.KthSmallest(array, 9));

            //Console.WriteLine(obj.KthSmallest(new int[,] { { 2000000000 } }, 1));

			//Console.Write(temp);

            //----------------------------------------------------------

			//var tree = new TreeNode(4)
			//{
			//	left = new TreeNode(2) { left = new TreeNode(1), right = new TreeNode(3) },
			//	right = new TreeNode(6)
			//	{ left = new TreeNode(5), right = new TreeNode(7) }
			//};

			//var tree2 = new TreeNode(4)
			//{
			//	left = new TreeNode(2)
			//	{
			//		left = new TreeNode(1)
			//		{
			//			left = new TreeNode(3)
			//			{
			//				left = new TreeNode(7)
			//				{ left = new TreeNode(9) }
			//			}
			//		}
			//	}
			//};


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

			//  {5,   6,  9, 14, 17, 17, 19},
			//  {8,  10, 14, 15, 21, 24, 28},
			//  {8,  10, 16, 21, 21, 26, 33},
			//  {13, 17, 17, 23, 26, 27, 33},
			//  {16, 22, 23, 27, 31, 31, 34},
			//  {16, 26, 28, 30, 32, 32, 37},
			//  {19, 31, 35, 35, 39, 44, 44},
			//  {20, 31, 39, 44, 48, 51, 52},
			//  {23, 36, 40, 47, 51, 51, 53},

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
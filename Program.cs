using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    class MainClass
    {
        //many question need this class for input or output
		public class TreeNode
		{
			public int val;
			public TreeNode left;
			public TreeNode right;
			public TreeNode(int x) { val = x; }
		}

		//many question need this class for input or output
		public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x)
            {
                val = x;
                next = null;
            }
        }


        //1. Two Sum
        public static int[] TwoSum(int[] nums, int target)
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

        //7. Reverse Integer
        public int Reverse(int x)
        {
            int signValue = x < 0 ? -1 : 1;
            string s = Math.Abs((long)x).ToString();
            s = string.Join("", s.Reverse());

            if (int.TryParse(s, out x))
            {
                return x * signValue;
            }
            else
            {
                return 0;
            }
        }

        //14. Longest Common Prefix
        private string getComm(string s1, string s2)
        {
            int ind = 0;

            while (ind < s1.Length && ind < s2.Length)
            {
                if (s1[ind] == s2[ind])
                {
                    ind++;
                }
                else
                {
                    break;
                }
            }

            return s1.Substring(0, ind);
        }

        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0)
            {
                return "";
            }

            string comm = strs[0];

            for (int i = 0; i < strs.Length - 1; i++)
            {
                string temp = getComm(strs[i], strs[i + 1]);
                comm = getComm(comm, temp);
            }

            return comm;
        }

        //20. Valid Parentheses
        public static bool IsValid(string s)
        {
            Stack<char> sk = new Stack<char>();

            foreach (var c in s)
            {
                switch (c)
                {
                    case '[':

                    case '(':

                    case '{':
                        sk.Push(c);
                        break;
                    case ')':
                        if (sk.Count() > 0 && sk.Peek() == '(')
                        {
                            sk.Pop();
                        }
                        else
                        {
                            sk.Push(c);
                        }
                        break;
                    case ']':
                        if (sk.Count() > 0 && sk.Peek() == '[')
                        {
                            sk.Pop();
                        }
                        else
                        {
                            sk.Push(c);
                        }
                        break;
                    case '}':
                        if (sk.Count() > 0 && sk.Peek() == '{')
                        {
                            sk.Pop();
                        }
                        else
                        {
                            sk.Push(c);
                        }
                        break;
                }
            }

            return sk.Count() == 0;
        }


		//21. Merge Two Sorted Lists
		public ListNode MergeTwoLists(ListNode l1, ListNode l2)
		{

			ListNode head = null;
			ListNode tail = null;
			ListNode toL1 = l1;
			ListNode toL2 = l2;

			while (toL1 != null || toL2 != null)
			{
				ListNode curr;

				if (toL1 != null && toL2 != null)
				{
					if (toL1.val < toL2.val)
					{
						curr = toL1;
						toL1 = toL1.next;
					}
					else
					{
						curr = toL2;
						toL2 = toL2.next;
					}

				}
				else if (toL1 != null)
				{
					curr = toL1;
					toL1 = toL1.next;
				}
				else
				{
					curr = toL2;
					toL2 = toL2.next;
				}

				curr.next = null;

				if (head == null)
				{
					head = curr;
					tail = curr;
				}
				else
				{
					tail.next = curr;
					tail = curr;
				}
			}

			return head;
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


        //28. Implement strStr()
        public int StrStr(string haystack, string needle)
        {
            return haystack.IndexOf(needle);
		}

		//38. Count and Say
		public string CountAndSay(int n)
		{
			string result = "1";

			for (int loop = 1; loop < n; loop++)
			{
				char preNum = result[0];
				int repeat = 1;

				List<string> temp = new List<string>();

				for (int i = 1; i < result.Length; i++)
				{
					if (result[i] == preNum)
					{
						repeat++;
					}
					else
					{
						temp.Add(string.Format("{0}{1}", repeat, preNum));

						repeat = 1;
						preNum = result[i];
					}
				}

				temp.Add(string.Format("{0}{1}", repeat, preNum));
				result = string.Join("", temp);          
			}

			return result;
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
		public static int[] PlusOne(int[] digits)
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

            if(temp[0] == 0)
            {
                int[] result = new int[temp.Length - 1];
                Array.Copy(temp,1, result,0,result.Length);
                return result;
            }
            else
            {
                return temp;
            }
        }

		//69. Sqrt(x)
		public int MySqrt(int x)
		{
            int beg = 0;
            int end = x;

            int result = 0;
            while(beg <= end)
            {
                int mid = (beg + end) / 2; 
                if( (long)mid*mid <= x)  //要注意這裡真的會爆, 測 2**30 , 除2也有 2**29 再自乘就 2**58 爆了
				{
                    result = mid;
                    beg = mid + 1;
                }
                else
                {
                    end = mid - 1;
                }
            }

            return result;
		}

        //70. Climbing Stairs
		public int ClimbStairs(int n)
		{
            long[] fib = new long[n+1];

            fib[0] = 1;
            fib[1] = 1;
            for (int i = 2; i < fib.Length; i++)
            {
                fib[i] = fib[i - 1] + fib[i - 2];
            }

            return (int)fib[n];
		}

		//88. Merge Sorted Array
		public static void Merge(int[] nums1, int m, int[] nums2, int n)
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

            Array.Copy(nums1, 0, nums1, nums1.Length-m, m);

            int toNum1 = nums1.Length - m ;
            int toNum2 = 0;
            int curr = 0;

            while(toNum1 < nums1.Length || toNum2 < n)
            {
                if(toNum1 < nums1.Length && toNum2 < n)
                {
                    if(nums1[toNum1] < nums2[toNum2])
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



        //101. Symmetric Tree
        public bool IsMirro(TreeNode left, TreeNode right)
		{
			if (left == null && right == null)
			{
				return true;
			}

			if (left == null || right == null)
			{
				return false;
			}

			return (left.val == right.val) &&
				   IsMirro(left.left, right.right) &&
				   IsMirro(left.right, right.left);
		}

		public bool IsSymmetric(TreeNode root)
		{
			return IsMirro(root, root);
		}



		//104. Maximum Depth of Binary Tree
		private int maxDeep = 0;

		private void DFS(TreeNode node, int deep)
		{
			if (node == null)
			{
				return;
			}

			deep++;
			maxDeep = Math.Max(maxDeep, deep);
			DFS(node.left, deep);
			DFS(node.right, deep);
		}

		public int MaxDepth(TreeNode root)
		{
			DFS(root, 0);
			return maxDeep;
		}

		//108. Convert Sorted Array to Binary Search Tree
		public class Span
		{
            public Span(int beg, int end, TreeNode papa, bool isleft)
			{
                Papa = papa;
				Beg = beg;
				End = end;
                IsLeft = isleft;
			}

            public TreeNode Papa;
			public int Beg;
			public int End;
            public bool IsLeft;
		}

		public static TreeNode SortedArrayToBST(int[] nums)
		{
            Queue<Span> queue = new Queue<Span>();
            TreeNode root = null;

            queue.Enqueue(new Span(0, nums.Length - 1, root, true));

			while (queue.Count() > 0)
			{
                Span s = queue.Dequeue();

                if (s.Beg <= s.End)
                {
                    int mid = (s.Beg + s.End) / 2;
                    TreeNode n = new TreeNode(nums[mid]);

                    if(s.Papa == null)
                    {
                        root = n;
                    }
                    else
                    {
                        if(s.IsLeft)
                        {
                            s.Papa.left = n;
                        }
                        else
                        {
                            s.Papa.right = n;
                        }
                    }                  

                    queue.Enqueue(new Span(s.Beg, mid - 1, n, true));
                    queue.Enqueue(new Span(mid + 1, s.End, n, false));
                }
                else
                {
                    //do nothing...
                }
			}

            return root;
		}

		//118. Pascal's Triangle
		//https://stackoverflow.com/questions/8142389/returning-ilistilistt
        //這是的回傳型別比較機車，需要花時間看上面的說明，…
		public static IList<IList<int>> Generate(int numRows)
		{
            List<IList<int>> result = new List<IList<int>>();

			if (numRows == 0)
			{
                return result;

			}
            
            result.Add(new List<int>(){1});

            for (int loop = 0; loop < numRows - 1; loop++)
            {
                List<int> temp = new List<int>();
                IList<int> preLayer = result[loop];

                temp.Add(preLayer[0]);

                for (int i = 0; i < preLayer.Count(); i++)
                {
                    if(i+1 < preLayer.Count())
                    {
                        temp.Add(preLayer[i] + preLayer[i+1]);
                    }
                }

                temp.Add(preLayer[preLayer.Count()-1]);

                result.Add(temp);
            }

            return result;
		}

		//121. Best Time to Buy and Sell Stock
		public int MaxProfit(int[] prices)
		{
            int maxProfit = 0;
            int currlow = int.MaxValue;
            for (int i = 0; i < prices.Length; i++)
            {
                if(prices[i] - currlow > 0)
                {
                    maxProfit = Math.Max(maxProfit, prices[i] - currlow);
                }

                currlow = Math.Min(currlow, prices[i]);
            }

            return maxProfit;
		}

		public static int MaxProfit2(int[] prices)
		{
			int maxProfit = 0;
			int currlow = int.MaxValue;
            int total = 0;
			for (int i = 0; i < prices.Length; i++)
			{
                //大過買價，而且明天更貴，撐
                if (prices[i] - currlow > 0 && i < prices.Length - 1 &&  prices[i+1] > prices[i])
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

		//125. Valid Palindrome
		public static bool IsPalindrome(string s)
		{
            if(s.Length == 0)
            {
                return true;
            }

            List<char> temp = new List<char>();
            foreach(var c in s)
            {
                if( ( c >= 'a' && c <= 'z' ) || ( c >='A' && c <= 'Z') || (c >= '0' && c <= '9') )
                {
                    temp.Add(char.ToLower(c));
                }
            }

            int head = 0;
            int tail = temp.Count() -1 ;
            while(head <= tail)
            {
                if(temp[head] == temp[tail])
                {
					head++;
					tail--;
                }
                else
                {
                    return false;
                }
            }

            return true;
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
            foreach(var num in nums)
            {
                xorValue = xorValue ^ num;
            }
            return xorValue;
		}

		//141. Linked List Cycle
		public bool HasCycle(ListNode head)
		{
            /*
             * 我也是傳統解，解答有一個很聰明的解，用兩隻指標，一個一次跑兩步，一個一次跑一步
             * 如果能追上，表示有迴圈，不然，就一定可以跑到 null，很強大，不用花空間 
            */

            if(head == null)
            {
                return false;
            }
            
            HashSet<ListNode> hs = new HashSet<ListNode>();

            while(head.next !=null)
            {
                if(hs.Contains(head))
                {
                    return true;
                }
                else
                {
                    hs.Add(head);
                    head = head.next;
                }
            }

            return false;
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
                if(top == data.Length -1)
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
                if(top >= 0)
                {
                    top--;
                }
			}

			public int Top()
			{
                if(top >= 0)
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
                if(top < 0)
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

        //160. Intersection of Two Linked Lists
		public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
		{
            /*
             * 我還是採標準版，我認為 O(1) space 的解法有點變態了
             * 而且我還是看沒有為何一定會在交叉點相遇，不會是在交叉區的中間嗎?? 不太好想像，就不想了... 
            */
            HashSet<ListNode> hs = new HashSet<ListNode>();
            while(headA != null)
            {
                hs.Add(headA);
                headA = headA.next;
            }

            while(headB!= null)
            {
                if(hs.Contains(headB))
                {
                    return headB;
                }
                else
                {
                    hs.Add(headB);
                    headB = headB.next;
                }
            }

            return null;
		}

		//169. Majority Element
		public int MajorityElement(int[] nums)
		{
            Stack<int> sk = new Stack<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if(sk.Count() > 0)
                {
                    if(sk.Peek() != nums[i])
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
            while(sk.Count() > 0)
            {
                cand.Add(sk.Pop());
            }

            foreach(var ca in cand)
            {
                if(nums.Where(x=> x== ca).Count() > nums.Length / 2)
                {
                    return ca;
                }
            }

            //impossible
            return -1;
		}

        //171. Excel Sheet Column Number
		public int TitleToNumber(string s)
		{
			int result = 0;

			int pow = s.Length - 1;
			for (int i = 0; i < s.Length; i++)
			{
				result += (s[i] - 'A' + 1) * (int)Math.Pow(26, pow);
				pow--;
			}

			return result;
		}

		//172. Factorial Trailing Zeroes
        //這題很數學，所以寫一下說明， 0 是靠 2*5 得來的，2到處有，但5就不一定
        //所以目標是計算 1-n 到底出現了多少個5的因數，所以一開始很天真的直接回傳 n/5
        //結果我忘了，5的平方數都不只擁有一個5，而10的倍數也會隨著放大而有更多的5
        //如果要一路除過去，也是算的出來，但沒有 logn ...
        //再來就神了，你先用 n/5 算一次，答案一定不夠的，前面大的數有多的 5 都沒算到
        //這時你把整個 n! 每個元素都拿掉一個5(你剛算過了)，你會發現它很神奇的變成 (n/5)!
        //所以你就遞迴的再來一次把缺算的拿進來，直到沒得除… Orz...
		public int TrailingZeroes(int n)
		{
			if (n == 0)
			{
				return 0;
			}
			else
			{
				return n / 5 + TrailingZeroes(n / 5);
			}
		}

		//189. Rotate Array
        //解答裡有個 reverse 法很聰明可以學一下
        //只是要自己寫 reverse
        //三步驟，全部 reverse ，前半reverse, 後半 reverse
        //O(n) & O(1) good~
		public static void Rotate(int[] nums, int k)
		{
            k = k % nums.Length;

            int[] temp = new int[nums.Length];
            Array.Copy(nums, nums.Length-k, temp , 0, k);
            Array.Copy(nums, 0, temp, k, nums.Length - k);
            Array.Copy(temp,nums,temp.Length);
		}

		//190. Reverse Bits
		//這裡要注意運算子優先權，shift 比+-還小 不能寫在同一行，會先加再 shift
        //https://msdn.microsoft.com/en-us/library/aa691323(v=vs.71).aspx
		public static uint reverseBits(uint n)
		{
            uint result = 0;
            for (int i = 0; i < 32; i++ )
            {
                uint temp = n & ((uint)1 << i);
                if(temp > 0)
                {
                    result = result << 1;
                    result++;
                }
                else
                {
                    result = result << 1;
                }
            }
            return result;
		}

		//191. Number of 1 Bits
		public int HammingWeight(uint n)
		{
			int result = 0;
			for (int i = 0; i < 32; i++)
			{
				uint temp = n & ((uint)1 << i);
				if (temp > 0)
				{					
					result++;
				}
			}
			return result;
		}

		//198. House Robber
        //這題 dp 沒想完真的可惜了，我都走到最後一動了
        //建立一個二維的狀態 dp，分別代表「有我」，「無我」
        //「有我」的值，必從前一個「無我」來，而「無我」值，則前面的有無沒關系，所以選個大的
        //我錯在 「無我必從前一個有我而來…」
		public static int Rob(int[] nums)
		{
            if (nums.Length ==0)
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

		//202. Happy Number
		private static int getDigiSum(int n)
        {
            int total = 0;
            int quot = 0;
            int rem;
            do
            {
                quot = Math.DivRem(n, 10, out rem);
                n = quot;
                total += (int)Math.Pow(rem, 2);
            }while(quot > 0);

            return total;
        }

		public static bool IsHappy(int n)
		{
            HashSet<int> hs = new HashSet<int>();
            hs.Add(n);

            while(n > 1)
            {
                n = getDigiSum(n);

                if(n == 1)
                {
                    return true;
                }
                else
                {
					if (!hs.Contains(n))
					{
                        hs.Add(n);
					}
                    else
                    {
                        return false;
                    }
                }
            }

            //impossible
            return true;
		}

		//204. Count Primes
		public static int CountPrimes(int n)
		{
            if(n < 2)
            {
                return 0;
            }

            int[] primeTb = new int[n];
            primeTb[0] = 1;
            primeTb[1] = 1;

            int ind = 2;
            while( (long)ind * ind <= n)
            {
                for (int i = ind * ind; i < primeTb.Length; i+=ind)
                {
                    primeTb[i] = 1;
                }
                ind++;
            }

            return primeTb.Where(x => x == 0).Count();
        }

		//206. Reverse Linked List
		public static ListNode ReverseList(ListNode head)
		{
            if(head == null)
            {
                return head;
            }

            ListNode tempHead = head;
            ListNode tempNext = head.next;
            head.next = null;

            while(tempNext != null)
            {
                ListNode temp = tempNext.next;
                tempNext.next = tempHead;
                tempHead = tempNext;
                tempNext = temp;
            }

            return tempHead;
		}

		//217. Contains Duplicate
		public bool ContainsDuplicate(int[] nums)
		{
            HashSet<int> hs = new HashSet<int>();
            foreach(var num in nums)
            {
                if(hs.Contains(num))
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

		//234. Palindrome Linked List
		public static bool IsPalindrome(ListNode head)
		{
			//找到中間點前後
			int count = 0;
			ListNode temp = head;
			while (temp != null)
			{
				count++;
				temp = temp.next;
			}

			if (count < 2)
			{
				return true;
			}

			int tar = count % 2 == 0 ? count / 2 + 1 : count / 2 + 2;

			temp = head;
			while (tar > 1)
			{
				temp = temp.next;
				tar--;
			}

			//反轉後半
			temp = ReverseList(temp);

			//比對前半與後半所有 node
			while (temp != null)
			{
				if (temp.val != head.val)
				{
					return false;
				}
				temp = temp.next;
				head = head.next;
			}

			return true;
		}

		//237. Delete Node in a Linked List
        //這題，反而讓我沒想到，不常寫 Linked List 的人應該都不昜想到
        //如果該 node 是一個複雜物件，含蓋多層指標，那我認為這樣作也是危險的
        //物件的 clone 不是一件簡單的事
		public void DeleteNode(ListNode node)
		{
			node.val = node.next.val;
			node.next = node.next.next;
		}

		public static bool IsAnagram(string s, string t)
		{
            if(s.Length != t.Length)
            {
                return false;
            }

            if(s.Length == 0)
            {
                return true;
            }

            int[,] count = new int[2, 26];

            for (int i = 0; i < s.Length; i++)
            {
                count[0, s[i] - 'a']++;
                count[1, t[i] - 'a']++;
            }

            for (int i = 0; i < 26; i++)
            {
                if(count[0,i] != count[1,i])
                {
                    return false;
                }
            }

            return true;
		}

        //268. Missing Number
        //這題就是現學現賣了，利用 xor 來找不重覆的值
		public static int MissingNumber(int[] nums)
		{
            if(nums.Length == 0)
            {
                return 0;
            }

            int sum = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                sum ^= nums[i];
            }

            for (int i = 0; i <= nums.Length; i++ )
            {
                sum ^= i;
            }

            return sum;
		}

        //283. Move Zeroes
		public static void MoveZeroes(int[] nums)
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
                if(nums[curr] != 0)
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

		//326. Power of Three
		public bool IsPowerOfThree(int n)
		{
            /* 弱 …
			if (n <= 0)
			{
				return false;
			}

			while (n > 1)
			{
				if (n % 3 == 0)
				{
					n = n / 3;
				}
				else
				{
					return false;
				}
			}

			return true;
			*/

            //解答裡的方法，我最愛這個，有兩個看不懂，用字串和log的不好理解
            //由於數字不大於int，然後3又是個質數，這造成 3 的 power 都沒有其它的質因數分解
            //所以 3**n 必債 3**m 的因數 當 m >= n
            //所以拿 3**19 來通殺所有 int 中的可能
            //這招對 2 5 7 11 ... 等質數的次方，也有效
            return (n > 0) && ((int)Math.Pow(3, 19) % 3 == 0);
		}

		public static void Main(string[] args)
        {
			int[] temp = new int[] { 0, 1, 0, 1 };
			MoveZeroes(temp);
			Console.WriteLine(string.Join(",", temp));
			
            temp = new int[] { 1, 0 };
			MoveZeroes(temp);
			Console.WriteLine(string.Join(",", temp));

			temp = new int[] { 0, 0 };
			MoveZeroes(temp);
			Console.WriteLine(string.Join(",", temp));

			temp = new int[] { 1, 1 };
			MoveZeroes(temp);
			Console.WriteLine(string.Join(",", temp));

			temp = new int[] { 0, 1 };
			MoveZeroes(temp);
			Console.WriteLine(string.Join(",", temp));

            temp = new int[] { 0, 1, 0, 3, 12 };
            MoveZeroes(temp);
            Console.WriteLine(string.Join(",",temp));


            //int[,] count = new int[2, 26];

            //Console.WriteLine(count.Length);
            //Console.WriteLine(count.GetLength(0));
            //Console.WriteLine(count.GetLength(1));


            //IsAnagram("nl", "cx");

            //ListNode temp1 = new ListNode(1);
            //ListNode temp2 = new ListNode(2);
            //ListNode temp3 = new ListNode(1);
            //temp1.next = temp2;
            //temp2.next = temp3;
            //temp3.next = null;
            //IsPalindrome(temp1);

            //Console.Write(CountPrimes(3)); 
            //getDigiSum(0);
            //Rob(new int[] { 1, 2, 8, 8, 2, 2 });
            //reverseBits(43261596);
            //Rotate(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 7);
            //IsPalindrome("123");
            //IsPalindrome("A man, a plan, a canal: Panama");
            //IsPalindrome("race a car");
            //MaxProfit2(new int[] { 1, 2, 3, 4, 5 });

            //Generate(5);

            //int[] num1 = new int[] { 1,3,4,6,0,0,0 };
            //int[] num2 = new int[] { 2,4,7 };
            //Merge(num1, 4, num2, 3);

            //SortedArrayToBST(new int[]{1, 2, 3, 4, 5, 6, 7, 8} );

            //Console.Write(PlusOne(new int[]{1,2,3}));
        }
    }
}

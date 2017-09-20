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


		public static void Main(string[] args)
        {
            IsPalindrome("123");

            IsPalindrome("A man, a plan, a canal: Panama");
            IsPalindrome("race a car");
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

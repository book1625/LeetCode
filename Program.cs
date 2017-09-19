using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    class MainClass
    {

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

        /*
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
        */

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


		/*
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
        */


		public static void Main(string[] args)
        {
            //Console.Write(PlusOne(new int[]{1,2,3}));
        }
    }
}

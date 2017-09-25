using System;
using System.Collections.Generic;
using System.Linq;
using LeetCode;

namespace LeetCode.Medium
{
    public partial class InterviewList
    {
		public static void Test()
		{
			InterviewList obj = new InterviewList();

			var temp = obj.Search(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 0);
			Console.Write(temp);
			//ListNode h1 = new ListNode(1) { next = new ListNode(4) { next = new ListNode(5) } };
			//obj.RemoveNthFromEnd(h1, 2);

			//obj.ThreeSum(new int[] { -1, 0, 1, 2, -1, -4, 1, 1, 2, 2, 0, -1 });
			//Console.WriteLine(obj.MyAtoi(""));
			//Console.WriteLine(obj.MyAtoi("0"));
			//Console.WriteLine(obj.MyAtoi("01"));
			//Console.WriteLine(obj.MyAtoi("10"));
			//Console.WriteLine(obj.MyAtoi("001"));
			//Console.WriteLine(obj.MyAtoi("010"));
			//Console.WriteLine(obj.MyAtoi("101"));
			//Console.WriteLine(obj.MyAtoi("1010"));
			//Console.WriteLine(obj.MyAtoi("00001"));

			//Console.WriteLine(obj.LongestPalindrome("abccba"));
			//Console.WriteLine(obj.LongestPalindrome("abcacba"));
			//Console.WriteLine(obj.LongestPalindrome("babad"));
			//Console.WriteLine(obj.LongestPalindrome("ab"));
			//Console.WriteLine(obj.LongestPalindrome("a"));
			//Console.WriteLine(obj.LongestPalindrome("ababb"));

			//ListNode h1 = new ListNode(5) { next = new ListNode(4) { next = new ListNode(5) } };
			//ListNode h2 = new ListNode(5) { next = new ListNode(6) { next = new ListNode(4) } };
			//var temp = obj.AddTwoNumbers(h1, h2);

			//ListNode h1 = new ListNode(5) ;
			//ListNode h2 = new ListNode(5) { next = new ListNode(6) { next = new ListNode(4) } };
			//var temp = obj.AddTwoNumbers(h1, h2);
		}
    }
}
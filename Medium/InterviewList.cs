using System;
using System.Collections.Generic;
using System.Linq;
using LeetCode;

namespace LeetCode.Medium
{
    public class InterviewList
    {
		//2. Add Two Numbers
		public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
		{
            if(l1==null && l2 ==null)
            {
                return null;
            }

            ListNode head = new ListNode(-1);
            ListNode curr = head;

            bool isCarry = false;
            while(l1!=null || l2!=null)
            {
                int v1 = 0;
                int v2 = 0;
                int sum = isCarry ? 1 : 0;

                if(l1 != null)
                {
                    v1 = l1.val;
                    l1 = l1.next;
                }

                if(l2 != null)
                {
                    v2 = l2.val;
                    l2 = l2.next;
                }

                sum += (v1 + v2);
                isCarry = sum >= 10;
                sum = sum % 10;

                ListNode node = new ListNode(sum);
                curr.next = node;
                curr = node;
            }

            if(isCarry)
            {
                curr.next = new ListNode(1);
            }

            return head.next;
		}

		//3. Longest Substring Without Repeating Characters
		public int LengthOfLongestSubstring(string s)
		{
            if(s.Length < 1)
            {
                return s.Length;  
            }

            HashSet<char> hs = new HashSet<char>();
            int[] maxLen = new int[s.Length];
            int dangerInd = -1;
            hs.Add(s[0]);
            maxLen[0] = 1;

            for (int i = 1; i < s.Length; i++)
            {
                if (!hs.Contains(s[i]))
                {
                    hs.Add(s[i]);
                    maxLen[i] = maxLen[i - 1] + 1;
                }
                else
                {
                    //這裡表示，先前一定出現過，往前找到最近的一個，並重建新的重覆檢查集合
                    hs = new HashSet<char>();
                    hs.Add(s[i]);
                    dangerInd = i - 1;
                    while (dangerInd >= 0 && hs.Contains(s[dangerInd]) == false)
                    {
                        hs.Add(s[dangerInd]);
                        dangerInd--;
                    }

                    maxLen[i] = i - dangerInd;
                }
            }

            return maxLen.Max();
		}

		//5. Longest Palindromic Substring
        //我的手法和解答中所使用的 expand around center 是一樣的
        //但它的碼比我的精簡的多
		private string GetPalindromeLenM(string s, int middle)
        {
            int left = middle - 1;
            int right = middle + 1;
            int len = 1;

            while (left >= 0 && right < s.Length)
            {
                if(s[left] == s[right])
                {
                    len += 2;
                    middle = left; //remember the start of string
                    left--;
                    right++;
                }
                else
                {
                    break;
                }   
            }
            return s.Substring(middle, len);
        }

		private string GetPalindromeLenL(string s, int left, int right)
		{
            int len = 0;
            int head = left;
			while (left >= 0 && right < s.Length)
			{
				if (s[left] == s[right])
				{
                    len += 2;
                    head = left;
					left--;
					right++;
				}
				else
				{
					break;
				}
			}
            return s.Substring(head, len);
		}

        public string LongestPalindrome(string s)
		{
            string maxLenStr = "";
            for (int i = 0; i < s.Length; i++ )
            {
                string temp1 = GetPalindromeLenL(s, i, i + 1);
                string temp2 = GetPalindromeLenM(s, i);

                if(temp1.Length > maxLenStr.Length)
                {
                    maxLenStr = temp1;
                }

                if(temp2.Length > maxLenStr.Length)
                {
                    maxLenStr = temp2;
                }
            }

            return maxLenStr;
		}

		//8. String to Integer (atoi)
		public int MyAtoi(string str)
		{
            //應該不會吧 xd
            if(str == null)
            {
                return 0;
            }

            //前後空白都可略
            str = str.Trim();
			
            //沒有字元也不處理
            if(str.Length == 0)
            {
                return 0;
            }

            //處理正負
            bool isNegative = false;
            int ind = 0;
            if(str[0] == '+' || str[0] == '-')
            {
                isNegative = str[0] == '-';
                ind = 1;
            }

            //到這裡就只能看見數字，其它的符號都要無效
            long result = 0;
            for (int i = ind; i < str.Length; i++ )
			{
				if (str[i] >= '0' && str[i] <= '9')
				{
					result *= 10;
                    result += (byte)str[i] - (byte)'0';

                    //避免加過頭，文字是可以長到見鬼的…
                    //要能放的下負極值，別忘了，負的在這也是用正值處理
                    if(result-1 >= int.MaxValue)
                    {
                        //都超過，就別作了…
                        break;
                    }
				}
                else 
                {
                    break;
                }
			}

            //該正負要先轉，不然下面要判斷極值就有問題
            if(isNegative)
            {
                result = -result;
            }

            //避免極正
            if(result > int.MaxValue)
            {
                return int.MaxValue;
            }

            //避免極負
            if(result< int.MinValue)
            {
                return int.MinValue;
            }

            return (int)result;
		}

        /*  上面這題為了要，至少要測下面所有例子才有可能正確，演算法也是摸出來的，沒有文件說明
        ""
        "   "
        "0"
        "1"
        "01"
        "10"
        "010"
        "001"
        "101"
        "0101"
        "0001"
        "2147483647"
        "2147483648"
        "-0"
        "-1"
        "-01"
        "-10"
        "-010"
        "-001"
        "-101"
        "-0101"
        "-0001"
        "-2147483648"
        "-2147483649"
        "-001 01"
        "-001+01"
        "-0011 01"
        "-001*01"
        "-001/01"
        "-"
        " - 2 "
        "- 2"
        "+-2"
        "-+2"
        "--2"
        "++2"
        "---2"
        "+++2"
        "2*5"
        "*25"
        "25*"
        "  -0012a42"
        " a-189"
        " -s123"
        "-123a"
        "- 1"
        "1800000000000000000"
        "-1800000000000000000"
        "9223372036854775809"
        "-9223372036854775809"
        */

		public static void Test()
        {
            InterviewList obj = new InterviewList();
            Console.WriteLine(obj.MyAtoi(""));
            Console.WriteLine(obj.MyAtoi("0"));
            Console.WriteLine(obj.MyAtoi("01"));
            Console.WriteLine(obj.MyAtoi("10"));
            Console.WriteLine(obj.MyAtoi("001"));
            Console.WriteLine(obj.MyAtoi("010"));
            Console.WriteLine(obj.MyAtoi("101"));
            Console.WriteLine(obj.MyAtoi("1010"));
            Console.WriteLine(obj.MyAtoi("00001"));

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

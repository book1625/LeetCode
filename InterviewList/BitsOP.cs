using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.InterviewList
{
    public class BitsOP
    {
		//190. Reverse Bits
		//這裡要注意運算子優先權，shift 比+-還小 不能寫在同一行，會先加再 shift
		//https://msdn.microsoft.com/en-us/library/aa691323(v=vs.71).aspx
		public uint reverseBits(uint n)
		{
			uint result = 0;
			for (int i = 0; i < 32; i++)
			{
				uint temp = n & ((uint)1 << i);
				if (temp > 0)
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

		//371. Sum of Two Integers
		public int GetSum(int a, int b)
		{
			//長年來我都一直記它就是加法，這是錯的離譜的記憶，趁此次修正了!!
			//return a | b;

			//這個演算就神奇了，真的把 0101 畫出來比較能體會
			//它靠遞迴產生迴圈，目標是把兩個數相異的 01 保留下來，相同的1就發動進位，直到沒有必要進位才結束
			// a ^ b 是保留相異處 a & b << 1 就是進位的動作

			//return b == 0 ? a : GetSum(a ^ b, (a & b) << 1);

			//我把它改寫成 loop 
			if (b == 0)
			{
				return a;
			}

			int curr = a;
			int carry = b;
			while (carry != 0)
			{
				int tempCurr = curr ^ carry;          //keep difference digits
				int tempCarry = (curr & carry) << 1;  //caculate carry digits,and keep them

				curr = tempCurr;
				carry = tempCarry;
			}

			return curr;
		}
    }
}

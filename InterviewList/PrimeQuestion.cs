using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.InterviewList
{
    public class PrimeQuestion
    {
		//204. Count Primes
		public int CountPrimes(int n)
		{
			if (n < 2)
			{
				return 0;
			}

			int[] primeTb = new int[n];
			primeTb[0] = 1;
			primeTb[1] = 1;

			int ind = 2;
			while ((long)ind * ind <= n)
			{
				for (int i = ind * ind; i < primeTb.Length; i += ind)
				{
					primeTb[i] = 1;
				}
				ind++;
			}

			return primeTb.Where(x => x == 0).Count();
		}
    }
}

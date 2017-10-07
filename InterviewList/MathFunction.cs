using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.InterviewList
{
    public class MathFunction
    {
		//69. Sqrt(x)
		public int MySqrt(int x)
		{
			int beg = 0;
			int end = x;

			int result = 0;
			while (beg <= end)
			{
				int mid = (beg + end) / 2;
				if ((long)mid * mid <= x)  //要注意這裡真的會爆, 測 2**30 , 除2也有 2**29 再自乘就 2**58 爆了
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

		//202. Happy Number
		public bool IsHappy(int n)
		{
			HashSet<int> hs = new HashSet<int>();
			hs.Add(n);

			while (n > 1)
			{
				n = getDigiSum(n);

				if (n == 1)
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

		private int getDigiSum(int n)
		{
			int total = 0;
			int quot = 0;
			int rem;
			do
			{
				quot = Math.DivRem(n, 10, out rem);
				n = quot;
				total += (int)Math.Pow(rem, 2);
			} while (quot > 0);

			return total;
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

		//412. Fizz Buzz
		public IList<string> FizzBuzz(int n)
		{
			List<string> result = new List<string>();
			for (int i = 1; i <= n; i++)
			{
				int rem3 = i % 3;
				int rem5 = i % 5;
				if (rem3 == 0 && rem5 == 0)
				{
					result.Add("FizzBuzz");
				}
				else if (rem3 == 0)
				{
					result.Add("Fizz");
				}
				else if (rem5 == 0)
				{
					result.Add("Buzz");
				}
				else
				{
					result.Add(i.ToString());
				}
			}

			return result;
		}

		//29. Divide Two Integers
		public int Divide(int dividend, int divisor)
		{
			int negValue = 1;
			if ((divisor < 0 && dividend > 0) || (divisor > 0 && dividend < 0))
			{
				negValue = -1;
			}

			//這裡也是陷井，abs 函式是你傳啥型別它就是啥，要用 long 接要傳 long
			long div = Math.Abs((long)dividend);
			long dsr = Math.Abs((long)divisor);
			long result = 0;
			while (div >= dsr)
			{
				long rate = 1; //這裡要特別小心，除1 就會遇到爆
				long tempDsr = dsr;
				while (div >= tempDsr << 1)
				{
					tempDsr <<= 1;
					rate <<= 1;
				}

				div -= tempDsr;
				result += rate;
			}
			result *= negValue;



			if ((result > int.MaxValue) || (result < int.MinValue))
			{
				return int.MaxValue;
			}
			else
			{
				return (int)result;
			}


			////連減是不夠快的，連加也是同理
            //long result = 0;
            //int negValue = 1;
            //if((divisor < 0 && dividend > 0) || (divisor > 0 && dividend < 0))
            //{
            //    negValue = -1;
            //}
            //long tempdiv = Math.Abs(divisor);
            //long tempV = Math.Abs(dividend);
            //while(tempV >= tempdiv)
            //{
            //    result++;
            //    tempV -= tempdiv;
            //}
            //result *= negValue;

            //if(result > int.MaxValue)
            //{
            //    return int.MaxValue;
            //}
            //if(result < int.MinValue)
            //{
            //    return int.MaxValue;
            //}
            //return (int)result;
		}

		//48. Rotate Image
		//這種題目沒解過就是想到死也想不出來，這是數學問題
		//先反轉再對稱交換，就是轉90度
		//這裡也學到 c# 裡  int[,] 和 int[][] 是不太一樣的
		//那個 Array.Reverse 只支援後者
		//https://stackoverflow.com/questions/597720/what-are-the-differences-between-a-multidimensional-array-and-an-array-of-arrays
		//原來後者叫 jagged array... 我喜歡
		public void Rotate(int[,] matrix)
		{
			int dim = matrix.GetLength(0); // matrix is n*n
			if (dim == 0)
			{
				return;
			}

			//reverse
			for (int i = 0; i < dim / 2; i++)
			{
				for (int j = 0; j < dim; j++)
				{
					int temp = matrix[i, j];
					matrix[i, j] = matrix[dim - i - 1, j];
					matrix[dim - i - 1, j] = temp;
				}
			}

			//swap sym
			for (int i = 0; i < dim; i++)
			{
				for (int j = 0; j < i; j++)
				{
					int temp = matrix[i, j];
					matrix[i, j] = matrix[j, i];
					matrix[j, i] = temp;
				}
			}
		}

		//50. Pow(x, n)
		//指數問題還沒有學乖嗎????? 連乘效率是有問題的!!!，這應該在上次 29 題計算有類似的概念
		//利用 x**2 直接把 pow 降一半，完全 O(logn)化
		//這是還會學到 double infinity 這個值，它只要算到爆，就會自動進 infinity
		//而小到小於  Epsilon 就歸 0,  1/infinity 也是歸 0
		//第一次理解到 double 的強大
		public double MyPow(double x, int n)
		{
			if (Math.Abs(x) < double.Epsilon) //這是查網路，人家判定 0 的寫法
				return 0;
			if (n == 0)
				return 1;

			long ln = n; //它很賤，會測 int.MinValue
			if (ln < 0)
			{
				ln = -ln;
				x = 1 / x;
			}

			if (ln % 2 == 0)
			{
				return MyPow(x * x, (int)(ln / 2));
			}
			else
			{
				return x * MyPow(x * x, (int)(ln / 2));
			}
		}

		//166. Fraction to Recurring Decimal
		//https://zh.wikipedia.org/wiki/%E7%84%A1%E7%90%86%E6%95%B8
		//這個題目我實在是無力了，連看解答都要看半天，這基本上就是數學問題，用程式模擬小數除法
		//上面的連結是在說明無理數，而這題是遇不到無理數的，因為無理數沒辦法寫成兩個整數的除法
		//所以，小數只有除盡和循環兩種狀態，不用怕，不然你也寫不出來… 
		//真正不好理解的是，當餘數開始重覆時，表示你會開始進入第一次的重覆，而這個餘數，包含了你一開始為了算整數部份除下來的那個，這個太容易略過了…
		//另外，除到是 0就整除了，但別把0拿去建字典，它是沒有發動循環的可能性
		//在循環前可能會有幾個不重覆的商 8/55 就是一例，這種一定要試到
		public string FractionToDecimal(int numerator, int denominator)
		{
			//0 就別算了，一不小心還會輸出負號成bug
			if (numerator == 0)
			{
				return "0";
			}

			string result = "";

			//強大的判斷異號，可以記下來
			if (numerator < 0 ^ denominator < 0)
			{
				result = "-";
			}

			//全部用正值去算，免干擾，用 long 才會除的安心，別忘了輸 int 可以輸 int.MinValue，取絕對值不用 long 會死的
			long num = Math.Abs((long)numerator);
			long den = Math.Abs((long)denominator);

			//首次取得商和餘，用來決定輸出長相，別忘了整除不要有小數點
			long rem;
			long qot;
			qot = Math.DivRem(num, den, out rem);
			result = string.Format("{0}{1}{2}", result, qot, rem == 0 ? "" : ".");

			//用它來背有出現過的餘數
			Dictionary<long, int> remToDenMap = new Dictionary<long, int>();

			while (!remToDenMap.ContainsKey(rem) && rem != 0)
			{
				//第一次的餘數，會因為這行而有記到，記的對應值是字串的當前長度，為了方便等一下插括號用的
				remToDenMap.Add(rem, result.Length);

				//開始放大除法，每次都把餘數放大十倍(全部往前移一位，用餘數等同用原本的數，這也是數學)
				//每次都可以順利的取出一位商數，並知道它帶來的餘數，作到餘0或是重覆為止
				//這裡最鬼的是，先用了才決定有沒有重覆，這點我也不會說明，是試case 試出來的
				rem *= 10;
				qot = Math.DivRem(rem, den, out rem);
				result += qot;
			}

			//如果有非0的重覆發生，就可以去找它上次發生的位置，插括號
			if (remToDenMap.ContainsKey(rem))
			{
				int ind = remToDenMap[rem];
				result = result.Insert(ind, "(");
				result += ")";
			}

			return result;
		}
	}
}

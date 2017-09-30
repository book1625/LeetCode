using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Medium
{
    public partial class InterviewList
    {
        //2. Add Two Numbers
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null)
            {
                return null;
            }

            ListNode head = new ListNode(-1);
            ListNode curr = head;

            bool isCarry = false;
            while (l1 != null || l2 != null)
            {
                int v1 = 0;
                int v2 = 0;
                int sum = isCarry ? 1 : 0;

                if (l1 != null)
                {
                    v1 = l1.val;
                    l1 = l1.next;
                }

                if (l2 != null)
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

            if (isCarry)
            {
                curr.next = new ListNode(1);
            }

            return head.next;
        }

        //3. Longest Substring Without Repeating Characters
        public int LengthOfLongestSubstring(string s)
        {
            if (s.Length < 1)
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
                if (s[left] == s[right])
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
            for (int i = 0; i < s.Length; i++)
            {
                string temp1 = GetPalindromeLenL(s, i, i + 1);
                string temp2 = GetPalindromeLenM(s, i);

                if (temp1.Length > maxLenStr.Length)
                {
                    maxLenStr = temp1;
                }

                if (temp2.Length > maxLenStr.Length)
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
            if (str == null)
            {
                return 0;
            }

            //前後空白都可略
            str = str.Trim();

            //沒有字元也不處理
            if (str.Length == 0)
            {
                return 0;
            }

            //處理正負
            bool isNegative = false;
            int ind = 0;
            if (str[0] == '+' || str[0] == '-')
            {
                isNegative = str[0] == '-';
                ind = 1;
            }

            //到這裡就只能看見數字，其它的符號都要無效
            long result = 0;
            for (int i = ind; i < str.Length; i++)
            {
                if (str[i] >= '0' && str[i] <= '9')
                {
                    result *= 10;
                    result += (byte)str[i] - (byte)'0';

                    //避免加過頭，文字是可以長到見鬼的…
                    //要能放的下負極值，別忘了，負的在這也是用正值處理
                    if (result - 1 >= int.MaxValue)
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
            if (isNegative)
            {
                result = -result;
            }

            //避免極正
            if (result > int.MaxValue)
            {
                return int.MaxValue;
            }

            //避免極負
            if (result < int.MinValue)
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

        /*
        private class ThreeNum
        {
            public int[] data;

            public ThreeNum(int n1, int n2, int n3)
            {
                data = new int[3] { n1, n2, n3 };
                Array.Sort(data);
            }

            //物件的比較，從 hashcode 先比，相同再比 Equals
            //所以這兩個都要覆寫，才有效
            
            public override int GetHashCode()
            {
                return 0;
            }

            public override bool Equals(object obj)
            {
                ThreeNum temp = (ThreeNum)obj;
                return temp.data[0] == this.data[0]
                        && temp.data[1] == this.data[1]
                        && temp.data[2] == this.data[2];
            }

            public List<int> ToList()
            {
                return new List<int>() { data[0], data[1], data[2] };
            }
        }
        */

		//15. 3Sum
		public IList<IList<int>> ThreeSum(int[] nums)
		{
            //這個演算法成本為 O(n**2)
            //首先，我一開始打算沿用我原本的 hash 來過濾重覆答案
            //所以我學它一樣使用固定 i , 夾擊剩下的所有元素，結果時間一直不會過
            //後來只好學它，在所有重覆元素區進行略過的動作，但我認為這一動並不容易想到!!
            //可是，就算如此，時間依然沒有過，很明顯我處理重覆的效率足以拖跨這一題
            //最後只好還是拿掉我的 hash 物件，直接加就會過…
            //這個系列的題目，有取兩和0(用夾擊)，取兩和絕對值最小(用絕對值排序)
            //取三和0(一定點配夾擊，取 x<=n 個和最大(用累計 dp 解)，取兩組 x,y<n 和最大(用雙向dp解)

            List<IList<int>> result = new List<IList<int>>();

            Array.Sort(nums);

            for (int i = 0; i < nums.Length - 2; i++)
            {
                if(i > 0 && nums[i] == nums[i-1])
                {
                    continue;
                }

                int left = i + 1;
                int right = nums.Length - 1;

                while (left < right)
                {
                    int sum = nums[i] + nums[left] + nums[right];
                    if(sum == 0)
                    {                       
                        result.Add(new List<int>(){nums[i], nums[left], nums[right]});
                        while (left < right && nums[left] == nums[left+1])
                        {
                            left++;
                        }
                        while(left < right && nums[right] == nums[right-1])
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

			/* 這個算法，比 O(n**2)大一些，因為是基於 n**2 上再去作推動一輪K

            HashSet<ThreeNum> result = new HashSet<ThreeNum>();
            Array.Sort(nums);

            for (int i = 0; i < nums.Length - 2; i++)
            {
                int k = nums.Length - 1;
                for (int j = i + 1; j < nums.Length - 1; j++)
                {
                    while (k > j)
                    {
                        int temp = nums[i] + nums[j] + nums[k];

						//check sum
						if (temp > 0)
						{
							k--;
						}
						else if(temp == 0)
						{
                            //add result
                            result.Add(new ThreeNum(nums[i], nums[j], nums[k]));
                            k--;
                            break;
						}
                        else
                        {
                            break;
                        }
                    }
                }
            }

            List<IList<int>> tempResult = new List<IList<int>>();

            foreach(var t in result)
            {
                tempResult.Add(t.ToList());   
            }

            return tempResult;
            */

		}

        //17. Letter Combinations of a Phone Number
        //我的想法是建立一個四進位的計算器，然後依照輸入的字元數量推動計數器取得所有組合，再去查表得到真正的資料
        //但是碼有些冗長了
		public IList<string> LetterCombinations(string digits)
		{
            string[,] map = new string[8,4]
            {
                {"a","b","c",""},
                {"d","e","f",""},
                {"g","h","i",""},
                {"j","k","l",""},
                {"m","n","o",""},
                {"p","q","r","s"},
                {"t","u","v",""},
                {"w","x","y","z"}
            };


            int[] count4bits = new int[digits.Length];
            Func<bool> inc = () =>
            {
                int  carry = 1;
                for (int i = count4bits.Length - 1; i >= 0; i--)
                {
                    count4bits[i] += (carry);

                    carry = 0;
                    if(count4bits[i] > 3)
                    {
                        count4bits[i] -= 4;
                        carry = 1;
                    }

                    if(carry == 0)
                    {
                        break;
                    }
                }
                return carry == 0; // if it is overflow
            };

            int[] index = new int[digits.Length];
            for (int i = 0; i < digits.Length; i++)
            {
                index[i] = digits[i] - '2';

                if(index[i] < 0 || index[i] > 7)
                {
                    return new List<string>();
                }
            }

            bool isOverflow = false;
            List<string> result = new List<string>();
            do
            {
                string curr = "";
                for (int i = 0; i < index.Length; i++)
                {
                    string temp = map[index[i], count4bits[i]];

                    if(temp != "")
                    {
                        curr += temp;
                    }
                    else
                    {
                        curr = "";
                        break;
                    }
                }

                if(!string.IsNullOrEmpty(curr))
                {
                    result.Add(curr);
                }

                isOverflow = !inc();
            } while (!isOverflow);

            return result;
		}

        //這是參考別人寫法以後，練習寫的，碼比較簡短
        //基本上，那個取出所有當前字串再加上一碼的作法，我不是很能認同，但在不考慮字串成本的情況下，這樣寫真的易懂
		public IList<string> LetterCombinations2(string digits)
		{
            //string 本身是陣列，沒必要分開來建二維
            string[] map = new string[8] {"abc","def","ghi","jkl","mno","pqrs","tuv","wxyz"};

            //檢查輸入，並轉成可用的索引，比較方便
			int[] index = new int[digits.Length];
			for (int i = 0; i < digits.Length; i++)
			{
				index[i] = digits[i] - '2';

				if (index[i] < 0 || index[i] > 7)
				{
					return new List<string>();
				}
			}

            List<string> result = new List<string>();
            result.Add("");
            List<string> temp = new List<string>();
            for (int i = 0; i < index.Count(); i++)
            {
                foreach (var addc in map[index[i]])
                {
                    foreach (var s in result)
                    {
                        temp.Add(s+addc);
                    }
                }

                result = temp;
                temp = new List<string>();
            }

            return result;
		}

        //19. Remove Nth Node From End of List
		public ListNode RemoveNthFromEnd(ListNode head, int n)
		{
            ListNode curr = head;
            ListNode pre = head;
            ListNode last = head;
            int count = 1;

            //用兩隻指標指向快和慢，慢就是要刪除的那個
            while(curr.next != null)
            {
                pre = curr;
                curr = curr.next;

                count++;

                if(count > n )
                {
                    last = last.next;
                }
            }

            //刪的時候，有分刪頭，刪尾，刪中間，這是要有不同寫法的，刪尾要有前一才有辦法刪
            //刪中可以用分身複制作弊
            if (last == head)
            {
                //remove head; 
                return head.next;
            }
            else if(last.next == null)
            {
                //remove tail
                pre.next = null;
            }
            else
            {
                //remove middle
                last.val = last.next.val;
                last.next = last.next.next;
            }

            return head;
		}

        //22. Generate Parentheses
		public IList<string> GenerateParenthesis(int n)
		{
            /*這個寫沒辦法真的舉出所有的答案，我也沒想透為何
             * 一直加各種 case 也是組不滿
            int loop = n;

            List<string> result = new List<string>();
            result.Add("E");    
            while(loop > 0)
            {
                loop--;

                List<string> temp = new List<string>();

                foreach(var s in result)
                {
                    temp.Add(s.Replace("E", "(E)"));
                    temp.Add(s.Replace("E", "E()"));
                    temp.Add(s.Replace("E", "()E"));
                }
                result = temp;
            }

            return result.Select(s => s.Replace("E", "")).Distinct().Where(s=>s.Length == n*2).ToList();
            */

            List<string> result = new List<string>();
            genParenthesisByRecursive(result, "", n, n);
            return result;
		}

        private void genParenthesisByRecursive(List<string> result, string currStr, int remLeft, int remRight)
        {
            //左邊沒放前，右邊是不能放的
            if (remLeft > remRight)
            {
                return;
            }

            if(remLeft > 0)
            {
                genParenthesisByRecursive(result, currStr+"(", remLeft-1, remRight);
            }

            if(remRight > 0)
            {
                genParenthesisByRecursive(result, currStr+")", remLeft, remRight-1);
            }

            if(remLeft == 0 && remRight == 0)
            {
                result.Add(currStr);
                return;
            }
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
            while(div>= dsr)
            {
                long rate = 1; //這裡要特別小心，除1 就會遇到爆
                long tempDsr = dsr;
                while(div >= tempDsr << 1)
                {
                    tempDsr <<= 1;
                    rate <<= 1;
                }

                div -= tempDsr;
                result += rate;
            }
			result *= negValue;



            if ( (result > int.MaxValue) || (result < int.MinValue) )
			{
				return int.MaxValue;
			}
            else
            {
                return (int)result;
            }


            /* 連減是不夠快的，連加也是同理
            long result = 0;
            int negValue = 1;
            if((divisor < 0 && dividend > 0) || (divisor > 0 && dividend < 0))
            {
                negValue = -1;
            }

            long tempdiv = Math.Abs(divisor);
            long tempV = Math.Abs(dividend);
            while(tempV >= tempdiv)
            {
                result++;
                tempV -= tempdiv;
            }
            result *= negValue;



            if(result > int.MaxValue)
            {
                return int.MaxValue;
            }

            if(result < int.MinValue)
            {
                return int.MaxValue;
            }

            return (int)result;
            */
		}

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
            for (int i = 0; i < nums.Length-1; i++)
            {
                if(nums[i] == target)
                {
                    return i;
                }

                if(nums[i] > nums[i+1])
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
    }
}

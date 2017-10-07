using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.InterviewList
{
    public class StringOP
    {
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

		//13. Roman to Integer
		//羅馬數字的產生規則不容易 https://zh.wikipedia.org/wiki/%E7%BD%97%E9%A9%AC%E6%95%B0%E5%AD%97
		//但解讀相對簡單，因為左減只會有一個位數，所以每次只要判定當下這個位數是要加還是要減
		//最後一個位，一定是加，因為它沒有下個位可以判定為左減
		public int RomanToInt(string s)
		{

			//如果 s 不合法字元，這個寫法就當定了，但如果沒有，這個寫法比較易讀

			if (s.Length == 0)
			{
				return 0;
			}

			Dictionary<char, int> romanMap = new Dictionary<char, int>();
			romanMap.Add('M', 1000);
			romanMap.Add('D', 500);
			romanMap.Add('C', 100);
			romanMap.Add('L', 50);
			romanMap.Add('X', 10);
			romanMap.Add('V', 5);
			romanMap.Add('I', 1);

			int sum = romanMap[s[s.Length - 1]];
			for (int i = 0; i < s.Length - 1; i++)
			{
				if (romanMap[s[i]] < romanMap[s[i + 1]])
				{
					sum -= romanMap[s[i]];
				}
				else
				{
					sum += romanMap[s[i]];
				}
			}
			return sum;
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
		public bool IsValid(string s)
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

		//125. Valid Palindrome
		public bool IsPalindrome(string s)
		{
			if (s.Length == 0)
			{
				return true;
			}

			List<char> temp = new List<char>();
			foreach (var c in s)
			{
				if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
				{
					temp.Add(char.ToLower(c));
				}
			}

			int head = 0;
			int tail = temp.Count() - 1;
			while (head <= tail)
			{
				if (temp[head] == temp[tail])
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

		//242. Valid Anagram
		public bool IsAnagram(string s, string t)
		{
			if (s.Length != t.Length)
			{
				return false;
			}

			if (s.Length == 0)
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
				if (count[0, i] != count[1, i])
				{
					return false;
				}
			}

			return true;
		}

		//344. Reverse String
		public string ReverseString(string s)
		{
			return new string(s.Reverse().ToArray());

			/* c# string is read onle, so, cannot reverse in place
            if(s.Length <= 1)
            {
                return s;
            }

            int beg = 0;
            int end = s.Length - 1;

            while(beg <= end)
            {
                char temp = s[beg];
                s[beg] = s[end];
                s[end] = temp;

                beg++;
                end++;
            }
            return s;
            */
		}

		//387. First Unique Character in a String
		public int FirstUniqChar(string s)
		{
			int[] chTb = new int[26];

			foreach (var c in s)
			{
				chTb[(byte)c - (byte)'a']++;
			}

			HashSet<int> hs = new HashSet<int>();
			for (int i = 0; i < chTb.Length; i++)
			{
				if (chTb[i] == 1)
				{
					hs.Add(i);
				}
			}

			for (int i = 0; i < s.Length; i++)
			{
				if (hs.Contains((byte)s[i] - (byte)'a'))
				{
					return i;
				}
			}

			return -1;
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

		//17. Letter Combinations of a Phone Number
		//我的想法是建立一個四進位的計算器，然後依照輸入的字元數量推動計數器取得所有組合，再去查表得到真正的資料
		//但是碼有些冗長了
		public IList<string> LetterCombinations(string digits)
		{
			string[,] map = new string[8, 4]
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
				int carry = 1;
				for (int i = count4bits.Length - 1; i >= 0; i--)
				{
					count4bits[i] += (carry);

					carry = 0;
					if (count4bits[i] > 3)
					{
						count4bits[i] -= 4;
						carry = 1;
					}

					if (carry == 0)
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

				if (index[i] < 0 || index[i] > 7)
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

					if (temp != "")
					{
						curr += temp;
					}
					else
					{
						curr = "";
						break;
					}
				}

				if (!string.IsNullOrEmpty(curr))
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
			string[] map = new string[8] { "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };

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
						temp.Add(s + addc);
					}
				}

				result = temp;
				temp = new List<string>();
			}

			return result;
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

			if (remLeft > 0)
			{
				genParenthesisByRecursive(result, currStr + "(", remLeft - 1, remRight);
			}

			if (remRight > 0)
			{
				genParenthesisByRecursive(result, currStr + ")", remLeft, remRight - 1);
			}

			if (remLeft == 0 && remRight == 0)
			{
				result.Add(currStr);
				return;
			}
		}

		//49. Group Anagrams
		//這題一開始就想寫的很雄心壯志，所以直接就從設計 hash 開始
		//結果反而忽略了以排序好的字串作 key 這種簡單解法
		//然後一直在 hash 上卡彈，遇到計算結果一樣的… XD
		public IList<IList<string>> GroupAnagrams(string[] strs)
		{
			Dictionary<string, List<string>> group = new Dictionary<string, List<string>>();

			foreach (var s in strs)
			{
				string tempKey = string.Join("", s.OrderBy(x => x));
				List<string> tarLs;
				if (group.TryGetValue(tempKey, out tarLs))
				{
					tarLs.Add(s);
				}
				else
				{
					tarLs = new List<string>();
					tarLs.Add(s);
					group.Add(tempKey, tarLs);
				}
			}

			List<IList<string>> result = new List<IList<string>>();
			foreach (var kv in group)
			{
				result.Add(kv.Value);
			}
			return result;
		}

		//失敗的 string hash for ignore order

		//private uint[] primeTb = new uint[] { 2,3,5,7,11,13,17,19,23,29,31,37,39,41,43,47,49,51,53,57,59,61,67,71,73,79,87,89,91,97 };
		//private uint getCustomHash(string s)
		//{
		//    uint hash = 1;
		//    foreach(var c in s)
		//    {
		//        hash *= primeTb[c - 'a'];
		//    }
		//    hash += (uint)s.Length * 13;
		//    return hash;
		//}

		//91. Decode Ways
		public int NumDecodings(string s)
		{
			if (s.Length == 0 || s[0] == '0')
			{
				return 0;
			}

			//dp 表在記，到目前這個數字，可以得到多少組合
			//數字有幾種情況…，寫在碼裡作說明
			int[] dp = new int[s.Length];
			dp[0] = 1;

			for (int i = 1; i < dp.Length; i++)
			{
				if (i + 1 < dp.Length && s[i + 1] == '0')
				{
					//這個數字被後面的 0 限定了，它目前無法產生任何組合的運算
				}
				else
				{
					//進來這，表示數字是自由的，那它又有兩種可能

					//它就是個可以獨立解碼的數，所以不含 0
					//那它的值就是前一個碼所能得到的組合數，加上它並沒有新增組合
					if (s[i] >= '1' && s[i] <= '9')
					{
						//non zero digit
						dp[i] += dp[i - 1];
					}

					//它如果可以和前面的數字作組合，則我們可以得到額外一條路線的所有組合
					//想像在兩個節點前，共有 n 組合，然後後面來的兩個數字，可以合併，也可以分開
					//如果我選擇分開，那就有前一個數字的組合那麼多組
					//如果我選擇組合，那就有 n 組，這兩種加起來才是我當前的所有可能…
					//這裡為了避最開頭就可以組合造成的超索引，所以加入判斷，不然初值會一直不對
					if (s[i - 1] == '1' && s[i] >= '0' && s[i] <= '9')
					{
						dp[i] += i - 2 >= 0 ? dp[i - 2] : 1;
					}
					else if (s[i - 1] == '2' && s[i] >= '0' && s[i] <= '6')
					{
						dp[i] += i - 2 >= 0 ? dp[i - 2] : 1;
					}
				}
			}
			return dp[s.Length - 1];
		}

		////可惜了，這題有 dp 解，所以遞迴解雖然答案對，卻不夠看了…
		//public int NumDecodings(string s)
		//{
		//    if (s.Length == 0 || s[0] == '0')
		//    {
		//        return 0;
		//    }

		//    recNumDecode(s, 0);
		//    return total;
		//}

		//private int total = 0;
		//private void recNumDecode(string s, int ind)
		//{
		//    if (ind == s.Length)
		//    {
		//        //is over...
		//        total++;
		//    }
		//    else if (s[ind] == '0')
		//    {
		//        //this is wrong path, we cannot deal 0
		//        //這個條件很容易忽略，我是因為最後剩一個0 才發現
		//        //測 202110 就知道了
		//        //有了這個檢查，反而下面原本都在查0的就不管它了，反正遞進去也是又 return 出來
		//        return;
		//    }
		//    else if (ind == s.Length - 1)
		//    {
		//        //is final
		//        recNumDecode(s, ind + 1);
		//    }
		//    else
		//    {
		//        //at least two digits
		//        if (s[ind] == '1')
		//        {
		//            recNumDecode(s, ind + 1);               
		//            recNumDecode(s, ind + 2);
		//        }
		//        else if (s[ind] == '2' && s[ind + 1] <= '6')
		//        {
		//            recNumDecode(s, ind + 1);               
		//            recNumDecode(s, ind + 2);
		//        }
		//        else
		//        {
		//            recNumDecode(s, ind + 1);
		//        }
		//    }
		//}

		//127. Word Ladder
		//這個寫法是照人家的解寫出來的，但我其實有些不認同
		//用湊字去猜，字長會成為新的參數，雖然我實測20個字元，就是要猜 25*20 個字
		//雖然級數沒有用 n 來的高，但也一定存在反例
		//這種站在小集合掃大集合的想法，有時候也很難反應過來，我一開始就是想說 25**20 大的見鬼怎麼可能全拿出來比
		//所以一直沒反應到，一次只會改一個位置，只有25種變化
		public int LadderLength(string beginWord, string endWord, IList<string> wordList)
		{
			HashSet<string> wordList2 = new HashSet<string>(wordList);
			List<string> next = new List<string>();
			HashSet<string> visited = new HashSet<string>();
			next.Add(beginWord);
			int loop = 1;
			while (next.Count() > 0)
			{
				List<string> tempNext = new List<string>();

				foreach (string curr in next)
				{
					if (curr == endWord)
					{
						return loop;
					}

					//create all possible word
					for (char c = 'a'; c <= 'z'; c++)
					{
						for (int i = 0; i < curr.Length; i++)
						{
							if (curr[i] != c)
							{
								char[] temp = curr.ToArray();
								temp[i] = c;
								string str = new string(temp);

								if (!visited.Contains(str) && wordList2.Contains(str))
								{
									tempNext.Add(str);
									visited.Add(str);
								}
							}
						}
					}
				}

				next = tempNext;
				loop++;
			}

			return 0;
		}


		////這個寫法，我認為精神有作到了，只是輸在字如果多了就開始慢，因為每次都要把沒用過的字再作一次字對
		//public int LadderLength(string beginWord, string endWord, IList<string> wordList)
		//{
		//    //1 全等 0 只差一碼 -1 差很大
		//    Func<string, string, int> compare = (x, y) =>
		//    {
		//        if (x == y)
		//            return 1;

		//        int fuzzy = 1;
		//        for (int i = 0; i < x.Length; i++)
		//        {
		//            if (x[i] != y[i])
		//            {
		//                fuzzy--;
		//            }

		//            if (fuzzy < 0)
		//            {
		//                return -1;
		//            }
		//        }

		//        return 0;
		//    };

		//    List<string> next = new List<string>();
		//    HashSet<int> visited = new HashSet<int>();
		//    next.Add(beginWord);
		//    int loop = 1;
		//    while (next.Count() > 0)
		//    {
		//        List<string> tempNext = new List<string>();

		//        foreach (string curr in next)
		//        {
		//            if (curr == endWord)
		//            {
		//                return loop;
		//            }

		//            for (int i = 0; i < wordList.Count(); i++)
		//            {
		//                if (!visited.Contains(i))
		//                {
		//                    int cmp = compare(curr, wordList[i]);
		//                    if (cmp == 0)
		//                    {
		//                        tempNext.Add(wordList[i]);
		//                        visited.Add(i);
		//                    }
		//                }
		//            }
		//        }

		//        next = tempNext;
		//        loop++;
		//    }

		//    return 0;
		//}

		//131. Palindrome Partitioning
		//https://discuss.leetcode.com/topic/6186/java-backtracking-solution
		//這個解法要我自己想，要想破頭的…
		//Backtracking 技法，應該要背下來
		//在自己成立的狀態下，把剩下的遞迴，能走完就是一份可用結果，每次退回都要拿到自己，回到前一個狀態
		//如果前一個狀態有多種可能要試，那它就是個迴圈，產生一個新的自己，又開始遞迴
		public IList<IList<string>> Partition(string s)
		{
			List<IList<string>> result = new List<IList<string>>();
			List<string> currResult = new List<string>();
			recPartition(s, 0, currResult, result);
			return result;
		}

		//遞迴可迴文的片段
		private void recPartition(string s, int index, List<string> currResult, List<IList<string>> result)
		{
			if (index == s.Length)
			{
				//這裡一定要用複制一份，很重要，因為 currResult 是共用物件，在遞迴裡也沒辦法重新生一個取代 
				result.Add(new List<string>(currResult));
			}
			else
			{
				for (int i = index; i < s.Length; i++)
				{
					//如果可以成功的迴文，就要往下檢查所有的剩餘部份
					if (isPalindrome(s, index, i))
					{
						//把自己加入
						currResult.Add(s.Substring(index, i - index + 1));
						//以自己存在的狀態下，往下 parse
						recPartition(s, i + 1, currResult, result);
						//把自己拿掉，回到前一個狀態
						currResult.RemoveAt(currResult.Count() - 1);
					}
				}
			}
		}

		//檢查迴文
		private bool isPalindrome(string s, int beg, int end)
		{
			if (beg == end)
				return true;

			while (beg <= end)
			{
				if (s[beg] != s[end])
					return false;

				beg++;
				end--;
			}
			return true;
		}

		//139. Word Break
		//遞迴版本又超時了，但至少通過八成的測試，正確度是有的
		//那個時間死掉的測試它很賤，給了超長a，然後字典有一個單獨a，所以切字時一直中小的... xd
		//我有試著從長的字先切，沒有用，一樣超時，沒關系，至少我努力的記得 BackTracking 的寫法
		//BackTracking 可以作到秀出所有切割的可能答案，但這題只是在問能不能切
		public bool WordBreak(string s, IList<string> wordDict)
		{
			//DP 解，看別人的，自己想一次
			//如果有張表，記錄這個字串在只考慮前面長度n時有沒有辦法切，那我們要的答案是長度完整時那一格的值
			//如果這張表存在，在判斷某個點y可不可以切，可以考慮借用所有 x (x < y) 的結果來配
			//也就是說 如果前面可以切到x，那我只要管 x-1到y 這個字串有沒有在字典裡，如果有，那就形成 y 可以切
			//一路推到 y = s.Length 就可以知道有沒有得切

			HashSet<string> dict = new HashSet<string>(wordDict);

			bool[] canBreak = new bool[s.Length + 1];    //記錄各種長度可不可以切，長度0保證可以切
			canBreak[0] = true;
			for (int len = 1; len <= s.Length; len++)  //從長度1開推動，驗證每個長度能不能切
			{
				for (int i = 0; i < len; i++)          //在每個長度下，不停的分兩半，前半問能不能切，後半問有沒有在字典裡
				{
					if (canBreak[i] && dict.Contains(s.Substring(i, len - i)))
					{
						canBreak[len] = true;
						break; //可以切就閃人了，目標達成
					}
				}
			}

			return canBreak[s.Length];
		}

		//遞迴版本，爆 TLE
		//public bool WordBreak(string s, IList<string> wordDict)
		//{
		//    return recWordBreak(s, 0, new HashSet<string>(wordDict));
		//}

		//private bool recWordBreak(string s, int index, HashSet<string> dict)
		//{
		//    if(index == s.Length)
		//    {
		//        //got a success bareking
		//        return true;
		//    }
		//    else
		//    {
		//        for (int i = index; i < s.Length; i++)
		//        {
		//            string str = s.Substring(index, i - index + 1);
		//            if(dict.Contains(str))
		//            {
		//                if(recWordBreak(s, i+1, dict))
		//                {
		//                    return true;
		//                }
		//            }
		//        }

		//        return false;
		//    }
		//}

		//150. Evaluate Reverse Polish Notation
		//這個是學校題，用 stack 來解 Reverse Polish Notation
		//有個延伸議題是 polish notation 是如何轉出來的，它是可以免括號的哦~~
		public int EvalRPN(string[] tokens)
		{
			if (tokens.Count() == 0)
			{
				return 0;
			}

			Stack<int> stk = new Stack<int>();

			Func<string, int> operate = (t) =>
			{
				int v1, v2;
				v1 = stk.Pop();
				v2 = stk.Pop();
				if (t == "+")
				{
					return v2 + v1;
				}
				else if (t == "-")
				{
					return v2 - v1;
				}
				else if (t == "*")
				{
					return v2 * v1;
				}
				else if (t == "/")
				{
					return v2 / v1;
				}

				//impossible
				return int.MinValue;
			};

			foreach (var t in tokens)
			{
				int temp;
				if (int.TryParse(t, out temp))
				{
					//it's a number
					stk.Push(temp);
				}
				else
				{
					//it's an operator
					stk.Push(operate(t));
				}
			}

			return stk.Pop();
		}

		//179. Largest Number
		//這題一開始就有想要用自訂的字串排序來解
		//但是邊解卻發現，特例多到見鬼了… 短的不一定對，長的不見得差，我以各領頭數字先分群
		//但每群中的排序卻需要類遞迴的解法，而且基準又是靠這個群的領先字元
		// 3，34，310，331 => 34，3，331，310 那個 3 的位置很難捉…
		//後來看了人家的解法，實在太聰明了… 就很簡單，兩個都拿來組組看，看那一組結果好就選它…xd
		public string LargestNumber(int[] nums)
		{
			if (nums.Count() == 0)
			{
				return "0";
			}

			List<string> data = new List<string>();
			foreach (var i in nums)
			{
				data.Add(i.ToString());
			}
			data.Sort(strcmp);

			string result = "";

			//資料只給了你一海面的0
			if (data.First()[0] == '0')
			{
				return "0";
			}

			foreach (var s in data)
			{
				result += s;
			}

			return result;
		}

		private int strcmp(string x, string y)
		{
			string s1 = x + y;
			string s2 = y + x;
			return s2.CompareTo(s1);
		}

		//227. Basic Calculator II
		//題目本身並不難，先乘除後加減可以在 stack 中打混掉
		//真正難發現的是，加減的計算，在 stack 中的順序是不對的，要一路正序才正常
		//stack 是為了解決該先算的要先算，但一路正常算就不該是 stack 的事了
		//https://discuss.leetcode.com/topic/16935/share-my-java-solution
		//這個解法和我的思路很像，但他更精簡，乘除也是直接作，加減就是轉正負值
		//最後它只是把一堆值加起來，不像我有順序的問題，這樣看起來很精簡
		//https://discuss.leetcode.com/topic/16807/17-lines-c-easy-20-ms
		//這一個我覺得是類似的，但他的碼真的少到我不太理解，那個c++ 的 in >> n 這樣可以直接切出一個數字??
		public int Calculate(string s)
		{
			Stack<string> sk = new Stack<string>();

			s = s.Replace(" ", "");

			string currDigit = "";

			//把算元推到 stack 裡，有乘除會先算完，把算過的拿掉，再推進去
			Action<string> pushOperand = (opn2) =>
			{
				if (sk.Count() == 0)
				{
					sk.Push(opn2);
					return;
				}

				string op = sk.Peek();

				if (op == "*" || op == "/")
				{
					op = sk.Pop();
					string opn1 = sk.Pop();

					//caculate
					int val = op == "*" ? int.Parse(opn1) * int.Parse(opn2) : int.Parse(opn1) / int.Parse(opn2);
					sk.Push(val.ToString());
				}
				else
				{
					sk.Push(opn2);
				}
			};

			//函式主體，把整個乘除先轉換掉，變成連續加減
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '0' && s[i] <= '9')
				{
					currDigit += s[i];

					if (i == s.Length - 1)
					{
						//last operand                                              
						pushOperand(currDigit);
						currDigit = "";
					}
				}
				else
				{
					pushOperand(currDigit);
					currDigit = "";

					sk.Push(s[i].ToString());
				}
			}

			//加減要一路正序的作過來才會對!! 這裡是最容易忘的
			List<string> ascData = sk.Reverse().ToList();
			int result = int.Parse(ascData[0]);
			for (int i = 1; i < ascData.Count() - 1; i += 2)
			{
				string op = ascData[i];
				string opn2 = ascData[i + 1];

				result = (op == "+") ? result + int.Parse(opn2) : result - int.Parse(opn2);
			}

			return result;
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


	}
}

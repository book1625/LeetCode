using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Medium
{
    public partial class InterviewList
    {
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
        public void recPartition(string s, int index, List<string> currResult, List<IList<string>> result)
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

            while(beg <= end)
            {
                if (s[beg] != s[end])
                    return false;
                
                beg++;
                end--;
            }
            return true;
        }

        //134. Gas Station
        //這題放中級是有點怪，不過它有些狀況一開始確實沒掌握到
        //主要有兩情況，tank 一旦負值就不能再跑了，不能全跑完再看有沒有負
        //一旦 tank 負了就是負了，就不能再跑第二段計算
        //這是  O(n**2) 的解法，看了別人寫的才發現有好幾種 O(n) 解，但都不太容易了解
        //我找了一個我覺得有看懂的來試寫一次，他的想法是，找到一個中間點可以開到底
        //而它剩下的油足夠開完其它的前半段，所以它一路累計前半的值，前試著在當前的點往下開
        //每次失敗它就放棄中間的所有點，直接從失敗點的下一站開始，並留下前半的累計
        //它為何可以直接放棄一段失敗過程中的所有點?? A 是這一段的起點 B 是失敗點 B-1 是失敗前一點
        //在到達 B-1 時，油都還是0以上，是進入了 B 成本太高才失敗的，所以 A ~ B-1 之間
        //不論你從任何點出發前往 B 你也湊不到一個足夠 B的成本，別忘了 A ~ B-1 一路都是保有油的狀態，
        //少掉 A 甚至 少掉 A+1，都只會讓油持平更少，不可能變多，因為這表示拿掉的是負的站點，但它是負的你一開始就過不來了
        //別忘了 A ~ B-1 每一個站點的油累計，不是變多，就是變0，不會是負的，所以你拿掉的區，不是正值就是0，不會是負的
        //…不過我覺得這覺這很難想到就是了，
        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int leftSum = 0;
            int rightSum = 0;
            int start = 0;
            for (int i = 0; i < gas.Length; i++)
            {
                rightSum += gas[i] - cost[i];
                if(rightSum < 0)
                {
                    leftSum += rightSum;
                    rightSum = 0;
                    start = i + 1;
                }
            }

            leftSum += rightSum;
            return leftSum >= 0 ? start : -1;
        }

        //原始寫法，會過關的，只是有些弱 
        public int CanCompleteCircuit2(int[] gas, int[] cost)		{
            int s1, s2, e1, e2 ;

            for (int i = 0; i < gas.Length; i++)
            {
                if(i == 0)
                {
                    s1 = 0;
                    e1 = gas.Length - 1;
                    s2 = -1;
                    e2 = -1;
                }
                else
                {
                    s1 = i;
                    e1 = gas.Length - 1;
                    s2 = 0;
                    e2 = s1 - 1;
                }

				int tank = 0;
				for (int j = s1; j <= e1; j++)
				{
                    tank += gas[j];
                    tank -= cost[j];
                    if (tank < 0)
                        break;
				}

                if (i != 0 && tank >= 0)
                {
                    for (int j = s2; j <= e2; j++)
                    {
                        tank += gas[j];
                        tank -= cost[j];
                        if (tank < 0)
                            break;
                    }
                }

                if(tank >= 0)
                {
                    return i;
                }
            }

            return -1;
		}

        //138. Copy List with Random Pointer
        //這題也沒辦法除錯，只能硬幹上傳，結果兩次就過了
        //只有忘記加上 random 指標可能是 null 就不要去查字典了的這行檢查 
		public RandomListNode CopyRandomList(RandomListNode head)
		{
            RandomListNode resultHead = null;
            Dictionary<RandomListNode, RandomListNode> map = new Dictionary<RandomListNode, RandomListNode>();
            RandomListNode src = head;
            RandomListNode curr = null;
            while(src != null)
            {
                RandomListNode temp = new RandomListNode(src.label);
                if(curr != null)
                {
                    curr.next = temp;
                }
                curr = temp;

                if(resultHead == null)
                {
                    resultHead = curr;
                }

                map.Add(src, curr);

                src = src.next;
            }

            src = head;
            curr = resultHead;
            while(src!=null)
            {
                if (src.random != null)
                {
                    curr.random = map[src.random];
                }

                src = src.next;
                curr = curr.next;
            }

            return resultHead;
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

            bool[] canBreak = new bool[s.Length+1];    //記錄各種長度可不可以切，長度0保證可以切
            canBreak[0] = true;
            for (int len = 1; len <= s.Length; len++)  //從長度1開推動，驗證每個長度能不能切
            {
                for (int i = 0; i < len; i++)          //在每個長度下，不停的分兩半，前半問能不能切，後半問有沒有在字典裡
                {
                    if(canBreak[i] && dict.Contains(s.Substring(i,len-i)))
                    {
                        canBreak[len] = true;
                        break; //可以切就閃人了，目標達成
                    }
                }
            }

            return canBreak[s.Length];
        }

		public bool WordBreak2(string s, IList<string> wordDict)
		{
            return recWordBreak(s, 0, new HashSet<string>(wordDict));
		}

        private bool recWordBreak(string s, int index, HashSet<string> dict)
        {
            if(index == s.Length)
            {
                //got a success bareking
                return true;
            }
            else
            {
                for (int i = index; i < s.Length; i++)
                {
                    string str = s.Substring(index, i - index + 1);
                    if(dict.Contains(str))
                    {
                        if(recWordBreak(s, i+1, dict))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

		//148. Sort List
		//這題雖然一開始就有想到應該是用 merge sort，但沒想到麻煩真多
        //而且 merge sort 的 nLogn 其中的 log 來自每次都把手上的集合對半分
        //這件事沒作就沒有義意，我原本只有把頭取下，這樣分一點用都沒有，會遞迴到爆
        //這題又麻煩在它是 linked list，所以切分與next指標的設置，檢查都有狀況
        public ListNode SortList(ListNode head)
		{
            if(head ==null || head.next == null)
            {
                return head;
            }
            ListNode tmph;
            cutIntoTwo(head, out head, out tmph);
            return recMergeSort(head, tmph);
		}

        private void cutIntoTwo(ListNode head, out ListNode left, out ListNode right)
        {
            if( head != null && head.next != null)
            {
				left = head;
                right = head.next;
                while (right != null && right.next != null)
                {
                    right = right.next.next;
                    left = left.next;
                }

                right = left.next;
                left.next = null;
                left = head;               
            }
            else
            {
                left = null;
                right = null;
            }
        }

        private ListNode recMergeSort(ListNode left, ListNode right )
        {
            //資料不夠小，就對半分，兩半再 merge
            //所以，一定是資料切到夠小了才 merge
            //然後開始收斂，一路 merge起來，直到又成為一條

            if (left.next != null)
            {
                ListNode tmpl;
                cutIntoTwo(left, out left, out tmpl);
                left = recMergeSort(left, tmpl);
            }

            if(right.next != null)
            {
                ListNode tmpr;
                cutIntoTwo(right, out right, out tmpr);
                right = recMergeSort(right, tmpr);
            }


            ListNode head = null;
            ListNode temp = null;
            ListNode curr = null;
            while (left != null || right != null)
            {
                if (left!= null && right != null)
                {
                    if (left.val < right.val)
                    {
                        temp = left;
                        left = left.next;
                    }
                    else
                    {
                        temp = right;
                        right = right.next;
                    }
                }
                else if (left != null)
                {
                    temp = left;
                    left = left.next;
                }
                else 
                {
                    temp = right;
                    right = right.next;
                }

                temp.next = null;
                if(curr != null)
                {
                    curr.next = temp;
                }
                curr = temp;

                if(head == null)
                {
                    head = curr;
                }
            }
            return head;
        }

		//150. Evaluate Reverse Polish Notation
		//這個是學校題，用 stack 來解 Reverse Polish Notation
        //有個延伸議題是 polish notation 是如何轉出來的，它是可以免括號的哦~~
		public int EvalRPN(string[] tokens)
		{
            if(tokens.Count() == 0)
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
                if(int.TryParse(t,out temp))
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

		public int MaxProduct(int[] nums)
		{
            if(nums.Length == 0)
                return 0;
            
            long[] dpMax = new long[nums.Length];
            long[] dpMin = new long[nums.Length];
            dpMax[0] = nums[0];
            dpMin[0] = nums[0];

            long max = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                //從 我，連乘最大可能，連乘最小可能 選出新的 最大，最小，重點是，這三個值都含有我的成份，這樣 dp 才有辦法合理
                dpMax[i] = Math.Max(nums[i], Math.Max(nums[i] * dpMin[i - 1], nums[i] * dpMax[i - 1]));
                dpMin[i] = Math.Min(nums[i], Math.Min(nums[i] * dpMin[i - 1], nums[i] * dpMax[i - 1]));

                //歷史最大值
                max = Math.Max(max, dpMax[i]);
            }

            return (int)max;
		}

		//162. Find Peak Element
        //這題初看很容易，只是找個峰值，codility 也有
        //但它的假設其實不一樣，它的頭尾可以是峰值
        //而且，它假設你可以和 int.minValue 比，但!! 相等它也判定是峰值
        //而題目又強調它不會給你連續的相同值，所以程式寫等號才會對…我是覺得有點無聊
        //如果我沒有另開空間來寫，則我的判定條件就一堆的醜
        //這題真正可怕的是，它有個不強迫條件，是你要用 O(logn)作完，這就可怕了
        //我沒真的寫，但我看了一下，它基本上就是用二分法，由於數字不會連續一樣
        //所以它就先隨便踩個中間，然後如果剛好踩中就結束，如果踩在下坡就往左半逼，反正就往右半逼，反正也不平有高原區
        //只要能逼中一個就行，對於只有二個元素含以下的，就算特例解
		public int FindPeakElement(int[] nums)
		{
            if(nums.Count() == 0)
            {
                return -1;
            }

            List<int> data = new List<int>();
            data.Add(int.MinValue);
            data.AddRange(nums);
            data.Add(int.MinValue);

            for (int i = 1; i < data.Count() - 1; i++)
            {
                if(data[i] >= data[i-1] && data[i] >= data[i+1])
                {
                    return i - 1;
                }
            }
            return -1;
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
            if( numerator < 0 ^ denominator < 0)
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
            result = string.Format("{0}{1}{2}", result, qot, rem==0 ? "" : ".");
         
            //用它來背有出現過的餘數
            Dictionary<long, int> remToDenMap = new Dictionary<long, int>();

            while(!remToDenMap.ContainsKey(rem) && rem != 0)
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
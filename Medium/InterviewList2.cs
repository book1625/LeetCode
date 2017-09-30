using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Medium
{
    public partial class InterviewList
    {
        //34. Search for a Range
        public int[] SearchRange(int[] nums, int target)
        {
            int beg = 0;
            int end = nums.Length - 1;
            int tar = -1;
            while (beg <= end)
            {
                int mid = (beg + end) / 2;
                if (nums[mid] == target)
                {
                    tar = mid;
                    break;
                }
                else if (nums[mid] < target)
                {
                    beg = mid + 1;
                }
                else
                {
                    end = mid - 1;
                }
            }

            if (tar == -1)
            {
                return new int[] { -1, -1 };
            }

            beg = tar;
            end = tar;

            while (beg - 1 >= 0 && nums[beg - 1] == target)
            {
                beg--;
            }

            while (end + 1 < nums.Length && nums[end + 1] == target)
            {
                end++;
            }

            return new int[] { beg, end };
        }

        //36. Valid Sudoku
        public bool IsValidSudoku(char[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                if (!IsValidRow(board, i, true))
                {
                    return false;
                }

                if (!IsValidRow(board, i, false))
                {
                    return false;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!IsValidSub(board, i * 3, j * 3))
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        private bool IsValidRow(char[,] board, int ind, bool isRow)
        {
            HashSet<char> hs = new HashSet<char>();
            for (int i = 0; i < 9; i++)
            {
                char temp = isRow ? board[ind, i] : board[i, ind];
                if (temp != '.')
                {
                    if (hs.Contains(temp))
                    {
                        return false;
                    }
                    else
                    {
                        hs.Add(temp);
                    }
                }
            }
            return true;
        }

        private bool IsValidSub(char[,] board, int x, int y)
        {
            HashSet<char> hs = new HashSet<char>();
            for (int i = x; i < x + 3; i++)
                for (int j = y; j < y + 3; j++)
                {
                    char temp = board[i, j];
                    if (temp != '.')
                    {
                        if (hs.Contains(temp))
                        {
                            return false;
                        }
                        else
                        {
                            hs.Add(temp);
                        }
                    }
                }

            return true;
        }

        //46. Permutations
        public IList<IList<int>> Permute(int[] nums)
        {
            if (nums.Length == 0)
            {
                return new List<IList<int>>() { new List<int>() };
            }

            List<IList<int>> result = new List<IList<int>>();

            var src = new List<int>(nums);
            var curr = new List<int>();
            curr.Add(src[0]);
            permuteRec(src, 1, curr, result);
            return result;
        }

        private void permuteRec(List<int> src, int srcindex, List<int> curr, List<IList<int>> result)
        {
            if (srcindex > src.Count() - 1)
            {
                result.Add(curr);
            }
            else
            {
                Func<List<int>, List<int>> cloneList = (ls) =>
                {
                    return new List<int>(ls);
                };

                int currValue = src[srcindex];
                List<int> cl;

                for (int i = 0; i < curr.Count(); i++)
                {
                    cl = cloneList(curr);
                    cl.Insert(i, currValue);
                    permuteRec(src, srcindex + 1, cl, result);
                }

                cl = cloneList(curr);
                cl.Add(currValue);
                permuteRec(src, srcindex + 1, cl, result);
            }
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

        //54. Spiral Matrix
        //也沒什麼神奇解，就是苦工的把 spiral 自幹一次，還要小心特例
        //左右邊界由外面算給函式用，函式會比較好理解，外面控起來也比較明白
        public IList<int> SpiralOrder(int[,] matrix)
        {
            if(matrix.Length == 0)
            {
                return new List<int>();
            }

            List<int> result = new List<int>();

            int begRow = 0;
            int endRow = matrix.GetLength(0) - 1;
            int begCol = 0;
            int endCol = matrix.GetLength(1) - 1;

            while(begRow <= endRow && begCol <= endCol)
            {
                sprial(matrix, begRow, endRow, begCol, endCol,result);

                begRow++;
                endRow--;
                begCol++;
                endCol--;
            }
            return result;
        }       

        private void sprial(int[,] matrix, int begRow, int endRow, int begCol, int endCol, List<int> result)
        {
            for (int c = begCol; c <= endCol; c++)
            {
                result.Add(matrix[begRow, c]);
            }

            for (int r = begRow + 1; r <= endRow; r++)
            {
                result.Add(matrix[r, endCol]);
            }

            //下面兩個檢查非常重要，因為在收到快結束時，會造成重覆上面兩個的行為(在等號成立時)
            //而且它們各自有著自己的重覆條件，用想的不容易，要用測的才會發現
            if (begRow < endRow)
            {
                for (int c = endCol - 1; c >= begCol; c--)
                {
                    result.Add(matrix[endRow, c]);
                }
            }

            if (begCol < endCol)
            {
                for (int r = endRow - 1; r >= begRow + 1; r--)
                {
                    result.Add(matrix[r, begRow]);
                }
            }
        }

        //55. Jump Game
        public bool CanJump(int[] nums)
        {
            //這叫「盡量往左跳，多跳幾次沒關系」
            //盡可能到能跳到的最左邊，如果這樣還找到不點可以跳過來，那就沒辦法
            //會不會有一個點在最左邊，但它到不了起點，可是往右一個點卻可以?? 
            //如果最左邊以後的點都到不了，那表示這些到不了的點，它裡面的 max 不夠大到可以走到最左邊
            //這樣那有辦法走到更右的一格，所以上面的假設不存在
            int left = nums.Length - 1;
            for (int i = left - 1; i >= 0; i--)
            {
                if (nums[i] + i >= left)
                {
                    left = i;
                }
            }

            return left == 0;
        }

        public bool CanJump2(int[] nums)
        {
            /*
             * 這個演算法是在 codility 上學會的，而且還可以計算最少的jump數
             * 然而… 它的速度不被 leetcode 接受，看了解答大概知道
             * 它只管問可不可以跳到，所以，它的時間要能過，要用 greedy 來拼… Orz
            */
            if(nums.Length == 0)
            {
                return false;
            }

            HashSet<int> nextPlace = new HashSet<int>();
            nextPlace.Add(0);

            while(nextPlace.Count() > 0 )
            {
                List<int> allPlace = nextPlace.ToList();
                nextPlace.Clear();
                foreach(int p in allPlace)
                {                    
                    int step = nums[p];

                    //由於步數可以是0，所以i得從0開始檢查，也就是可以不走
                    //但… 不走就會造成無限廻圈，永遠都有下個 plce (死都不走的留在原地)
                    //所以只好把單放一個0，作為特例來處理
                    if (p == nums.Length - 1)
                    {
                        return true;
                    }

                    for (int i = 1; i <= step; i++)
                    {
                        if (p + i < nums.Length - 1)
                        {
                            nextPlace.Add(p + i);
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                    
            }

            return false;
        }
    }
}
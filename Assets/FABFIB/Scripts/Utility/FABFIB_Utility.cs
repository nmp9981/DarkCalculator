namespace FABFIB
{
    public static class FABFIB_Utility
    {
        /// <summary>
        /// 내림차순으로 입력했는가?
        /// </summary>
        /// <returns></returns>
        public static bool IsDescendingOrderInput(string input)
        {
            bool input01 = (input[0] >= input[1]);
            bool input02 = (input[1] >= input[2]);

            //내림차순
            if (input01 && input02) return true;
            return false;
        }
    }
}

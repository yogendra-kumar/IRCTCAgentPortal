public class common
{
    public static int[] FindcharIndexInString(string objString, char objChar)
    {
        int[] IntArray = new int[100];
        int index = 0;
        for (int i = objString.IndexOf(objChar); i > -1; i = objString.IndexOf(objChar, i + 1))
        {
            // for loop end when i=-1 ('a' not found)
            IntArray[index] = i;
        }
        return IntArray;
    }
}
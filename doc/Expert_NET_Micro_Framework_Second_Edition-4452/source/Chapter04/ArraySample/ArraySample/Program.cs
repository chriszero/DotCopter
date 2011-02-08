using Microsoft.SPOT.Hardware;

namespace ArraySample
{
    public class Class1
    {
        public static void Main()
        {
            //combining two byte arrays
            byte[] byteArray1 = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            byte[] byteArray2 = new byte[] { 8, 9, 10, 11, 12, 13, 14, 15 };
            byte[] byteArray3 = Utility.CombineArrays(byteArray1, byteArray2);
            byte[] byteArray4 = Utility.CombineArrays(byteArray1, //array 1
                                                      2, //start index 1
                                                      3, //number of elements in 1
                                                      byteArray2, //array 2
                                                      5, //start index 2
                                                      2); //number of elements in 2
        }
    }
}

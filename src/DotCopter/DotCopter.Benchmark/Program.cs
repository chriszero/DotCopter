namespace DotCopter.Benchmark
{
    public class Program
    {
        public static void Main()
        {
            //attempt to stabilize
            for (int i = 0; i < 5000; i++)
                i = i;

            new PropertyTest().Run(1000);
            new BitConverterTest().Run(1000);
        }

    }
}

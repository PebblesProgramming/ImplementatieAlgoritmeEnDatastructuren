using CustomAlgoritmen;

namespace Implementation
{

    class Program
    {
        static void Main(string[] args)
        {
            double result = Test.Add(1, 1);
            Console.WriteLine($"1 + 1 = {result}");

            var test = new Lijst1<int>();
            test.add(1);
            test.add(2);
            test.add(10);

           
        }

        public void test(Lijst1<int> list)
        {
            for (int i = 0; i < list.size(); i++)
            {
                Console.WriteLine(list.get(i));
            }
        }

    }
}
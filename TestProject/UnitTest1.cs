using CustomAlgoritmen;

namespace TestProject
{
    public class AddTests
    {
        [Fact]
        public void Add_TwoPositiveNumbers_ReturnCorrectSum()
        {
            double a = 1;
            double b = 2;

            double result = Test.Add(a, b);

            Assert.Equal(3, result);
        }
    }
}
namespace RuilwinkelVaals.Tests
{
    public class Sum
    {
        public int firstNumber { get; set; }
        public int secondNumber { get; set; }
        public int ExpectedResult { get; set; }
        public int givenResult { get; set; }

        public Sum(Sum givenSum)
        {
            this.firstNumber = givenSum.firstNumber;
            this.secondNumber = givenSum.secondNumber;
            this.givenResult = givenSum.secondNumber + givenSum.firstNumber;
            this.ExpectedResult = givenSum.ExpectedResult;
        }

        public Sum()
        {

        }
    }
}
using System;
using Xunit;

namespace RuilwinkelVaals.Tests
{
    public class UnitTest1
    {
        public Sum sum;
        public int givenNumber = 2;
        public int givenNumber2 = 3;
        public int expectedResult = 5;


        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
        [Fact]
        public void SumObjectTest()
        {
            //creating a local instance of an object
            Sum content = new Sum();
            //assigning it values
            content.secondNumber = givenNumber2;
            content.firstNumber = givenNumber;
            content.givenResult = content.firstNumber + content.secondNumber;
            //checking if object has the right values
            Assert.Equal(expectedResult, content.givenResult);
            Assert.Equal(givenNumber, content.firstNumber);
            Assert.Equal(givenNumber2, content.secondNumber);
        }

        [Fact]
        public void SumConstructorTestTrue()
        {
            //creating an instance of an object
            Sum input = new Sum();
            //assigning it values
            input.secondNumber = givenNumber2;
            input.firstNumber = givenNumber;
            input.givenResult = input.firstNumber + input.secondNumber;
            input.ExpectedResult = expectedResult;
            //using that object to generate a new object with the second constructor
            Sum content = new Sum(input);
            //comparing those two objects to eachother
            Assert.NotEqual(input, content); //this one should return false as the names are different but it should show the values are the same
            Assert.Equal(input.givenResult, content.givenResult);
            Assert.Equal(input.firstNumber, content.firstNumber);
            Assert.Equal(input.secondNumber, content.secondNumber);
            //comparing them to the begin values
            Assert.Equal(expectedResult, content.givenResult);
            Assert.Equal(givenNumber, content.firstNumber);
            Assert.Equal(givenNumber2, content.secondNumber);
        }
        [Theory]
        [InlineData(3, 2, 5)]
         public void SumConstructorTestTheory(int x, int y, int z)
        {
            //creating an instance of an object
            Sum input = new Sum();
            //assigning it values
            input.secondNumber = x;
            input.firstNumber = y;
            input.givenResult = input.firstNumber + input.secondNumber;
            input.ExpectedResult = z;
            //using that object to generate a new object with the second constructor
            Sum content = new Sum(input);
            //comparing those two objects to eachother
            Assert.NotEqual(input, content); //this one should return false as the names are different but it should show the values are the same
            Assert.Equal(input.givenResult, content.givenResult);
            Assert.Equal(input.firstNumber, content.firstNumber);
            Assert.Equal(input.secondNumber, content.secondNumber);
            //comparing them to the begin values
            Assert.Equal(expectedResult, content.givenResult);
            Assert.Equal(givenNumber, content.firstNumber);
            Assert.Equal(givenNumber2, content.secondNumber);
        }
    }
}

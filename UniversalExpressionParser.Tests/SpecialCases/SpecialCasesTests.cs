using NUnit.Framework;

namespace UniversalExpressionParser.Tests.SpecialCases
{
    // TODO: Make sure all these cases are considered either by simulation in UniversalExpressionParser.Tests.SuccessfulParseTests.ExpressionParserSuccessfulTests
    // or by error tests in ExpressionParseErrorTests.UniversalExpressionParser.Tests.ExpressionParseErrorTests
    [TestFixture]
    internal class SpecialCasesTests : ParseExpressionFromFileBase
    {
        protected override string BaseFolder => "SpecialCases";

        #region SpecialTexts

        // TODO: Add this case to ExpressionParseErrorTests
        [Test]
        public void SpecialTexts_ParseTexts1()
        {
            ParseExpressionFromFile("SpecialTexts", "ParseTexts1", 2);
        }

        // TODO: Add this case to ExpressionParseErrorTests
        [Test]
        public void SpecialTexts_ParseTexts2()
        {
            ParseExpressionFromFile("SpecialTexts", "ParseTexts2", 3);
        }

        #endregion

        #region SpecialNumbers

        // Included in ExpressionParserSuccessfulTests
        [Test]
        public void SpecialNumbers_ExpWithPointInRightSide()
        {
            ParseExpressionFromFile("SpecialNumbers", "ExpWithPointInRightSide", 0);
        }

        // Included in ExpressionParserSuccessfulTests
        [Test]
        public void SpecialNumbers_ExpWithFollowingSpace()
        {
            ParseExpressionFromFile("SpecialNumbers", "ExpWithFollowingSpace", 0);
        }

        // Included in ExpressionParserSuccessfulTests
        [Test]
        public void SpecialNumbers_ExpWithPointInOperators()
        {
            ParseExpressionFromFile("SpecialNumbers", "ExpWithPointInOperators", 0);
        }
        #endregion


        [Test]
        public void TempDiagnostics()
        {
            ParseExpressionFromFile(null, "TempDiagnostics");
        }
    }
}
using JetBrains.Annotations;
using System.Collections.Generic;

namespace UniversalExpressionParser.Tests.OperatorTemplates
{
    public static class OperatorTemplatesCollection
    {
        private delegate OperatorTemplateBase CreateTemplateDelegate();

        //#pragma warning disable 414
        //        // Temporarily set this to one of the template generating functions, to debug the specific template only.
        //        // Example:  TemplateToTest = CreateTemplate9;
        //        // After debugging, set the value to null, to test all the templates.
        //        private static CreateTemplateDelegate TemplateToTest = null; // CreateTemplate9;
        //#pragma warning restore 414

        static OperatorTemplatesCollection()
        {
            //if (TemplateToTest != null)
            //    OperatorTemplates = new List<OperatorTemplateBase>()
            //    {
            //        TemplateToTest()
            //    };
            //else
            OperatorTemplates = new List<OperatorTemplateBase>
                {
                    CreateTemplate0(), CreateTemplate1(), CreateTemplate2(),
                    CreateTemplate3(), CreateTemplate4(), CreateTemplate5(),
                    CreateTemplate6(), CreateTemplate7(), CreateTemplate8(),
                    CreateTemplate9(), CreateTemplate10(), CreateTemplate11(),
                    CreateTemplate12()
                };
        }

        [NotNull, ItemNotNull]
        public static IReadOnlyList<OperatorTemplateBase> OperatorTemplates { get; }

        /// <summary>
        /// "x1 bin9 x2 bin1 x3" expression that should be parsed to bin9(x1, bi1(x2, x3))
        /// </summary>
        private static OperatorTemplateBase CreateTemplate0()
        {
            return BinaryOperatorTemplate.CreateWithRightOperatorTemplate(OperatorPriority.Priority9,
                BinaryOperatorTemplate.CreateWithNoChildTemplates(OperatorPriority.Priority1));

        }

        /// <summary>
        /// "x1 bin5 pref1 pref8 x2" expression that should be parsed to: bin5 (x1, pref1(pref8(x2)))
        /// </summary>
        private static OperatorTemplateBase CreateTemplate1()
        {
            return BinaryOperatorTemplate.CreateWithRightOperatorTemplate(OperatorPriority.Priority5,
                new PrefixOperatorTemplate(OperatorPriority.Priority1,
                    new PrefixOperatorTemplate(OperatorPriority.Priority8)));

        }

        /// <summary>
        /// "x1 bin5 pref1 pref8 x2 bin0 x3 post1 post9" expression that should be parsed to: 
        /// post9(post1(bin5 (x1, pref1(pref8(bin0(x2, x3))))))
        /// </summary>
        private static OperatorTemplateBase CreateTemplate2()
        {
            return new PostfixOperatorTemplate(OperatorPriority.Priority9,
                new PostfixOperatorTemplate(OperatorPriority.Priority1,
                    BinaryOperatorTemplate.CreateWithRightOperatorTemplate(OperatorPriority.Priority5,
                        new PrefixOperatorTemplate(OperatorPriority.Priority1,
                            new PrefixOperatorTemplate(OperatorPriority.Priority8,
                                BinaryOperatorTemplate.CreateWithNoChildTemplates(OperatorPriority.Priority0))))));
        }

        /// <summary>
        /// "x1 bin5 pref2 pref8 pref9 x2 post9 post1" expression that should be parsed
        /// to: bin5 (x1, pref2(pref8(pref9(post1(post9(x2))))))
        /// </summary>
        private static OperatorTemplateBase CreateTemplate3()
        {
            return BinaryOperatorTemplate.CreateWithRightOperatorTemplate(OperatorPriority.Priority5,
                new PrefixOperatorTemplate(OperatorPriority.Priority2,
                    new PrefixOperatorTemplate(OperatorPriority.Priority8,
                        new PrefixOperatorTemplate(OperatorPriority.Priority9,
                            new PostfixOperatorTemplate(OperatorPriority.Priority1,
                                new PostfixOperatorTemplate(OperatorPriority.Priority9))))));
        }

        /// <summary>
        /// "x1 bin5 pref1 pref8 pref9 x2 post8 post2" expression that should be parsed
        /// to: bin5 (x1, post2(post8(pref1(pref8(pref9(x2))))))
        /// </summary>
        private static OperatorTemplateBase CreateTemplate4()
        {
            return BinaryOperatorTemplate.CreateWithRightOperatorTemplate(OperatorPriority.Priority5,
                new PostfixOperatorTemplate(OperatorPriority.Priority2,
                    new PostfixOperatorTemplate(OperatorPriority.Priority8,
                        new PrefixOperatorTemplate(OperatorPriority.Priority1,
                            new PrefixOperatorTemplate(OperatorPriority.Priority8,
                                new PrefixOperatorTemplate(OperatorPriority.Priority9))))));
        }

        /// <summary>
        /// "pref9 pref0 x1 bin2 x2 bin4 x3 bin1 x4 post0 post8" expression that should be parsed
        ///     to: pref9(pref0(post8(post0, bin4(bin1(x3, x4), bin2(x1, x2))))))
        /// </summary>
        private static OperatorTemplateBase CreateTemplate5()
        {
            return new PrefixOperatorTemplate(OperatorPriority.Priority9,
                new PrefixOperatorTemplate(OperatorPriority.Priority0,
                    new PostfixOperatorTemplate(OperatorPriority.Priority8,
                        new PostfixOperatorTemplate(OperatorPriority.Priority0,
                            BinaryOperatorTemplate.CreateWithTwoChildTemplates(OperatorPriority.Priority4,
                                BinaryOperatorTemplate.CreateWithNoChildTemplates(OperatorPriority.Priority1),
                                BinaryOperatorTemplate.CreateWithNoChildTemplates(OperatorPriority.Priority2)
                                    )))));
        }

        /// <summary>
        /// "x1 bin1 x2" expression that should be parsed to bin1(x1,x2)
        /// </summary>
        private static OperatorTemplateBase CreateTemplate6()
        {
            return BinaryOperatorTemplate.CreateWithNoChildTemplates(OperatorPriority.Priority1);
        }

        /// <summary>
        /// "perf0 x1 bin1 x2" expression that should be parsed to bin1(x1,x2)
        /// </summary>
        private static OperatorTemplateBase CreateTemplate7()
        {
            return BinaryOperatorTemplate.CreateWithLeftOperatorTemplate(OperatorPriority.Priority1,
                new PrefixOperatorTemplate(OperatorPriority.Priority0));
        }

        /// <summary>
        /// "x1 bin1 x2 post0" expression that should be parsed to bin1(x1,x2)
        /// </summary>
        private static OperatorTemplateBase CreateTemplate8()
        {
            return BinaryOperatorTemplate.CreateWithRightOperatorTemplate(OperatorPriority.Priority1,
                new PostfixOperatorTemplate(OperatorPriority.Priority0));
        }

        /// <summary>
        /// "pref1 pref1 pref1 x1 post1 post1 post1 bin1 pref1 pref1 pref1 y1 post1 post1 post1" expression that should be parsed
        /// post1(post1(post1(bin1(post1(post1(post1(pref1(pref1(pref1(x1)))))), pref1(pref1(pref1(y1)))))))
        /// </summary>
        private static OperatorTemplateBase CreateTemplate9()
        {
            // pref1 pref1 pref1 x1
            var prefixes =
                new PrefixOperatorTemplate(OperatorPriority.Priority1,
                    new PrefixOperatorTemplate(OperatorPriority.Priority1,
                        new PrefixOperatorTemplate(OperatorPriority.Priority1)));

            // post1 post1 post1(pref1 pref1 pref1 x1)
            var postfixes =
                new PostfixOperatorTemplate(OperatorPriority.Priority1,
                    new PostfixOperatorTemplate(OperatorPriority.Priority1,
                        new PostfixOperatorTemplate(OperatorPriority.Priority1, prefixes)));

            // pref1 pref1 pref1 y1
            prefixes =
                new PrefixOperatorTemplate(OperatorPriority.Priority1,
                    new PrefixOperatorTemplate(OperatorPriority.Priority1,
                        new PrefixOperatorTemplate(OperatorPriority.Priority1)));

            // bin1(pref1 pref1 pref1 x1 post1 post1 post1, pref1 pref1 pref1 y1)
            var binary = BinaryOperatorTemplate.CreateWithTwoChildTemplates(OperatorPriority.Priority1, postfixes, prefixes);

            return new PostfixOperatorTemplate(OperatorPriority.Priority1,
                    new PostfixOperatorTemplate(OperatorPriority.Priority1,
                        new PostfixOperatorTemplate(OperatorPriority.Priority1, binary)));
        }

        /// <summary>
        /// "pref2 pref0 x1 post0 post3 bin4 pref2 pref0 x2 post0 post1" should be parsed to 
        ///  bin4(post3(post0(pref2(pref0(x1)))), pref2(pref0(post1(post0(x2)))))
        /// </summary>
        private static OperatorTemplateBase CreateTemplate10()
        {
            return BinaryOperatorTemplate.CreateWithTwoChildTemplates(OperatorPriority.Priority4,
                new PostfixOperatorTemplate(OperatorPriority.Priority3,
                    new PostfixOperatorTemplate(OperatorPriority.Priority0,
                        new PrefixOperatorTemplate(OperatorPriority.Priority2,
                            new PrefixOperatorTemplate(OperatorPriority.Priority0)))),

                new PrefixOperatorTemplate(OperatorPriority.Priority2,
                    new PrefixOperatorTemplate(OperatorPriority.Priority0,
                        new PostfixOperatorTemplate(OperatorPriority.Priority1,
                            new PostfixOperatorTemplate(OperatorPriority.Priority0)))));
        }

        /// <summary>
        /// "pref2 pref0 x1 post0 post5 bin4 pref6 pref0 x2 post0 post1" should be parsed to
        /// bin4(post5(post0(pref2(pref0(x1)))), pref6(pref0(post1(post0(x2)))))
        /// </summary>
        private static OperatorTemplateBase CreateTemplate11()
        {
            return BinaryOperatorTemplate.CreateWithTwoChildTemplates(OperatorPriority.Priority4,
                new PostfixOperatorTemplate(OperatorPriority.Priority5,
                    new PostfixOperatorTemplate(OperatorPriority.Priority0,
                        new PrefixOperatorTemplate(OperatorPriority.Priority2,
                            new PrefixOperatorTemplate(OperatorPriority.Priority0)))),
                new PrefixOperatorTemplate(OperatorPriority.Priority6,
                    new PrefixOperatorTemplate(OperatorPriority.Priority0,
                        new PostfixOperatorTemplate(OperatorPriority.Priority1,
                            new PostfixOperatorTemplate(OperatorPriority.Priority0)))));
        }

        /// <summary>
        /// "pref8 pref0 x1 post0 post5 bin4 pref6 pref0 x2 post0 post7" should be parsed to
        /// pref8(pref0(post7(post0(bin4(post5(post0(x1)), pref6(pref0(x2)))))))
        /// </summary>
        private static OperatorTemplateBase CreateTemplate12()
        {
            return new PrefixOperatorTemplate(OperatorPriority.Priority8,
                new PrefixOperatorTemplate(OperatorPriority.Priority0,
                    new PostfixOperatorTemplate(OperatorPriority.Priority7,
                        new PostfixOperatorTemplate(OperatorPriority.Priority0,
                            BinaryOperatorTemplate.CreateWithTwoChildTemplates(OperatorPriority.Priority4,
                                new PostfixOperatorTemplate(OperatorPriority.Priority5,
                                    new PostfixOperatorTemplate(OperatorPriority.Priority0)),

                            new PrefixOperatorTemplate(OperatorPriority.Priority6,
                                new PrefixOperatorTemplate(OperatorPriority.Priority0)))))));
        }
    }
}

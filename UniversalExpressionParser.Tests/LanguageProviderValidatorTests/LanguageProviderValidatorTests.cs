using JetBrains.Annotations;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OROptimizer.Diagnostics.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestsSharedLibrary;
using TestsSharedLibrary.Diagnostics.Log;
using TextParser;
using UniversalExpressionParser.Tests.TestLanguage;

namespace UniversalExpressionParser.Tests.LanguageProviderValidatorTests
{
    [TestFixture]
    public class LanguageProviderValidatorTests : TestsBase
    {
        private static bool LogToLog4NetFile = true;

        //[NotNull]
        //private readonly DefaultExpressionLanguageProviderValidator _defaultExpressionLanguageProviderValidator = new DefaultExpressionLanguageProviderValidator();

        static LanguageProviderValidatorTests()
        {
            LogHelper.RemoveContext();

            if (LogToLog4NetFile)
                TestHelpers.RegisterLogger();
            else
                LogHelper.RegisterContext(new LogHelper4TestsContext());
        }

        [SetUp]
        public void SetUp()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOnOperatorsAndKeywords();
        }


        [Test]
        public void TestSuccessfulValidation()
        {
            new DefaultExpressionLanguageProviderValidator()
                .Validate(new TestLanguageProviderForLanguageProviderValidationTests());
        }


        [Test]
        public void CommentMarkers_AllNullOrAllNotNull()
        {
            VerifyExpressionLanguageValidation(
            (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.LineCommentMarker = null;
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.MultilineCommentStartMarker = null;
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.MultilineCommentEndMarker = null;
                });
        }

        [Test]
        public void CommentMarkers_Whitespaces()
        {
            ProcessSpaceCharacters(spaceCharacterString =>
            {
                // LineCommentMarker
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.LineCommentMarker = $"line{spaceCharacterString}Comment";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.LineCommentMarker = "lineComment";
                    });

                // MultilineCommentStartMarker
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.MultilineCommentStartMarker = $"comment{spaceCharacterString}Start";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.MultilineCommentStartMarker = "commentStart";
                    });

                // MultilineCommentEndMarker
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.MultilineCommentEndMarker = $"comment{spaceCharacterString}Start";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.MultilineCommentEndMarker = "commentStart";
                    });

            });
        }

        [Test]
        public void CommentMarkers_ValidateUnique()
        {
            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.LineCommentMarker = "commentStart";
                    expressionLanguageProvider.MultilineCommentStartMarker = "commentStart";
                },
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.LineCommentMarker = "lineComment";
                    expressionLanguageProvider.MultilineCommentStartMarker = "commentStart";
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.LineCommentMarker = "commentEnd";
                    expressionLanguageProvider.MultilineCommentEndMarker = "commentEnd";
                },
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.LineCommentMarker = "lineComment";
                    expressionLanguageProvider.MultilineCommentEndMarker = "commentEnd";
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.MultilineCommentStartMarker = "commentEnd";
                    expressionLanguageProvider.MultilineCommentEndMarker = "commentEnd";
                },
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.MultilineCommentStartMarker = "commentStart";
                    expressionLanguageProvider.MultilineCommentEndMarker = "commentEnd";
                });
        }

        [Test]
        public void ValidateCommentMarkers_NoSpecialNonOperatorCharacters()
        {
            foreach (var specialCharacter in SpecialCharactersCacheThreadStaticContext.Context.SpecialNonOperatorCharacters)
            {
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.LineCommentMarker = $"lineComme{specialCharacter}nt";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.LineCommentMarker = "lineComme#nt";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.MultilineCommentStartMarker = $"commentS{specialCharacter}tart";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.MultilineCommentStartMarker = "commentS#tart";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.MultilineCommentEndMarker = $"commentE{specialCharacter}nd";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.MultilineCommentEndMarker = "commentE#nd";
                    });
            }
        }

        [Test]
        public void ValidateSuccessForSeparatorPresentAndCodeBlockMarkersNotPresent()
        {
            var testLanguageProviderForLanguageProvider = new TestLanguageProviderForLanguageProviderValidationTests();
            testLanguageProviderForLanguageProvider.ExpressionSeparatorCharacter = ';';
            testLanguageProviderForLanguageProvider.CodeBlockStartMarker = null;
            testLanguageProviderForLanguageProvider.CodeBlockEndMarker = null;

            new DefaultExpressionLanguageProviderValidator().Validate(testLanguageProviderForLanguageProvider);
        }

        [Test]
        public void CodeSeparatorMarkers_SomeMarkersMissing()
        {
            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.CodeBlockStartMarker = "{";
                },
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ExpressionSeparatorCharacter = '\0';
                    expressionLanguageProvider.CodeBlockStartMarker = null;
                    expressionLanguageProvider.CodeBlockEndMarker = null;
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.CodeBlockEndMarker = "}";
                },
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ExpressionSeparatorCharacter = '\0';
                    expressionLanguageProvider.CodeBlockStartMarker = null;
                    expressionLanguageProvider.CodeBlockEndMarker = null;
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.CodeBlockStartMarker = null;
                },
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                    expressionLanguageProvider.CodeBlockStartMarker = "{";
                    expressionLanguageProvider.CodeBlockEndMarker = "}";
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.CodeBlockStartMarker = "";
                },
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                    expressionLanguageProvider.CodeBlockStartMarker = "{";
                    expressionLanguageProvider.CodeBlockEndMarker = "}";
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.CodeBlockEndMarker = null;
                },
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                    expressionLanguageProvider.CodeBlockStartMarker = "{";
                    expressionLanguageProvider.CodeBlockEndMarker = "}";
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.CodeBlockEndMarker = "";
                },
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                    expressionLanguageProvider.CodeBlockStartMarker = "{";
                    expressionLanguageProvider.CodeBlockEndMarker = "}";
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ExpressionSeparatorCharacter = '\0';
                },
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                    expressionLanguageProvider.CodeBlockStartMarker = "{";
                    expressionLanguageProvider.CodeBlockEndMarker = "}";
                });
        }

        [Test]
        public void CodeSeparatorMarkers_SpecialNonOperatorCharacters()
        {
            foreach (var specialCharacter in SpecialCharactersCacheThreadStaticContext.Context.SpecialNonOperatorCharacters)
            {
                if (specialCharacter != ';')
                    VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = specialCharacter;
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                        expressionLanguageProvider.CodeBlockStartMarker = "{";
                        expressionLanguageProvider.CodeBlockEndMarker = "}";
                    });

                if (specialCharacter != '{')
                    VerifyExpressionLanguageValidation(
                        (expressionLanguageProvider) =>
                        {
                            expressionLanguageProvider.CodeBlockStartMarker = $"Beg{specialCharacter}in";
                        },
                        (expressionLanguageProvider) =>
                        {
                            expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                            expressionLanguageProvider.CodeBlockStartMarker = "{{Beg#in";
                            expressionLanguageProvider.CodeBlockEndMarker = "}";
                        });

                if (specialCharacter != '}')
                    VerifyExpressionLanguageValidation(
                        (expressionLanguageProvider) =>
                        {
                            expressionLanguageProvider.CodeBlockEndMarker = $@"e}}n{specialCharacter}d";
                        },
                        (expressionLanguageProvider) =>
                        {
                            expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                            expressionLanguageProvider.CodeBlockStartMarker = "{";
                            expressionLanguageProvider.CodeBlockEndMarker = "e}n#d";
                        });
            }
        }



        [Test]
        public void CodeSeparatorMarkers_SeparatorCharactersCanUseOnlyLimitedSetOfCharacters()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            var separatorCharacters = GetValidCodeSeparatorCharacters();

            foreach (var separatorCharacter in separatorCharacters)
            {
                VerifyExpressionLanguageValidation(null,
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = separatorCharacter;
                    });
            }

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ExpressionSeparatorCharacter = '\'';
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ExpressionSeparatorCharacter = '\"';
                });

            for (int isUpperCaseLetter = 0; isUpperCaseLetter < 2; ++isUpperCaseLetter)
            {
                var aChar = isUpperCaseLetter == 1 ? 'A' : 'a';
                for (var charInd = 0; charInd < 26; ++charInd)
                {
                    VerifyExpressionLanguageValidation(
                        (expressionLanguageProvider) =>
                        {
                            expressionLanguageProvider.ExpressionSeparatorCharacter = (char)(aChar + charInd);
                        });
                }
            }

            for (var charInd = 0; charInd <= 9; ++charInd)
            {
                var character = charInd.ToString()[0];

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = character;
                    });
            }
        }

        [Test]
        public void CodeSeparatorMarkers_WhiteSpaces()
        {
            ProcessSpaceCharacters(space =>
            {
                foreach (var spaceChar in space)
                    VerifyExpressionLanguageValidation(
                        (expressionLanguageProvider) =>
                        {
                            expressionLanguageProvider.ExpressionSeparatorCharacter = spaceChar;
                        },
                        (expressionLanguageProvider) =>
                        {
                            expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                            expressionLanguageProvider.CodeBlockStartMarker = "{";
                            expressionLanguageProvider.CodeBlockEndMarker = "}";
                        });


                #region Space in code block start marker
                VerifyExpressionLanguageValidation(
                            (expressionLanguageProvider) =>
                            {
                                expressionLanguageProvider.CodeBlockStartMarker = space;
                            },
                            (expressionLanguageProvider) =>
                            {
                                expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                                expressionLanguageProvider.CodeBlockStartMarker = "{";
                                expressionLanguageProvider.CodeBlockEndMarker = "}";
                            });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockStartMarker = $"Be{space}gin";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockStartMarker = $"{space}Begin";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockStartMarker = $"Begin{space}";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });
                #endregion

                #region Space in code block end marker
                VerifyExpressionLanguageValidation(
                            (expressionLanguageProvider) =>
                            {
                                expressionLanguageProvider.CodeBlockEndMarker = space;
                            },
                            (expressionLanguageProvider) =>
                            {
                                expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                                expressionLanguageProvider.CodeBlockStartMarker = "{";
                                expressionLanguageProvider.CodeBlockEndMarker = "}";
                            });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockStartMarker = $"En{space}d";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockStartMarker = $"{space}End";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockStartMarker = $"End{space}";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = ';';
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });
                #endregion

            });
        }

        [Test]
        public void CodeSeparatorMarkers_CodeBlockMarkersCannotContainCommentMarkers()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            // Code block markers cannot be equal to code block markers, and cannot contain comment markers
            // However, comment markers can contain code block markers.

            // Test code separator marker cannot be the same as code comment marker
            var codeSeparatorCharacters = GetValidCodeSeparatorCharacters();

            foreach (var codeSeparatorCharacter in codeSeparatorCharacters)
            {
                VerifyExpressionLanguageValidation(
                   (expressionLanguageProvider) =>
                   {
                       expressionLanguageProvider.ExpressionSeparatorCharacter = codeSeparatorCharacter;
                       expressionLanguageProvider.LineCommentMarker = codeSeparatorCharacter.ToString();
                   },
                   (expressionLanguageProvider) =>
                   {
                       expressionLanguageProvider.ExpressionSeparatorCharacter = codeSeparatorCharacter;
                       expressionLanguageProvider.LineCommentMarker = codeSeparatorCharacter == '/' ? "+" : "/";
                       expressionLanguageProvider.MultilineCommentStartMarker = "remstart";
                       expressionLanguageProvider.MultilineCommentEndMarker = "remend";
                   });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = codeSeparatorCharacter;
                        expressionLanguageProvider.MultilineCommentStartMarker = codeSeparatorCharacter.ToString();
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = codeSeparatorCharacter;
                        expressionLanguageProvider.LineCommentMarker = "rem";
                        expressionLanguageProvider.MultilineCommentStartMarker = codeSeparatorCharacter == '/' ? "+" : "/";
                        expressionLanguageProvider.MultilineCommentEndMarker = "remend";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = codeSeparatorCharacter;
                        expressionLanguageProvider.MultilineCommentEndMarker = codeSeparatorCharacter.ToString();
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = codeSeparatorCharacter;
                        expressionLanguageProvider.LineCommentMarker = "rem";
                        expressionLanguageProvider.MultilineCommentStartMarker = "remstart";
                        expressionLanguageProvider.MultilineCommentEndMarker = codeSeparatorCharacter == '/' ? "+" : "/";
                    });
            }

            // Test code separator markers can be in comment markers
            #region LineCommentMarker
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                    null,
                    (testLanguageProvider, conflictingIdentifierInfo) =>
                    {
                        testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier1;
                        testLanguageProvider.LineCommentMarker = conflictingIdentifierInfo.Identifier2;
                    }, new[] { '{' }, null,
                    conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.LineCommentMarker = conflictingIdentifierInfo.Identifier2;
                }, new[] { '}' }, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);
            #endregion

            #region MultilineCommentStartMarker
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                    null,
                    (testLanguageProvider, conflictingIdentifierInfo) =>
                    {
                        testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier1;
                        testLanguageProvider.MultilineCommentStartMarker = conflictingIdentifierInfo.Identifier2;
                    }, new[] { '{' }, null,
                    conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.MultilineCommentStartMarker = conflictingIdentifierInfo.Identifier2;
                }, new[] { '}' }, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);
            #endregion

            #region MultilineCommentEndMarker
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.MultilineCommentEndMarker = conflictingIdentifierInfo.Identifier2;
                }, new[] { '{' }, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.MultilineCommentEndMarker = conflictingIdentifierInfo.Identifier2;
                }, new[] { '}' }, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);
            #endregion


            // Test comment markers cannot be in code separator markers
            #region LineCommentMarker
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                    (testLanguageProvider, conflictingIdentifierInfo) =>
                    {
                        testLanguageProvider.LineCommentMarker = conflictingIdentifierInfo.Identifier1;
                        testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier2;
                    }, null, null, new[] { '{' },
                    conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.LineCommentMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null, new[] { '{' },
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);
            #endregion

            #region MultilineCommentStartMarker
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.MultilineCommentStartMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null, new[] { '{' },
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.MultilineCommentStartMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null, new[] { '{' },
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);
            #endregion

            #region MultilineCommentEndMarker
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.MultilineCommentEndMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null, new[] { '{' },
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.MultilineCommentEndMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null, new[] { '{' },
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);
            #endregion
        }

        private List<char> GetValidCodeSeparatorCharacters()
        {
            return new List<char>(SpecialCharactersCacheThreadStaticContext.Context.SpecialOperatorCharacters) { ';' };
        }

        [Test]
        public void CodeSeparatorMarkers_ConflictsWithOtherCodeMarkers()
        {
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier2;
                },
                new[] { '{' }, new[] { '}' },
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType != ConflictingIdentifierType.NoConflict);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier2;
                },
                null,
                new[] { '{' }, new[] { '}' },
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier2;
                },
                new[] { '}' }, new[] { '{' },
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType != ConflictingIdentifierType.NoConflict);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier1;
                    testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier2;
                }, null,
                new[] { '}' }, new[] { '{' },
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);
        }

        [Test]
        public void CodeSeparatorMarkers_ConflictsWithOtherSeparatorCharacter()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            var codeSeparatorCharacters = new List<char>(SpecialCharactersCacheThreadStaticContext.Context.SpecialOperatorCharacters) { ';' };

            foreach (var separatorChar in codeSeparatorCharacters)
            {
                // Code block start marker
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockStartMarker = separatorChar.ToString();
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = separatorChar;
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockStartMarker = $"Be{separatorChar}in";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = separatorChar;
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockStartMarker = $"{separatorChar}Begin";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = separatorChar;
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockStartMarker = $"Begin{separatorChar}";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = separatorChar;
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });

                // Code block end marker
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockEndMarker = separatorChar.ToString();
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = separatorChar;
                        expressionLanguageProvider.CodeBlockEndMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockEndMarker = $"En{separatorChar}";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = separatorChar;
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockEndMarker = $"{separatorChar}End";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = separatorChar;
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.CodeBlockEndMarker = $"End{separatorChar}";
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.ExpressionSeparatorCharacter = separatorChar;
                        expressionLanguageProvider.CodeBlockStartMarker = "Begin";
                        expressionLanguageProvider.CodeBlockEndMarker = "End";
                    });
            }
        }

        [Test]
        public void Keywords_NullOrEmptyOrContainsWhitespaces()
        {
            VerifyExpressionLanguageValidation(null,
                (testLanguageProvider) =>
                {
                    SetupKeywordForTest(testLanguageProvider, "where");
                });

            VerifyExpressionLanguageValidation(
                (testLanguageProvider) =>
                {
                    SetupKeywordForTest(testLanguageProvider, null);
                });

            VerifyExpressionLanguageValidation(
                (testLanguageProvider) =>
                {
                    SetupKeywordForTest(testLanguageProvider, "");
                });

            ProcessSpaceCharacters(space =>
            {
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Keywords.Clear();
                        expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests($"{space}where"));
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Keywords.Clear();
                        expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests($"whe{space}re"));
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Keywords.Clear();
                        expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests($"where{space}"));
                    });
            });
        }

        [Test]
        public void Keywords_SpecialNonOperatorCharacters()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            foreach (var specialOperatorCharacter in SpecialCharactersCacheThreadStaticContext.Context.SpecialOperatorCharacters)
            {
                VerifyExpressionLanguageValidation(null,
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests(specialOperatorCharacter.ToString()));
                        expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests($"{specialOperatorCharacter}keyword"));
                        expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests($"keyword{specialOperatorCharacter}"));
                        expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests($"key{specialOperatorCharacter}word"));
                    });
            }

            foreach (var specialNonOperatorCharacter in SpecialCharactersCacheThreadStaticContext.Context.SpecialNonOperatorCharacters)
            {
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests(specialNonOperatorCharacter.ToString()));
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests($"{specialNonOperatorCharacter}keyword"));
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests($"keyword{specialNonOperatorCharacter}"));
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests($"key{specialNonOperatorCharacter}word"));
                    });
            }
        }

        [Test]
        public void Keywords_UniqueKeywordIds()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            VerifyExpressionLanguageValidation(null,
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests(1, "where"));
                    expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests(2, "pragma"));
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests(1, "where"));
                    expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests(1, "pragma"));
                });
        }

        [Test]
        public void Keywords_UniqueKeywordNames()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            VerifyExpressionLanguageValidation(null,
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests(1, "where"));
                    expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests(2, "pragma"));
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests(1, "where"));
                    expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests(2, "where"));
                });
        }

        [Test]
        public void Keywords_ConflictsWithCommentMarkers()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            // Keywords can be in comment markers
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);
                    testLanguageProvider.LineCommentMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);
                    testLanguageProvider.MultilineCommentStartMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);
                    testLanguageProvider.MultilineCommentEndMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);

            // Comment markers cannot be in keywords
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.LineCommentMarker = conflictingIdentifierInfo.Identifier1;
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);
                }, null, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.MultilineCommentStartMarker = conflictingIdentifierInfo.Identifier1;
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);

                }, null, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.MultilineCommentEndMarker = conflictingIdentifierInfo.Identifier1;
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);

                }, null, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);
        }

        [Test]
        public void Keywords_ConflictsWithCodeBlockMarkers()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            // Operator name part cannot be the same as code separator 
            foreach (var codeSeparatorCharacter in GetValidCodeSeparatorCharacters())
            {
                VerifyExpressionLanguageValidation(
                    (testLanguageProvider) =>
                    {
                        testLanguageProvider.ExpressionSeparatorCharacter = codeSeparatorCharacter;
                        SetupKeywordForTest(testLanguageProvider, codeSeparatorCharacter.ToString());
                    },
                    (testLanguageProvider) =>
                    {
                        var operatorCharacter = codeSeparatorCharacter == ';' ? '+' : codeSeparatorCharacter;
                        SetupKeywordForTest(testLanguageProvider, operatorCharacter.ToString());
                    });
            }

            // Operator parts can be in code block markers
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);

                    testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);

                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);


            // Code block markers cannot be in operator parts
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier1;
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);

                }, null, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier1;
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);

                }, null, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);
        }

        [Test]
        public void Operators_NoNullsAndWhitespaces()
        {
            VerifyExpressionLanguageValidation(null,
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.Operators.Clear();
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.PostfixUnaryOperator));
                });

            VerifyExpressionLanguageValidation(
                (testLanguageProvider) =>
                {
                    SetupOperatorForTest(testLanguageProvider, null);
                });

            VerifyExpressionLanguageValidation(
                (testLanguageProvider) =>
                {
                    SetupOperatorForTest(testLanguageProvider, "");
                });

            VerifyExpressionLanguageValidation(
                (testLanguageProvider) =>
                {
                    testLanguageProvider.Operators.Clear();
                    testLanguageProvider.Operators.Add(new OperatorInfoForTest(null, OperatorType.PostfixUnaryOperator));
                });

            VerifyExpressionLanguageValidation(
                (testLanguageProvider) =>
                {
                    testLanguageProvider.Operators.Clear();
                    testLanguageProvider.Operators.Add(new OperatorInfoForTest(new string[] { }, OperatorType.PostfixUnaryOperator));
                });

            ProcessSpaceCharacters(space =>
            {
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Operators.Clear();
                        expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", $"{space}NOT" }, OperatorType.PostfixUnaryOperator));
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Operators.Clear();
                        expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.PostfixUnaryOperator));
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Operators.Clear();
                        expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", $"N{space}OT" }, OperatorType.PostfixUnaryOperator));
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Operators.Clear();
                        expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.PostfixUnaryOperator));
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Operators.Clear();
                        expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", $"NOT{space}" }, OperatorType.PostfixUnaryOperator));
                    },
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Operators.Clear();
                        expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.PostfixUnaryOperator));
                    });
            });

        }

        [Test]
        public void Operators_UniquePerOperatorType()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            VerifyExpressionLanguageValidation(null,
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.PostfixUnaryOperator));
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.PrefixUnaryOperator));
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.BinaryOperator));
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.PostfixUnaryOperator));
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.PostfixUnaryOperator));
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.PrefixUnaryOperator));
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.PrefixUnaryOperator));
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.BinaryOperator));
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "IS", "NOT" }, OperatorType.BinaryOperator));
                });
        }

        [Test]
        public void Operators_UniqueOperatorIds()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            VerifyExpressionLanguageValidation(null,
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(1, new[] { "IS", "NOT" }, OperatorType.PostfixUnaryOperator));
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(2, new[] { "IS" }, OperatorType.PrefixUnaryOperator));
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(1, new[] { "IS", "NOT" }, OperatorType.PostfixUnaryOperator));
                    expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(1, new[] { "IS" }, OperatorType.PrefixUnaryOperator));
                });
        }

        [Test]
        public void Operators_SpecialNonOperatorCharacters()
        {
            foreach (var specialOperatorCharacter in SpecialCharactersCacheThreadStaticContext.Context.SpecialOperatorCharacters)
            {
                VerifyExpressionLanguageValidation(null,
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(
                            new[] {
                                $"{specialOperatorCharacter}",
                                $"IS{specialOperatorCharacter}",
                                $"{specialOperatorCharacter}IS",
                                $"I{specialOperatorCharacter}S"}, OperatorType.PostfixUnaryOperator));
                    });
            }

            foreach (var specialNonOperatorCharacter in SpecialCharactersCacheThreadStaticContext.Context.SpecialNonOperatorCharacters)
            {
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { $"IS{specialNonOperatorCharacter}" },
                            OperatorType.PostfixUnaryOperator));
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { $"{specialNonOperatorCharacter}IS" },
                            OperatorType.PostfixUnaryOperator));
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { $"I{specialNonOperatorCharacter}S" },
                            OperatorType.PostfixUnaryOperator));
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { specialNonOperatorCharacter.ToString() },
                            OperatorType.PostfixUnaryOperator));
                    });
            }
        }

        void SetupOperatorForTest(TestLanguageProviderForLanguageProviderValidationTests testLanguageProvider, string operatorNamePart)
        {
            testLanguageProvider.Operators.Clear();
            testLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "Part1", operatorNamePart, "Part3" },
                OperatorType.PostfixUnaryOperator));
        }

        [Test]
        public void Operators_ConflictsWithCommentMarkers()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            // Operator parts can be in comment markers
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupOperatorForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);
                    testLanguageProvider.LineCommentMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupOperatorForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);
                    testLanguageProvider.MultilineCommentStartMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupOperatorForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);
                    testLanguageProvider.MultilineCommentEndMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);

            // Comment markers cannot be in operator parts
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.LineCommentMarker = conflictingIdentifierInfo.Identifier1;
                    SetupOperatorForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);
                }, null, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.MultilineCommentStartMarker = conflictingIdentifierInfo.Identifier1;

                    SetupOperatorForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);

                }, null, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.MultilineCommentEndMarker = conflictingIdentifierInfo.Identifier1;
                    SetupOperatorForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);

                }, null, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);
        }

        [Test]
        public void Operators_ConflictsWithCodeBlockMarkers()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            // Operator name part cannot be the same as code separator 
            foreach (var codeSeparatorCharacter in GetValidCodeSeparatorCharacters())
            {
                VerifyExpressionLanguageValidation(
                    (testLanguageProvider) =>
                    {
                        testLanguageProvider.ExpressionSeparatorCharacter = codeSeparatorCharacter;
                        SetupOperatorForTest(testLanguageProvider, codeSeparatorCharacter.ToString());
                    },
                    (testLanguageProvider) =>
                    {
                        var operatorCharacter = codeSeparatorCharacter == ';' ? '+' : codeSeparatorCharacter;
                        SetupOperatorForTest(testLanguageProvider, operatorCharacter.ToString());
                    });
            }

            // Operator parts can be in code block markers
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupOperatorForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);

                    testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupOperatorForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);

                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier2;
                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);


            // Code block markers cannot be in operator parts
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.CodeBlockStartMarker = conflictingIdentifierInfo.Identifier1;
                    SetupOperatorForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);

                }, null, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.CodeBlockEndMarker = conflictingIdentifierInfo.Identifier1;
                    SetupOperatorForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);

                }, null, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);
        }

        void SetupKeywordForTest(TestLanguageProviderForLanguageProviderValidationTests testLanguageProvider, string keyword)
        {
            testLanguageProvider.Keywords.Clear();
            testLanguageProvider.Keywords.Add(new LanguageKeywordInfo(1, keyword));
        }

        [Test]
        public void Operators_ConflictsWithKeywords()
        {
            // Keywords and operator do not conflict
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupOperatorForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);

                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType != ConflictingIdentifierType.NoConflict);

            // Operator name part is in keyword is fine.
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.Operators.Clear();
                    testLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { conflictingIdentifierInfo.Identifier1 },
                        OperatorType.PostfixUnaryOperator));

                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);
                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);

            // One of operator name part is in keyword is fine.
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                null,
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    testLanguageProvider.Operators.Clear();
                    testLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { "Part1", conflictingIdentifierInfo.Identifier1, "Part2" },
                        OperatorType.PostfixUnaryOperator));

                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier2);
                }, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.IdentifiersAreEqual);


            // Keyword is in operator name part fails.
            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);

                    testLanguageProvider.Operators.Clear();
                    testLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { conflictingIdentifierInfo.Identifier1 },
                        OperatorType.PostfixUnaryOperator));
                }, null, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);

            VerifyExpressionLanguageValidationWithConflictingIdentifiers(
                (testLanguageProvider, conflictingIdentifierInfo) =>
                {
                    SetupKeywordForTest(testLanguageProvider, conflictingIdentifierInfo.Identifier1);

                    testLanguageProvider.Operators.Clear();
                    testLanguageProvider.Operators.Add(new OperatorInfoForTest(new[] { conflictingIdentifierInfo.Identifier1, "Part1", "Part2" },
                        OperatorType.PostfixUnaryOperator));
                }, null, null, null,
                conflictingIdentifierInfo => conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict);
        }

        [Test]
        public void TextEnclosingCharacters()
        {
            VerifyExpressionLanguageValidation(null,
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ConstantTextStartEndMarkerCharacters = new List<char> { '\'', '\"', '`' };
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ConstantTextStartEndMarkerCharacters = new List<char> { '\'', '\"', 'a' };
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.ConstantTextStartEndMarkerCharacters = new List<char> { '\'', '\"', '#' };
                });
        }

        private List<NumericTypeDescriptor> GetValidDescriptorsList()
        {
            return new List<NumericTypeDescriptor>
            {
                new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.ExponentFormatValueId, new[] {@"^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)"}),
                new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.FloatingPointValueId, new[] {@"^\d*[.]\d+", @"^\d+[.]\d*"}),
                new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.IntegerValueId, new[] {@"^\d+"})
            };
        }

        [Test]
        public void NumericTypeDescriptors_Valid()
        {
            VerifyExpressionLanguageValidation(null,
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.NumericTypeDescriptors = GetValidDescriptorsList();
                });
        }

        [Test]
        public void NumericTypeDescriptors_NullItems()
        {
            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.NumericTypeDescriptors = GetValidDescriptorsList();

                    expressionLanguageProvider.NumericTypeDescriptors[0] = null;
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.NumericTypeDescriptors = GetValidDescriptorsList();

                    expressionLanguageProvider.NumericTypeDescriptors[1] = null;
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.NumericTypeDescriptors = GetValidDescriptorsList();

                    expressionLanguageProvider.NumericTypeDescriptors[2] = null;
                });
        }

        [Test]
        public void NumericTypeDescriptors_DuplicateIds()
        {
            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.NumericTypeDescriptors = new List<NumericTypeDescriptor>
                    {
                        new NumericTypeDescriptor(1, new [] {@"^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)"}),
                        new NumericTypeDescriptor(1, new [] {@"^\d+"})
                    };
                });
        }

        [Test]
        public void NumericTypeDescriptors_RegularExpressionMissing()
        {
            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.NumericTypeDescriptors = new List<NumericTypeDescriptor>
                    {
                        new NumericTypeDescriptor(1, new [] {@"^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)"}),
                        new NumericTypeDescriptor(2, null)
                    };
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.NumericTypeDescriptors = new List<NumericTypeDescriptor>
                    {
                        new NumericTypeDescriptor(1, new [] {@"^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)"}),
                        new NumericTypeDescriptor(2, new string[] {})
                    };
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.NumericTypeDescriptors = new List<NumericTypeDescriptor>
                    {
                        new NumericTypeDescriptor(1, new [] {@"^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)"}),
                        new NumericTypeDescriptor(2, new [] {@"^\d+", ""})
                    };
                });

            ProcessSpaceCharacters(space =>
            {
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.NumericTypeDescriptors = new List<NumericTypeDescriptor>
                        {
                            new NumericTypeDescriptor(1, new [] {@"^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)"}),
                            new NumericTypeDescriptor(2,
                                new [] {
                                    @"^\d*[.]\d+",
                                    space})
                        };
                    });
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.NumericTypeDescriptors = new List<NumericTypeDescriptor>
                        {
                            new NumericTypeDescriptor(1, new [] {@"^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)"}),
                            new NumericTypeDescriptor(2,
                                new [] {
                                    @"^\d*[.]\d+",
                                    $@"^\d+{space}[.]\d*"})
                        };
                    });

                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.NumericTypeDescriptors = new List<NumericTypeDescriptor>
                        {
                            new NumericTypeDescriptor(1, new [] {@"^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)"}),
                            new NumericTypeDescriptor(2,
                                new [] {
                                    @"^\d*[.]\d+",
                                    $@"{space}^\d+[.]\d*"})
                        };
                    });
                VerifyExpressionLanguageValidation(
                    (expressionLanguageProvider) =>
                    {
                        expressionLanguageProvider.NumericTypeDescriptors = new List<NumericTypeDescriptor>
                        {
                            new NumericTypeDescriptor(1, new [] {@"^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)"}),
                            new NumericTypeDescriptor(2,
                                new [] {
                                    @"^\d*[.]\d+",
                                    $@"^\d+[.]\d*{space}"})
                        };
                    });
            });
        }

        [Test]
        public void NumericTypeDescriptors_StartsOrEndsWithInvalidCharacter()
        {
            VerifyExpressionLanguageValidation(null,
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.NumericTypeDescriptors = new List<NumericTypeDescriptor>
                    {
                        new NumericTypeDescriptor(1, new [] {@"^\d+"})
                    };
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.NumericTypeDescriptors = new List<NumericTypeDescriptor>
                    {
                        // Should start with '^'
                        new NumericTypeDescriptor(1, new [] {@"\d+"})
                    };
                });

            VerifyExpressionLanguageValidation(
                (expressionLanguageProvider) =>
                {
                    expressionLanguageProvider.NumericTypeDescriptors = new List<NumericTypeDescriptor>
                    {
                        // Cannot end '$' character
                        new NumericTypeDescriptor(1, new [] {@"^\d+$"})
                    };
                });
        }

        private void SetupIsValidLiteralCharacter(TestLanguageProviderForLanguageProviderValidationTests testLanguageProvider,
            IEnumerable<char> explicitlyAllowedCharacters = null,
            IEnumerable<char> explicitlyProhibitedCharacters = null)
        {
            bool IsValidLiteralCharacter(char character, int positionInLiteral, ITextSymbolsParserState textSymbolsParserState)
            {
                if (explicitlyAllowedCharacters != null && explicitlyAllowedCharacters.Contains(character))
                    return true;

                if (explicitlyProhibitedCharacters != null && explicitlyProhibitedCharacters.Contains(character))
                    return false;

                if (character == '_' || SpecialCharactersCacheThreadStaticContext.Context.IsSpecialOperatorCharacter(character))
                    return true;

                if (Char.IsNumber(character))
                    return positionInLiteral > 0;

                return Helpers.IsLatinLetter(character);
            }

            testLanguageProvider.IsValidLiteralCharacter = IsValidLiteralCharacter;
        }

        void SetupCommentAndCodeBlockMarkersForLiteralTesting(TestLanguageProviderForLanguageProviderValidationTests testLanguageProvider)
        {
            testLanguageProvider.LineCommentMarker = "rem";
            testLanguageProvider.MultilineCommentStartMarker = "remstart";
            testLanguageProvider.MultilineCommentEndMarker = "remend";

            testLanguageProvider.ExpressionSeparatorCharacter = ';';
            testLanguageProvider.CodeBlockStartMarker = "Begin";
            testLanguageProvider.CodeBlockEndMarker = "End";
        }

        [Test]
        public void Literals_AnyOperatorCharactersCanBeUsed()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            VerifyExpressionLanguageValidation(null,
                (testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider);
                });
        }

        [Test]
        public void Literals_NoSpecialNonOperatorCharaters()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            foreach (var separatorChar in SpecialCharactersCacheThreadStaticContext.Context.SpecialNonOperatorCharacters)
            {
                VerifyExpressionLanguageValidation(
                    (testLanguageProvider) =>
                    {
                        SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                        SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    });
            }
        }

        [Test]
        public void Literals_NoOperatorCharacterOverlapWithCommentMarkers()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            // No conflicts with special chars used in code separator markers
            foreach (var separatorChar in SpecialCharactersCacheThreadStaticContext.Context.SpecialOperatorCharacters)
            {
                // LineCommentMarker
                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.LineCommentMarker = separatorChar.ToString();
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.LineCommentMarker = $"re{separatorChar}m";
                });


                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.LineCommentMarker = $"rem{separatorChar}";
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.LineCommentMarker = $"{separatorChar}rem";
                });

                // MultilineCommentStartMarker
                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.MultilineCommentStartMarker = separatorChar.ToString();
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.MultilineCommentStartMarker = $"rem{separatorChar}start";
                });


                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.MultilineCommentStartMarker = $"remstart{separatorChar}";
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.MultilineCommentStartMarker = $"{separatorChar}remstart";
                });

                // MultilineCommentEndMarker
                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.MultilineCommentEndMarker = separatorChar.ToString();
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.MultilineCommentEndMarker = $"rem{separatorChar}end";
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.MultilineCommentEndMarker = $"remend{separatorChar}";
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.MultilineCommentEndMarker = $"{separatorChar}remend";
                });
            }
        }

        [Test]
        public void Literals_NoOperatorCharacterOverlapWithCodeMarkers()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            // No conflicts with special chars used in code separator markers
            foreach (var separatorChar in SpecialCharactersCacheThreadStaticContext.Context.SpecialOperatorCharacters)
            {
                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.ExpressionSeparatorCharacter = separatorChar;
                });

                // CodeBlockStartMarker
                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.CodeBlockStartMarker = separatorChar.ToString();
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.CodeBlockStartMarker = $"Be{separatorChar}gin";
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.CodeBlockStartMarker = $"{separatorChar}begin";
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.CodeBlockStartMarker = $"Begin{separatorChar}";
                });

                // CodeBlockEndMarker
                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.CodeBlockEndMarker = separatorChar.ToString();
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.CodeBlockEndMarker = $"En{separatorChar}d";
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.CodeBlockEndMarker = $"{separatorChar}end";
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    testLanguageProvider.CodeBlockEndMarker = $"End{separatorChar}";
                });
            }
        }

        [Test]
        public void Literals_NoOperatorCharacterOverlapWithOperators()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            // No conflicts with special chars used in code separator markers
            foreach (var separatorChar in SpecialCharactersCacheThreadStaticContext.Context.SpecialOperatorCharacters)
            {
                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    SetupOperatorForTest(testLanguageProvider, separatorChar.ToString());
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    SetupOperatorForTest(testLanguageProvider, $"op{separatorChar}er");
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    SetupOperatorForTest(testLanguageProvider, $"{separatorChar}oper");
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    SetupOperatorForTest(testLanguageProvider, $"oper{separatorChar}");
                });
            }
        }

        [Test]
        public void Literals_NoOperatorCharacterOverlapWithKeywords()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            // No conflicts with special chars used in code separator markers
            foreach (var separatorChar in SpecialCharactersCacheThreadStaticContext.Context.SpecialOperatorCharacters)
            {
                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    SetupKeywordForTest(testLanguageProvider, separatorChar.ToString());
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    SetupKeywordForTest(testLanguageProvider, $"whe{separatorChar}re");
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    SetupKeywordForTest(testLanguageProvider, $"{separatorChar}where");
                });

                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { separatorChar });
                    SetupKeywordForTest(testLanguageProvider, $"where{separatorChar}");
                });
            }
        }

        [Test]
        public void Literals_LiteralCannotStartWithNumber()
        {
            TestLanguageProviderForLanguageProviderValidationTests.TurnOffOperatorsAndKeywords();

            for (var i = 0; i <= 9; ++i)
            {
                VerifyExpressionLanguageValidation((testLanguageProvider) =>
                {
                    SetupCommentAndCodeBlockMarkersForLiteralTesting(testLanguageProvider);
                    SetupIsValidLiteralCharacter(testLanguageProvider, new[] { $"{i}"[0] });
                });
            }

        }

        private void ProcessSpaceCharacters([NotNull] Action<string> spaceCharacterProcessor)
        {
            var spaces = new string[] { " ", "\t", Environment.NewLine };

            foreach (var paceCharacterString in spaces)
                spaceCharacterProcessor(paceCharacterString);
        }

        string ChangeTextCaseBasedOnTextLanguageCaseSensitivity([NotNull] IExpressionLanguageProvider expressionLanguageProvider, [NotNull] string text)
        {
            if (expressionLanguageProvider.IsLanguageCaseSensitive)
                return text;

            var transformedText = new StringBuilder();
            foreach (var character in text)
            {

                if (character == Char.ToLower(character))
                    transformedText.Append(Char.ToUpper(character));
                else
                    transformedText.Append(Char.ToLower(character));
            }

            return transformedText.ToString();
        }

        private void ProcessComments([NotNull] IExpressionLanguageProvider expressionLanguageProvider, [NotNull] Action<string> commentMarkerProcessor)
        {
            if (expressionLanguageProvider.LineCommentMarker != null)
                commentMarkerProcessor(ChangeTextCaseBasedOnTextLanguageCaseSensitivity(expressionLanguageProvider, expressionLanguageProvider.LineCommentMarker));

            if (expressionLanguageProvider.MultilineCommentStartMarker != null)
                commentMarkerProcessor(ChangeTextCaseBasedOnTextLanguageCaseSensitivity(expressionLanguageProvider, expressionLanguageProvider.MultilineCommentStartMarker));

            if (expressionLanguageProvider.MultilineCommentEndMarker != null)
                commentMarkerProcessor(ChangeTextCaseBasedOnTextLanguageCaseSensitivity(expressionLanguageProvider, expressionLanguageProvider.MultilineCommentEndMarker));
        }

        private void ProcessCodeSeparators([NotNull] IExpressionLanguageProvider expressionLanguageProvider, [NotNull] Action<string> codeSeparatorProcessor)
        {
            if (expressionLanguageProvider.ExpressionSeparatorCharacter != '\0')
                codeSeparatorProcessor(ChangeTextCaseBasedOnTextLanguageCaseSensitivity(expressionLanguageProvider, expressionLanguageProvider.ExpressionSeparatorCharacter.ToString()));

            if (expressionLanguageProvider.CodeBlockStartMarker != null)
                codeSeparatorProcessor(ChangeTextCaseBasedOnTextLanguageCaseSensitivity(expressionLanguageProvider, expressionLanguageProvider.CodeBlockStartMarker));

            if (expressionLanguageProvider.CodeBlockEndMarker != null)
                codeSeparatorProcessor(ChangeTextCaseBasedOnTextLanguageCaseSensitivity(expressionLanguageProvider, expressionLanguageProvider.CodeBlockEndMarker));
        }

        private void VerifyExpressionLanguageValidation(
            Action<TestLanguageProviderForLanguageProviderValidationTests> transformForFailure = null, 
            Action<TestLanguageProviderForLanguageProviderValidationTests> transformForSuccess = null)
        {
            var expressionLanguageProvider = new TestLanguageProviderForLanguageProviderValidationTests();

            Assert.AreEqual(3, expressionLanguageProvider.NumericTypeDescriptors.Count);

            Assert.AreEqual(3, expressionLanguageProvider.ConstantTextStartEndMarkerCharacters.Count);
            Assert.IsTrue(expressionLanguageProvider.ConstantTextStartEndMarkerCharacters.Any(x => x == '\'') &&
                          expressionLanguageProvider.ConstantTextStartEndMarkerCharacters.Any(x => x == '\"') &&
                          expressionLanguageProvider.ConstantTextStartEndMarkerCharacters.Any(x => x == '`'));

            Assert.IsNotNull(expressionLanguageProvider.LineCommentMarker);
            Assert.IsNotNull(expressionLanguageProvider.MultilineCommentStartMarker);
            Assert.IsNotNull(expressionLanguageProvider.MultilineCommentEndMarker);

            Assert.IsTrue(expressionLanguageProvider.ExpressionSeparatorCharacter != '\0');
            Assert.IsNotNull(expressionLanguageProvider.CodeBlockStartMarker);
            Assert.IsNotNull(expressionLanguageProvider.CodeBlockEndMarker);

            transformForSuccess?.Invoke(expressionLanguageProvider);

            var defaultExpressionLanguageProviderValidator = new DefaultExpressionLanguageProviderValidator();
            
            defaultExpressionLanguageProviderValidator.Validate(expressionLanguageProvider);

            if (transformForFailure != null)
            {
                transformForFailure(expressionLanguageProvider);
                TestsHelper.ValidateExceptionIsThrown(() => { defaultExpressionLanguageProviderValidator.Validate(expressionLanguageProvider); });
            }
        }

        private void VerifyExpressionLanguageValidationWithConflictingIdentifiers(
            Action<TestLanguageProviderForLanguageProviderValidationTests, ConflictingIdentifierInfo> transformForFailure = null,
            Action<TestLanguageProviderForLanguageProviderValidationTests, ConflictingIdentifierInfo> transformForSuccess = null,
            char[] specialCharactersInIdentifier1 = null, char[] specialCharactersInIdentifier2 = null,
            Func<ConflictingIdentifierInfo, bool> ignoreConflictingIdentifierInfo = null)
        {
            Assert.IsTrue(transformForFailure != null || transformForSuccess != null);

            for (int isLanguageCaseSensitive = 0; isLanguageCaseSensitive < 2; ++isLanguageCaseSensitive)
            {
                var conflictingIdentifiers = GetConflictingIdentifiers(specialCharactersInIdentifier1, specialCharactersInIdentifier2);

                foreach (var conflictingIdentifierInfo in conflictingIdentifiers)
                {
                    if (ignoreConflictingIdentifierInfo?.Invoke(conflictingIdentifierInfo) ?? false)
                        continue;

                    for (var transformedIdentifiersTrial = 0; transformedIdentifiersTrial < 2; ++transformedIdentifiersTrial)
                    {
                        var transformedConflictingIdentifierInfo = conflictingIdentifierInfo;

                        if (transformedIdentifiersTrial == 1)
                        {
                            if (conflictingIdentifierInfo.ConflictingIdentifierType == ConflictingIdentifierType.NoConflict ||
                                isLanguageCaseSensitive == 0)
                                transformedConflictingIdentifierInfo =
                                    new ConflictingIdentifierInfo(conflictingIdentifierInfo.Identifier1.ToUpper(),
                                        conflictingIdentifierInfo.Identifier2,
                                        conflictingIdentifierInfo.ConflictingIdentifierType);
                            else
                                continue;
                        }

                        Action<TestLanguageProviderForLanguageProviderValidationTests> transformForSuccess2 = null;

                        if (transformForSuccess != null)
                            transformForSuccess2 = (testLanguage) =>
                            {
                                testLanguage.IsLanguageCaseSensitive = isLanguageCaseSensitive == 1;
                                transformForSuccess?.Invoke(testLanguage, transformedConflictingIdentifierInfo);
                            };

                        Action<TestLanguageProviderForLanguageProviderValidationTests> transformForFailure2 = null;

                        if (transformForFailure != null)
                            transformForFailure2 = (testLanguage) =>
                            {
                                testLanguage.IsLanguageCaseSensitive = isLanguageCaseSensitive == 1;
                                transformForFailure.Invoke(testLanguage, transformedConflictingIdentifierInfo);
                            };

                        VerifyExpressionLanguageValidation(transformForFailure2, transformForSuccess2);
                    }
                }
            }
        }

        [Test]
        public void TempTest()
        {
            var testLanguageProvider = new TestLanguageProviderForLanguageProviderValidationTests();
            _ = Helpers.CheckIfIdentifiersConflict(testLanguageProvider, "bcd", "bcd}");
        }

        public enum ConflictingIdentifierType
        {
            NoConflict,
            Identifier1IInIdentifier2,
            Identifier1IInIdentifier2AtZeroPosition,
            IdentifiersAreEqual
        }

        private class ConflictingIdentifierInfo
        {
            public ConflictingIdentifierInfo([NotNull] string identifier1, [NotNull] string identifier2, ConflictingIdentifierType conflictingIdentifierType)
            {
                Identifier1 = identifier1;
                Identifier2 = identifier2;
                ConflictingIdentifierType = conflictingIdentifierType;
            }

            [NotNull] public string Identifier1 { get; }
            [NotNull] public string Identifier2 { get; }
            public ConflictingIdentifierType ConflictingIdentifierType { get; }
        }

        private IReadOnlyList<ConflictingIdentifierInfo> GetConflictingIdentifiers(char[] specialCharactersInIdentifier1 = null, char[] specialCharactersInIdentifier2 = null)
        {
            var specialCharactersInIdentifier1ToUse = new List<char>(SpecialCharactersCacheThreadStaticContext.Context.SpecialOperatorCharacters);
            if (specialCharactersInIdentifier1 != null && specialCharactersInIdentifier1.Length > 0)
                specialCharactersInIdentifier1ToUse.AddRange(specialCharactersInIdentifier1);

            var specialCharactersInIdentifier2ToUse = new List<char>(SpecialCharactersCacheThreadStaticContext.Context.SpecialOperatorCharacters);
            if (specialCharactersInIdentifier2 != null && specialCharactersInIdentifier2.Length > 0)
                specialCharactersInIdentifier2ToUse.AddRange(specialCharactersInIdentifier2);

            var conflictingIdentifiers = new List<ConflictingIdentifierInfo>();

            conflictingIdentifiers.Add(new ConflictingIdentifierInfo("bcd", "bce",
                ConflictingIdentifierType.NoConflict));

            conflictingIdentifiers.Add(new ConflictingIdentifierInfo("bcd", "bcde",
                ConflictingIdentifierType.NoConflict));

            conflictingIdentifiers.Add(new ConflictingIdentifierInfo("bcd", "abcd",
                ConflictingIdentifierType.NoConflict));

            conflictingIdentifiers.Add(new ConflictingIdentifierInfo("bcd", "bcd",
                ConflictingIdentifierType.IdentifiersAreEqual));

            #region identifier1 has special character and is contained in identifier2
            foreach (var specialCharacter in specialCharactersInIdentifier1ToUse)
            {
                if (SpecialCharactersCacheThreadStaticContext.Context.IsSpecialNonOperatorCharacter(specialCharacter) &&
                    (specialCharactersInIdentifier2 == null || !specialCharactersInIdentifier2.Any(x => x == specialCharacter)))
                    continue;


                // TODO: Add no conflict cases
                var identifier1 = $"bcd{specialCharacter}";
                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"{identifier1}e",
                    ConflictingIdentifierType.Identifier1IInIdentifier2AtZeroPosition));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}e",
                    ConflictingIdentifierType.NoConflict));

                identifier1 = $"{specialCharacter}bcd";
                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}e",
                    ConflictingIdentifierType.NoConflict));

                identifier1 = $"{specialCharacter}bcd{specialCharacter}";
                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}e",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"bcda{identifier1}e",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                /*#region Old cases
                identifier1 = $"{specialCharacter}bcd";

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"{identifier1}e",
                    ConflictingIdentifierType.Identifier1IInIdentifier2AtZeroPosition));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}e",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                identifier1 = $"b{specialCharacter}cd";
                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"{identifier1}e",
                    ConflictingIdentifierType.Identifier1IInIdentifier2AtZeroPosition));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}e",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                identifier1 = $"bcd{specialCharacter}";
                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"{identifier1}e",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}e",
                    ConflictingIdentifierType.Identifier1IInIdentifier2)); 
                #endregion*/
            }
            #endregion

            foreach (var specialCharacter in specialCharactersInIdentifier2ToUse)
            {
                // TODO: Add no conflict cases

                var identifier1 = "bcd";
                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"{identifier1}{specialCharacter}",
                    ConflictingIdentifierType.Identifier1IInIdentifier2AtZeroPosition));

                
                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"{specialCharacter}{identifier1}",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"{specialCharacter}{identifier1}{specialCharacter}",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}{specialCharacter}{identifier1}",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}{specialCharacter}{identifier1}",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                conflictingIdentifiers.Add(new ConflictingIdentifierInfo(identifier1, $"a{identifier1}{specialCharacter}{identifier1}{specialCharacter}",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                /*#region OLD CASES
                // identifier 1 preceded by regular character, but proceeded with special character Ok
                conflictingIdentifiers.Add(new ConflictingIdentifierInfo("bcd", $"abcd{specialCharacter}",
                    ConflictingIdentifierType.NoConflict));

                // identifier 2 starts with identifier 2, followed by special character2
                conflictingIdentifiers.Add(new ConflictingIdentifierInfo("bcd", $"bcd{specialCharacter}",
                    ConflictingIdentifierType.Identifier1IInIdentifier2AtZeroPosition));

                // identifier 1 succeeded by a special character in identifier2
                conflictingIdentifiers.Add(new ConflictingIdentifierInfo("bcd", $"bcd{specialCharacter}e",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                // identifier 1 occurs twice in identifier 2, but only the second occurrence causes issue, since the first occurrence is not preceded by special character
                conflictingIdentifiers.Add(new ConflictingIdentifierInfo("bcd", $"abcd{specialCharacter}bcd",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                // identifier 1 preceded by special character in identifier2
                conflictingIdentifiers.Add(new ConflictingIdentifierInfo("bcd", $"a{specialCharacter}bcd",
                    ConflictingIdentifierType.Identifier1IInIdentifier2));

                foreach (var specialCharacter2 in specialCharactersInIdentifier2ToUse)
                {
                    // identifier 1 succeeded and preceded by a special character in identifier2
                    conflictingIdentifiers.Add(new ConflictingIdentifierInfo("bcd", $"a{specialCharacter}bcd{specialCharacter2}e",
                        ConflictingIdentifierType.Identifier1IInIdentifier2));
                } 
                #endregion*/
            }

            return conflictingIdentifiers;
        }

    }
}

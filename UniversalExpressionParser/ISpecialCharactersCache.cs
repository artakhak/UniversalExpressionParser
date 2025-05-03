using JetBrains.Annotations;
using System.Collections.Generic;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Represents a cache for special characters used in certain contexts. It classifies characters
    /// into special operator characters, special non-operator characters, or a combination of both.
    /// </summary>
    public interface ISpecialCharactersCache
    {
        /// <summary>
        /// List of special operator character such as '+', '-', ':', '*', '~', '!', etc.
        /// </summary>
        IEnumerable<char> SpecialOperatorCharacters { get; }

        /// <summary>
        /// List of special non-operator character such '`', '\', '/', '(', ')', '[', ']', ';', ',', etc.
        /// </summary>
        IEnumerable<char> SpecialNonOperatorCharacters { get; }

        /// <summary>
        /// List of special all operator and non-operator character such '+', '-', ':', '*', '~', '!', '`', '\', '/', '(', ')', '[', ']', ';', ',', etc.
        /// </summary>
        IEnumerable<char> SpecialCharacters { get; }

        /// <summary>
        /// Returns true, if <paramref name="character"/> is a special operator character such as '+', '-', ':', '*', '~', '!', etc.
        /// </summary>
        /// <param name="character">Character to check.</param>
        bool IsSpecialOperatorCharacter(char character);

        /// <summary>
        /// Returns true, if <paramref name="character"/> is a special non-operator character such '`', '\', '/', '(', ')', '[', ']', ';', ',', etc.
        /// </summary>
        /// <param name="character">Character to check.</param>
        bool IsSpecialNonOperatorCharacter(char character);

        /// <summary>
        /// Returns true, if <paramref name="character"/> is either a special operator character such as '+', '-', ':', '*', '~', '!', etc,
        /// or is a special non-operator character such '`', '\', '/', '(', ')', '[', ']', ';', ',', etc.
        /// </summary>
        /// <param name="character">Character to check.</param>
        bool IsSpecialCharacter(char character);
    }

    /// <inheritdoc />
    public class SpecialCharactersCache : ISpecialCharactersCache
    {
        [NotNull]
        // ReSharper disable once InconsistentNaming
        private readonly HashSet<char> _specialOperatorCharactersSet = new HashSet<char>();

        [NotNull]
        // ReSharper disable once InconsistentNaming
        private readonly HashSet<char> _specialNonOperatorCharactersSet = new HashSet<char>();

        [NotNull]
        // ReSharper disable once InconsistentNaming
        private readonly HashSet<char> _specialCharactersSet = new HashSet<char>();

        /// <summary>
        /// Default constructor that initializes data using <see cref="DefaultSpecialCharacters.SpecialOperatorCharacters"/> and
        /// <see cref="DefaultSpecialCharacters.SpecialNonOperatorCharacters"/>.
        /// </summary>
        public SpecialCharactersCache(): this(DefaultSpecialCharacters.SpecialOperatorCharacters, DefaultSpecialCharacters.SpecialNonOperatorCharacters)
        {
        }

        public SpecialCharactersCache(IEnumerable<char> specialOperatorCharacters, IEnumerable<char> specialNonOperatorCharacters)
        {
            foreach (var specialOperatorCharacter in specialOperatorCharacters)
            {
                _specialOperatorCharactersSet.Add(specialOperatorCharacter);
                _specialCharactersSet.Add(specialOperatorCharacter);
            }

            foreach (var specialNonOperatorCharacter in specialNonOperatorCharacters)
            {
                _specialNonOperatorCharactersSet.Add(specialNonOperatorCharacter);
                _specialCharactersSet.Add(specialNonOperatorCharacter);
            }
        }
        
        /// <inheritdoc />
        public IEnumerable<char> SpecialOperatorCharacters => _specialOperatorCharactersSet;

        /// <inheritdoc />
        public IEnumerable<char> SpecialNonOperatorCharacters => _specialNonOperatorCharactersSet;

        public IEnumerable<char> SpecialCharacters => _specialCharactersSet;

        /// <inheritdoc />
        public bool IsSpecialOperatorCharacter(char character) => _specialOperatorCharactersSet.Contains(character);

        /// <inheritdoc />
        public bool IsSpecialNonOperatorCharacter(char character) => _specialNonOperatorCharactersSet.Contains(character);

        /// <inheritdoc />
        public bool IsSpecialCharacter(char character) => _specialCharactersSet.Contains(character);
    }
}
using System.Collections.Generic;

namespace UniversalExpressionParser
{
    /// <summary>
    /// A class for default operator and non-operator characters that are used to tokenize the text.<br/>
    /// Example of special operator characters are '+', '-', '$', etc.<br/>
    /// Example of special non-operator characters are '`', '\', '/', etc.<br/>
    /// </summary>
    public static class DefaultSpecialCharacters
    {
        private static readonly List<char> _specialOperatorCharacters = new List<char>();
        private static readonly List<char> _specialNonOperatorCharacters = new List<char>();

        static DefaultSpecialCharacters()
        {
            _specialOperatorCharacters.Add('~');
            _specialOperatorCharacters.Add('!');

            _specialOperatorCharacters.Add('@');
            _specialOperatorCharacters.Add('#');
            _specialOperatorCharacters.Add('$');
            _specialOperatorCharacters.Add('%');
            _specialOperatorCharacters.Add('^');
            _specialOperatorCharacters.Add('&');
            _specialOperatorCharacters.Add('|');

            _specialOperatorCharacters.Add('-');
            _specialOperatorCharacters.Add('+');

            _specialOperatorCharacters.Add('*');
            _specialOperatorCharacters.Add('/');

            _specialOperatorCharacters.Add('<');
            _specialOperatorCharacters.Add('>');
            _specialOperatorCharacters.Add('?');
            _specialOperatorCharacters.Add(':');

            _specialOperatorCharacters.Add('=');
            // '.' can be added as special operator character in custom implementations of IExpressionLanguageProvider
            //_specialOperatorCharacters.Add('.');

            _specialNonOperatorCharacters.Add('`');
            _specialNonOperatorCharacters.Add('\'');
            _specialNonOperatorCharacters.Add('"');
            _specialNonOperatorCharacters.Add('(');
            _specialNonOperatorCharacters.Add(')');
            _specialNonOperatorCharacters.Add('[');
            _specialNonOperatorCharacters.Add(']');
            _specialNonOperatorCharacters.Add('{');
            _specialNonOperatorCharacters.Add('}');
            _specialNonOperatorCharacters.Add(',');
            _specialNonOperatorCharacters.Add(';');
            _specialNonOperatorCharacters.Add('\\');
        }

        /// <summary>
        /// List of special operator character such as '+', '-', ':', '*', '~', '!', etc.
        /// </summary>
        public static IEnumerable<char> SpecialOperatorCharacters => _specialOperatorCharacters;

        /// <summary>
        /// List of special non-operator character such '`', '\', '/', '(', ')', '[', ']', ';', ',', etc.
        /// </summary>
        public static IEnumerable<char> SpecialNonOperatorCharacters => _specialNonOperatorCharacters;
    }
}
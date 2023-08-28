using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

namespace ThinkSpark.Shared.Extensions.Common
{
    public static class StringExtension
    {
        /// <summary>
        /// Tests a string to be Like another string containing SQL Like style wildcards
        /// </summary>
        /// <param name="value">string to be searched</param>
        /// <param name="searchString">the search string containing wildcards</param>
        /// <returns>value.Like(searchString)</returns>
        /// <example>value.Like("a")</example>
        /// <example>value.Like("a%")</example>
        /// <example>value.Like("%b")</example>
        /// <example>value.Like("a%b")</example>
        /// <example>value.Like("a%b%c")</example>
        /// <remarks>base author -- Ruard van Elburg from StackOverflow, modifications by dvn</remarks>
        /// <remarks>converted to a String extension by sja</remarks>
        /// <seealso cref="https://stackoverflow.com/questions/1040380/wildcard-search-for-linq"/>
        public static bool Like(this string value, string searchString)
        {
            bool result = false;

            var likeParts = searchString.Split(new char[] { '%' });

            for (int i = 0; i < likeParts.Length; i++)
            {
                if (likeParts[i] == string.Empty)
                {
                    continue;   // "a%"
                }

                if (i == 0)
                {
                    if (likeParts.Length == 1) // "a"
                    {
                        result = value.Equals(likeParts[i], StringComparison.OrdinalIgnoreCase);
                    }
                    else // "a%" or "a%b"
                    {
                        result = value.StartsWith(likeParts[i], StringComparison.OrdinalIgnoreCase);
                    }
                }
                else if (i == likeParts.Length - 1) // "a%b" or "%b"
                {
                    result &= value.EndsWith(likeParts[i], StringComparison.OrdinalIgnoreCase);
                }
                else // "a%b%c"
                {
                    int current = value.IndexOf(likeParts[i], StringComparison.OrdinalIgnoreCase);
                    int previous = value.IndexOf(likeParts[i - 1], StringComparison.OrdinalIgnoreCase);
                    result &= previous < current;
                }
            }

            return result;
        }

        /// <summary>
        /// Tests a string containing SQL Like style wildcards to be ReverseLike another string 
        /// </summary>
        /// <param name="value">search string containing wildcards</param>
        /// <param name="compareString">string to be compared</param>
        /// <returns>value.ReverseLike(compareString)</returns>
        /// <example>value.ReverseLike("a")</example>
        /// <example>value.ReverseLike("abc")</example>
        /// <example>value.ReverseLike("ab")</example>
        /// <example>value.ReverseLike("axb")</example>
        /// <example>value.ReverseLike("axbyc")</example>
        /// <remarks>reversed logic of Like String extension</remarks>
        public static bool ReverseLike(this string value, string compareString)
        {
            bool result = false;

            var likeParts = value.Split(new char[] { '%' });

            for (int i = 0; i < likeParts.Length; i++)
            {
                if (likeParts[i] == string.Empty)
                {
                    continue;   // "a%"
                }

                if (i == 0)
                {
                    if (likeParts.Length == 1) // "a"
                    {
                        result = compareString.Equals(likeParts[i], StringComparison.OrdinalIgnoreCase);
                    }
                    else // "a%" or "a%b"
                    {
                        result = compareString.StartsWith(likeParts[i], StringComparison.OrdinalIgnoreCase);
                    }
                }
                else if (i == likeParts.Length - 1) // "a%b" or "%b"
                {
                    result &= compareString.EndsWith(likeParts[i], StringComparison.OrdinalIgnoreCase);
                }
                else // "a%b%c"
                {
                    int current = compareString.IndexOf(likeParts[i], StringComparison.OrdinalIgnoreCase);
                    int previous = compareString.IndexOf(likeParts[i - 1], StringComparison.OrdinalIgnoreCase);
                    result &= previous < current;
                }
            }

            return result;
        }

        /// <summary>
        /// Captaliza uma string, isto é, deixa toda primeira letra de cada palavra maiúscula.
        /// </summary>
        /// <param name="value">String a ser captalizada.</param>
        /// <returns>Retorna a string captalizada.</returns>
        public static string ToCapitalize(this string value)
        {
            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            var result = textInfo.ToTitleCase(value.ToLower());
            return result;
        }

        /// <summary>
        /// Obtem somente os números de uma string de caracteres e números.
        /// </summary>
        /// <param name="value">String de caracteres com numeros, letras, pontuações etc.</param>
        /// <returns>Retorna a somente os números.</returns>
        public static long ToNumber(this string value)
        {
            var newValue = string.Empty;
            var matches = Regex.Matches(value, @"\d+");

            foreach (var match in matches)
                newValue += match;

            var result = string.IsNullOrEmpty(newValue) ? "0" : newValue;
            return long.Parse(result);
        }

        /// <summary>
        /// Gera um hash padrao MD5 de uma string.
        /// </summary>
        /// <param name="value">String que sera gerada o hash.</param>
        /// <returns>Retorna um hash MD5.</returns>
        public static string ToCreateMd5(this string value)
        {
            using (MD5 md5 = MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(value);
                var hashBytes = md5.ComputeHash(inputBytes);
                var stringBuilder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                    stringBuilder.Append(hashBytes[i].ToString("X2"));

                return stringBuilder.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Adiciona espaço insere o próximo valor e quebra linha. 
        /// </summary>
        /// <param name="stringBuilder">Objeto com o texto a ser conctenado.</param>
        /// <param name="value">Valor a ser concatenado.</param>
        public static void AppendLineWithWhiteSpace(this StringBuilder stringBuilder, string value, int countWhiteSpace)
        {
            string whiteSpaces = string.Empty;

            for (int i = 0; i < countWhiteSpace; i++)
                whiteSpaces = whiteSpaces + " ";

            stringBuilder.AppendFormat($"{0}{1}{2}", whiteSpaces, value, Environment.NewLine);
        }

        /// <summary>
        /// Valida um cnpj se é verdadeiro ou não.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsCpf(this string value)
        {
            value = value.ToNumber().ToString();
            if (string.IsNullOrEmpty(value))
                return false;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            value = value.Trim();
            value = value.Replace(".", "").Replace("-", "");
            if (value.Length != 11)
                return false;
            tempCpf = value.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            var result = value.EndsWith(digito);
            return result;
        }

        /// <summary>
        /// Valida um cnpj se é verdadeiro ou não.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsCnpj(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            value = value.Trim();
            value = value.Replace(".", "").Replace("-", "").Replace("/", "");
            if (value.Length != 14)
                return false;
            tempCnpj = value.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            var result = value.EndsWith(digito);
            return result;
        }

        /// <summary>
        /// Valida se o status informado é do tipo Ativo, Inativo ou Excluído.
        /// </summary>
        /// <param name="value"></param>
        public static bool ValidateStatus(this string value)
        {
            if (value != "A" && value != "I" && value != "E" || !string.IsNullOrEmpty(value))
                return true;
            return false;

        }

        /// <summary>
        /// Verifica se duas listas têm os mesmos valores.
        /// </summary>
        /// <typeparam name="TEntity">Tipo do objeto.</typeparam>
        /// <param name="list">Lista 1 a ser comparada.</param>
        /// <param name="other">Lista 2 a ser comparada.</param>
        public static bool EquivalentTo<TEntity>(this List<TEntity> list, List<TEntity> other) where TEntity : IEquatable<TEntity>
        {
            if (list.Except(other).Any())
                return false;

            if (other.Except(list).Any())
                return false;

            return true;
        }

        /// <summary>
        /// Formatar uma string CNPJ
        /// </summary>
        /// <param name="CNPJ">string CNPJ sem formatacao</param>
        /// <returns>string CNPJ formatada</returns>
        /// <example>Recebe '99999999999999' Devolve '99.999.999/9999-99'</example>
        public static string ToFormatCNPJ(string CNPJ)
        {
            var expression = @"00\.000\.000\/0000\-00";
            return Convert.ToUInt64(CNPJ).ToString(expression);
        }

        /// <summary>
        /// Formatar uma string CPF
        /// </summary>
        /// <param name="CPF">string CPF sem formatacao</param>
        /// <returns>string CPF formatada</returns>
        /// <example>Recebe '99999999999' Devolve '999.999.999-99'</example>
        public static string ToFormatCPF(string CPF)
        {
            var expression = @"000\.000\.000\-00";
            return Convert.ToUInt64(CPF).ToString(expression);
        }

        /// <summary>
        /// Retira a Formatacao de uma string CNPJ/CPF
        /// </summary>
        /// <param name="Codigo">string Codigo Formatada</param>
        /// <returns>string sem formatacao</returns>
        /// <example>Recebe '99.999.999/9999-99' Devolve '99999999999999'</example>
        public static string ToUnformatted(string Codigo)
        {
            var result = Codigo.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
            return result;
        }

        /// <summary>
        /// Encontra e remove qualquer caractere especial, deixando apenas números e letras.
        /// </summary>
        /// <param name="Codigo">string Codigo Formatada</param>
        /// <returns>string sem formatacao</returns>
        /// <example>Recebe '99.999.999/9999-99' Devolve '99999999999999'</example>
        public static string RemoveSpecialChars(this string input) => input == null ? null : Regex.Replace(input, @"[^0-9a-zA-Z]", "");

        /// <summary>
        /// Valida se uma senha é valida.
        /// </summary>
        /// <param name="value">Senha a ser validada.</param>
        /// <returns>Retorna verdadeiro ou falso.</returns>
        public static bool HasValidPassword(string value)
        {
            var lowercase = new Regex("[a-z]+");
            var uppercase = new Regex("[A-Z]+");
            var digit = new Regex("(\\d)+");
            var symbol = new Regex("(\\W)+");
            var result = (lowercase.IsMatch(value) && uppercase.IsMatch(value) && digit.IsMatch(value) && symbol.IsMatch(value));
            return result;
        }

    }
}

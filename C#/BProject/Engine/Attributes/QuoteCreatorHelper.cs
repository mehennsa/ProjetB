using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class QuoteCreatorHelper
    {
        #region Cache

        private static IDictionary<QuoteType, Type> _cache = new Dictionary<QuoteType, Type>();

        #endregion

        public static IQuote CreateQuote(QuoteType quoteType, double value = 0, DateTime? date = null)
        {

            if (_cache.ContainsKey(quoteType))
            {
                return Activator.CreateInstance(_cache[quoteType], constructArgs(value, date)) as IQuote;
            }

            // IQuote type
            Type iQuoteType = typeof(IQuote);
            IQuote returnQuote = null;
            bool found = false;

            foreach (var attribute in iQuoteType.GetCustomAttributes(true))
            {
                QuoteImplementation impl = attribute as QuoteImplementation;

                if (impl != null && quoteType.Equals(impl.Type))
                {

                    found = true;
                    object[] args = constructArgs(value, date);
                    returnQuote = Activator.CreateInstance(impl.ImplType, args) as IQuote;
                    _cache.Add(quoteType, impl.ImplType);
                    break;
                }
            }

            if (found)
            {
                return returnQuote;
            }
            else
            {
                throw new Exception("No implementation found!! Do you miss an attribute declaration?");
            }
        }

        private static object[] constructArgs(double value, DateTime? date)
        {
            DateTime realDate = date.HasValue ? date.Value : DateTime.Today;
            return new object[] { value, realDate };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public enum QuoteType
    {
        OPEN,
        CLOSE,
        HIGH,
        LOW,
        VOLUME,
        MA,
        EMA,
        HMA,
        WMA,
        RSI,
        ROC,
        MACDLine,
        NONE
    }

    //
    // Représente l'interface d'un cours publié ou calculé.
    // Est défini par un date et une valeur.
    // On connait le nom de l'asset associé via l'interface IAsset.
    //
    [QuoteImplementation(Type = QuoteType.OPEN, ImplType = typeof(Open))]
    [QuoteImplementation(Type = QuoteType.CLOSE, ImplType = (typeof(Close)))]
    [QuoteImplementation(Type = QuoteType.HIGH, ImplType = (typeof(High)))]
    [QuoteImplementation(Type = QuoteType.LOW, ImplType = (typeof(Low)))]
    [QuoteImplementation(Type = QuoteType.VOLUME, ImplType = (typeof(Volume)))]
    public interface IQuote : ICloneable, IComparable<IQuote>
    {
        // L'affectation de Value et Date ne peut pas se faire à l'extérieur de la classe implémentant l'interface.
        double Value { get; }
        DateTime Date { get; }
    }

    //
    // Représente un cours.
    // Classe abstraite utilisé pour définir les getter et private setter.
    //
    public abstract class Quote: IQuote
    {
        protected double _value;
        protected DateTime _date;

        public Quote(double value, DateTime date)
        {
            _value = value;
            _date = date;
        }

        // add parameterless constructors for other inherithed classes
        public Quote() { }

        public double Value
        {
            get { return _value; }
            private set { _value = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            private set { _date = value; }
        }

        public abstract object Clone();

        public override bool Equals(object obj)
        {
            if (!Object.ReferenceEquals(this.GetType(), obj.GetType()))
                return false;
            else
            {
                var quote = obj as Quote;
                return (this.Value == quote.Value && this.Date == quote.Date);
            }
        }


        public int CompareTo(IQuote other)
        {
            if (other.Value < Value)
            {
                return 1;
            }
            else if (other.Value > Value)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}

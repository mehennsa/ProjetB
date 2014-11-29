using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    //
    // Représente un cours publié.
    //
    public abstract class Stock : Quote
    {
        public Stock(double value, DateTime date) : base(value, date) { }

        // !! Avoir une fonction permettant de remplir le cours grâce au service en liaison avec la BD ? !!
    }

    //
    // Cours d'ouverture
    //
    public class Open : Stock
    {
        public Open(double value, DateTime date) : base(value, date) { }

        public override object Clone()
        {
            return new Open(this.Value, this.Date);
        }
    }

    //
    // Cours de fermeture
    //
    public class Close : Stock
    {
        public Close(double value, DateTime date) : base(value, date) { }

        public override object Clone()
        {
            return new Close(this.Value, this.Date);
        }
    }

    //
    // Cours le plus haut de la journée
    //
    public class High : Stock
    {
        public High(double value, DateTime date) : base(value, date) { }
        
        public override object Clone()
        {
            return new High(this.Value, this.Date);
        }
    }

    //
    // Cours le plus bas de la journée
    //
    public class Low : Stock
    {
        public Low(double value, DateTime date) : base(value, date) { }
        
        public override object Clone()
        {
            return new Low(this.Value, this.Date);
        }
    }

    //
    // Volume échangé dans la journée
    //
    public class Volume : Stock
    {
        public Volume(double value, DateTime date) : base(value, date) { }

        public override object Clone()
        {
            return new Volume(this.Value, this.Date);
        }
    }
}

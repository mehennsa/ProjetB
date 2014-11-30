using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EstimatorFeeder
{
    public interface IEstimatorFeeder
    {

       void RecordValue(string ticker, string estimatorName, IQuote valueToRecord, DateTime date);

       Curve GetEstimatorForTicker(string ticker, string estimatorName, List<DateTime> date);
    }
}

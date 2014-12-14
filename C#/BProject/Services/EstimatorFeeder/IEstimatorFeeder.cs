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

       void RecordValue(string ticker, string estimatorName, IQuote valueToRecord);
       
       void DeleteValue(string ticker, string estimatorName, DateTime StartDate, DateTime EndDate);

       Curve GetEstimatorForTicker(string ticker, String type, string estimatorName, DateTime StartDate, DateTime EndDate);
    }
}

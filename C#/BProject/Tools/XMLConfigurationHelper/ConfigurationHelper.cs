using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tools
{
    public static class ConfigurationHelper
    {
        #region files fields

        private const string ConfigDirectory = @".\Config\";

        private const string frenchCalendar = "frenchCalendar.xml";

        #endregion

        #region Cache

        private static IDictionary<string, object> _cache = new Dictionary<string, object>();

        private const string calendarCacheKey = "Calendar";

        #endregion

        #region Calendar nodes

        private const string yearNode = "Year";

        private const string monthNode = "Month";

        private const string dayNode = "Day";

        private const string yearDependantAttribute = "YearDependant";

        #endregion

        #region Methods

        public static IList<DateTime> getHolidaysForYear(int year)
        {
            if (_cache.ContainsKey(calendarCacheKey + year.ToString()))
                return _cache[calendarCacheKey + year.ToString()] as IList<DateTime>;

            IList<DateTime> holidays = new List<DateTime>();

            XmlDocument doc = new XmlDocument();

            doc.Load(ConfigDirectory + frenchCalendar);

            XmlNodeList holidaysNode = doc.DocumentElement.SelectNodes("/Holidays/Holiday");

            foreach (XmlNode node in holidaysNode)
            {
                int dayNumber = ParseIntNode(node, dayNode);
                int monthNumber = ParseIntNode(node, monthNode);
                bool yearDependant = false;
                if (node.Attributes[yearDependantAttribute] != null)
                    yearDependant = ParseBoolAttribute(node.Attributes[yearDependantAttribute]);
                int yearNumber;

                if (!yearDependant)
                {
                    yearNumber = year;
                }
                else
                {
                    yearNumber = ParseIntNode(node, yearNode);
                }
                if (yearNumber == year)
                {
                    holidays.Add(new DateTime(yearNumber, monthNumber, dayNumber));
                }
            }
            _cache.Add(calendarCacheKey + year.ToString(), holidays);
            return holidays;
        }

        #endregion

        #region Utils
        private static int ParseIntNode(XmlNode parent, string nodeName)
        {
            return int.Parse(parent.SelectSingleNode(nodeName).InnerText);
        }

        private static bool ParseBoolNode(XmlNode parent, string nodeName)
        {
            return bool.Parse(parent.SelectSingleNode(nodeName).InnerText);
        }

        private static bool ParseBoolAttribute(XmlAttribute att)
        {
            return bool.Parse(att.InnerText);
        }

        #endregion
    }
}

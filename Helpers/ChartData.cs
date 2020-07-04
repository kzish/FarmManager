using FarmManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedLinkAdminPortal.Helpers
{
    public class ChartData
    {
     /*  public static string chart1data(string date_from,string date_to)
        {
            string chartdata = string.Empty;
            var db = new dbContext();
            var query = from W in db.MWhatsappUsageStats
                        join C in db.MClient
                        on W.AspNetUserId equals C.Id
                        select new
                        {
                            date = W.Date,
                            client_name = C.Name,
                        };

            //filter by date
            if (!string.IsNullOrEmpty(date_from))
            {
                query = query.Where(i => i.date >= DateTime.Parse(date_from));
            }
            if (!string.IsNullOrEmpty(date_to))
            {
                query = query.Where(i => i.date <= DateTime.Parse(date_to));
            }


            //
            var query_ = query.GroupBy(
                i => i.client_name,
                (key, g) => new
                {
                    client_name = key,
                    request_count = g.Count()
                });
            foreach (var item in query_.ToList())
            {
                chartdata += $"['{item.client_name}',{item.request_count}],";
            }
            return chartdata;
        }

        public static string chart2data(string date_from, string date_to)
        {
            string chartdata = string.Empty;
            var db = new dbContext();
            var query = from W in db.MJobStatusRequestsStats
                        join C in db.MClient
                        on W.AspNetUserId equals C.Id
                        select new
                        {
                            date = W.Date,
                            client_name = C.Name,
                        };

            //filter by date
            if (!string.IsNullOrEmpty(date_from))
            {
                query = query.Where(i => i.date >= DateTime.Parse(date_from));
            }
            if (!string.IsNullOrEmpty(date_to))
            {
                query = query.Where(i => i.date <= DateTime.Parse(date_to));
            }


            //
            var query_ = query.GroupBy(
                i => i.client_name,
                (key, g) => new
                {
                    client_name = key,
                    request_count = g.Count()
                });
            foreach (var item in query_.ToList())
            {
                chartdata += $"['{item.client_name}',{item.request_count}],";
            }
            return chartdata;
        }*/
    }
}

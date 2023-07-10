using DozenDispleasedDudes.Library.Models;
using DozenDispleasedDudes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DozenDispleasedDudes.Library.Services
{
    public class BillService
    {
        private List<Bill> activeInvoice;
        private List<Bill> closedInvoices;
        //private List<Bill> closedInvoices;
        private static BillService? instance;
        public List<Bill> ActiveInvoices { get { return activeInvoice; } }
        public List<Bill> ClosedInvoices { get { return closedInvoices; } }
        //public List<Time> ClosedInvoices { get { return closedInvoices; } }

        private static object _lock = new object();
        public static BillService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new BillService();//how to make this work with a parameter
                    }
                }

                return instance;
            }

        }
        private BillService()
        {
            activeInvoice = new List<Bill>();
            closedInvoices = new List<Bill>();
        }
        public void send(Bill bill)
        {
            if (bill != null)
            {
                ClosedInvoices.Add(bill);
                ActiveInvoices.Remove(bill);
            }
        }
        public void cancel(Bill bill)
        {
            if (bill != null)
            {
                TimeService.Current.setBillId(bill.timeList,0);
                ActiveInvoices.Remove(bill);
            }
        }
        public Bill? Get(int id)
        {
            return activeInvoice.FirstOrDefault(p => p.InvoiceId == id);
        }
        private int LastId
        {
            get
            {
                return ActiveInvoices.Any() ? ActiveInvoices.Select(c => c.InvoiceId).Max() : 0;
            }
        }
        public void AddOrUpdate(Bill b)
        {
            if (b.InvoiceId == 0)
            {
                b.TotalCost = TotalCost(b.timeList);
                
                b.InvoiceId = LastId + 1;
                TimeService.Current.setBillId(b.timeList, b.InvoiceId);
                ActiveInvoices.Add(b);
            }

        }
        public decimal TotalCost(List<Time> times)
        {
            decimal TotalCost = 0;
            foreach (Time t in times)
            {
                TotalCost = TotalCost + TimeService.Current.GetCost(t);
            }
            return TotalCost;
        }
        public decimal? CostPerProj(int ProjectId, List<Time>? times)
        {
            List<Time> ts = new List<Time>();
            ts = TimeService.Current.GetTimesByProjectId(ProjectId);
            if (times != null)
            {
                ts = (ts.Where(t => times.Contains(t))).ToList();
            }

            return TotalCost(ts);
        }
        

    }
}

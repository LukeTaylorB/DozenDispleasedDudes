using DozenDispleasedDudes.Library.Models;
using DozenDispleasedDudes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DozenDispleasedDudes.Library.Services
{
    public class TimeService
    {
        private List<Time> times;
        private static TimeService? instance;
        private static object _lock = new object();

        public static TimeService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new TimeService();//how to make this work with a parameter
                    }
                }

                return instance;
            }
        }
        public List<Time> Times { get { return times; } }
        public TimeService()
        {
            times = new List<Time>();
        }
        public decimal GetCost(Time t)
        {
            return t.EmployeeRate * t.Hours;
        }
            
        public void UnselectByList(List<Time> times)
        {
            foreach (Time t in times)
            {
                t.IsSelected = false;
            }
            
        }
        
        /*
        public Tuple<Project,List<Time>> ProjTime(Project proj, List<Time> times)
        {
            return new Tuple<Project, List<Time>>(proj,times); //if this works I wasted soooo much time L
        }
          */
        public void setBillId(List<Time> times,int id)
        {
            if(id != 0)
            {
                foreach (Time t in times)
                {
                    t.BillId = id;
                }
            }
            else
            {
                foreach (Time t in times)
                {
                    t.BillId = null;
                }
            }
            
        }
        public void AddTime(Time t)
        {
            times.Add(t);
            //Time newTime = new Time(date, narrative, hours, projectId, employeeId);
            //times.Add(newTime);
        }

        public void RemoveTime(Time time)
        {
            times.Remove(time);
        }

        public List<Time> GetAllTimes()
        {
            return times;
        }

        public List<Time> GetTimesByProjectId(int projectId)
        {
            return times.FindAll(time => time.ProjectId == projectId);
        }

        public List<Time> GetTimesByEmployeeId(int employeeId)
        {
            return times.FindAll(time => time.EmployeeId == employeeId);

        }
    }
}

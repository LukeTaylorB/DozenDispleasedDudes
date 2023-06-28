using DozenDispleasedDudes.Library.Models;
using DozenDispleasedDudes.Models;
using System;
using System.Collections.Generic;
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

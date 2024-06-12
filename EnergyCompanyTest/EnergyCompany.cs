using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnergyCompanyExercise
{
    public class EnergyCompany
    {
        private readonly List<EndPoint> endPoints;

        public EnergyCompany()
        {
            endPoints = [];
        }
        public void AddEndPoint(EndPoint endPoint)
        {
            endPoints.Add(endPoint);
        }
        public void DeleteEndPoint(string serialNumber)
        {
            EndPoint e = GetEndPoint(serialNumber);
            endPoints.Remove(e);
        }
        public List<EndPoint> EndPoints => endPoints;
        [return: MaybeNull]
        public Boolean EndPointExists(string serialNumber)
        {
            var serialNumberQuery = from ep in endPoints
                                    where ep.SerialNumber == serialNumber
                                    select ep;

            return serialNumberQuery.Any();
        }
        public EndPoint GetEndPoint(string serialNumber)
        {
            var serialNumberQuery = from ep in endPoints
                                    where ep.SerialNumber == serialNumber
                                    select ep;

            return serialNumberQuery.Single();
        }
    }
}
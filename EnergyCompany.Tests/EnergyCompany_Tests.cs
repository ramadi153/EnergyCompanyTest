using EnergyCompanyExercise;
using System.Net;

namespace EnergyCompanyExercise
{
    public class EnergyCompany_Tests
    {
        [Fact]
        public void AddEndPointTest()
        {
            // Setup
            EnergyCompany energyCompany = new();
            var serialNumber = "sn1";
            EndPoint endPoint = new(serialNumber, 0, 0, "mv1", 0);

            // Action
            energyCompany.AddEndPoint(endPoint);

            // Assert
            Assert.Contains(endPoint, energyCompany.EndPoints);
        }
        [Fact]
        public void AddMultipleEndPointsTest()
        {
            // Setup
            EnergyCompany energyCompany = new();
            EndPoint endPoint1 = new("sn1", 0, 0, "mv1", 0);
            EndPoint endPoint2 = new("sn2", 0, 0, "mv2", 0);

            // Action
            energyCompany.AddEndPoint(endPoint1);
            energyCompany.AddEndPoint(endPoint2);

            // Assert
            Assert.Contains(endPoint1, energyCompany.EndPoints);
            Assert.Contains(endPoint2, energyCompany.EndPoints);
        }
        [Fact]
        public void DeleteEndPointTest()
        {
            // Setup
            EnergyCompany energyCompany = new();
            EndPoint endPoint = new("sn1", 0, 0, "mv1", 0);
            energyCompany.AddEndPoint(endPoint);

            // Action
            energyCompany.DeleteEndPoint(endPoint.SerialNumber);

            // Assert
            Assert.DoesNotContain(endPoint, energyCompany.EndPoints);
        }
        [Fact]
        public void FindEndPointTest()
        {
            // Setup
            EnergyCompany energyCompany = new();
            var serialNumber = "sn1";
            var state = 0;
            EndPoint endPoint = new(serialNumber, 0, 0, "mv1", state);
            energyCompany.AddEndPoint(endPoint);

            // Action
            EndPoint? e = energyCompany.GetEndPoint(serialNumber);

            // Assert
            Assert.NotNull(e);
            Assert.Equal(e.SerialNumber, endPoint.SerialNumber);
        }
    }
}
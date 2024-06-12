using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyCompanyExercise
{
    public class EndPoint
    {
        private readonly string serialNumber;
        private readonly int meterModelId;
        private readonly int meterNumber;
        private readonly string meterVersion;
        private int state;

        public EndPoint(string serialNumber, int meterModelId, int meterNumber, string meterVersion, int state)
        {
            this.serialNumber = serialNumber;
            this.meterModelId = meterModelId;
            this.meterNumber = meterNumber;
            this.meterVersion = meterVersion;
            this.state = state;
        }

        public string SerialNumber { get { return serialNumber; } }
        public int State {
            get { return state; }
            set { 
                state = value;
            }
        }
        override
        public string ToString()
        {
            StringBuilder stringBuilder = new ();
            stringBuilder.AppendLine("EndPoint information")
                .AppendLine($"Serial Number: {serialNumber}")
                .AppendLine($"Meter Model ID: {GetModelIDText(meterModelId)}")
                .AppendLine($"Meter Number: {meterNumber}")
                .AppendLine($"Meter Version: {meterVersion}")
                .Append($"State: {GetStateText(state)}");
            return stringBuilder.ToString();
        }
        private static string GetStateText(int state)
        {
            var resultText = state switch
            {
                0 => "Disconnected",
                1 => "Connected",
                2 => "Armed",
                _ => "Unknown",
            };
            return resultText;
        }
        private static string GetModelIDText(int modelID)
        {
            var resultText = modelID switch
            {
                16 => "NSX1P2W",
                17 => "NSX1P3W",
                18 => "NSX2P3W",
                19 => "NSX2P4W",
                _ => "Unknown",
            };
            return resultText;
        }
    }
}
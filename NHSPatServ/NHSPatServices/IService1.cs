using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NHSPatServices
{
    //these are the methods which must be included in any service contract
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<PlottableObject> GetPlottableObjects(string gp, string question, string disease = "Asthma");
        [OperationContract]
        List<MappableObject> GetMappableObjects(string gp, string question, string disease = "Asthma");
    }//end service

    //the following data contracts, data members and enum members are available 
    //to anyone who enters a contract with this service
    [DataContract]
    public class PlottableObject
    {
        [DataMember]
        public string Ref_ID { get; set; }
        [DataMember]
        public double Value { get; set; }

        public PlottableObject(string value, double average)
        {
            Ref_ID = value;
            Value = average;
        }
    }//end plottableObject
    [DataContract]
    public class Disease
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Code { get; set; } 

        public Disease(string name)
        {
            this.Name = name;
            this.Code = setCode(name);
        }


        public string setCode(string name)
        {
            switch (name)
            {
                case "Asthma":
                    return "AST";
                case "Chronic Obstructive Pulmonary Disease":
                    return "COPD";
                case "Thyroid":
                    return "THY";
                case "Hypertension":
                    return "HYP";
                case "Dementia":
                    return "DEM";
                case "Osteoporosis":
                    return "OST";
                case "Atrial Fibrillation":
                    return "AF";
                case "Heart Failure":
                    return "HF";
                case "Learning Disabilities":
                    return "LD";
                case "Coronary Heart Disease":
                    return "CHD";
                case "Epilepsy":
                    return "EP";
                case "Chronic Kidney Disease":
                    return "CKD";
                case "Rheumatoid Arthritis":
                    return "RA";
                case "Depression":
                    return "DEP";
                case "Diabetes":
                    return "DM";
                case "Cancer":
                    return "CAN";
                case "Smoking Indicators":
                    return "SMOK";
                case "Stroke":
                    return "STIA";
                case "Palliative Care":
                    return "PC";
                case "Mental Health":
                    return "MH";
                case "Peripheral Atrial Disease":
                    return "PAD";
                case "Obesity":
                    return "OB";
                case "Cardiovascular Disease - Primary Prevention":
                    return "CVDPP";
                default:
                    return "NOT SET!!";

            }//end switch
        }//end setCode
    }//end disease
    [DataContract]
    public class MappableObject
    {
        [DataMember]
        public double Longitude { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public string Information {
            get {
                return Information; }
            set {
                Information = Information + value; } }

        public MappableObject(string information, double latitude,double longitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Information = information;
        }
    }//end mappableObject

    [DataContract]
    public class SearchCriteria
    {
        [DataMember]
        public string gp;
        [DataMember]
        public Disease disease { get; set; }
        [DataMember]
        public string question { get; set; }

        public SearchCriteria(string gp, string question, string diseaseName = "Asthma")
        {
            this.gp = gp;
            this.question = question;
            this.disease = new Disease (diseaseName);

        }
    }//end search criteria

}//end class

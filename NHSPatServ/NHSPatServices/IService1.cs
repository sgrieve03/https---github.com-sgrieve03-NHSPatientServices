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
        List<PlottableObject> GetPlottableObjects(SearchCriteria searchQuery);
        [OperationContract]
        List<MappableObject> GetMappableObjects(SearchCriteria searchQuery);
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
        public diseaseEnum Code { get; set; } 

        public Disease(string name)
        {
            this.Name = name;
            this.Code = diseaseEnum.AST;
        }
        public Disease(string name, diseaseEnum code)
        {
            this.Name = name;
            this.Code = code;
        }
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
        public Search search { get; set; }
    }//end search criteria

    [DataContract]
    public enum Search
    {
        [EnumMember]
        AverageAllDiseaseInEngland,
        [EnumMember]
        MaxAllDiseaseInEngland,
        [EnumMember]
        MinAllDiseaseInEngland,
        [EnumMember]
        AverageSpecificDiseaseInEngland,
        [EnumMember]
        MaxSpecificDiseaseInEngland,
        [EnumMember]
        MinSpecificDiseaseInEngland,
        [EnumMember]
        AverageAllDiseaseInNHSTrust,
        [EnumMember]
        MaxAllDiseaseInNHSTrust,
        [EnumMember]
        MinAllDiseaseInNHSTrust,
        [EnumMember]
        AverageSpecificDiseaseInNHSTrust,
        [EnumMember]
        MaxSpecificDiseaseInNHSTrust,
        [EnumMember]
        MinSpecificDiseaseInNHSTrust,
        [EnumMember]
        TotalAllDiseaseInSpecificGP,
        [EnumMember]
        TotalSpecificDiseaseInGP,
        [EnumMember]
        AverageStaffInEngland,
        [EnumMember]
        AverageStaffInNHSTrust,
        [EnumMember]
        TotalStaffInSpecificGP,
        [EnumMember]
        AveragePatientInEngland,
        [EnumMember]
        AveragePatientInNHSTrust,
        [EnumMember]
        TotalPatientInSpecificGP,
        [EnumMember]
        AverageRatingInEngland,
        [EnumMember]
        AverageRatingInTrust,
        [EnumMember]
        TotalRatingInGP,
    }//end search

    [DataContract]
    public enum diseaseEnum
    {
        [EnumMember]
        AST,
        [EnumMember]
        COPD,
        [EnumMember]
        THY,
        [EnumMember]
        HYP,
        [EnumMember]
        DEM,
        [EnumMember]
        OST,
        [EnumMember]
        AF,
        [EnumMember]
        HF,
        [EnumMember]
        LD,
        [EnumMember]
        CHD,
        [EnumMember]
        EP,
        [EnumMember]
        CKD,
        [EnumMember]
        RA,
        [EnumMember]
        DEP,
        [EnumMember]
        DM,
        [EnumMember]
        CAN,
        [EnumMember]
        SMOK,
        [EnumMember]
        STIA,
        [EnumMember]
        PC,
        [EnumMember]
        MH,
        [EnumMember]
        PAD,
        [EnumMember]
        OB,
        [EnumMember]
        CVDPP
    }//end disease

}//end class

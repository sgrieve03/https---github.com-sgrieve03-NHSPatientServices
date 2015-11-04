using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text; 

namespace NHSPatServices
{
    public class Service1 : IService1
    {
        //create object to connect service to database entities
        NHSPatServ.NHSPatientServicesEntities dbContext;
        
        public Service1()
        {
            dbContext = new NHSPatServ.NHSPatientServicesEntities();

        }

        //this method returns a list of objects which can be plotted on a map
        public List<MappableObject> GetMappableObjects(string gp, string question, string diseaseName = "Asthma")
        {
            SearchCriteria searchQuery = new SearchCriteria(gp, question, diseaseName);
            List<MappableObject> myList = new List<MappableObject>();
            MappableObject pobj;
            var diseaseTotals = dbContext.sp_AverageSpecificDiseaseInNHSTrust(gp, searchQuery.disease.Code);
            
            var result = dbContext.sp_plot();
            foreach (var r in result)
            {
                pobj = new MappableObject(r.Parent_Name, (double)r.Latitude, (double)r.Longitude);
                myList.Add(pobj);

            }
            int i = 0;
            foreach(var v in diseaseTotals)
            {
                myList.ElementAt(i).Information +=searchQuery.disease.Name+": "+ v.Value;
            }
            
            
            return myList ;
            
        }

     
        //this method returns a list of objects which can be plotted on a graph. the list is determined by the search criteria
        public List<PlottableObject> GetPlottableObjects(string gp, string question, string diseaseName = "Asthma" )
        {
            SearchCriteria searchQuery = new SearchCriteria(gp, question, diseaseName);

            PlottableObject obj;
            List<PlottableObject> objList = new List<PlottableObject>();
            objList.Add(new PlottableObject("fordfs", 22));

            dbContext = new NHSPatServ.NHSPatientServicesEntities();

            switch (searchQuery.question)
            {
                case "AverageAllDiseaseInEngland":
                    try
                    {
                        PlottableObject objy;
                        List<PlottableObject> objListy = new List<PlottableObject>();
                       
                        var result = dbContext.sp_AverageAllDiseaseInEngland();

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                objy = new PlottableObject(r.Disease_Name.TrimEnd(), (double)r.Average_Number_of_Cases);
                                objListy.Add(objy);

                            }
                            return objListy;
                        }
                        
                    }
                    catch { Exception ex; }
                    
                    break;

                case "AverageAllDiseaseInNHSTrust":
                    try
                    {
                        var result = dbContext.sp_AverageAllDiseaseInNHSTrust(searchQuery.gp);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject(r.disease_name.TrimEnd(), (double)r.Average);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "AveragePatientInEngland":
                    try
                    {
                        var result = dbContext.sp_AveragePatientInEngland();

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject("Average number of patients in England", (double)r.Value);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "AveragePatientInNHSTrust":
                    try
                    {
                        var result = dbContext.sp_AveragePatientInNHSTrust(searchQuery.gp);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject("Average number of patients in NHS Trust", (double)r.Value);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "AverageRatingInEngland":
                    try
                    {
                        var result = dbContext.sp_AverageRatingInEngland();

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject("Can't remember", (double)r.Q14_Cant_Remember);
                                objList.Add(obj);
                                obj = new PlottableObject("Few days later", (double)r.Q14_Few_Days_Later);
                                objList.Add(obj);
                                obj = new PlottableObject("A week or more", (double)r.Q14_Week_Or_More);
                                objList.Add(obj);
                                obj = new PlottableObject("Next working day", (double)r.Q14_Next_Working_Day);
                                objList.Add(obj);
                                obj = new PlottableObject("On same day", (double)r.Q14_On_Same_day);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "AverageRatingInTrust":
                    try
                    {
                        var result = dbContext.sp_AverageRatingInTrust(searchQuery.gp);
                        var result2 = dbContext.sp_NameOfTrust(searchQuery.gp);
                        
                        if (result != null)
                        {
                            objList.Add(new PlottableObject(result2.ToString(), 0.0));

                            foreach (var r in result)
                            {
                                obj = new PlottableObject("Can't remember", (double)r.Q14_Cant_Remember);
                                objList.Add(obj);
                                obj = new PlottableObject("Few days later", (double)r.Q14_Few_Days_Later);
                                objList.Add(obj);
                                obj = new PlottableObject("A week or more", (double)r.Q14_Week_Or_More);
                                objList.Add(obj);
                                obj = new PlottableObject("Next working day", (double)r.Q14_Next_Working_Day);
                                objList.Add(obj);
                                obj = new PlottableObject("On same day", (double)r.Q14_On_Same_day);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "AverageSpecificDiseaseInEngland":
                    try
                    {
                        var result = dbContext.sp_AverageSpecificDiseaseInEngland(searchQuery.disease.Code);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject(r.Indicator_Group.TrimEnd(), (int)r.average);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "AverageSpecificDiseaseInNHSTrust":
                    try
                    {
                        var result = dbContext.sp_AverageSpecificDiseaseInNHSTrust(searchQuery.disease.Code, searchQuery.gp);
                        var result2 = dbContext.sp_NameOfTrust(searchQuery.gp);
                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject(result2.ToString(), r.Value);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "AverageStaffInEngland":
                    try
                    {
                        var result = dbContext.sp_AverageStaffInEngland();

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject("Average number of staff in England", (double)r.Value);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "AverageStaffInNHSTrust":
                    try
                    {
                        var result = dbContext.sp_AverageStaffInNHSTrust(searchQuery.gp);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject("Average number of Staff in Trust", (double)r.Value);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
         
                case "MaxAllDiseaseInEngland":
                    try
                    {
                        var result = dbContext.sp_MaxAllDiseaseInEngland();

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject(r.Disease_Name.TrimEnd(), (int)r.Max_Number_of_cases);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "MaxAllDiseaseInNHSTrust":
                    try
                    {
                        var result = dbContext.sp_MaxAllDiseaseInNHSTrust(searchQuery.gp);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject(r.Disease_Name, (int)r.Max_Number_of_Cases);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "MaxSpecificDiseaseInEngland":
                    try
                    {
                        var result = dbContext.sp_MaxSpecificDiseaseInEngland(searchQuery.disease.Code);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject("Max number of disease", (int)r.Value);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "MaxSpecificDiseaseInNHSTrust":
                    try
                    {
                        var result = dbContext.sp_MaxSpecificDiseaseInNHSTrust(searchQuery.disease.Code, searchQuery.gp);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject(r.Disease_Name.TrimEnd(), (int)r.Max_Number_of_Cases);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "MinAllDiseaseInEngland":
                    try
                    {
                        var result = dbContext.sp_MinAllDiseaseInEngland();

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject(r.Disease_Name.TrimEnd(), (int)r.Min_Number_of_Cases);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "MinAllDiseaseInNHSTrust":
                    try
                    {
                        var result = dbContext.sp_MinAllDiseaseInNHSTrust(searchQuery.gp);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject(r.disease_name.TrimEnd(), (int)r.Min_Number_of_Cases);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "MinSpecificDiseaseInEngland":
                    try
                    {
                        var result = dbContext.sp_MinSpecificDiseaseInEngland(searchQuery.disease.Code.ToString());

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject("Min Disease Occurance in England", (int)r.Value);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                case "MinSpecificDiseaseInNHSTrust":
                    try
                    {
                        var result = dbContext.sp_MinSpecificDiseaseInNHSTrust(searchQuery.disease.Code.ToString(), searchQuery.gp);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject("Min Desease occurance in NHS Trust", (int)r.Value);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;
                
                // returns the total number of a specific disease from a specific gp
                case "TotalAllDiseaseInSpecificGP":
                    try
                    {
                        var result = dbContext.sp_TotalAllDiseaseInSpecificGP(searchQuery.gp);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject(r.Disease_Name.TrimEnd(), (int)r.Number_of_cases);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;

                case "TotalPatientInSpecificGP":
                    try
                    {
                        var result = dbContext.sp_TotalPatientInSpecificGP(searchQuery.gp);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject("Total number of patients", (int)r.Value);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;

                //returns the total rating for a particular gp with regards waiting times
                case "TotalRatingInGP":
                    try
                    {
                        var result = dbContext.sp_TotalRatingInGP(searchQuery.gp);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject("Can't remember", (double)r.Q14_Cant_Remember);
                                objList.Add(obj);
                                obj = new PlottableObject("Few days later", (double)r.Q14_Few_Days_Later);
                                objList.Add(obj);
                                obj = new PlottableObject("A week or more", (double)r.Q14_Week_Or_More);
                                objList.Add(obj);
                                obj = new PlottableObject("Next working day", (double)r.Q14_Next_Working_Day);
                                objList.Add(obj);
                                obj = new PlottableObject("On same day", (double)r.Q14_On_Same_day);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;

                //this element returns the title and count of a particular illness
                case "TotalSpecificDiseaseInGP":
                    try
                    {
                        var result = dbContext.sp_TotalSpecificDiseaseInGP(searchQuery.disease.Code, searchQuery.gp);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject(r.Disease_Name.TrimEnd(), (int)r.Number_of_cases);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;

                //this element of the switch returns the title and quantity of each type 
                //of staff employed at a single gp practice
                case "TotalStaffInSpecificGP":
                    try
                    {
                        var result = dbContext.sp_TotalStaffInSpecificGP(searchQuery.gp);

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject(r.Job_Title.TrimEnd(), (int)r.Total);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break; 
            }
            
            return objList;
        }
    }

   
}

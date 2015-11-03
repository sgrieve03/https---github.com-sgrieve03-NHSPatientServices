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
        NHSPatServ.NHSPatientServicesEntities7 dbContext = new NHSPatServ.NHSPatientServicesEntities7();


        //this method returns a list of objects which can be plotted on a map
        public List<MappableObject> GetMappableObjects(SearchCriteria searchQuery)
        {
            List<MappableObject> myList = new List<MappableObject>();
            MappableObject pobj;
            var diseaseTotals = dbContext.sp_AverageSpecificDiseaseInNHSTrust(searchQuery.gp, searchQuery.disease.Code.ToString());
            var result = dbContext.sp_plot();
            foreach (var r in result)
            {
                pobj = new MappableObject(r.Parent_Name, (double)r.Latitude, (double)r.Longitude);
                myList.Add(pobj);

            }

            foreach(var v in diseaseTotals)
            {

            }
            
            
            return myList ;
            
        }

     
        //this method returns a list of objects which can be plotted on a graph. the list is determined by the search criteria
        public List<PlottableObject> GetPlottableObjects(SearchCriteria searchQuery)
        {
            PlottableObject obj;
            List<PlottableObject> objList = new List<PlottableObject>();

            switch (searchQuery.search)
            {
                case Search.AverageAllDiseaseInEngland:
                    try
                    {
                        var result = dbContext.sp_AverageAllDiseaseInEngland();

                        if (result != null)
                        {
                            foreach (var r in result)
                            {
                                obj = new PlottableObject(r.Disease_Name.TrimEnd(), (double)r.Average_Number_of_Cases);
                                objList.Add(obj);

                            }
                        }
                    }
                    catch { Exception ex; }
                    break;

                case Search.AverageAllDiseaseInNHSTrust:
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
                case Search.AveragePatientInEngland:
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
                case Search.AveragePatientInNHSTrust:
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
                case Search.AverageRatingInEngland:
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
                case Search.AverageRatingInTrust:
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
                case Search.AverageSpecificDiseaseInEngland:
                    try
                    {
                        var result = dbContext.sp_AverageSpecificDiseaseInEngland(searchQuery.disease.Code.ToString());

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
                case Search.AverageSpecificDiseaseInNHSTrust:
                    try
                    {
                        var result = dbContext.sp_AverageSpecificDiseaseInNHSTrust(searchQuery.disease.Code.ToString(), searchQuery.gp);
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
                case Search.AverageStaffInEngland:
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
                case Search.AverageStaffInNHSTrust:
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
         
                case Search.MaxAllDiseaseInEngland:
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
                case Search.MaxAllDiseaseInNHSTrust:
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
                case Search.MaxSpecificDiseaseInEngland:
                    try
                    {
                        var result = dbContext.sp_MaxSpecificDiseaseInEngland(searchQuery.disease.Code.ToString());

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
                case Search.MaxSpecificDiseaseInNHSTrust:
                    try
                    {
                        var result = dbContext.sp_MaxSpecificDiseaseInNHSTrust(searchQuery.disease.Code.ToString(), searchQuery.gp);

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
                case Search.MinAllDiseaseInEngland:
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
                case Search.MinAllDiseaseInNHSTrust:
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
                case Search.MinSpecificDiseaseInEngland:
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
                case Search.MinSpecificDiseaseInNHSTrust:
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
                case Search.TotalAllDiseaseInSpecificGP:
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

                case Search.TotalPatientInSpecificGP:
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
                case Search.TotalRatingInGP:
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
                case Search.TotalSpecificDiseaseInGP:
                    try
                    {
                        var result = dbContext.sp_TotalSpecificDiseaseInGP(searchQuery.disease.Code.ToString(), searchQuery.gp);

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
                case Search.TotalStaffInSpecificGP:
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

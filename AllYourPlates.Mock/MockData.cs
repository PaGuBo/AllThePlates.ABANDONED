using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllYourPlates.Domain.Entities;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using AllYourPlates.Common.Withings.Entities;

namespace AllYourPlates.Mock
{
    public class MockData
    {

        public List<User> MemoryList { get; set;}
        public List<User> DiskList { get; set; }

        public MockData(string absolutePath)
        {
            InitMemoryList();
            InitDiskList(absolutePath);
        }

        private void InitDiskList(string absolutePath)
        {

            string rootDir = absolutePath + @"Photos\";
            //string rootDir = @"W:\allyourplates\Photos\";
            DiskList = new List<User>();
            foreach (string userFolder in Directory.GetDirectories(rootDir))
            {
                User u = new User();
                u.Name = userFolder.Replace(rootDir, "");
                
                //set up the plates
                u.Plates = new List<Plate>();
                foreach (string platePicture in Directory.GetFiles(userFolder + @"\Plate").Where(s => s.Contains(".jpg")))
                {
                    string fileName = platePicture.Replace(userFolder + @"\Plate\", "");
                    u.Plates.Add(new Plate { Meal = Meal.Breakfast,
                                             Time = new DateTime(Convert.ToInt16(fileName.Substring(0, 4)),
                                                                 Convert.ToInt16(fileName.Substring(5, 2)),
                                                                 Convert.ToInt16(fileName.Substring(8, 2))),
                                             Title = fileName});
                }

                WebClient client = new WebClient();
                string json = String.Empty;
                RootObject body = null;
                try
                {
                    json = client.DownloadString("http://wbsapi.withings.net/measure?action=getmeas&userid=31797&publickey=4381767533c7ecd4");
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    body = serializer.Deserialize<RootObject>(json);
                }

                catch (WebException ex)
                {
                    //add error logging her
                }
                

                //set up the body images
                u.BodyShots = new List<BodyShot>();
                foreach (string bodyPicture in Directory.GetFiles(userFolder + @"\Body").Where(s => s.Contains(".jpg")))
                {
                    string fileName = bodyPicture.Replace(userFolder + @"\Body\", "");
                    DateTime time = new DateTime(Convert.ToInt16(fileName.Substring(0, 4)),
                                            Convert.ToInt16(fileName.Substring(5, 2)),
                                            Convert.ToInt16(fileName.Substring(8, 2)));
                    
                    decimal? weight = null;
                    if (body != null)
                    {
                        Measuregrp measuregrp = body.Body.Measuregrps.FirstOrDefault(x => x.FormatedDate.ToString("MM/dd/yyyy") == time.ToString("MM/dd/yyyy"));
                        if (measuregrp != null)
                        {
                            Measure measure = measuregrp.Measures.FirstOrDefault(y => y.Type == 1);
                            weight = (decimal)((double)measure.Value * Math.Pow(10, measure.Unit) * 2.2046);
                            //weight = weight * Math.Pow(10, measuregrp.Measures[0]. int.Parse(measure["unit"].ToString()))
                        }
                    }

                    u.BodyShots.Add(new BodyShot
                    {
                        FilePath = fileName,
                        Time = time,
                        Weight = weight
                    });
                }
  
                u.Workouts = new List<Workout>();
                DiskList.Add(u);
            }


        }

        private void InitMemoryList()
        {
            MemoryList = new List<User>();

            MemoryList.Add(new User
            {
                Name = "Pavel",
                Plates = new List<Plate> {
                    new Plate { Meal = Meal.Breakfast, Time = new DateTime(2012, 02, 01, 6, 0, 0),  Title = "Eggs and Bacon" },
                    new Plate { Meal = Meal.Luncheon,  Time = new DateTime(2012, 02, 01, 11, 0, 0), Title = "Peanut butter jelly sandwich" },
                    new Plate { Meal = Meal.Dinner,    Time = new DateTime(2012, 02, 01, 17, 0, 0), Title = "Pasta with meatballs" },
                    new Plate { Meal = Meal.Breakfast, Time = new DateTime(2012, 02, 02, 6, 0, 0),  Title = "French Toast" },
                    new Plate { Meal = Meal.Luncheon,  Time = new DateTime(2012, 02, 02, 6, 0, 0),  Title = "Chipotle burritto" },
                    new Plate { Meal = Meal.Dinner,    Time = new DateTime(2012, 02, 02, 6, 0, 0),  Title = "Lamb chops" },
                    new Plate { Meal = Meal.Breakfast, Time = new DateTime(2012, 02, 03, 6, 0, 0),  Title = "Omelette" },
                    new Plate { Meal = Meal.Luncheon,  Time = new DateTime(2012, 02, 03, 6, 0, 0),  Title = "Panera sandwich" },
                    new Plate { Meal = Meal.Dinner,    Time = new DateTime(2012, 02, 03, 6, 0, 0),  Title = "Sushi" },
                    new Plate { Meal = Meal.Breakfast, Time = new DateTime(2012, 02, 04, 6, 0, 0),  Title = "Waffles" },
                    new Plate { Meal = Meal.Luncheon,  Time = new DateTime(2012, 02, 04, 6, 0, 0),  Title = "Salmon salad" },
                    new Plate { Meal = Meal.Dinner,    Time = new DateTime(2012, 02, 04, 6, 0, 0),  Title = "Steak and mashed potatoes" }
                },
                BodyShots = new List<BodyShot>()
            });


            MemoryList.Add(new User
            {
                Name = "Homer",
                Plates = new List<Plate> {
                    new Plate { Meal = Meal.Breakfast, Time = new DateTime(2012, 02, 01, 6, 0, 0),  Title = "Doughnut" },
                    new Plate { Meal = Meal.Luncheon,  Time = new DateTime(2012, 02, 01, 11, 0, 0), Title = "Bacon" },
                    new Plate { Meal = Meal.Dinner,    Time = new DateTime(2012, 02, 01, 17, 0, 0), Title = "Porkchops" },
                    new Plate { Meal = Meal.Breakfast, Time = new DateTime(2012, 02, 02, 6, 0, 0),  Title = "Slushie" },
                    new Plate { Meal = Meal.Luncheon,  Time = new DateTime(2012, 02, 02, 6, 0, 0),  Title = "Apple Pie" },
                    new Plate { Meal = Meal.Dinner,    Time = new DateTime(2012, 02, 02, 6, 0, 0),  Title = "Mofongo" }
                },
                BodyShots = new List<BodyShot>()
            });

            MemoryList.Add(new User
            {
                Name = "Bruce",
                Plates = new List<Plate> {
                    new Plate { Meal = Meal.Breakfast, Time = new DateTime(2012, 02, 01, 6, 0, 0),  Title = "Apples" },
                    new Plate { Meal = Meal.Luncheon,  Time = new DateTime(2012, 02, 01, 11, 0, 0), Title = "Bananas" },
                    new Plate { Meal = Meal.Dinner,    Time = new DateTime(2012, 02, 01, 17, 0, 0), Title = "Kiwi" },
                    new Plate { Meal = Meal.Breakfast, Time = new DateTime(2012, 02, 02, 6, 0, 0),  Title = "Pineapple" },
                    new Plate { Meal = Meal.Luncheon,  Time = new DateTime(2012, 02, 02, 6, 0, 0),  Title = "Pears" },
                    new Plate { Meal = Meal.Dinner,    Time = new DateTime(2012, 02, 02, 6, 0, 0),  Title = "Oranges" }
                },
                BodyShots = new List<BodyShot>()
            });

            MemoryList.Add(new User
            {
                Name = "Clark",
                Plates = new List<Plate> {
                    new Plate { Meal = Meal.Breakfast, Time = new DateTime(2012, 02, 01, 6, 0, 0),  Title = "Beef" },
                    new Plate { Meal = Meal.Luncheon,  Time = new DateTime(2012, 02, 01, 11, 0, 0), Title = "Pork" },
                    new Plate { Meal = Meal.Dinner,    Time = new DateTime(2012, 02, 01, 17, 0, 0), Title = "Veal" },
                    new Plate { Meal = Meal.Breakfast, Time = new DateTime(2012, 02, 02, 6, 0, 0),  Title = "Venison" },
                    new Plate { Meal = Meal.Luncheon,  Time = new DateTime(2012, 02, 02, 6, 0, 0),  Title = "Chicken" },
                    new Plate { Meal = Meal.Dinner,    Time = new DateTime(2012, 02, 02, 6, 0, 0),  Title = "Fish" }
                },
                BodyShots = new List<BodyShot>()
            });
        }


    }
}
